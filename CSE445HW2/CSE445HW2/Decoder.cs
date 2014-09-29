using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Encryption
{
    class Decoder
    {
        edu.asu.eas.venus.Service service1 = new edu.asu.eas.venus.Service();

        public string Decrypt(string encryptedorder)
        {
            string decryptedOrder = service1.Decrypt(encryptedorder);

            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            StringReader reader = new StringReader(decryptedOrder);
            
            serializer.Deserialize(reader);

            Console.WriteLine("Deserializing",reader);
            
            return null;
        }
    }
}
