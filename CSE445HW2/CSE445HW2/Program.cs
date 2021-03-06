﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSE445HW2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //instantiate these to negative numbers to know if they are valid or not
            
            //to allow for user to input, change these to -1;
            int NUM_SUPPLIERS = 3;
            int NUM_AGENCIES = 5;

            //num agenecies and num suppliers must be positive integers

            //read number of suppliers from keyboard
            while (NUM_SUPPLIERS < 1)
            {
                //prompt the user
                Console.Write("Number of Hotel Suppliers [eg. 3]: ");

                //assign input from console to variable
                NUM_SUPPLIERS = readIntFromConsole();

                //raise error string if input integer is less than 0
                if (NUM_SUPPLIERS < 1)
                {
                    Console.WriteLine("Input must be greater than 0");
                }
            }


            //read number of agencies from keyboard
            //loop until an interger larger than 0 is inputted
            while (NUM_AGENCIES < NUM_SUPPLIERS)
            {
                //prompt user
                Console.Write("Number of Agencies [eg. 5]: ");

                //read int
                NUM_AGENCIES = readIntFromConsole();

                //raise error for invalid input
                if (NUM_AGENCIES < NUM_SUPPLIERS)
                {
                    Console.WriteLine("The number of Travel Agencies must be >= the number of Hotel Suppliers.");
                }
            }


            //Print the number of agencies and hotel suppliers;
            Console.WriteLine("There are {0} Hotel Suppliers and {1} Travel Agencies", NUM_SUPPLIERS, NUM_AGENCIES);

            //Prompt the user to press enter begin the process.
            Console.WriteLine("Press ENTER to begin the process.");

            //read a line to begin the process.
            Console.ReadLine();

            //set up TravelAgency Objects
            ArrayList travelAgencyObjects = new ArrayList();

            for (int i = 0; i < NUM_AGENCIES; i++)
            {
                //i+1 is so IDs start at 1 and not zero
                TravelAgency newAgency = new TravelAgency(i + 1);
                travelAgencyObjects.Add(newAgency);

            }

            //set up HotelSupplier Threads
            ArrayList hotelSupplierThreads = new ArrayList();
            for (int i = 0; i < NUM_SUPPLIERS; i++)
            {
                //create each new HotelSupplier
                //i+1 is so IDs start at 1 and not zero
                HotelSupplier newSupplier = new HotelSupplier(i + 1);

                //subscribe each agency to each new HotelSupplier
                for (int j = 0; j < NUM_AGENCIES; j++)
                {
                    newSupplier.subscribeToPriceCut(((TravelAgency)travelAgencyObjects[j]).hotelPriceHasBeenCut);
                }

                //create a thread that will start runHotelSupplierOperation
                hotelSupplierThreads.Add(new Thread(newSupplier.runHotelSupplierOperation));
            }


            //start each HotelSupplier thread
            for (int i = 0; i < NUM_SUPPLIERS; i++)
            {
                ((Thread)hotelSupplierThreads[i]).Start();
            }

            //join each HotelSupplier thread
            for (int i = 0; i < NUM_SUPPLIERS; i++)
            {
                ((Thread)hotelSupplierThreads[i]).Join();
            }

            //wait for all running threads to finish executing
            Thread.Sleep(500);

            Console.Write("Operation complete.  Press ENTER to exit...");
            Console.ReadLine();

        }

        //End main
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //Helper method to read an integer from console
        private static int readIntFromConsole(){
            int result = -1; ;

            //boolean to track valid input
            Boolean inputReceived = false;

            //loop until input is valid
            while (!inputReceived)
            {
                //read line of input
                string input = Console.ReadLine();

                //try to parse input as integer
                try
                {
                    result = Int32.Parse(input);
                    //if parse is successful break out of loop.
                    inputReceived = true;
                }
                catch (Exception e)  //catch error where input is a non integer
                {
                    //print error message
                    Console.WriteLine("Please enter an integer.");
                }
            }

            return result;
        }
    }
}
