using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS490_Project2
{
    /* min heap class, a binary tree in the array form sorting nodes by priority */
    class MinHeap
    {
        // variables
        public Node[] heap { get; set; }
        public int maxSize { get; set; }
        public int currentSize { get; set; }

        // Constructor 
        public MinHeap(int n)
        {
            maxSize = n;
            heap = new Node[maxSize];
            currentSize = 0;
        }

        // get parrent/child node
        int getParentPos(int nodePos)
        {
            return nodePos / 2;
        }
        public int getLeftChildPos(int nodePos)
        {
            return nodePos * 2;
        }
        public int getRightChildPos(int nodePos)
        {
            return nodePos * 2 + 1;
        }

        // remove the smallest ode from the min heap
        public Node removeNode()
        {
            Node removedNode = heap[0];
            if (currentSize >= 1)
            {
                heap[0] = heap[currentSize - 1]; // move the very end of the node to the top

            }
            else
            {
                // no process in the heap, return null
                return null;
            }
            heapify(0);
            currentSize--;
            return removedNode;
        }

        // swap two nodes by passing their array positions
        void swapNodes(int node1Pos, int node2Pos)
        {
            Node temp = heap[node2Pos];
            heap[node2Pos] = heap[node1Pos];
            heap[node1Pos] = temp;
        }

        // test to see if a node is a leaf node 
        bool isLeaf(int pos)
        {
            if (pos >= (currentSize / 2) && pos <= currentSize)
            {
                return true;
            }
            return false;
        }

        // insert a node to the min heap
        public void insertNode(Node node)
        {
            if (currentSize >= maxSize) return;

            // insert the node at the end of the array, then use compare and swap to maintain order
            heap[currentSize] = node;
            currentSize++;

            int currentPos = currentSize - 1;
            while (heap[currentPos].priority < heap[getParentPos(currentPos)].priority)
            {
                swapNodes(currentPos, getParentPos(currentPos));
                currentPos = getParentPos(currentPos);
            }

        }

        // function to put nodes in order depending on priority
        public void heapify(int pos)
        {
            if (!isLeaf(pos))
            {
                // compare priority with child nodes, swap nodes until both child nodes' priorities are greater than the "root"'s priority
                if (heap[pos].priority > heap[getLeftChildPos(pos)].priority || heap[pos].priority > heap[getRightChildPos(pos)].priority)
                {
                    if (heap[getLeftChildPos(pos)].priority < heap[getRightChildPos(pos)].priority)
                    {
                        swapNodes(pos, getLeftChildPos(pos));
                        heapify(getLeftChildPos(pos));
                    }
                    else
                    {
                        swapNodes(pos, getRightChildPos(pos));
                        heapify(getRightChildPos(pos));
                    }
                }
            }
        }

        // function to sort the heap to min heap
        public void minHeap()
        {
            for (int i = (currentSize / 2 - 1); i >= 0; i--)
            {
                heapify(i);
            }
        }

        // print out the heap, not used in the program
        public void printHeap()
        {
            for (int i = 0; i < currentSize; i++)
            {
                Console.WriteLine(heap[i].processID + " ");
            }
        }
    }
}
