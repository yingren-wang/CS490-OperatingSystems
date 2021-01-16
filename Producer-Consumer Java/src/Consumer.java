/*
Consumer class, this class is a runnable class that consumes one process at a time from the heap

CS 490 - Operating System
10-13-2020
Author: Yingren Wang
 */

public class Consumer implements Runnable{
    private MinHeap minHeap;
    private int consumerId;
    public boolean stoppedConsumer = false;
    public int consumedCount = 0;

    // constructor
    public Consumer(MinHeap mh, int id){
        this.minHeap = mh;
        this.consumerId = id;
        System.out.println(printConsumer(consumerId) + consumerId + " is starting...");

    }

    // print function, consumer2 has increased indent
    public String printConsumer(int id){
        if(id == 1){
            return "Consumer ";
        }
        else{
            return "            Consumer ";
        }
    }

    public void run(){
        while(true){
            try{
                Node node;
                synchronized (minHeap){
                    // do nothing if there's no node in the heap
                    while(minHeap.getCurrentSize() == 0){
                        if(stoppedConsumer == true) throw new InterruptedException();
                        System.out.println(printConsumer(consumerId) + consumerId + " is idle");
                        minHeap.wait(1000);
                    }
                    // consumed node
                    node = minHeap.removeNode();
                    consumedCount++;
                    minHeap.minHeap();
                }

                if(node == null){
                    System.out.println(printConsumer(consumerId)+ consumerId + " is idle");
                    Thread.sleep(2000);
                }
                else {
                    // "run" the process by implementing sleep
                    Thread.sleep(node.timeSlice);
                    System.out.println(printConsumer(consumerId) + consumerId + " finished Process: " + node.processID + " pri: " + node.priority + " at " + System.currentTimeMillis());
                }
            }
            catch(InterruptedException exception){
                // exception
                if(!stoppedConsumer){
                    // first time being interrupted, don't break
                    stoppedConsumer = true;
                }else{
                    // second time being interrupted, break
                    System.out.println(printConsumer(consumerId)+ consumerId + " exiting - completed " + consumedCount + " processes...");
                    break;
                }
            }
        }
        return;
    }
}