# CS490-OperatingSystems

**Author:**   Yingren Wang

**Course:** 	CS 490 Intro to Operating Systems

**Semester:**	Fall 2020

**Projects:**	Producer and Consumer Pattern in Java and C#

**Summary:**

The project implements the Producer-Consumer pattern using Java and C#.

**Classes:**
- Node: represent a process, including a process id, a priority, and a timeslice
- Min-Heap: to store the process nodes, the lowest priority number is always at the top of the heap ready to go when the next process is chosen
- Runnable/Thread type for Consumer: to "consume"/retrieve a node off the top of the heap, when a process has completed the execution, the thread reports the process id, priority value, and the time when it completed, when there's no work available on the heap, thread goes to sleep for some pre-determined idle time
- Runnable/Thread type for Producer: to create and add processes to the heap, thread wakes up at random periodic intervals and add a random amount of proceesses into the heap
