# Assignment 10: Flight Route Network Navigator - Implementation Notes

**Name:** Zoe Mukaba

## Graph Data Structure Understanding

**How adjacency list representation works for flight networks:**
[Explain your understanding of how Dictionary<string, List<Flight>> provides O(1) airport lookups, efficient sparse graph storage, and why this is better than adjacency matrix for flight networks with 16 airports and 52 flights]

Answer:

Using a Dictionary<string, List<Flight>> basically means each airport code is a key, and the value is a list of flights going out of that airport. So when the program wants to find all outgoing flights from, let’s say, SEA, it can get them instantly in O(1) time by doing routes["SEA"].
This works really well for a flight network because it’s a sparse graph; meaning most airports don’t connect to every other airport. With only 16 airports and 52 flights, an adjacency list is perfect and doesn’t waste memory.
An adjacency matrix would be 16×16 (256 entries), mostly empty, and slower to scan.
So the adjacency list gives fast lookups, less memory use, and fits real airline networks better.



**Difference between BFS and Dijkstra's algorithms:**
[Explain when to use BFS (shortest path by hops) vs Dijkstra's (shortest path by cost), and how each algorithm guarantees finding optimal paths]

Answer:
BFS is used when all edges have equal weight. In this case, BFS is perfect when we only care about number of stops. It naturally finds the route with the smallest number of hops because it explores level by level.
Dijkstra’s algorithm is used when edges have different costs. Here, flights have different prices, so Dijkstra helps us find the cheapest route.


## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it implementing BFS traversal, Dijkstra's priority queue logic, path reconstruction from parent maps, or understanding graph algorithms?]

Answer:
The hardest part for me was understanding Dijkstra’s algorithm in the context of flights. Precisely keeping track of distances, the priority queue, and updating costs correctly.

**How you solved it:**
[Explain your solution approach and what helped you figure it out - research, drawing graphs on paper, debugging with breakpoints, testing with simple examples, etc.]

What helped was rewriting the steps of the algorithms in my own words. I also tested small examples to make sure I understood how the parent dictionary was being updated. Running the program often and checking intermediate values made everything easier to understand.
Answer:

**Most confusing concept:**
[What was hardest to understand about graph traversal, queue/priority queue usage, parent map path reconstruction, or algorithm termination conditions?]

Answer:
The combination of the priority queue + relaxation step + parent tracking in Dijkstra. It’s a lot happeningm which made it a bit confusing.  

## Algorithm Implementation Details

**BFS Implementation (FindRoute and FindShortestRoute):**
[Describe how you implemented the queue-based traversal, visited tracking with HashSet, parent map for path reconstruction, and why BFS guarantees shortest path in unweighted graphs]

Answer:
My BFS uses a queue to explore airports level by level. 
A HashSet tracks which airports were already visited so we don’t loop forever. 
I used a parent dictionary to remember how each airport was reached.
After reaching the destination, I reconstructed the path by walking backwards through the parent map. Because BFS explores in layers, the first time we reach the destination is guaranteed to be the shortest path by stops.

**Dijkstra's Implementation (FindCheapestRoute):**
[Explain how you used PriorityQueue<string, decimal>, implemented the relaxation step, tracked distances, and reconstructed the cheapest path]

Answer:
I created a PriorityQueue<string, decimal> so the algorithm always expands the airport with the lowest current cost. I stored distances for every airport (initially decimal.MaxValue), then updated them whenever I found a cheaper route (relaxation step).
When the destination is finally dequeued, I used the same parent map logic to rebuild the path.

**Path Reconstruction Logic:**
[Describe your approach to building the final route from the parent map, handling the reverse traversal, and ensuring the path goes from origin to destination]

Answer: 
I started from the destination and followed parent pointers backward until the origin was reached. Each airport code was stored in a list, then the list was reversed at the end so it appears in the correct order.
The main thing was making sure to stop when the origin was reached; otherwise, the path is invalid.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your clean BFS implementation, efficient Dijkstra's algorithm, well-structured network analysis methods, or thorough error handling]

Answer:
I’m proud that all algorithms worked well after debugging and doing the test run. My code ended up pretty clean, especially the BFS and Dijkstra implementations. 
Also, I handled bad input well and kept everything consistent.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps optimizing priority queue usage, adding more comprehensive error handling, implementing bidirectional search, or adding visualization features]

