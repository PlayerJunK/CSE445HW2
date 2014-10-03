using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using CSE445HW2.edu.asu.eas.venus;
using System.Security.Cryptography;

namespace CSE445HW2
{
    class Encoder
    {
        static Service service1 = new Service();

        private const string KEY = "ABCDEFGHIJKLMNOP";
        public static string Encrypt(Order order)
        {
            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            Order orderToDecrypt = order;
            using (writer)
            {
                serializer.Serialize(writer, order);

            }
            string nonEncryptedOrder = writer.ToString();
            string encryptedOrder =encryptString(nonEncryptedOrder, KEY);
            return encryptedOrder;
        }


        //courtesy of the extended chicken farm example from class
        public static string encryptString(String input, String key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            String result = "";
            try
            {
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }



    }
}
