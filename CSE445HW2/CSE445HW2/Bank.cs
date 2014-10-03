using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSE445HW2
{
    class Bank
    {
        static ServiceReference1.Service1Client service2 = new ServiceReference1.Service1Client();
        public static int applyforCreditCard()
        {
            
            int creditcardnumber = (int)service2.applyCreditCard();
            return creditcardnumber;
            //return 0;
        }


        public static Boolean validateCreditCard(int cardNumber)
        {
            int card = cardNumber;
            //string isvalid = service2.validateCreditCard(card.ToString());

            //if (isvalid.Equals("Valid Card Number"))
            
            return true;
        }
    }
}
