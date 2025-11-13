using System;
using System.Collections;
using System.Collections.Generic;
 
namespace Week4DoublyLinkedLists.Core
{
    /*
     * ========================================
     * ASSIGNMENT 3: DOUBLY LINKED LIST IMPLEMENTATION
     * ========================================
     *
     * 🎯 IMPLEMENTATION GUIDE:
     * Step 1: Node<T> class (already provided below)
     * Step 2: Basic DoublyLinkedList<T> structure (already provided below)
     * Step 3: Add Methods (AddFirst, AddLast, Insert) - START HERE
     * Step 4: Traversal Methods (DisplayForward, DisplayBackward, ToArray)
     * Step 5: Search Methods (Contains, Find, IndexOf)
     * Step 6: Remove Methods (RemoveFirst, RemoveLast, Remove, RemoveAt)
     * Step 7: Advanced Operations (Clear, Reverse)
     *
     * 💡 TESTING STRATEGY:
     * - Implement each step completely before moving to the next
     * - Use the CoreListDemo to test each step as you complete it
     * - Focus on pointer manipulation - draw diagrams if helpful
     * - Handle edge cases: empty list, single element, etc.
     *
     * 📚 KEY RESOURCES:
     * - GeeksforGeeks Doubly Linked List: https://www.geeksforgeeks.org/dsa/doubly-linked-list/
     * - Each TODO comment includes specific reference links
     *
     * 🚀 START WITH: Step 3 (Add Methods) - look for "STEP 3A" below
     */
    /// <summary>
    /// STEP 1: Node class for doubly linked list (✅ COMPLETED)
    /// Contains data and pointers to next and previous nodes
    /// 📚 Reference: https://www.geeksforgeeks.org/dsa/doubly-linked-list/#node-structure
    /// </summary>
    /// <typeparam name="T">Type of data stored in the node</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Data stored in this node
        /// </summary>
        public T Data { get; set; }
       
        /// <summary>
        /// Reference to the next node in the list
        /// </summary>
        public Node<T>? Next { get; set; }
       
        /// <summary>
        /// Reference to the previous node in the list
        /// </summary>
        public Node<T>? Previous { get; set; }
       
        /// <summary>
        /// Constructor to create a new node with data
        /// </summary>
        /// <param name="data">Data to store in the node</param>
        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
       
