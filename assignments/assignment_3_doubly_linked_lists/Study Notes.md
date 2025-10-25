
How I ran it
1.	dotnet run at the assignment root.
2.	Used:
1) Core List Operations Demo (Part A)
2) Music Playlist Manager (Part B)

Part A: Core Doubly Linked List

Step 3: AddFirst, AddLast, Insert

Input sequence
3
AddFirst 10
AddLast 15
Insert index=1 value=20
Observed
Forward: [ 10 ]
Forward: [ 10 15 ]

Inserted 20 at index 1
Forward: [ 10 20 15 ]
Verdict:  Head/tail set correctly; middle insert works.

Step 4: Traversal (Forward, Backward, ToArray, foreach)

Input
4
Observed
Forward: [ 10 20 15 ]
Backward: [ 15 20 10 ]
Array result: [10, 20, 15]
Foreach result: 10 20 15
Verdict:  Forward/backward mirror; array & enumeration consistent.

Step 5: Search (negative, positive)

Input 1 (not present)
5
value = 5
Observed
Contains(5):  Not found
Find(5):  Not found
IndexOf(5): Not found (returned -1)
Input 2 (present)
5
value = 20
Observed
Contains(20):  Found
Find(20):  Found node with value 20
IndexOf(20):  Found at index 1
Verdict:  Both cases correct.

Step 6: Remove (RemoveFirst, RemoveLast, Remove, RemoveAt)

Inputs & Observed
6 → 1 (RemoveFirst)
•	Removed first element: 10
Forward: [ 20 15 ]

6 → 2 (RemoveLast)
•	 Removed last element: 15
Forward: [ 20 ]

6 → 3 (Remove by value 20)
•	 Removed 20
Forward: [ ]
(After re-adding items)
6 → 4 (RemoveAt index 1 when list was [ 10 20 15 ])
- Removed 20 from index 1
Forward: [ 10 15 ]
Verdict:  All four remove paths update links & ends properly; empty-list guard shows warning when appropriate.

Step 7: Advanced (Reverse, Clear)

Reverse
Before reverse:
Forward: [ 10 15 ]
After reverse:
Forward: [ 15 10 ]
- Reverse operation successful!
Clear
Before clear - Count: 2
After clear:
Count: 0
IsEmpty: True
First: null
Last: null
Verdict:  In-place reverse swaps head/tail; clear resets fully.

Part B: Music Playlist Manager

10a: Adding (AddSong, AddSongAt)

AddSongAt error handling (out-of-range)
Enter position (0 to 4): -1
   Error: ... (Parameter 'position')

Enter position (0 to 4): 999
  Error: ... (Parameter 'position')
Add custom song (duration validation)
Duration (mm:ss): 3:09
- Invalid duration format. Use mm:ss ...
Duration (mm:ss): 03:09
 Added: Midnight Sun by Zara Larsson (03:09)
AddSong (append)
-Successfully added: Test Song by Test Artist (03:00)
Total songs now: 3
AddSongAt (valid insert)
Enter position (0 to 3): 1
 Successfully added at position 1: Position Song by Test Artist (02:00)
Verdict:  Both add APIs work; invalid positions and duration format correctly rejected.

10b: Removing (RemoveSong, RemoveSongAt)

RemoveSong (by object / current selection)
Current song: Imagine by John Lennon (03:03)
Testing RemoveSong:
  Successfully removed: Imagine by John Lennon (03:03)
RemoveSongAt (by index)
Enter position (0 to 2): 1
 Successfully removed song at position 1
Notes: In the demo, RemoveSong uses GetCurrentSong() as the target, so it will remove whatever is currently selected. After removal, the current moves; so running the same remove twice in a row won’t error; it just acts on the new current song.

Verdict:  Both removal paths (by object and by index) behave correctly.

10c: Navigation (Next, Previous, JumpToSong)

Next / boundary at end
Next:
•	Moved to next song ... Position: 4 of 4
Next again:
•	 Already at end of playlist
Previous / boundary at beginning
Previous:
•	 Moved to previous song ... Position: 1 of 4
Previous again:
•	 Already at beginning of playlist

JumpToSong
Enter position (0 to 3): 2
•	 Jumped to position 2
Current song: Imagine by John Lennon (03:03)
Verdict:  Navigation, including both edges, works as intended.

11: Display Methods (DisplayPlaylist, DisplayCurrentSong, GetCurrentSong)

DisplayPlaylist (marks current and sums duration)
=== Playlist: Student Demo Playlist ===
Total songs: 4

  1. Bohemian Rhapsody by Queen (05:55)
  2. Position Song by Test Artist (02:00)
► 3. Midnight Sun by Zara Larsson (03:09)
  4. Test Song by Test Artist (03:00)

Total duration: 00:14:04
DisplayCurrentSong
Now Playing (2 of 3):
Midnight Sun - Zara Larsson [, 0] (03:09) []
(Minor cosmetic) The [, 0] [] bits look like leftover placeholders in Song.ToString() or in DisplayCurrentSong() formatting. Functionally fine, just a UI string tidy-up if desired.

GetCurrentSong
Current song object: Midnight Sun by Zara Larsson (03:09)
Verdict:  Display works and highlights current; total duration updates correctly.

What’s been tested

Part A
•	Step 3: AddFirst, AddLast, Insert 
•	Step 4: DisplayForward, DisplayBackward, ToArray, IEnumerable<T> 
•	Step 5: Contains, Find, IndexOf (hit both found & not found) 
•	Step 6: RemoveFirst, RemoveLast, Remove(value), RemoveAt(index) 
•	Step 7: Reverse, Clear 

Part B
•	10a: AddSong, AddSongAt (valid & invalid positions; duration validation) 
•	10b: RemoveSong (current selection), RemoveSongAt 
•	10c: Next, Previous (both edges), JumpToSong 
•	11: DisplayPlaylist (current-mark & duration total), DisplayCurrentSong, GetCurrentSong 
•	Utilities: ResetPlaylist 

Other:
•	End/beginning navigation guards shown.
•	Position and total duration update after add/remove.

Challenges Faced
•	Handling pointer wiring and head/tail updates during insert and remove.
•	Validating song positions and duration format in Part B.
•	Managing edge cases like empty list, single-node list, and start/end navigation.

Performance Reflection
•	Doubly linked lists give O(1) inserts/removes when you already have a node reference.
•	Traversal and search are O(n), similar to arrays/lists.
•	Perfect for playlist or undo/redo features where moving forward/backward matters.
•	Not ideal for random index lookups compared to List<T>.
