using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;


namespace Hotel_Supplier_Unit_Tests
{
    /// <summary>
    /// Summary description for SerializationTest
    /// </summary>
    [TestClass]
    public class SerializationTest
    {

        private string name;
        private Boolean isMale;
        private int age;
        public SerializationTest()
        {
            //
            // TODO: Add constructor logic here
            //
            name = "JJ";
            isMale = true;
            age = new Random().Next(10, 21);

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        private void SerializeObject(string filename)
        {
            Console.WriteLine("Writing With TextWriter");

            XmlSerializer serializer =
            new XmlSerializer(typeof(OrderedItem));
            OrderedItem i = new OrderedItem();
            i.ItemName = "Widget";
            i.Description = "Regular Widget";
            i.Quantity = 10;
            i.UnitPrice = (decimal)2.30;
            i.Calculate();

            /* Create a StreamWriter to write with. First create a FileStream
               object, and create the StreamWriter specifying an Encoding to use. */
            FileStream fs = new FileStream(filename, FileMode.Create);
            TextWriter writer = new StreamWriter(fs, new UTF8Encoding());
            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, i);
            writer.Close();
        }
    }
}
