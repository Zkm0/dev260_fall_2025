# Media Library Manager

> A simple C# console application for managing a personal media collection by adding, viewing, searching, updating, and deleting media items.

---

## What I Built (Overview)

**Problem this solves:**  
_Explain the real-world task your app supports and why it's useful (2–4 sentences)._

**Your Answer:**
This application helps users manage a small personal media library, such as movies, books, or music, without relying on spreadsheets or manual tracking. It provides a structured way to store media information and quickly find or update items by ID. 
The app is useful for practicing applied data structure selection while still representing a realistic use case.

**Core features:**  
_List the main features your application provides (Add, Search, List, Update, Delete, etc.)_

**Your Answer:**

- Add a new media item with validation
- View all media items
- Search for a media item by ID
- Update an existing media item
- Delete a media item
- Exit the application safely

## How to Run

**Requirements:**  
_List required .NET version, OS requirements, and any dependencies._

**Your Answer:**
- .NET SDK 6.0 or later  
- Windows, macOS, or Linux  
- No external libraries or dependencies required  

**Build:**
```bash
git clone https://github.com/Zkm0/dev260_fall_2025.git
cd final_project/MediaLibraryManager
dotnet build
```

**Run:**  
dotnet run

**Your Answer:**

```bash
dotnet run
```

**Sample data (if applicable):**  
_Describe where sample data lives and how to load it (e.g., JSON file path, CSV import)._

**Your Answer:**
N/A. Media items are entered manually through the console menu.
---

## Using the App (Quick Start)

**Typical workflow:**  
_Describe the typical user workflow in 2–4 steps._

**Your Answer:**

1. Start the application using dotnet run
2. Add one or more media items using the menu
3. View or search media items by ID
4. Update or delete items as needed

**Input tips:**  
_Explain case sensitivity, required fields, and how common errors are handled gracefully._

**Your Answer:**
- Media IDs must be unique and cannot be empty

- Media titles must be unique (case insensitive)

- Leading and trailing spaces are trimmed automatically

- Invalid input is handled gracefully without crashing the app

---

## Data Structures (Brief Summary)

> Full rationale goes in **DESIGN.md**. Here, list only what you used and the feature it powers.

**Data structures used:**  
_List each data structure and briefly explain what feature it powers._

**Your Answer:**

- `Dictionary<string, MediaItem>` → Fast lookup, update, and deletion by media ID
- `List<MediaItem>` → Displaying all media items in insertion order
- `HashSet<string>` → Ensuring media titles remain unique

- _(Add others: Queue, Stack, SortedDictionary, custom BST/Graph, etc.)_

---

## Manual Testing Summary

> No unit tests required. Show how you verified correctness with 3–5 test scenarios.
Manual testing was performed through the console.

**Test scenarios:**  
_Describe each test scenario with steps and expected results._

**Your Answer:**

**Scenario 1: Add valid media item**

- Steps: Add a media item with a unique ID, title, type, and valid year
- Expected result: Expected result: Item is added successfully
- Actual result: Item added and visible in the list

**Scenario 2: Prevent duplicate ID or title**

- Steps: Attempt to add a second item with an existing ID or title
- Expected result: Operation is rejected with an error message
- Actual result: Duplicate input was correctly blocked

**Scenario 3: Search for media by ID**

- Steps: Search using an existing and a non-existing ID
- Expected result: Existing item is displayed; missing item shows “not found”
- Actual result: Behavior matched expectations

**Scenario 4: Update media item (optional)**

- Steps: Update title, type, or year while keeping other fields unchanged
- Expected result: Only selected fields are updated
- Actual result: Update worked as expected

**Scenario 5: Delete media item (optional)**

- Steps: Delete an item and then attempt to search for it
- Expected result: Item is removed and no longer accessible
- Actual result: Item was deleted successfully

---

## Known Limitations

**Limitations and edge cases:**  
_Describe any edge cases not handled, performance caveats, or known issues._

**Your Answer:**

