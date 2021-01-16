/*
Producer class, this class is a runnable class that produces and insert to heap random amount of processes every random
amount of time until the producer has been woken up for minWakeUpTimes

CS 490 - Operating System
10-13-2020
Author: Yingren Wang
*/

import java.util.Random;

public class Producer implements Runnable{
    private MinHeap minHeap;
    private int producerId = 1;
    private int countProcess = 0;
    public boolean stoppedProducer = false;
    private int countProducer = 0;
    private int minWakeUpTimes = 3; // how many times producer is gonna wake up

    // constructor
    public Producer(MinHeap mh, int id){
        this.minHeap = mh;
        this.producerId = id;
    }

    public void run(){
        // wake up producers minWakeUpTimes times
        for(int i = 0; i < minWakeUpTimes; i++){
            try{
                producerId = i + 1;
                System.out.println("* Starting Producer " + producerId + "...");
                countProducer++;

                synchronized (minHeap){
                    // do nothing when there the heap is full
                    while(minHeap.getCurrentSize() == 100){
                        minHeap.wait(1000);
                    }

                    int priority, timeSlice;

                    // creating and adding processes
                    System.out.println("* Producer Adding Nodes");

                    Random random = new Random();
                    int numProcess = random.nextInt(20 - 10) + 10; // random amount of processes to be produce [10, 20]

                    // each time producer wakes, producer creates numProcess number of processes
                    for(int j = 1; j <= numProcess; j++){
                        priority = random.nextInt(8); // random number for priority [0, 8]
                        timeSlice = random.nextInt(500) + 500; // random timeSlice [500, 1000]
                        Node node = new Node(countProcess, priority, timeSlice);
                        minHeap.insertNode(node);
                        countProcess++;
                    }

                    // print out producer info
                    int currentSize = minHeap.getCurrentSize();
                    System.out.println("* Producer thinks there are " + currentSize + " processes in the heap");

                    // Other print out functions, not used in the program
                    // print out how many processes the producer just produced
                    // System.out.println("* Producer " + producerId + " just produced " + numProcess + " nodes");
                    // print out how many processes the producer has produced so far
                    // System.out.println("* Producer thinks there are " + countProcess + " processes in total");
                }
                    // sleep time range [1000 - 5000], time interval between waking up the producer
                    Random random = new Random();
                    Thread.sleep(random.nextInt(5000 - 1000) + 1000);
            }
            catch(Exception exception){
                // exception
            }
        }
        // determine whether producer should stop
        if(countProducer >= minWakeUpTimes){
            stoppedProducer = true;
            System.out.println("* Producer has completed its tasks...");
        }

    }

}