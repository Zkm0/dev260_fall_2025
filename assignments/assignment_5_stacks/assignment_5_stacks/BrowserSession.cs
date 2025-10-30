using System;
using System.Collections.Generic;

namespace Assignment5
{
    /// <summary>
    /// Manages browser navigation state with back and forward stacks
    /// </summary>
    public class BrowserSession
    {
        private Stack<WebPage> backStack;
        private Stack<WebPage> forwardStack;
        private WebPage? currentPage;

        public WebPage? CurrentPage => currentPage;
        public int BackHistoryCount => backStack.Count;
        public int ForwardHistoryCount => forwardStack.Count;
        public bool CanGoBack => backStack.Count > 0;
        public bool CanGoForward => forwardStack.Count > 0;

        public BrowserSession()
        {
            backStack = new Stack<WebPage>();
            forwardStack = new Stack<WebPage>();
            currentPage = null;
        }

        /// <summary>
        /// Navigate to a new URL
        /// TODO: Implement this method
        /// - If there's a current page, push it to back stack
        /// - Clear the forward stack (new navigation invalidates forward history)
        /// - Set the new page as current
        /// </summary>
        public void VisitUrl(string url, string title)
        {
            // guard/validation
            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine(" Invalid URL.");
                return;
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine(" Please provide a page title.");
                return;
            }

            // if there was an open page, it becomes "back" history
            if (currentPage != null)
                backStack.Push(currentPage);

            // anytime a brand-new URL is visited, forward history is gone
            forwardStack.Clear();

            // to set the new current
            currentPage = new WebPage(url, title);


            Console.WriteLine($"Visited: {title} ({url})");
        }

        /// <summary>
        /// Navigate back to previous page
        /// TODO: Implement this method
        /// - Check if back navigation is possible
        /// - Move current page to forward stack
        /// - Pop page from back stack and make it current
        /// </summary>
        public bool GoBack()
        {
            // can't go back if no current 
            if (currentPage == null || backStack.Count == 0)
            {
                Console.WriteLine(" No page to go back to.");
                return false;
            }

            // current goes to forward, the top of back becomes current
            forwardStack.Push(currentPage);
            currentPage = backStack.Pop();

            Console.WriteLine($" Back to: {currentPage.Title} ({currentPage.Url})");
            return true;
        }

        /// <summary>
        /// Navigate forward to next page
        /// TODO: Implement this method
        /// - Check if forward navigation is possible
        /// - Move current page to back stack
        /// - Pop page from forward stack and make it current
        /// </summary>
        public bool GoForward()
        {
            // can't move forward if nothing stored 
            if (currentPage == null || forwardStack.Count == 0)
            {
                Console.WriteLine("No page to go forward to.");
                return false;
            }

            // current goes to back, the top of forward becomes current
            backStack.Push(currentPage);
            currentPage = forwardStack.Pop();

            Console.WriteLine($"Forward to: {currentPage.Title} ({currentPage.Url})");
            return true;
        }

        /// <summary>
        /// Get navigation status information
        /// </summary>
        public string GetNavigationStatus()
        {
            var status = $" Navigation Status:\n";
            status += $"   Back History: {BackHistoryCount} pages\n";
            status += $"   Forward History: {ForwardHistoryCount} pages\n";
            status += $"   Can Go Back: {(CanGoBack ? " Yes" : " No")}\n";
            status += $"   Can Go Forward: {(CanGoForward ? " Yes" : " No")}";
            return status;
        }

        /// <summary>
        /// Display back history (most recent first)
        /// TODO: Implement this method
        /// Expected output format:
        /// 📚 Back History (most recent first):
        ///    1. Google Search (https://www.google.com)
        ///    2. GitHub Homepage (https://github.com)
        ///    3. Stack Overflow (https://stackoverflow.com)
        /// 
        /// If empty, show: "   (No back history)"
        /// Use foreach to iterate through backStack (it gives LIFO order automatically)
        /// </summary>
        public void DisplayBackHistory()
        {
            Console.WriteLine(" Back History:");
            if (backStack.Count == 0)
            {
                Console.WriteLine("   (No back history)");
                return;
            }

            // Stack enumerates from tbhe top to the bottom, listing most recent first.
            Console.WriteLine("Back History:");
            if (backStack.Count == 0) { Console.WriteLine("   (No back history)"); return; }

            int i = 1;
            foreach (var page in backStack)
            {
                Console.WriteLine($"   {i}. {page.Title} ({page.Url})");
                i++;
            }
        }

        /// <summary>
        /// Display forward history (next page first)
        /// TODO: Implement this method
        /// Expected output format:
        /// 📖 Forward History (next page first):
        ///    1. Documentation Page (https://docs.microsoft.com)
        ///    2. YouTube (https://www.youtube.com)
        /// 
        /// If empty, show: "   (No forward history)"
        /// Use foreach to iterate through forwardStack (it gives LIFO order automatically)
        /// </summary>
        public void DisplayForwardHistory()
        {
            Console.WriteLine(" Forward History (next page first):");
            if (forwardStack.Count == 0)
            {
                Console.WriteLine("(No forward history)");
                return;
            }

            Console.WriteLine(" Forward History (next page first):");
            if (forwardStack.Count == 0) { Console.WriteLine("(No forward history)"); return; }

            int i = 1;
            foreach (var page in forwardStack)
            {
                Console.WriteLine($"   {i}. {page.Title} ({page.Url})");
                i++;
            }
        }

        /// <summary>
        /// Clear all navigation history
        /// TODO: Implement this method
        /// Expected behavior:
        /// - Count total pages to be cleared (backStack.Count + forwardStack.Count)
        /// - Clear both backStack and forwardStack
        /// - Print confirmation: "✅ Cleared {totalCleared} pages from navigation history."
        /// Note: This does NOT clear the current page, only the navigation history
        /// </summary>
        public void ClearHistory()
        {
            int totalCleared = backStack.Count + forwardStack.Count;
            backStack.Clear();
            forwardStack.Clear();

            Console.WriteLine($" Cleared {totalCleared} pages from navigation history.");
        }
    }
}
