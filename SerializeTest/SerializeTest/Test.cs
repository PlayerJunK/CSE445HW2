using System;
using System.Xml.Serialization;
using System.IO;

namespace Test
{




    public class Test
    {
        public static void Main()
        {
            Order myOrder = new Order();
            myOrder.x = "hello";
            string test = TestExt.SerializeObject(myOrder);

            //XmlSerializer x = new XmlSerializer(myOrder.GetType());
            //x.Serialize(Console.Out, myOrder);

            //Console.ReadKey();
           

            Console.WriteLine(test);

            Order newOrder = (Order)TestExt.DeserializeObject<Test.Order>(test);


            Console.WriteLine(newOrder.x);
        }

        public class Order
        {
            public string x;
            public Order()
            { 

}
        }
    }


    public static class TestExt
    {
        public static Object DeserializeObject<T>(this string toDeserialize)
        {
            //XmlRootAttribute xr = new XmlRootAttribute("Test");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Test.Order));
            StringReader textReader = new StringReader(toDeserialize);

            return xmlSerializer.Deserialize(textReader);
        }

        public static string SerializeObject<T>(this T toSerialize)
        {

            StringWriter textWriter = new StringWriter();
            //XmlRootAttribute xr = new XmlRootAttribute("Test");
            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("", "");
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();

        }
    }



}


