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
            public Object Order;
        }
 
        private cell[] orderBuffer;
        int bufferCapacity;
        const int BUFFERSIZE = 3;
     
        public MultiCellBuffer()
        {
            orderBuffer = new cell[BUFFERSIZE];
            bufferCapacity = 0;
            for (int i = 0; i < BUFFERSIZE; i++)
            {
                orderBuffer[i].id = -1;
                orderBuffer[i].Order = null;
            }   
        }

        /*Lock the method so only one thread will be able to access. If there are already 3 orders in the buffer,
        the thread will wait until an order has been removed and is notified (PulseAll).*/
        public void addObjectwithID(int id, Object o)
        {

            Monitor.Enter(this);
            while (bufferCapacity >= BUFFERSIZE)
            {
                try
                {
                    Monitor.Wait(this);
                }
                catch { }
            }
               
            for (int i = 0; i < BUFFERSIZE; i++)
            {
                if (orderBuffer[i].Order == null)
                {
                    orderBuffer[i].id = id;
                    orderBuffer[i].Order = o;
                    bufferCapacity++;
                    break;
                }
            }
            Monitor.PulseAll(this);

            Monitor.Exit(this);
            
        }

        /*Lock the method so only one thread will be able to access. If there are no orders in the buffer,
        the thread will wait until an order has been entered and is notified (PulseAll).*/
        public Object getObjectwithID(int id)
        {
            Object result = null;
            Monitor.Enter(this);
            while (bufferCapacity <= 0)
            {
                try
                {
                    Monitor.Wait(this);
                }
                catch { }
            }
                
            for (int i = 0; i < BUFFERSIZE; i++)
            {
                if (orderBuffer[i].id == id)
                {
                    result = orderBuffer[i].Order;
                    bufferCapacity--;
                    orderBuffer[i].id = -1;
                    orderBuffer[i].Order = null;
                    break;
                }
            }
            Monitor.PulseAll(this);

            Monitor.Exit(this);
            return result;
            
        }


    }
}