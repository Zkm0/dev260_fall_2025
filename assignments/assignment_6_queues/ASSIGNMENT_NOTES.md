# Assignment 6: Game Matchmaking System - Implementation Notes

**Name:** Zoe Mukaba

## Multi-Queue Pattern Understanding

**How the multi-queue pattern works for game matchmaking:**

The system uses three queues: Casual , Ranked , and QuickPlay to manage different types of matchmaking.

Casual is a FIFO (first-in, first-out) queue, where any two players can be matched quickly regardless of skill level.

Ranked is stricter, it only matches players who are within ±2 skill levels to make games fairer. 

QuickPlay tries to find a good skill match first, but if the queue has been waiting too long or has more than 4 players, it matches the first two players just be fast.

Each queue uses its own logic but works in parallel to give players flexibility depending on what type of game they want (fast, fair, or balanced).

## Challenges and Solutions

**Biggest challenge faced:**
The hardest part was implementing skill-based matching in the Ranked and QuickPlay modes.
Figuring out how to filter players within a skill range while keeping the rest of the queue intact took me long. 

**How you solved it:**
I used a temporary list of queued players, looked for pairs that met the skill difference condition, 
and rebuilt the queue after removing matched players so that the order of the other players wasn't lost.

**Most confusing concept:**
Understanding how to manage multiple queues at once without mixing them up was confusing at first. 
I also had to remember to call JoinQueue() and LeaveQueue()properly so that players didn't stay marked as “in queue” after a match.

## Code Quality

**What you're most proud of in your implementation:**
I really like the queue management and match creation logic; 
especially how each game mode follows its own rules. 
I also like how the console output looks clear and understandable. 

**What you would improve if you had more time:**
I'd refine the matchmaking logic for QuickPlay to include estimated wait times based on queue growth, 
and maybe add a way to track past matches to avoid repeated matches.

## Testing Approach

**How you tested your implementation:**
I created multiple demo players with different skill levels and tried each game mode separately. 
I used the menu options to simulate joining queues, checking status, running matches, and viewing player stats & System stats.

**Test scenarios you used:**
- Players with very different skills in Ranked (to make sure no match was created).

- Multiple players in Casual (to check FIFO order).

- QuickPlay with more than 4 players (to see if it prioritized speed).

- Single player in queue (to check if it showed “not enough players” messages).

**Issues you discovered during testing:**
At first, players stayed in queue after being matched, so I added proper LeaveQueue()calls. 
Also, I forgot to include waiting times in the display until I added GetQueueTime()for each player.

## Game Mode Understanding

**Casual Mode matching strategy:**
Matches the first two players in line using a FIFO (first-in, first-out) system.
In my code, I dequeue the first two players, call LeaveQueue()on both, and create a new Matchobject.
It's the simplest and fastest mode since it doesn't check skill levels.

**Ranked Mode matching strategy:**
Looks for players within ±2 skill levels and only pairs them if they match the condition  to keep the competition fair. 
I used a nested loop to compare skill ratings and match the first valid pair.
When they matched, both playrs are removed from the queue, and the rest stay in order.

**QuickPlay Mode matching strategy:**
Tries to find skill-balanced pairs first, but if the queue gets big, it just matches the first two players for a faster gameplay.


## Real-World Applications

**How this relates to actual game matchmaking:**
This system works a lot like what real online games do behind the scenes; for example Apex Legrnds which also separate players into different queues based on mode or skill.
Just like in my code, they try to find a balance between fairness and speed, Ranked modes focus on even skill levels, while QuickPlay or Casual modes are more about fast access.
It made me realize that the same queue structures and pairing logic I used here are what real matchmaking servers use for bigger systems.

**What you learned about game industry patterns:**
I learned that queues are part of how big systems manage hundreds or thousands of players at once.
I also understood how game designers have to constantly make trade-offs between waiting time and match quality.
Working on this helped me see how algorithms and data structures play a part in shaping the player's experience in real games.

## Stretch Features

None implemented.
(If I had more the opportunity, I would’ve tried to add a feature to avoid recent opponents).

## Time Spent

**Total time:** 12.5 hrs

**Breakdown:**

- Understanding the assignment and queue concepts: 2 hours
- Implementing the 6 core methods: 6 hours
- Testing different game modes and scenarios: 1hr
- Debugging and fixing issues: 3 hrs
- Writing these notes: 0.5hr

**Most time-consuming part:** 
What was most time consuming was implementing the skill-based matching logic for Ranked and QuickPlay. 
Debugging also consumed a lot of time, with my computer issues, it took even more time than it would have taken.

## Key Learning Outcomes

**Queue concepts learned:**
How to manage multiple queues that follow different matching rules while keeping everything synchronized.

**Algorithm design insights:**
I learned how to write logical filters for skill-based pairing and to think about fairness compared to speed in matchmaking algorithms.

**Software engineering practices:**
I got better at writing clean and commented code; 
I got better at handling errors and debugging them. 