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
        //Establish a reference for the lock. Scope of lock will be limited to the methods inside this class.
        private System.Object myLock = new System.Object();
        
        private Order [] orderBuffer = new Order [3];
        int semaphore = 0;
        bool writable = true;
        public MultiCellBuffer()
        {
            orderBuffer[0] = null;
            orderBuffer[1] = null;
            orderBuffer[2] = null;
        }

        //Lock the method so only one thread will be able to access. May need to be below while statement.
        public void setOneCell(Order o)
        {
            lock (myLock)
            {
                while (semaphore >= 3)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch { }
                }
                orderBuffer[semaphore] = o;
                semaphore++;
            }
        }

        //Lock the method so only one thread will be able to access. May need to be below while statement.
        public Order getOneCell()
        {
            lock (myLock)
            {
                while (semaphore <= 0)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch { }
                }
                semaphore--;
                Monitor.PulseAll(this);
                return this.orderBuffer[semaphore];
            }
        }
        

    }
}
