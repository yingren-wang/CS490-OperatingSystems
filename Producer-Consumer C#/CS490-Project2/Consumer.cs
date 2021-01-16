using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CS490_Project2
{
    /* consumer class, consume tasks from the minHeap */
    class Consumer
    {
        private MinHeap minHeap;
        private int consumerId;
        public bool stoppedConsumer = false;
        public int consumedCount = 0;

        // constructor
        public Consumer(MinHeap mh, int id)
        {
            this.minHeap = mh;
            this.consumerId = id;
            Console.WriteLine(printConsumer(consumerId) + consumerId + " is starting...");

        }
        // print function, consumer2 has increased indent
        public String printConsumer(int id)
        {
            if (id == 1)
            {
                return "Consumer ";
            }
            else
            {
                return "            Consumer ";
            }
        }

        public void run()
        {
            while (true)
            {
                try
                {
                    Node node;
                    lock(minHeap){
                        // do nothing if there's no node in the heap
                        while (minHeap.currentSize == 0)
                        {
                            if (stoppedConsumer == true) throw new ThreadInterruptedException();
                            Console.WriteLine(printConsumer(consumerId) + consumerId + " is idle");
                            // minHeap.wait(1000);
                            Thread.Sleep(1000);
                            
                        }
                        // consumed node
                        node = minHeap.removeNode();
                        consumedCount++;
                        minHeap.minHeap();
                    }

                    if (node == null)
                    {
                        Console.WriteLine(printConsumer(consumerId) + consumerId + " is idle");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        // "run" the process by implementing sleep
                        Thread.Sleep(node.timeSlice);
                        Console.WriteLine(printConsumer(consumerId) + consumerId + " finished Process: " + node.processID + " pri: " + node.priority + " at " + Environment.TickCount);
                    }
                }
                catch (ThreadInterruptedException exception)
                {
                    // exception
                    if (!stoppedConsumer)
                    {
                        // first time being interrupted, don't break
                        stoppedConsumer = true;
                    }
                    else
                    {
                        // second time being interrupted, break
                        Console.WriteLine(printConsumer(consumerId) + consumerId + " exiting - completed " + consumedCount + " processes...");
                        break;
                    }
                }
            }
        }
    }
}
