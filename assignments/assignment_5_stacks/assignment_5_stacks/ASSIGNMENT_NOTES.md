# Assignment 5: Browser Navigation System - Implementation Notes

**Name:** Zoe Mukaba

## Dual-Stack Pattern Understanding

**How the dual-stack pattern works for browser navigation:**
I understood that the dual-stack pattern uses two stacks, Back and Forward, to keep track of the navigation history.
When we visit a new page, the current one goes to the back stack, and the forward stack is cleared.

When we go Back, the current page moves to the forward stack, and the top of the back stack becomes the new current page; and when we go Forward, we can observe the opposite happening.
So, the constant moving between the two stacks is what makes the browser effect of moving through an history in both directions.

## Challenges and Solutions

**Biggest challenge faced:**
I think what was hard fror me was getting the navigation logic right between the two stacks; especially when visiting a new page after going back. I did forget that it should've cleared the forward history.

**How you solved it:**
I went and read the assignment description again, and added a step in VisitUrl()to always clear forward whenever a new page is visited. Testing step by step helped me make sure it was working well.

**Most confusing concept:**
It was remembering which stack represents which direction. I mixed them up at first. Using clear print statements helped me visualize the flow better.

## Code Quality

**What you're most proud of in your implementation:**
I think the logic is pretty clear to read and understand. I like how it handles invalid inputs (like empty URLs or titles) and it gives clear feedback.

**What you would improve if you had more time:**
Maybe I'd add color or icons for a better readability. And I'd add a feature for 

## Testing Approach

**How you tested your implementation:**
For testing, I followed the whole console flow: visiting pages, going back and forward, and checking if history cleared correctly after visiting a new URL.
I also tried edge cases like going through “Back” or “Forward” when the stacks were empty to make sure it worked.

**Issues you discovered during testing:**
I realized the forward stack wasn't clearing until I added that line in VisitUrl().

## Stretch Features
None implemented.

## Time Spent

**Total time:** ~4.5 - 5 hours

**Breakdown:**

- Understanding the assignment: ~1 hour
- Implementing the 6 methods: ~2 hours
- Testing and debugging: ~1h30'
- Writing these notes: ~30'

**Most time-consuming part:** 
The testing and tracing the dual-stack flow carefully until it worked well for the entire navigation menu. Also, debugging the errors I encountered. 
