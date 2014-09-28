using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445HW2
{
    public static class OrderProcessing
    {
        private static MultiCellBuffer processedOrders;
        private static MultiCellBuffer unProcessedOrders;


        //hotel suppliers will use this once they have processed the order.
        public static void addProcessedOrder(Order orderToAdd)
        {
            //create an entry in the table
            //with the travel agency id and the order to be returned to
            //the travel agency with that specific id.

            //(destination travel agency id, order to be returned)
            processedOrders.addObjectWithID(orderToAdd.TravelAgencyID, orderToAdd);
        }

        public static Order getProcessedOrderForAgencyID(int travelAgencyID)
        {
            return (Order)processedOrder.getObjectWithID(travelAgencyID);
        }


        //once a travel agency decides that they would like to purchase something,
        //they will create an order and add it to the multicell buffer
        public static void addUnProcessedOrder(Order orderToAdd)
        {
            //when adding to the table, set the id to the id of the destination hotel supplier
            unProcessedOrders.addObjectWithID(orderToAdd.HotelSupplierID, orderToAdd);

        }


        public static Order getUnProcessedOrder(int hotelSupplierID)
        {
            return (Order)unProcessedOrders.getObjectWithID(hotelSupplierID);
        }


    }
}
