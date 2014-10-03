using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSE445HW2.ServiceReference1;
namespace CSE445HW2
{
    class Bank
    {
        static Service1Client bankService = new Service1Client();
        public static int applyforCreditCard()
        {
            
            int creditcardnumber = (int)bankService.applyCreditCard();

            return creditcardnumber;
        }


        public static bool validateCreditCard(int cardNumber)
        {
            int card = cardNumber;

            return bankService.validateCreditCard(card);
        }
    }
}
