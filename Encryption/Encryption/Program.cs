using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Encryption
{
    [Serializable]
    public class EncryptionDecrytion
    {

        static void Main()
        {


            edu.asu.eas.venus.Service service1 = new edu.asu.eas.venus.Service();
            /*ServiceReference1.Service1Client service2 = new ServiceReference1.Service1Client();
            int creditcardnumber = (int)service2.applyCreditCard();

            Console.WriteLine("Credit card Number =",creditcardnumber);
            /*string cardtostring = creditcardnumber.ToString();
            Console.WriteLine(cardtostring[0]);
            string text;
            Console.WriteLine("Enter string");
            text=Console.ReadLine();
            string encryptedtest = service1.Encrypt(text);
            Console.WriteLine(text);

            // Encrypts the credit card number
            string encryptedtext = service1.Encrypt(creditcardnumber.ToString());
            Console.WriteLine(encryptedtext);


            // Checking the validity of the credit card
            string ifvalidate = service2.validateCreditCard(encryptedtext);
            Console.WriteLine(ifvalidate);
            
            
            string decryptedtext = service1.Decrypt(encryptedtext);
            Console.WriteLine(decryptedtext);*/
            Bankbank bankobj = new Bankbank();
            Console.WriteLine("Calling applyCreditcard");
            int CC = bankobj.applyforCreditCard();
            Console.WriteLine(CC);


            Boolean isvalid = bankobj.validateCreditCard(CC);
            Console.WriteLine(isvalid);


            Encoder encoderobj=new Encoder();
            string encrypted = encoderobj.Encrypt("Nitish");

            Console.WriteLine(encrypted);


            Decoder decoderobj = new Decoder();
            string decrypted = decoderobj.Decrypt(encrypted);
            Console.ReadKey();

        }
    }

}