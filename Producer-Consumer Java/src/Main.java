/*
This Program implements consumer-producer
Program will create 2 instances of consumer thread and 1 producer thread and consume from/produce to a shared object minHeap

CS 490 - Operating System
10-13-2020
Author: Yingren Wang
*/

public class Main {
    public static void main(String[] args){
        // create a single heap
        MinHeap minHeap = new MinHeap(100);

        // spawn 2 instances of consumer thread
        Consumer consumer1 = new Consumer(minHeap, 1);
        Consumer consumer2 = new Consumer(minHeap, 2);

        Thread consumerThread1 = new Thread(consumer1);
        consumerThread1.start();
        Thread consumerThread2 = new Thread(consumer2);
        consumerThread2.start();

        // spawn producer thread
        Producer producer = new Producer(minHeap, 1);
        Thread producerThread = new Thread(producer);
        producerThread.start();

        try{
            producerThread.join();

            if(producer.stoppedProducer){
                // when producer is stopped, start watching queue
                System.out.println("Queuewatcher has started...");
                Thread.sleep(1000);
                consumerThread1.interrupt();
                consumerThread2.interrupt();
            }

            // interrupt consumers if there's no processes in the queue
            if(minHeap.getCurrentSize() <= 0) {
                consumerThread1.interrupt();
                consumerThread2.interrupt();
            }

            consumerThread1.join();
            consumerThread2.join();
            System.out.println("Queuewatcher is exiting...");
        }
        catch (InterruptedException e){
            // ignore
        }

        System.out.println("Main program exiting");
        System.exit(0);
    }
}