Answer:
I would improve the "Compare All Route Options" feature to display both BFS and Dijkstra results side-by-side.

## Real-World Applications

**How this relates to actual routing systems:**
[Describe how your implementation connects to real-world systems like Google Flights, Google Maps navigation, social network friend suggestions, or internet packet routing]

Answer:
This assignment is a simplified version of how real airlines or apps like Google Flights handle routes. 
They use graphs to represent airports and flights, then use algorithms like BFS, Dijkstra, or even more advanced ones to choose the best route based on time, money, or number of stops. 
It’s also similar to network routing on the internet or account suggestions on social media.

**What you learned about graph algorithms:**
[What insights did you gain about graph traversal techniques, the power of BFS and Dijkstra's for different optimization goals, and how adjacency lists make sparse graphs efficient?]

Answer:
I have a better understanding of graph algorithms and how different algorithms solve different types of shortest path questions. 
BFS is fast and simple for equal-weight edges, while Dijkstra handles weighted edges well. 
I also saw how adjacency lists keep things efficient and clear.

## Testing and Verification

**Test cases you created:**
[List the specific test scenarios you used - which airport pairs did you test? Did you verify shortest vs cheapest routes differ? How did you test edge cases like disconnected airports or origin=destination?]

Answer:
I tested:

SEA → SFO for direct flights.

SEA → MIA using BFS and Dijkstra to compare the difference.

Checking SEA destinations.

Hub airports (top 5).

Network statistics to make sure the counts made sense.

Routes with cost constraints and stop limits.

Edge cases like entering lowercase codes, or airports that didn’t exist in the CSV.

**Interesting findings from testing:**
[Describe any surprising results - routes that took unexpected paths, cost vs stops tradeoffs you discovered, or hub airports you identified]

Answer:
The cheapest route and the shortest route were not the same at all, which felt realistic.
Some airports had surprisingly high outgoing flight counts (like SFO and LAX), showing how hub airports form naturally in the network.

## Optional Challenge

[If you implemented the optional FindRoutesByCriteria method with DFS and constraints, describe your approach here. If not, write "Not implemented - focused on core requirements"]

Answer:
The optional FindRoutesByCriteria method was implemented using DFS with backtracking.
My Approach:

The main method first validates the inputs, sets up the starting path with the origin airport, and prepares a visited set to avoid cycles.

A recursive DFS helper is then used to explore all possible routes.

When the helper reaches the destination, it adds a copy of the current path to the results.

The search stops early if the number of stops reaches the maximum allowed.

For each outgoing flight, the helper calculates the new total cost. If the cost exceeds the limit or the next airport has already been visited, that branch is skipped.

Otherwise, the next airport is added to the path, marked as visited, explored recursively, and then removed afterward during backtracking.

## Time Spent

**Total time:** [12 hours]

**Breakdown:**

- Understanding graph concepts and assignment requirements: [1.5 hours]
- Implementing basic search operations (TODO #1-3): [2 hours]
- Implementing BFS pathfinding (TODO #4-5): [1 hour]
- Implementing Dijkstra's algorithm (TODO #6): [3 hours]
- Implementing network analysis (TODO #8-10): [2 hours]
- Testing with flights.csv and edge cases: [0.5 hour]
- Debugging graph traversal algorithms: [1.5 hours]
- Writing these notes: [30 min]

**Most time-consuming part:** [Which aspect took the longest and why - understanding Dijkstra's algorithm, debugging path reconstruction, implementing priority queue logic, etc.]

Answer:
Understanding and implementing Dijkstra’s algorithm correctly while keeping track of parents, distances, and visited nodes.
## Key Takeaways

**Most important lesson learned:**
[What's the single most valuable insight you gained from this assignment about graph algorithms, pathfinding, or algorithm design?]

Answer:
I learned that Graph algorithms look intimidating at first, but breaking them down step by step makes them understandable and less confusing. 

**How this changed your understanding of data structures:**
[How did working with graphs expand your perspective on data organization compared to arrays, linked lists, trees, etc.?]

Answer:
Before this assignment, lists and trees felt more straightforward, but graphs showed how real-world problems are better modeled. They represent real-world problems better, and working with adjacency lists showed how choosing the right structure really affects speed and understanding.