# Project Design & Rationale

**Instructions:** Replace prompts with your content. Be specific and concise. If something doesn't apply, write "N/A" and explain briefly.

---

## Data Model & Entities

**Core entities:**  
_List your main entities with key fields, identifiers, and relationships (1–2 lines each).

**Your Answer:**
The application manages a single core entity representing a media item stored in the library.

**Entity A:**

- Name: MediaItem  
- Key fields:Id, Title, Type, Year 
- Identifiers:Id (string) 
- Relationships:Stored and accessed through multiple data structures (Dictionary, List, HashSet) for different access patterns.

**Entity B (if applicable):**

- Name: N/A
- Key fields: N/A
- Identifiers: N/A
- Relationships: N/A

**Identifiers (keys) and why they're chosen:**  
_Explain your choice of keys (e.g., string Id, composite key, case-insensitive, etc.)._

**Your Answer:**
Each media item uses a string-based `Id` as its unique identifier. A string ID is easy for users to input, flexible, and works well as a key in a dictionary. IDs are required to be unique to ensure reliable lookup, update, and deletion operations.

---

## Data Structures — Choices & Justification

_List only the meaningful data structures you chose. For each, state the purpose, the role it plays in your app, why it fits, and alternatives considered._

### Structure #1

**Chosen Data Structure:**  
_Dictionary<string, MediaItem>_

**Your Answer:**
Dictionary<string, MediaItem>
A dictionary mapping media IDs to their corresponding MediaItem objects.

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
Powers fast search, update, and delete operations by media ID.
This structure is used whenever the user searches for, updates, or deletes a media item by its ID.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
The dictionary provides average case O(1) time complexity for lookups, inserts, and removals. 
Since IDs are unique and frequently used for direct access, a dictionary is the most efficient and clear choice. 
The expected data size is small to moderate, making memory usage reasonable.

**Alternatives considered:**  
_List alternatives (e.g., List<T>, SortedDictionary, custom tree) and why you didn't choose them._

**Your Answer:**
A List<MediaItem> could be used, but searching by ID would require O(n) time. A SortedDictionary was not chosen because ordering by ID was not required.

---

### Structure #2

**Chosen Data Structure:**  
_List<MediaItem>_

**Your Answer:**
A list containing all media items in insertion order.

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
Used to display all media items to the user.
This structure supports the "View all media" feature by allowing simple iteration through all items.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
Lists are simple, readable, and efficient for sequential access. 
Iterating over the list runs in O(n), which is acceptable for displaying all items.
It also preserves insertion order, which is useful for a more predictable output.


**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**
A linked list was unnecessary since random access and frequent insertions in the middle were not required. A tree structure would add an unnecessary complexity.
---

### Structure #3

**Chosen Data Structure:**  
_HashSet<string>_

**Your Answer:**
A hash set storing media titles.

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
Ensures that media titles remain unique.
This structure is checked whenever a new media item is added or an existing title is updated.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
HashSet provides average case O(1) time for add and contains checks.
It efficiently prevents duplicate titles without requiring a full scan of the list or dictionary.

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**
Checking uniqueness using the List or Dictionary values would require O(n) time. 
A dictionary keyed by title was unnecessary since titles are not used for lookup.

---

### Additional Structures (if applicable)

_Add more sections if you used additional structures like Queue for workflows, Stack for undo, HashSet for uniqueness, Graph for relationships, BST/SortedDictionary for ordered views, etc._

**Your Answer:**
N/A. No additional data structures were needed for the scope of this project.
---

## Comparers & String Handling

**Comparer choices:**  
_Explain what comparers you used and why (e.g., StringComparer.OrdinalIgnoreCase for keys)._

**Your Answer:**
StringComparer.OrdinalIgnoreCase is used for title storage.

**For keys:**
Media titles stored in the HashSet use case-insensitive comparison to prevent duplicates that differ only by casing.

**For display sorting (if different):**
N/A. No sorting is applied during display.

**Normalization rules:**  
_Describe how you normalize strings (trim whitespace, collapse duplicates, canonicalize casing)._

**Your Answer:**
User input strings are trimmed to remove leading and trailing whitespace. Empty strings are rejected where invalid.

**Bad key examples avoided:**  
_List examples of bad key choices and why you avoided them (e.g., non-unique names, culture-varying text, trailing spaces, substrings that can change)._

- Using titles as primary keys (titles can change).  
- Using case sensitive strings for uniqueness (would allow duplicates like "Inception" vs "inception").  
- Allowing empty or whitespace only identifiers.
---

## Performance Considerations

**Expected data scale:**  
_Describe the expected size of your data (e.g., 100 items, 10,000 items)._

**Your Answer:**
The application is expected to handle small to moderate datasets (approximately 100 to 5,000 media items).

**Performance bottlenecks identified:**  
_List any potential performance issues and how you addressed them._

**Your Answer:**
The main potential performance issue is listing all media items, which requires iterating through the entire list and runs in O(n) time. This operation could become slower as the number of items increases. This is addressed by limiting this operation to display only functionality and by using a Dictionary for all frequent lookup, update, and delete operations, which avoids repeated linear scans for common actions. 

**Big-O analysis of core operations:**  
_Provide time complexity for your main operations (Add, Search, List, Update, Delete)._

**Your Answer:**

- Add:    O(1) average  
- Search: O(1) average  
- List:   O(n)  
- Update: O(1) average  
- Delete: O(1) average  

---

## Design Tradeoffs & Decisions

**Key design decisions:**  
_Explain major design choices and why you made them._

**Your Answer:**
The application uses simple, well-known .NET collections instead of custom data structures to prioritize clarity and correctness.
Each structure has a single, well-defined responsibility.

**Tradeoffs made:**  
_Describe any tradeoffs between simplicity vs performance, memory vs speed, etc._

**Your Answer:**
The design favors simplicity and readability over advanced features such as persistence or sorting. 
Maintaining multiple data structures slightly increases memory usage but significantly improves performance and clarity.

**What you would do differently with more time:**  
_Reflect on what you might change or improve._

**Your Answer:**
With more time, I would add file persistence so the media library can be saved between sessions. I would also add optional sorting and filtering features for larger datasets, and improve the UI.

