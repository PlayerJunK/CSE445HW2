using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445HW2
{
    public class TravelAgency
    {
        const int MULTIPLIER_MIN = 1;
        const int MULTIPLIER_MAX = 3;
        static Random numGenerator = new Random();
        int agencyID;
        public TravelAgency(int agencyID)
        {
            this.agencyID = agencyID;

        }


        public void hotelPriceHasBeenCut(int supplierID, int newPrice){

            //calculate the number of rooms to buy for a given price.
            int desiredNumRoomsToBuy = calculateNumRoomsToBuy(newPrice);



            //create an order object for the purchase
            //Order newOrder = new Order(agencyID, supplierID, desiredNumRoomsToBuy, newPrice, creditCardNumber);

            //just for testing purposes
            if(desiredNumRoomsToBuy != 0)
                Console.WriteLine("Agency #{0} purchases {1} rooms from HotelSupplier #{2} at ${3} each", agencyID, desiredNumRoomsToBuy, supplierID, newPrice);

            
        }


        //calculates the number of rooms to buy by weighting the current price of rooms versus the 
        //average price of rooms.  It also incorporates a random constant multiplier to ensure that
        //all travel agencies are not purchasing the same amount of rooms for every price.
        private int calculateNumRoomsToBuy(int currentPrice)
        {
            //calculate average hotel price (assuming randomly distributed prices)
            double avgHotelPrice = (HotelSupplier.MAX_PRICE + HotelSupplier.MIN_PRICE) / 2.0;

            int result;

            //if the price is larger than 110% of the average price, purchase no rooms.
            if (currentPrice > 1.1 * avgHotelPrice)
            {
                result = 0;
            }
                //else purchase an amount proportional to the deal that is available
            else
            {

                //this random multiplier is to ensure that not all the travel agencies have identical
                //purchases

                double randomConstantMultiplier = 2 * numGenerator.NextDouble();
                
                //difference in current price of hotel room vs average price of hotel room
                double priceDifference = avgHotelPrice - currentPrice;
                double percentageOff = 100 * priceDifference/avgHotelPrice;

                double randomizedPurchaseFactor = percentageOff * randomConstantMultiplier;

                //case where the current price is slightly more than the average price
                //(100% of avgPrice < currentPrice <= 110% of avgPrice)
 
                if (randomizedPurchaseFactor < 0)
                {
                    //double form of result that needs to be rounded
                    double intermediateResult = Math.Abs(randomizedPurchaseFactor / (double)numGenerator.Next(1, 3));

                    //round intermediateResult and assign to result
                    result = (int)Math.Round(new Decimal(intermediateResult));

                }
                else
                {
                    //else the currentPrice is less than the average price.
                    //(currentPrice <= 100% of averagePrice


                    //since the randomizedPurchaseFactor == 0 when the currentPrice is exactly equal 
                    //to the average price, this random addtion mitigates that issue and 
                    //adds additional purchase weight to better deals
                    randomizedPurchaseFactor += numGenerator.Next(0, 3);

                    //round the randomized purchase factor and assign it to result
                    result = (int)Math.Round(new Decimal(randomizedPurchaseFactor));
                }
            }

            //return the calculate number of results
            return result;

        }
    }
}
