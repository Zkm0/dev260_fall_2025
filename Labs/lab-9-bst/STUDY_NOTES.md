# Assignment 9: BST File System Navigator - Implementation Notes

**Name:** Zoe Mukaba

## Binary Search Tree Pattern Understanding

**How BST operations work for file system navigation:**
[Explain your understanding of how O(log n) searches, automatic sorting through in-order traversal, and hierarchical file organization work together for efficient file management]

Answer:

What I understood from this assignment is that a BST keeps everything organized automatically.
Every time a file or directory gets added, the tree puts it either on the left or the right depending on the name.
This makes searching really fast because instead of checking everything one by one, the search goes down one path.
So operations like FindFile feel almost instant.
Also, in order traversal lists things in alphabetical order, which is basically how real file systems show folders and files.


## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it recursive tree algorithms, custom file/directory comparison logic, or complex BST deletion?]

Answer:
The hardest part for me was the delete operation.
Deleting in a BST isn’t just removing something. I had to think about whether the node has no children, one child, or two children.
And because we also have to consider file type (file vs directory), it made the logic tricky for me.

**How you solved it:**
[Explain your solution approach and what helped you figure it out - research, debugging, testing strategies, etc.]

Answer:
I broke the problem into smaller pieces, and I tested each situation (like deleting a file, deleting a directory) to see what the tree actually did.
Looking up “in order successor” helped me understand how to replace a deleted node that has two children.
After testing multiple times with dotnet run, the behavior finally made sense.

**Most confusing concept:**
[What was hardest to understand about BST operations, recursive thinking, or file system hierarchies?]

Answer:
The recursion part.
It took me a while to understand how each recursive call returns a node back up the chain so the parent can reconnect the tree properly.
It felt confusing that even when you delete something, you still have to return a TreeNode to keep the structure intact.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your recursive algorithms, custom comparison logic, or efficient tree traversal]

Answer:
I’m proud that my recursive logic is clean across create, search, and delete.
My comparison rules also worked well.
And after correcting the methods, both FindFile and DeleteItem behave exactly as expected.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better error handling, more efficient algorithms, or additional features]

Answer:
I’d probably add better messages so I can clearly see the structure changing during the test.
I’d also maybe optimize things for larger data sets.

## Real-World Applications

**How this relates to actual file systems:**
[Describe how your implementation connects to tools like Windows File Explorer, macOS Finder, database indexing, etc.]

Answer:
Windows Explorer, macOS Finder, Linux file systems, everything is basically stored in a hierarchical tree.
Even though they use more advanced versions, the idea is the same:
keeping data sorted so searches and navigation are fast.
This assignment helped me understand the behind the scenes of how files are organized.

**What you learned about tree algorithms:**
[What insights did you gain about recursive thinking, tree traversal, and hierarchical data organization?]

Answer:
I learned how helpful recursion is when dealing with data that naturally branches out.
Tree traversal made more sense to me, especially how choosing in order, pre order, or post order gives different kinds of results.
I also realized how important comparison logic is when organizing mixed data types like files and directories.

## Stretch Features

[If you implemented any extra credit features like file pattern matching or directory size analysis, describe them here. If not, write "None implemented"]

Answer:
None Implemented.

## Time Spent

**Total time:** [8.5 hrs]

**Breakdown:**

- Understanding BST concepts and assignment requirements: 2 hours
- Implementing the 8 core TODO methods: 3 hours
- Testing with different file scenarios: 1 hour
- Debugging recursive algorithms and BST operations: 3 hours
- Writing these notes: 30 min

**Most time-consuming part:** [Which aspect took the longest and why - recursive thinking, BST deletion, custom comparison logic, etc.]

Answer:
Definitely the deletion logic, just because of the recursive structure and the three different cases.