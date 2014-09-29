using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Encryption
{
    class Encoder
    {
        edu.asu.eas.venus.Service service1 = new edu.asu.eas.venus.Service();

        
        public string Encrypt(Object order)
        {
            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(order.GetType());
            using (writer)
            {
                serializer.Serialize(writer, order);

            }
            Console.WriteLine(writer);
            string encryptedOrder = service1.Encrypt(writer.ToString());
            return encryptedOrder;
        }
    }
}
