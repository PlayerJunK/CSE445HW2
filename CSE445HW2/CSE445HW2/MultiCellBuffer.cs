using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CSE445HW2
{
    class MultiCellBuffer
    {
        struct cell{
            public int id;
            public Object Order = new Object();
        }
        //Establish a reference for the lock. Scope of lock will be limited to the methods inside this class.
        private System.Object myLock = new System.Object();
        private cell[] orderBuffer = new cell[3];
        int bufferCapacity;
        const int BUFFERSIZE = 3;
     
        public MultiCellBuffer()
        {
            bufferCapacity = 0;
            for (int i = 0; i < 3; i++)
            {
                orderBuffer[i].id = -1;
                orderBuffer[i].Order = null;
            }   
        }

        /*Lock the method so only one thread will be able to access. If there are already 3 orders in the buffer,
        the thread will wait until an order has been removed and is notified (PulseAll).*/
        public void addObjectwithID(int id, Object o)
        {
            lock (myLock)
            {
                while (bufferCapacity >= BUFFERSIZE)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch { }
                }
                for (int i = 0; i <= BUFFERSIZE; i++)
                {
                    if (orderBuffer[i].Order == null)
                    {
                        orderBuffer[i].id = id;
                        orderBuffer[i].Order = o;
                    }
                }
                bufferCapacity++;  
                Monitor.PulseAll(this);
            }
        }

        /*Lock the method so only one thread will be able to access. If there are no orders in the buffer,
        the thread will wait until an order has been entered and is notified (PulseAll).*/
        public Object getObjectwithID(int id)
        {
            lock (myLock)
            {
                cell temp = new cell();
                while (bufferCapacity <= 0)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch { }
                }
                for (int i = 0; i <= BUFFERSIZE; i++)
                {
                    if (orderBuffer[i].id == id)
                    {
                        temp = orderBuffer[i];
                        bufferCapacity--;
                        orderBuffer[i].id = -1;
                        orderBuffer[i].Order = null;
                        Monitor.PulseAll(this);
                        return temp.Order;
                    }
                }
               
                Monitor.PulseAll(this);
                return null;
            }
        }


    }
}