- Data is not persisted between sessions
- No sorting or filtering options beyond basic listing

## Comparers & String Handling

**Keys comparer:**  
_Describe what string comparer you used (e.g., StringComparer.OrdinalIgnoreCase) and why._

**Your Answer:**
StringComparer.OrdinalIgnoreCase is used for media titles to prevent duplicates that differ only by letter casing.

**Normalization:**  
_Explain how you normalize strings (trim whitespace, consistent casing, duplicate checks)._

**Your Answer:**
User input strings are trimmed to remove extra whitespace. Empty values are rejected where invalid.

---

## Credits & AI Disclosure

**Resources:**  
_List any articles, documentation, or code snippets you referenced or adapted._

**Your Answer:**
- Microsoft .NET documentation
- C# language reference

- **AI usage (if any):**  
   _Describe what you asked AI tools, what code they influenced, and how you verified correctness._

  **Your Answer:**

AI tools, precisely Copilot, were used to assist with planning the application structure, clarifying data structure choices, and reviewing logic for correctness.

  ***

## Challenges and Solutions

**Biggest challenge faced:**  
_Describe the most difficult part of the project - was it choosing the right data structures, implementing search functionality, handling edge cases, designing the user interface, or understanding a specific algorithm?_

**Your Answer:**
The biggest challenge was deciding how to keep multiple data structures in sync while supporting fast lookups and clean display logic.

**How you solved it:**  
_Explain your solution approach and what helped you figure it out - research, consulting documentation, debugging with breakpoints, testing with simple examples, refactoring your design, etc._

**Your Answer:**
This was solved by clearly defining the responsibility of each data structure and updating all relevant structures during add, update, and delete operations. Testing each operation step-by-step helped ensure consistency.

**Most confusing concept:**  
_What was hardest to understand about data structures, algorithm complexity, key comparers, normalization, or organizing your code architecture?_

**Your Answer:**
Understanding when to favor simplicity over additional optimization was initially confusing, especially when thinking of performance tradeoffs.

## Code Quality

**What you're most proud of in your implementation:**  
_Highlight the best aspect of your code - maybe your data structure choices, clean architecture, efficient algorithms, intuitive user interface, thorough error handling, or elegant solution to a complex problem._

**Your Answer:**
I am most proud of the clean structure and clear data structure usage. Each structure has a specific purpose, and the code remains readable and easy to follow without unnecessary complexity.

**What you would improve if you had more time:**  
_Identify areas for potential improvement - perhaps adding more features, optimizing performance, improving error handling, adding data persistence, refactoring for better maintainability, or enhancing the user experience._

**Your Answer:**
With more time, I would add file persistence so data can be saved between sessions and introduce optional sorting features to improve usability. 
I would also reorganize the menu logic into separate classes to make the application easier to extend.

## Real-World Applications

**How this relates to real-world systems:**  
_Describe how your implementation connects to actual software systems - e.g., inventory management, customer databases, e-commerce platforms, social networks, task managers, or other applications in the industry._

**Your Answer:**
This application reflects patterns used in inventory management systems, content libraries, and administrative tools where fast lookup, validation, and data consistency are required.

**What you learned about data structures and algorithms:**  
_What insights did you gain about choosing appropriate data structures, performance tradeoffs, Big-O complexity in practice, the importance of good key design, or how data structures enable specific features?_

**Your Answer:**
This project reinforced the importance of choosing data structures based on access patterns and practicability, rather than habit. Using the right structure early can eliminate performance issues and simplify code.

## Submission Checklist

- [x] Public GitHub repository link submitted
- [x] README.md completed (this file)
- [x] DESIGN.md completed
- [x] Source code included and builds successfully
- [x] (Optional) Slide deck or 5–10 minute demo video link (unlisted)

**Demo Video Link (optional):* https://drive.google.com/file/d/1MeodzlHM3KUlzBOREGYCQgXsgdm3D2Xx/view?usp=drivesdk *

