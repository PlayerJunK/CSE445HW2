using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using CSE445HW2.edu.asu.eas.venus;
using System.Security.Cryptography;

namespace CSE445HW2
{
    public class Decoder
    {
       static Service service1 = new Service();
       private const string KEY = "ABCDEFGHIJKLMNOP";

        public static Order Decrypt(string encryptedorder)
        {
            string decryptedOrder = decryptString(encryptedorder, KEY);

            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            StringReader reader = new StringReader(decryptedOrder);


            
            Order result = (Order) serializer.Deserialize(reader);

            
            return result;
        }


        //courtesy of extended chicken farm example in class
        public static String decryptString(String input, string key)
        {
            Byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
