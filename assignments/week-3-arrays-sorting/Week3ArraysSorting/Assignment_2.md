# Assignment 2: Arrays & Sorting

## Part A: Game Choice
I chose **Tic-Tac-Toe (3×3)** for this project.  
The board uses a 2D array (`char[,]`) and displays as an ASCII grid (`.` for empty).  
Players enter moves as `row col` like `2 3`. Invalid or taken cells are rejected. 
The program checks rows, columns, and diagonals for a win and declares a draw when the board is full.  
After each round, it asks if you want to play again (`y/n`) and resets the array if yes.  
The 2D array stores and updates all moves directly. 

## Part B: Book Catalog
I used a **recursive mergesort** (split + merge).  
- **Time:** O(n log n) average/worst. **Space:** O(n) extra for merging.  
Titles are normalized with `Trim()` and `ToUpperInvariant()` so lookups are case-insensitive.  
I built two 2D arrays, `int[26,26] start` and `end`, to map ranges of titles by their first two letters.  
Non-letters are mapped to ‘A’ (0) for simplicity.  
The lookup normalizes the query, finds the [start,end) range in O(1),  
then binary searches within that slice in O(log k). If a title isn’t found, the app shows other suggestions from that slice.

## How to Run 
1. the file `books.txt` needs to be in the same folder as the project file ( Week3ArraysSorting.csproj) so the program can find the book list when it runs.
2. Open the terminal in the folder, run: dotnet run
3. When the menu appears, we see:

Enter 1 to play Tic-Tac-Toe

Enter 2 to search book titles

Enter 0 to exit
