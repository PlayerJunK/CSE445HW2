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

        List<int> CreditcardArray = new List<int>();

        // This function generates a random 8-digit card number from 90005000 to 99999999 and returns it to the user.
        public int applyCreditCard()
        {
            Random randomnumber = new Random();

            int cardnumber=randomnumber.Next(90005000,99999999);
            CreditcardArray.Add(cardnumber);
            
            return cardnumber;

        }

        // This function takes the encrypted value of the card number, decrypts it and checks for its authentication
        public string validateCreditCard(int cardNumber)
        {
            // hotel supplier verifies the credit card
            // Using http://venus.eas.asu.edu/WSRepository/Services/EncryptionWcf/Service.svc as Web service reference for Encryption and Decryption.

            foreach (int i in CreditcardArray)
            {
                // int cardnumberStored = CreditcardArray[i];
                if (cardNumber == CreditcardArray[i])
                {
                    return "Valid Card Number";
                }

            }
            return "InValid Card Number";
            

        }






        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
