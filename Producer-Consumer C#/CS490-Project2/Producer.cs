using System;
using System.Threading;

namespace CS490_Project2
{
    /* producer class, produce tasks in the form of Node and add tasks to the minHeap */
    class Producer
    {
        private MinHeap minHeap;
        private int producerId = 1;
        private int countProcess = 0;
        public bool stoppedProducer = false;
        private int countProducer = 0;
        private int minWakeUpTimes = 3; // how many times producer is gonna wake up

        // constructor
        public Producer(MinHeap mh, int id)
        {
            this.minHeap = mh;
            this.producerId = id;
        }

        public void run()
        {
            // wake up producers minWakeUpTimes times
            for (int i = 0; i < minWakeUpTimes; i++)
            {
                Random random = new Random();
                try
                {
                    producerId = i + 1;
                    Console.WriteLine("* Starting Producer " + producerId + "...");
                    countProducer++;

                    lock(minHeap){
                        // do nothing when there the heap is full
                        while (minHeap.currentSize == 100)
                        {
                            Thread.Sleep(1000);
                            //minHeap.wait(1000);
                        }

                        int priority, timeSlice;

                        // creating and adding processes
                        Console.WriteLine("* Producer Adding Nodes");
                        
                        int numProcess = random.Next(20 - 10) + 10; // random amount of processes to be produce [10, 20]

                        // each time producer wakes, producer creates numProcess number of processes
                        for (int j = 1; j <= numProcess; j++)
                        {
                            priority = random.Next(9); // random number for priority [0, 8]
                            timeSlice = random.Next(500, 1001); // random timeSlice [500, 1000]
                            Node node = new Node(countProcess, priority, timeSlice);
                            minHeap.insertNode(node);
                            countProcess++;
                        }

                        // print out producer info
                        int currentSize = minHeap.currentSize;
                        Console.WriteLine("* Producer thinks there are " + currentSize + " processes in the heap");

                        // Other print out functions, not used in the program
                        // print out how many processes the producer just produced
                        // Console.WriteLine("* Producer " + producerId + " just produced " + numProcess + " nodes");
                        // print out how many processes the producer has produced so far
                        // Console.WriteLine("* Producer thinks there are " + countProcess + " processes in total");
                    }
                    // sleep time range [1000 - 5000], time interval between waking up the producer
                    Thread.Sleep(random.Next(1000, 5001));
                }
                catch (ThreadInterruptedException exception)
                {
                    // exception
                }
            }
            // determine whether producer should stop
            if (countProducer >= minWakeUpTimes)
            {
                stoppedProducer = true;
                Console.WriteLine("* Producer has completed its tasks...");
            }

        }
    }
}
