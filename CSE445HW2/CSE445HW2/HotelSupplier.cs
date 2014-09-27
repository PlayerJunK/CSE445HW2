using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace CSE445HW2
{
    public class HotelSupplier
    {



        //hard coded values for hotel price.  Might be worth it to 
        //provide a constructor or accessor to set these values
        public const int MAX_PRICE = 200;
        public const int MIN_PRICE = 100;

        //how many times the price will be changed.
        const int NUM_ITERATIONS = 50;

        //random number generator that will be used for all HotelSupplier instances
        static Random numGenerator = new Random();

        int price;
        int supplierID;

        //counts the number of price cuts that have been made
        int numPriceCuts;

        //delegate and event for this class
        private delegate void PriceCutEvent(int id, int newPrice);
        private event PriceCutEvent priceHasBeenCut;

        public HotelSupplier(int supplierID)
        {
            this.supplierID = supplierID;
            numPriceCuts = 0;

            //get first random number
            price = numGenerator.Next(MIN_PRICE, MAX_PRICE);

        }

        public void runHotelSupplierOperation()
        {
            while (numPriceCuts != 10)
            {
                //So prices are not updated too quickly
                Thread.Sleep(500);

                //update the price with a random price between the lower and upper prices
                setPrice(getNewPrice());
            }
            
        }

        private void setPrice(int newPrice)
        {
            //checks to see if the new price is lower
            Boolean newPriceIsLower = newPrice < this.price;
            this.price = newPrice;
            
            //if the new price is lower and there is at least one subscriber to the event
            if (newPriceIsLower && priceHasBeenCut != null)
            {
                //notify all of the subscribers that hotel #{supplierID} has cut their prices to ${newPrice}
                priceHasBeenCut(supplierID, newPrice);

                //keep track of the number of price cuts
                numPriceCuts++;
            }
        }
        
        //allow methods to subscribe to the pricecut event
        //the Action parameter allows methods that return void to be passed in to this function.
        //the <int, int> parameters for the Action specify the supplier ID and the price, respectively.
        public void subscribeToPriceCut(Action<int, int> travelAgencyEventListener)
        {
            //now the address of the Action (function) passed to this method will
            //be called when the priceHasBeenCut event is triggered
            priceHasBeenCut += new PriceCutEvent(travelAgencyEventListener);
        }

        
        //price calculation is done here.  Any improvements to the price calculation function
        //will be put here.
        private int getNewPrice()
        {
            return numGenerator.Next(MIN_PRICE, MAX_PRICE);
        }
    }
}
