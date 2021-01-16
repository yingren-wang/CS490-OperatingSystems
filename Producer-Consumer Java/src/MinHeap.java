/*
MinHeap class is a binary tree that the value in each internal node is smaller than or equal to the values in the children of that node
This min heap is designed to store the nodes in order based on their priority, priority 0 is the highest priority

CS 490 - Operating System
10-13-2020
Author: Yingren Wang
*/

public class MinHeap {
    private Node heap[];
    private int currentSize;
    private int maxSize;

    // constructor
    public MinHeap(int maxSize){
        this.maxSize = maxSize;
        this.currentSize = 0;
        heap = new Node[this.maxSize + 1];
    }

    // getter
    public int getCurrentSize(){return currentSize;}

    // get parent/child node
    synchronized int getParentPos(int nodePos){
        return nodePos / 2;
    }
    synchronized public int getLeftChildPos(int nodePos){
        return nodePos * 2;
    }
    synchronized public int getRightChildPos(int nodePos){
        return nodePos * 2 + 1;
    }

    // remove the smallest node from the min heap
    synchronized public Node removeNode(){
        Node removedNode = heap[0];
        if(currentSize >= 1){
            heap[0] = heap[currentSize - 1]; // move the very end of the node to the top

        }
        else{
            // no process in the heap, return null
            return null;
        }
        heapify(0);
        currentSize--;
        return removedNode;
    }

    // swap two nodes by passing their array positions
    synchronized void swapNodes(int node1Pos, int node2Pos){
        Node temp = heap[node2Pos];
        heap[node2Pos] = heap[node1Pos];
        heap[node1Pos] = temp;
    }

    // test to see if a node is a leaf node
    synchronized boolean isLeaf(int pos){
        if(pos >= (currentSize / 2) && pos <= currentSize){
            return true;
        }
        return false;
    }

    // insert a node to the min heap
    synchronized public void insertNode(Node node){
        if(currentSize >= maxSize) return;

        // insert the node at the end of the array, then use compare and swap to maintain order
        heap[currentSize] = node;
        currentSize++;

        int currentPos = currentSize - 1;
        while(heap[currentPos].priority < heap[getParentPos(currentPos)].priority){
            swapNodes(currentPos, getParentPos(currentPos));
            currentPos = getParentPos(currentPos);
        }
    }

    // function to put nodes in order depending on priority
    synchronized public void heapify(int pos){
        if(!isLeaf(pos)){
            // compare priority with child nodes, swap nodes until both child nodes' priorities are greater than the "root"'s priority
            if(heap[pos].priority > heap[getLeftChildPos(pos)].priority || heap[pos].priority > heap[getRightChildPos(pos)].priority){
                if(heap[getLeftChildPos(pos)].priority < heap[getRightChildPos(pos)].priority){
                    swapNodes(pos, getLeftChildPos(pos));
                    heapify(getLeftChildPos(pos));
                }
                else{
                    swapNodes(pos, getRightChildPos(pos));
                    heapify(getRightChildPos(pos));
                }
            }
        }
    }

    // function to sort the heap to min heap
    synchronized public void minHeap(){
        for(int i = (currentSize/2 - 1); i >= 0; i--){
            heapify(i);
        }
    }

    // print out the heap, not used in the program
    synchronized public void printHeap(){
        for(int i = 0; i < currentSize; i++){
            System.out.println(heap[i].processID + " ");
        }
    }
}
