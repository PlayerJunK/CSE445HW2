using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445HW2
{
    public static class OrderProcessing
    {
        private static MultiCellBuffer processedOrders = new MultiCellBuffer();
        private static MultiCellBuffer unProcessedOrders = new MultiCellBuffer();

        //definiton event that will be triggered if a processed order is added.
        private static delegate void ProcessedOrderAddedEvent(int destinationTravelAgencyID);
        private static delegate void UnProcessedOrderAdded(int destinationHotelSupplierID);

        //create actual event objects that will be triggered if a processed or unprocessed order has been added
        private static event ProcessedOrderAddedEvent signalThatAnOrderHasBeenProcessed;
        private static event UnProcessedOrderAdded signalThatANewOrderHasBeenSubmitted;

        //hotel suppliers will use this once they have processed the order.
        public static void addProcessedOrder(int destinationTravelAgencyID, String encryptedOrderToAdd)
        {
            //create an entry in the table
            //with the travel agency id and the order to be returned to
            //the travel agency with that specific id.

            //(destination travel agency id, order to add to buffer)
            processedOrders.addObjectwithID(destinationTravelAgencyID, encryptedOrderToAdd);
            
            //if there are travel agencies subscribed to the event trigger the event.
            if (signalThatAnOrderHasBeenProcessed != null)
            {
                //notify all travel agencies that an order has been processed
                signalThatAnOrderHasBeenProcessed(destinationTravelAgencyID);
            }
        }

        public static string getProcessedOrderForAgencyID(int travelAgencyID)
        {
            return (string)processedOrders.getObjectwithID(travelAgencyID);
        }


        //once a travel agency decides that they would like to purchase something,
        //they will create an order and add it to the multicell buffer
        public static void addUnProcessedOrder(int destinationHotelSupplierID, string orderToAdd)
        {
            //when adding to the table, set the id to the id of the destination hotel supplier
            unProcessedOrders.addObjectwithID(destinationHotelSupplierID, orderToAdd);

            //if there are hotel suppliers subscribed to the event that emits when a new order is submitted
            if (signalThatANewOrderHasBeenSubmitted != null)
            {

                //send a signal to all HotelSuppliers to let them know that a new order has been submitted.
                signalThatANewOrderHasBeenSubmitted(destinationHotelSupplierID);
            }

        }


        public static string getUnProcessedOrder(int hotelSupplierID)
        {

            //returns an encrypted string that maps to an Order object
            return (string)unProcessedOrders.getObjectwithID(hotelSupplierID);
        }


        //This method allows other objects to subscribe to the ProcessedOrderAddedEvent.
        //The ProcessedOrderAddedEvent is designed to notify travel agencies that an order has 
        //been reviewed by a hotel supplier
        public static void callMethodWhenAnOrderIsProcessed(Action<int> subscriberMethodForTravelAgency)
        {
            //The method that will get called in the travel agency object takes a single integer, which is
            //the id of the travel agency for which the new order that was added is destined.

            //subscribe the method from the travel agency to the event that will be triggered when a new 
            //method is processed from the hotel supplier.
            signalThatAnOrderHasBeenProcessed += new ProcessedOrderAddedEvent(subscriberMethodForTravelAgency);
        }

        //Similar to the callMethodWhenAnOrderIsProcessed method above, this method takes as a parameter a method
        //from the HotelSuppliers that should be caled if a new order is submitted to the order processor.
        public static void callMethodWhenAnNewOrderIsSubmitted(Action<int> subscriberMethodForHotelSupplier)
        {
            //The subscriber method has a single parameter which is designed to be the destination id for
            //the hotel supplier for which the order is destined.

            //subscribe the hotelsupplier method to the event so that it is notified when a new order is submitted
            signalThatANewOrderHasBeenSubmitted += new UnProcessedOrderAdded(subscriberMethodForHotelSupplier);

        }



    }
}