        /// <summary>
        /// String representation of the node for debugging
        /// </summary>
        /// <returns>String representation of the node's data</returns>
        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }
    }
   
    /// <summary>
    /// STEP 2: Generic doubly linked list implementation (✅ STRUCTURE COMPLETED)
    /// Supports forward and backward traversal with efficient insertion/deletion
    /// 📚 Reference: https://www.geeksforgeeks.org/dsa/doubly-linked-list/
    ///
    /// 🎯 YOUR TASK: Implement the methods marked with TODO in Steps 3-7
    /// </summary>
    /// <typeparam name="T">Type of elements stored in the list</typeparam>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        #region Private Fields
       
        private Node<T>? head;     // First node in the list
        private Node<T>? tail;     // Last node in the list
        private int count;         // Number of elements in the list
       
        #endregion
       
        #region Public Properties
       
        /// <summary>
        /// Gets the number of elements in the list
        /// </summary>
        public int Count => count;
       
        /// <summary>
        /// Gets whether the list is empty
        /// </summary>
        public bool IsEmpty => count == 0;
       
        /// <summary>
        /// Gets the first node in the list (readonly)
        /// </summary>
        public Node<T>? First => head;
       
        /// <summary>
        /// Gets the last node in the list (readonly)
        /// </summary>
        public Node<T>? Last => tail;
       
        #endregion
       
        #region Constructor
       
        /// <summary>
        /// Initialize an empty doubly linked list
        /// </summary>
        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }
       
        #endregion
       
        #region Step 3: Add Methods - TODO: Students implement these step by step
       
        /// <summary>
        /// STEP 3A: Add an item to the beginning of the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-beginning-in-doubly-linked-list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddFirst(T item)
        {
            // to make a new node that will become the new first (head)
            var node = new Node<T>(item);

            if (head == null)
            {
                //list is empty and this node is both first and last
                head = node;
                tail = node;
            }
            else
            {
            
                node.Next = head;       // new node points forward to old head
                head.Previous = node;   // old head points back to new node
                head = node;            // to move head pointer to the new node
            }

            // One item was added
            count++;
        }
       
        /// <summary>
        /// STEP 3B: Add an item to the end of the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-end-in-doubly-linked-list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddLast(T item)
        {
            // make a new node that will sit at the end, at the tail
            var node = new Node<T>(item);

            if (tail == null)
            {
                // empty list: this node is both the first and the last
                head = node;
                tail = node;
            }
            else
            {
            
                tail.Next = node;        // current tail points forward to new node
                node.Previous = tail;    // new node points back to current tail
                tail = node;             // move tail pointer to the new node
            }

            //One item was added
            count++;
        }
       
        /// <summary>
        /// Convenience method - calls AddLast
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(T item) => AddLast(item);
       
        /// <summary>
        /// STEP 3C: Insert an item at a specific index
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-after-a-given-node-in-doubly-linked-list
        /// </summary>
        /// <param name="index">Index to insert at (0-based)</param>
        /// <param name="item">Item to insert</param>
        public void Insert(int index, T item)
        {
            // 1) validate range (0..count)
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // 2)reuse existing methods
            if (index == 0) { AddFirst(item); return; }
            if (index == count) { AddLast(item); return; }

            // 3)find node that's currently at index
            var at = GetNodeAt(index);    // won't be null because 0<index<count
            var node = new Node<T>(item);

            // insert before "at"
            node.Previous = at.Previous;
            node.Next = at;
            at.Previous!.Next = node;
            at.Previous = node;

    
            count++;
        }
       
        #endregion
       
        #region Step 4: Traversal and Display Methods - TODO: Students implement these
       
        /// <summary>
        /// STEP 4A: Display the list in forward direction  
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#forward-traversal
        /// </summary>
        public void DisplayForward()
        {
            // show a readable snapshot of the list from head to tail
            if (head == null)
            {
                Console.WriteLine("Forward: [ ]");
                return;
            }

            Console.Write("Forward: [ ");
            var cur = head;               // to start at the first node
            while (cur != null)
            {
                Console.Write(cur.Data);  // print the value
                if (cur.Next != null) Console.Write(" ");
                cur = cur.Next;           // move forward
            }
            Console.WriteLine(" ]");
        }
       
        /// <summary>
        /// STEP 4B: Display the list in backward direction
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#backward-traversal
        /// </summary>
        public void DisplayBackward()
        {
            // show the list from tail to head 
            if (tail == null)
            {
                Console.WriteLine("Backward: [ ]");
                return;
            }

            Console.Write("Backward: [ ");
            var cur = tail;               //tostart at the last node
            while (cur != null)
            {
                Console.Write(cur.Data);
                if (cur.Previous != null) Console.Write(" ");
                cur = cur.Previous;       //move backward
            }
            Console.WriteLine(" ]");
        }
       
        /// <summary>
        /// STEP 4C: Convert the list to an array
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>Array containing all list elements</returns>
        public T[] ToArray()
        {
            // to copy values into a new array in forward order
            var result = new T[count];
            int i = 0;
            var cur = head;
            while (cur != null)
            {
                result[i++] = cur.Data;
                cur = cur.Next;
            }
            return result;
        }
       
        #endregion
       
        #region Step 5: Search Methods - TODO: Students implement these
       
        /// <summary>
        /// STEP 5A: Check if the list contains a specific item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to check for</param>
        /// <returns>True if item is in the list</returns>
        public bool Contains(T item)
        {
            // walk the list and return true as soon as we see the item
            var cmp = EqualityComparer<T>.Default;
            var cur = head;
            while (cur != null)
            {
                if (cmp.Equals(cur.Data, item)) return true;
                cur = cur.Next;
            }
            return false;
        }
       
        /// <summary>
        /// STEP 5B: Find the first node containing the specified item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>Node containing the item, or null if not found</returns>
        public Node<T>? Find(T item)
        {
            // to return the first node whose Data matches the item
            var cmp = EqualityComparer<T>.Default;
            var cur = head;
            while (cur != null)
            {
                if (cmp.Equals(cur.Data, item)) return cur;
                cur = cur.Next;
            }
            return null;
        }
       
        /// <summary>
        /// STEP 5C: Find the index of an item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>Index of the item, or -1 if not found</returns>
        public int IndexOf(T item)
        {
            // to count positions as we move forward; -1 if missing
            var cmp = EqualityComparer<T>.Default;
            int i = 0;
            var cur = head;
            while (cur != null)
            {
                if (cmp.Equals(cur.Data, item)) return i;
                i++;
                cur = cur.Next;
            }
            return -1;
        }
       
        #endregion
       
        #region Step 6: Remove Methods - TODO: Students implement these
       
        /// <summary>
        /// STEP 6A: Remove the first item in the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-beginning-in-doubly-linked-list
        /// </summary>
        /// <returns>The removed item</returns>
        public T RemoveFirst()
        {
            // remove from the front; throw if empty
            if (head == null) throw new InvalidOperationException("List is empty.");

            var value = head.Data;     //to save what we're removing

            if (head == tail)
            {
                // only one node in the list
                head = null;
                tail = null;
            }
            else
            {
                // move head forward and clear its Previous
                head = head.Next;
                head!.Previous = null;
            }

            count--;                  
            return value;
        }
       
        /// <summary>
        /// STEP 6B: Remove the last item in the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-end-in-doubly-linked-list
        /// </summary>
        /// <returns>The removed item</returns>
        public T RemoveLast()
        {
            // remove from the end; throw if empty
            if (tail == null) throw new InvalidOperationException("List is empty.");

            var value = tail.Data;

            if (head == tail)
            {
                // only one node in the list
                head = null;
                tail = null;
            }
            else
            {
                //to move tail backward and clear its Next
                tail = tail.Previous;
                tail!.Next = null;
            }

            count--;
            return value;
        }
       
        /// <summary>
        /// STEP 6C: Remove the first occurrence of an item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item was found and removed</returns>
        public bool Remove(T item)
        {
            // find the node; if found, remove that node
            var cmp = EqualityComparer<T>.Default;
            var cur = head;
            while (cur != null)
            {
                if (cmp.Equals(cur.Data, item))
                {
                    RemoveNode(cur);
                    return true;
                }
                cur = cur.Next;
            }
            return false;
        }
       
        /// <summary>
        /// STEP 6D: Remove item at a specific index
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-a-specific-position-in-doubly-linked-list
        /// </summary>
        /// <param name="index">Index to remove (0-based)</param>
        /// <returns>The removed item</returns>
        public T RemoveAt(int index)
        {
            // to check range
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // fast paths for ends
            if (index == 0) return RemoveFirst();
            if (index == count - 1) return RemoveLast();

            // middle removal
            var node = GetNodeAt(index);
            var value = node.Data;
            RemoveNode(node);
            return value;
        }
       
        #endregion
       
        #region Step 7: Advanced Operations - TODO: Students implement these
       
        /// <summary>
        /// STEP 7A: Remove all items from the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://docs.microsoft.com/en-us/dotnet/standard/collections/
        /// </summary>
        public void Clear()
        {
            // to reset pointers and count
            head = null;
            tail = null;
            count = 0;
        }
       
        /// <summary>
        /// STEP 7B: Reverse the list in-place
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/reverse-a-doubly-linked-list/
        /// </summary>
        public void Reverse()
        {
            // swap Next/Previous on each node, then swap head/tail
            if (head == null || head == tail) return; 

            var cur = head;
            Node<T>? temp = null;

            while (cur != null)
            {
                // swap the pointers on this node
                temp = cur.Previous;
                cur.Previous = cur.Next;
                cur.Next = temp;

                // move "forward" which is now Previous (because we swapped)
                cur = cur.Previous;
            }

            // after the loop, temp is at the old head's previous,so the new head is temp.Previous
            if (temp != null)
            {
                tail = head;
                head = temp.Previous;
            }
        }
       
        #endregion
       
        #region Helper Methods - TODO: Students may need these for advanced operations
       
        /// <summary>
        /// Get node at specific index (helper for internal use)
        /// Optimizes traversal by starting from head or tail based on index
        /// Used by Insert, RemoveAt, and other positional operations
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <param name="index">Index to get node at (0-based)</param>
        /// <returns>Node at the specified index</returns>
        private Node<T> GetNodeAt(int index)
        {
            // validate
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // choose a side to start from to reduce steps
            if (index < count / 2)
            {
                // walk from the head forwards
                int i = 0;
                var cur = head;
                while (cur != null)
                {
                    if (i == index) return cur;
                    i++;
                    cur = cur.Next;
                }
            }
            else
            {
                // walk from the tail backwards
                int i = count - 1;
                var cur = tail;
                while (cur != null)
                {
                    if (i == index) return cur;
                    i--;
                    cur = cur.Previous;
                }
            }

            // we should always return inside the loops
            throw new InvalidOperationException("GetNodeAt failed unexpectedly.");
        }
       
        /// <summary>
        /// Remove a specific node from the list (helper method)
        /// Handles all the pointer manipulation for node removal
        /// Used by Remove, RemoveAt, and other removal operations
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="node">Node to remove (must not be null)</param>
        private void RemoveNode(Node<T> node)
        {
            // four cases: only node, head, tail, middle
            if (node == head && node == tail)
            {
                // only one node total
                head = null;
                tail = null;
            }
            else if (node == head)
            {
                // removing the first node
                head = node.Next;
                head!.Previous = null;
            }
            else if (node == tail)
            {
                // removing the last node
                tail = node.Previous;
                tail!.Next = null;
            }
            else
            {
                // removing from the middle: bypass the node
                node.Previous!.Next = node.Next;
                node.Next!.Previous = node.Previous;
            }

            count--; // one fewer node now
        }
       
        #endregion
       
        #region IEnumerable Implementation
       
        /// <summary>
        /// Get enumerator for foreach support
        /// </summary>
        /// <returns>Enumerator for the list</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
       
        /// <summary>
        /// Non-generic enumerator implementation
        /// </summary>
        /// <returns>Non-generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
       
        #endregion
       
        #region Display Methods for Testing and Debugging
       
        /// <summary>
        /// Display detailed information about the list structure
        /// Perfect for testing and understanding the list state
        /// </summary>
        public void DisplayInfo()
        {
            Console.WriteLine("=== DOUBLY LINKED LIST STATE ===");
            Console.WriteLine($"Count: {Count}");
            Console.WriteLine($"IsEmpty: {IsEmpty}");
            Console.WriteLine($"First: {(head?.Data?.ToString() ?? "null")}");
            Console.WriteLine($"Last: {(tail?.Data?.ToString() ?? "null")}");
            Console.WriteLine();
           
            // Show both traversal directions
            try
            {
                DisplayForward();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Forward:  [TODO: Implement DisplayForward in Step 4a]");
            }
           
            try
            {
                DisplayBackward();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Backward: [TODO: Implement DisplayBackward in Step 4b]");
            }
           
            Console.WriteLine();
        }
       
        #endregion
    }
}
