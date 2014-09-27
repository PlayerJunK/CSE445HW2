using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445HW2
{
    public class Order
    {
        int pricePerRoom;
        long creditCardNumber;
        int numRooms;
        int hotelSupplierID;
        int travelAgencyID;


        Boolean validOrder;

        public Order(int travelAgencyID, int hotelSupplierID, int numRooms, int pricePerRoom, long creditCardNumber)
        {
            this.creditCardNumber = creditCardNumber;
            this.travelAgencyID = travelAgencyID;
            this.hotelSupplierID = hotelSupplierID;
            this.numRooms = numRooms;
            this.pricePerRoom = pricePerRoom;
            validOrder = false;
        }

        public Boolean isValidOrder()
        {
            return validOrder;
        }

        public void setValidOrder(Boolean validOrder)
        {
            this.validOrder = validOrder;
        }

        public string ToString()
        {
            string result = "Travel Agency " + this.travelAgencyID + " bought " + this.numRooms + " rooms from Hotel Supplier " +
                this.hotelSupplierID + " at $" + this.pricePerRoom + " for a total of $" + totalPrice();


            //display invalid message if the order is not valid.
            if (this.validOrder == false)
            {
                result = "Invalid purchase between Travel Agency " + this.travelAgencyID + " and Hotel Supplier " + this.hotelSupplierID;
            }

            return result;
        }

        public int totalPrice()
        {
            return numRooms * pricePerRoom;
        }

        //accessors and modifiers
        public int PricePerRoom
        {
            get { return pricePerRoom; }
            set { pricePerRoom = value; }
        }

        public int NumRooms
        {
            get { return numRooms; }
            set { numRooms = value; }
        }

        public int HotelSupplierID
        {
            get { return hotelSupplierID; }
            set { hotelSupplierID = value; }
        }

        public int TravelAgencyID
        {
            get { return travelAgencyID; }
            set { travelAgencyID = value; }
        }

        public long CreditCardNumber
        {
            get { return creditCardNumber; }
            set { creditCardNumber = value; }
        }

    }
}
