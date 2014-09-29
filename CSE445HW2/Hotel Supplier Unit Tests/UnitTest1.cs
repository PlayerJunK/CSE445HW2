using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSE445HW2;
using System.Collections;
using System.Threading;

namespace Hotel_Supplier_Unit_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int NUM_SUPPLIERS = 3;
            int NUM_AGENCIES = 5;

            //set up TravelAgency Objects
            ArrayList travelAgencyObjects = new ArrayList();
            
            for (int i = 0; i < NUM_AGENCIES; i++)
            {
                //i+1 is so IDs start at 1 and not zero
                TravelAgency newAgency = new TravelAgency(i + 1);
                travelAgencyObjects.Add(newAgency);

            }

            //set up HotelSupplier Threads
            ArrayList hotelSupplierThreads = new ArrayList();
            for (int i = 0; i < NUM_SUPPLIERS; i++)
            {
                //create each new HotelSupplier
                //i+1 is so IDs start at 1 and not zero
                HotelSupplier newSupplier = new HotelSupplier(i + 1);

                //subscribe each agency to each new HotelSupplier
                for (int j = 0; j < NUM_AGENCIES; j++)
                {
                    newSupplier.subscribeToPriceCut(((TravelAgency)travelAgencyObjects[j]).hotelPriceHasBeenCut);
                }

                //create a thread that will start runHotelSupplierOperation
                hotelSupplierThreads.Add(new Thread(newSupplier.runHotelSupplierOperation));
            }


            //start each HotelSupplier thread
            for (int i = 0; i < NUM_SUPPLIERS; i++)
            {
                ((Thread)hotelSupplierThreads[i]).Start();
            }

            //join each HotelSupplier thread
            for (int i = 0; i < NUM_SUPPLIERS; i++)
            {
                ((Thread)hotelSupplierThreads[i]).Join();
            }



                Assert.IsTrue(true);
        }


        //method to call if price is correctly updated
        private void priceCutNotification(int id, int price)
        {
            string result = "Hotel #" + id + " cut price to $" + price;
            Console.WriteLine(result);
        }
    }
}
