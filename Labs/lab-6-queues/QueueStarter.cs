using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Queue<T> Essential Operations:
- new Queue<SupportTicket>()        // Create empty queue
- queue.Enqueue(item)               // Add item to back (FIFO)
- queue.Dequeue()                   // Remove and return front item
- queue.Peek()                      // Look at front item (don't remove)
- queue.Clear()                     // Remove all items
- queue.Count                       // Get number of items

Safety Rules:
- ALWAYS check queue.Count > 0 before Dequeue() or Peek()
- Empty queue Dequeue() throws InvalidOperationException
- Empty queue Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (queue.Count > 0) { ... }
- FIFO order: First item enqueued is first item dequeued
- Enumeration: foreach gives front-to-back order

Helpful Icons:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display
- ℹ️ Information
- 📊 Stats
- 🎫 Ticket
- 🔄 Process
*/

namespace QueueLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Complete the TODO steps to build a complete IT Support Desk Queue system.
    /// </summary>
    class Program
    {
        // TODO Step 1: Set up your data structures and tracking variables

        // the main FIFO queue for tickets
        private static readonly Queue<SupportTicket> ticketQueue = new Queue<SupportTicket>();

        // counts how many tickets created so far 
        private static int ticketCounter = 1;

        // count operations (submit, process, peek, display, clear, urgent, search)
        private static int totalOperations = 0;

        // track when this session started 
        private static readonly DateTime sessionStart = DateTime.Now;

        // pre-defined ticket options for easy selection during demos
        private static readonly string[] CommonIssues = {
            "Login issues - cannot access email",
            "Password reset request",
            "Software installation help",
            "Printer not working",
            "Internet connection problems",
            "Computer running slowly",
            "Email not sending/receiving",
            "VPN connection issues",
            "Application crashes on startup",
            "File recovery assistance",
            "Monitor display problems",
            "Keyboard/mouse not responding",
            "Video conference setup help",
            "File sharing permission issues",
            "Security software alert"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("🎫 IT Support Desk Queue Management");
            Console.WriteLine("===================================");
            Console.WriteLine("Building a ticket queue system with FIFO processing\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "submit":
                        HandleSubmitTicket();
                        break;
                    case "2":
                    case "process":
                        HandleProcessTicket();
                        break;
                    case "3":
                    case "peek":
                    case "next":
                        HandlePeekNext();
                        break;
                    case "4":
                    case "display":
                    case "queue":
                        HandleDisplayQueue();
                        break;
                    case "5":
                    case "urgent":
                        HandleUrgentTicket();
                        break;
                    case "6":
                    case "search":
                        HandleSearchTicket();
                        break;
                    case "7":
                    case "stats":
                        HandleQueueStatistics();
                        break;
                    case "8":
                    case "clear":
                        HandleClearQueue();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            string nextTicket = ticketQueue.Count > 0 ? ticketQueue.Peek().TicketId : "None";

            Console.WriteLine("┌─ Support Desk Queue Operations ─────────────────┐");
            Console.WriteLine("│ 1. Submit      │ 2. Process    │ 3. Peek/Next  │");
            Console.WriteLine("│ 4. Display     │ 5. Urgent     │ 6. Search      │");
            Console.WriteLine("│ 7. Stats       │ 8. Clear      │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            Console.WriteLine($"Queue: {ticketQueue.Count} tickets | Next: {nextTicket} | Total ops: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // TODO Step 2: handle submitting new tickets 
        static void HandleSubmitTicket()
        {
            Console.WriteLine("\n Submit New Support Ticket");
            Console.WriteLine("Choose from common issues or enter custom:");

          
            // display quick selection options
            for (int i = 0; i < Math.Min(5, CommonIssues.Length); i++)
            {
                Console.WriteLine($"  {i + 1}. {CommonIssues[i]}");
            }
            Console.WriteLine("  6. Enter custom issue");
            Console.WriteLine("  0. Cancel");
            
            Console.Write("\nSelect option (0-6): ");
            string? choice = Console.ReadLine();
            
            if (choice == "0")
            {
                Console.WriteLine(" Ticket submission cancelled.\n");
                return;
            }
            
            string description = "";
            
            if (int.TryParse(choice, out int index) && index >= 1 && index <= 5)
            {
                description = CommonIssues[index - 1];
            }
            else if (choice == "6")
            {
                Console.Write("Enter issue description: ");
                description = Console.ReadLine()?.Trim() ?? "";
            }
           
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine(" Description cannot be empty; ticket submission cancelled.\n");
                return;
            }
            // TODO:
            // 1. Create ticket ID using ticketCounter (format: "T001", "T002", etc.)
            // 2. Create new SupportTicket with ID, description, "Normal" priority, and "User"
            // 3. Enqueue the ticket to ticketQueue
            // 4. Increment ticketCounter and totalOperations
            // 5. Show success message with ticket ID, description, and queue position

            // 1) Build ticket ID like T001
            string ticketId = $"T{ticketCounter:D3}";

            // 2) Make the ticket; normal priority submitted by the user
            var ticket = new SupportTicket(ticketId, description, "Normal", "User");

            // 3) put it at the back of the queue 
            ticketQueue.Enqueue(ticket);

            // 4) increment counters
            ticketCounter++;
            totalOperations++;

            // 5) success message + ticket infos
            int position = ticketQueue.Count; 
            Console.WriteLine($"   Ticket {ticket.TicketId} submitted!");
            Console.WriteLine($"   Issue: {description}");
            Console.WriteLine($"   Position in queue: {position} (first in, first out)\n");

        }

        // TODO Step 3: Handle processing tickets (Dequeue)
        static void HandleProcessTicket()
        {
            // TODO:
            // 1. Display header "Process Next Ticket"
            // 2. Check if ticketQueue has items (guard clause!)
            // 3. If empty, show "No tickets in queue to process" message
            // 4. If not empty:
            //    - Dequeue the next ticket from front of queue
            //    - Increment totalOperations
            //    - Display "Processing ticket:" message
            //    - Show ticket details using ToDetailedString() method
            //    - Check if queue still has tickets after dequeue
            //    - If more tickets exist, show next ticket info using Peek()
            //    - If queue is now empty, show "all tickets processed" message


            Console.WriteLine("\n Process Next Ticket");
            Console.WriteLine("----------------------");

            // to make sure there’s something to process
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine(" No tickets in queue to process.\n");
                return;
            }

            // take the first ticket (front of the queue)
            SupportTicket next = ticketQueue.Dequeue();
            totalOperations++;

            // display what we’re processing
            Console.WriteLine($"Processing ticket {next.TicketId}...");
            Console.WriteLine(next.ToDetailedString());

            // show what’s next or say queue empty
            if (ticketQueue.Count > 0)
            {
                SupportTicket upcoming = ticketQueue.Peek();
                Console.WriteLine($"\n Next up: {upcoming.TicketId} - {upcoming.Description}\n");
            }
            else
            {
                Console.WriteLine("\n All tickets processed, queue is now empty!\n");
            }
        }

        // TODO Step 4: Handle peeking at next ticket
        static void HandlePeekNext()
        {
            // TODO:
            // 1. Display header "View Next Ticket"
            // 2. Check if ticketQueue has items (guard clause!)
            // 3. If empty, show "Queue is empty. No tickets to view" message
            // 4. If not empty:
            //    - Use Peek() to look at front ticket without removing it
            //    - Display "Next ticket to be processed:" message
            //    - Show ticket details using ToDetailedString() method
            //    - Show position information (1 of X in queue)
            // 5. Remember: Peek doesn't modify the queue!

            Console.WriteLine("\n View Next Ticket");
            Console.WriteLine("-------------------");

            // empty queue
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine(" Queue is empty, no tickets to view.\n");
                return;
            }

            // use Peek() to look at the first ticket
            SupportTicket next = ticketQueue.Peek();

            // to display info
            Console.WriteLine($"Next ticket to be processed:\n");
            Console.WriteLine(next.ToDetailedString());

            // to show position info
            Console.WriteLine($"Position: 1 of {ticketQueue.Count} in queue\n");

            totalOperations++;
        }

        // TODO Step 5: Handle displaying the full queue
        static void HandleDisplayQueue()
        {
            // TODO:
            // 1. Display header "Current Support Queue (FIFO Order):"
            // 2. Check if queue is empty
            // 3. If empty, show "Queue is empty - no tickets waiting" and return
            // 4. If not empty:
            //    - Show total ticket count
            //    - Use foreach to enumerate through queue (front to back order)
            //    - Display each ticket with position number (01, 02, 03, etc.)
            //    - Use ToString() method on each ticket for display
            //    - Mark the first ticket with "← Next" to show it's next to be processed
            //    - Increment position counter for each ticket

            Console.WriteLine("\n Current Support Queue (FIFO Order)");
            Console.WriteLine("-------------------------------------");

            // empty
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine(" Queue is empty, no tickets waiting.\n");
                return;
            }

            // show count
            Console.WriteLine($"Total tickets: {ticketQueue.Count}\n");

            // front to back listing with position numbers
            int position = 1;
            foreach (var t in ticketQueue)
            {
                // mark the first item as “Next”
                string marker = (position == 1) ? " ← Next" : "";
                Console.WriteLine($"{position:D2}. {t}{marker}");
                position++;
            }

            Console.WriteLine(); 
            totalOperations++;
        }

        // TODO Step 6: Handle clearing the queue
        static void HandleClearQueue()
        {
            // TODO:
            // 1. Display header "Clear All Tickets"
            // 2. Check if queue is empty
            // 3. If empty, show "Queue is already empty. Nothing to clear" and return
            // 4. If not empty:
            //    - Save current ticket count before clearing
            //    - Ask for confirmation: "This will remove X tickets. Are you sure? (y/N):"
            //    - Read user response and convert to lowercase
            //    - If response is "y" or "yes":
            //      - Clear the ticketQueue
            //      - Increment totalOperations
            //      - Show success message with count of cleared tickets
            //    - If response is anything else, show "Clear operation cancelled"
           
            Console.WriteLine("\n Clear All Tickets");
            Console.WriteLine("--------------------");

            // check if queue empty
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine(" Queue is already empty, nothing to clear.\n");
                return;
            }

            // ask for confirmation
            int countBefore = ticketQueue.Count;
            Console.Write($" This will remove {countBefore} ticket(s). Are you sure? (y/N): ");
            string? answer = Console.ReadLine()?.Trim().ToLower();

            if (answer == "y" || answer == "yes")
            {
                // clear and confirm
                ticketQueue.Clear();
                totalOperations++;
                Console.WriteLine($" Cleared {countBefore} ticket(s) from the queue.\n");
            }
            else
            {
                Console.WriteLine(" Clear operation cancelled.\n");
            }

        }

        // TODO Step 7: Handle urgent ticket submission (Priority)
        static void HandleUrgentTicket()
        {
            // TODO:
            // 1. Display header "Submit Urgent Ticket"
            // 2. Show explanation: "Urgent tickets are processed first!"
            // 3. Prompt for urgent issue description
            // 4. Validate description is not empty or whitespace
            // 5. If empty, show error and return
            // 6. If valid:
            //    - Create ticket ID using "U" prefix and ticketCounter (format: "U001", "U002", etc.)
            //    - Create new SupportTicket with ID, description, "Urgent" priority, and "User"
            //    - For basic implementation: use regular Enqueue (note: real system would prioritize)
            //    - Increment ticketCounter and totalOperations
            //    - Show success message with ticket ID and description
            //    - Add note explaining that real systems would jump to front of queue

            Console.WriteLine("\n Submit Urgent Ticket");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Urgent tickets are prioritized and should be handled first.\n");

            // prompt for description
            Console.Write("Enter urgent issue description: ");
            string? desc = Console.ReadLine()?.Trim();

            // validate input
            if (string.IsNullOrWhiteSpace(desc))
            {
                Console.WriteLine(" Description cannot be empty; urgent ticket cancelled.\n");
                return;
            }

            // build ID with U prefix (U001)
            string ticketId = $"U{ticketCounter:D3}";

            // create the ticket 
            var urgent = new SupportTicket(ticketId, desc, "Urgent", "User");

            // enqueue 
            ticketQueue.Enqueue(urgent);

            // update counters and confirm
            ticketCounter++;
            totalOperations++;

            Console.WriteLine($" Urgent ticket submitted: {urgent.TicketId}");
            Console.WriteLine($"   Description: {desc}");
            Console.WriteLine($"   Added to queue (position {ticketQueue.Count}).");
            Console.WriteLine(" In a real help desk, urgent items would jump ahead of normal tickets.\n");
        }

        // TODO Step 8: Handle searching for tickets
        static void HandleSearchTicket()
        {
            // TODO:
            // 1. Display header "Search Tickets"
            // 2. Check if queue is empty
            // 3. If empty, show "Queue is empty. No tickets to search" and return
            // 4. If not empty:
            //    - Prompt for search term: "Enter ticket ID or description keyword:"
            //    - Validate search term is not empty or whitespace
            //    - If empty, show error and return
            //    - Convert search term to lowercase for case-insensitive search
            //    - Initialize found flag to false and position counter to 1
            //    - Display "Search results:" header
            //    - Loop through queue using foreach:
            //      - Check if ticket ID or description contains search term (use ToLower())
            //      - If match found, display position and ticket info, set found flag
            //      - Increment position counter
            //    - After loop, if no matches found, show "No tickets found matching '[searchterm]'"

            Console.WriteLine("\n Search Tickets");
            Console.WriteLine("-----------------");

            //  empty queue guard
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine(" Queue is empty. No tickets to search.\n");
                return;
            }

            // ask for a term
            Console.Write("Enter ticket ID (e.g., T001/U001) or a keyword: ");
            string? term = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(term))
            {
                Console.WriteLine(" Search term cannot be empty.\n");
                return;
            }

            string needle = term.ToLower();
            bool foundAny = false;
            int pos = 1; 

            Console.WriteLine("\n Search results:");
            foreach (var t in ticketQueue)
            {
                // case insensitive match on ID/description
                bool idMatch = t.TicketId.ToLower().Contains(needle);
                bool textMatch = (t.Description ?? string.Empty).ToLower().Contains(needle);

                if (idMatch || textMatch)
                {
                    foundAny = true;
                    // show position and simple line and a “next” marker if the first item
                    string marker = (pos == 1) ? " ← Next" : "";
                    Console.WriteLine($"  {pos:D2}. {t}{marker}");
                }
                pos++;
            }

            if (!foundAny)
            {
                Console.WriteLine($" No tickets found matching \"{term}\".");
            }
            else
            {
               
                totalOperations++;
            }

            Console.WriteLine();

        }

        static void HandleQueueStatistics()
        {
            Console.WriteLine("\n📊 Queue Statistics");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Current Queue Status:");
            Console.WriteLine($"- Tickets in queue: {ticketQueue.Count}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Session duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Next ticket ID: T{ticketCounter:D3}");
            
            if (ticketQueue.Count > 0)
            {
                var oldestTicket = ticketQueue.Peek();
                Console.WriteLine($"- Longest waiting: {oldestTicket.TicketId} ({oldestTicket.GetFormattedWaitTime()})");
                
                // Count by priority
                int normal = 0, high = 0, urgent = 0;
                foreach (var ticket in ticketQueue)
                {
                    switch (ticket.Priority.ToLower())
                    {
                        case "normal": normal++; break;
                        case "high": high++; break;
                        case "urgent": urgent++; break;
                    }
                }
                Console.WriteLine($"- By priority: 🟢 Normal({normal}) 🟡 High({high}) 🔴 Urgent({urgent})");
            }
            else
            {
                Console.WriteLine("- Queue is empty");
            }
            Console.WriteLine();
        }

        static void ShowSessionSummary()
        {
            Console.WriteLine("\n📋 Final Session Summary");
            Console.WriteLine("========================");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Session Statistics:");
            Console.WriteLine($"- Duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Tickets remaining: {ticketQueue.Count}");
            
            if (ticketQueue.Count > 0)
            {
                Console.WriteLine($"- Unprocessed tickets:");
                int position = 1;
                foreach (var ticket in ticketQueue)
                {
                    Console.WriteLine($"  {position:D2}. {ticket}");
                    position++;
                }
                Console.WriteLine("\n⚠️ Remember to process remaining tickets!");
            }
            else
            {
                Console.WriteLine("✨ All tickets processed - excellent work!");
            }
            
            Console.WriteLine("\nThank you for using the Support Desk Queue System!");
            Console.WriteLine("You've learned FIFO queue operations and real-world ticket management! 🎫\n");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}