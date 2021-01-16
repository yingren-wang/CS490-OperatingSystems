using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS490_Project2
{
    /* Node class, this class represents a task that can be produced by Producers or consumed by Consumers*/
    class Node
    {
        // variables
        public int priority { get; set; }
        public int processID { get; set; }
        public int timeSlice { get; set; }

        // Constructor
        public Node(int id, int p, int ts)
        {
            processID = id;
            priority = p;
            timeSlice = ts;
        }
    }
}
