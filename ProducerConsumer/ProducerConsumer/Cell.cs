using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Cell
    {
        int cellContents;
        bool State = false;
        public int Read()
        {
            lock (this) // Synchronizing block of code.
            {
                if (!State)
                { // Wait until Cell.Write is done producing
                    try
                    {
                        Monitor.Wait(this); // Waits for the Monitor.Pulse in Write
                    }
                    catch (SynchronizationLockException e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                }
                System.Diagnostics.Debug.WriteLine(String.
                Format("Consumed cell item {0}", cellContents));
                State = false; // Consumption is done.
                Monitor.Pulse(this); // Pulse tells Cell.Write that  Cell.Read is finished.
            }
            return cellContents;
        }
        public void Write(int n)
        {
            lock (this) // Synchronization block
            {
                if (State)
                { // Wait until Cell.Read is done consumption.
                    try
                    {
                        Monitor.Wait(this); // Wait for the Monitor. Pulse in Read.
                    }
                    catch (SynchronizationLockException e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                }
                cellContents = n;
                System.Diagnostics.Debug.WriteLine(String.
                Format("Produced cell item {0}", cellContents));
                State = true; // Set State to indicate production is done.
                Monitor.Pulse(this); // Pulse tells Cell.Read that Cell.Write is finished.
            }
        }
    }
}
