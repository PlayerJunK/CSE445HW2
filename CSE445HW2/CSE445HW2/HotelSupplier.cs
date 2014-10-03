using System;
using System.Collections.Generic;
using System.Linq;
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

        //how many times the price will be cut.
        const int NUM_PRICE_CUTS = 10;

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


            //subscribe to the orderprocessing object to know 
            //when an unprocessed order has been submitted
            OrderProcessing.callMethodWhenAnNewOrderIsSubmitted(unProcessedOrderAddedToBuffer);
        }

        public void runHotelSupplierOperation()
        {
            while (numPriceCuts != NUM_PRICE_CUTS)
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
        //will be put here.ti
        private int getNewPrice()
        {
            return numGenerator.Next(MIN_PRICE, MAX_PRICE);
        }


        //method that will be called when an unprocessed order is added to the orderprocessing
        public void unProcessedOrderAddedToBuffer(int destinationSupplierID)
        {
            //the order that was added is intended for this supplier
            if (destinationSupplierID == this.supplierID)
            {
                //get the encrypted order from the order processor
                string encryptedOrderToProcess = OrderProcessing.getUnProcessedOrder(this.supplierID);

                //decrypt the string and assign it to an Order Object
                Order newOrderToProcess = Decoder.Decrypt(encryptedOrderToProcess);

                //validate the order
                Boolean orderIsValid = validateOrder(newOrderToProcess);

                //if the order is valid
                if (orderIsValid)
                {
                    //set order as a valid order
                    newOrderToProcess.ValidOrder = true;
                }

                //create new thread and send to order processed as processed order
                //this occurs regardless of whether or not the order was validated

                //this thread is just creating a new thread to run the method after the =>
                Thread threadToProcessOrder = new Thread(() => OrderProcessing.addProcessedOrder(this.supplierID, Encoder.Encrypt(newOrderToProcess)));

                //start thread
                threadToProcessOrder.Start();

            }
        }


        //verifies the validity of an order by checking with the bank.
        private Boolean validateOrder(Order orderToValidate)
        {
            //check with the bank object to see if the credit card is registered
            return Bank.validateCreditCard(orderToValidate.CreditCardNumber);


        }
    }
}
