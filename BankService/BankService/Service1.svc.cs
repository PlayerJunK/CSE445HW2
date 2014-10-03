using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace BankService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        static List<int> CreditcardArray = new List<int>();
        static Random randomnumber = new Random();

        // This function generates a random 8-digit card number from 90005000 to 99999999 and returns it to the user.
        public int applyCreditCard()
        {

            int cardnumber=randomnumber.Next(90005000,99999999);
            CreditcardArray.Add(cardnumber);
            
            return cardnumber;

        }

        // This function takes the encrypted value of the card number, decrypts it and checks for its authentication
        public bool validateCreditCard(int cardNumber)
        {
            // hotel supplier verifies the credit card
            for (int i = 0; i < CreditcardArray.Count; i++)
            {
                // int cardnumberStored = CreditcardArray[i];
                if (cardNumber == CreditcardArray[i])
                {
                    return true;
                }

            }
            return false;
            

        }

    }
}
