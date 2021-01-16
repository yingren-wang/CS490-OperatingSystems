/*
Node class, contains info of a process

CS 490 - Operating System
10-13-2020
Author: Yingren Wang
 */

class Node {
    public int processID;
    public int priority;
    public int timeSlice;

    public Node(int id, int p, int ts){
        processID = id;
        priority = p;
        timeSlice = ts;
    }
}