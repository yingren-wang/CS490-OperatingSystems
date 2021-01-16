using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CS490_Project2
{
    /* Main class to initiate producer and consumers */
    class MainClass
    {
        static void Main(string[] args) {
            // create a single heap
            MinHeap minHeap = new MinHeap(100);

            // spawn producer thread
            Producer producer = new Producer(minHeap, 1);
            Thread producerThread = new Thread(new ThreadStart(producer.run));
            producerThread.Start();

            // spawn 2 instances of consumer thread
            Consumer consumer1 = new Consumer(minHeap, 1);
            Consumer consumer2 = new Consumer(minHeap, 2);

            Thread consumerThread1 = new Thread(new ThreadStart(consumer1.run));
            Thread consumerThread2 = new Thread(new ThreadStart(consumer2.run));
            consumerThread1.Start();
            consumerThread2.Start();

            try
            {
                producerThread.Join();

                if (producer.stoppedProducer)
                {
                    // when producer is stopped, start watching queue
                    Console.WriteLine("Queuewatcher has started...");
                    Thread.Sleep(1000);
                    consumerThread1.Interrupt();
                    consumerThread2.Interrupt();
                }

                // interrupt consumers if there's no processes in the queue
                if (minHeap.currentSize <= 0)
                {
                    consumerThread1.Interrupt();
                    consumerThread2.Interrupt();
                }

                consumerThread1.Join();
                consumerThread2.Join();
                Console.WriteLine("Queuewatcher is exiting...");
            }
            catch (ThreadInterruptedException e)
            {
                // ignore
            }

            Console.WriteLine("Main program exiting");
            Console.ReadKey(); // prevents the console from exiting automatically
        }
    }
}
