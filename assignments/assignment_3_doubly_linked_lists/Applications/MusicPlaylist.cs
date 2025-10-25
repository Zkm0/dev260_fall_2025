using System;
using System.Linq;
using Week4DoublyLinkedLists.Core;
 
namespace Week4DoublyLinkedLists.Applications
{
    /// <summary>
    /// Represents a song in the music playlist
    /// Contains all metadata about a musical track
    /// </summary>
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
       
        public Song(string title, string artist, TimeSpan duration, string album = "", int year = 0, string genre = "")
        {
            Title = title;
            Artist = artist;
            Duration = duration;
            Album = album;
            Year = year;
            Genre = genre;
        }
       
        public override string ToString()
        {
            return $"{Title} by {Artist} ({Duration:mm\\:ss})";
        }
       
        public string ToDetailedString()
        {
            return $"{Title} - {Artist} [{Album}, {Year}] ({Duration:mm\\:ss}) [{Genre}]";
        }
    }
   
    /// <summary>
    /// Music playlist manager using doubly linked list for efficient navigation
    /// Demonstrates practical application of doubly linked lists
    ///
    /// PART B IMPLEMENTATION GUIDE:
    /// Step 8: Song class (already provided above)
    /// Step 9: Playlist core structure (implement below)
    /// Step 10: Playlist management (AddSong, RemoveSong, Navigation)
    /// Step 11: Display and basic management
    /// </summary>
    public class MusicPlaylist
    {
        #region Step 9: Playlist Core Structure - TODO: Students implement these properties
       
        private DoublyLinkedList<Song> playlist;
        private Node<Song>? currentSong;     // Currently selected song node
       
        // playlist properties
        public string Name { get; set; }
        public int TotalSongs => playlist.Count;
        public bool HasSongs => playlist.Count > 0;
        public Song? CurrentSong => currentSong?.Data;
       
        /// <summary>
        /// Initialize a new music playlist
        /// </summary>
        /// <param name="name">Name of the playlist</param>
        public MusicPlaylist(string name = "My Playlist")
        {
            Name = name;
            playlist = new DoublyLinkedList<Song>();
            currentSong = null;
        }
       
        #endregion
       
        #region Step 10: Playlist Management - TODO: Students implement these step by step
       
        #region Step 10a: Adding Songs (5 points)
       
        /// <summary>
        /// Add a song to the end of the playlist
        /// Time Complexity: O(1) due to doubly linked list tail pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="song">Song to add</param>
        public void AddSong(Song song)
        {
            // simple guard
            if (song == null) throw new ArgumentNullException(nameof(song));

            // add at the end
            playlist.AddLast(song);

            // if it was empty before, make this the current song
            if (currentSong == null)
            {
                currentSong = playlist.First; // first node is the one just added
            }
        }
       
        /// <summary>
        /// Add a song at a specific position in the playlist
        /// Time Complexity: O(n) for position-based insertion
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="position">Position to insert at (0-based)</param>
        /// <param name="song">Song to add</param>
        public void AddSongAt(int position, Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            if (position < 0 || position > TotalSongs)
                throw new ArgumentOutOfRangeException(nameof(position));

            // insert using linked list API
            playlist.Insert(position, song);

            // if this was the very first addition, set current
            if (currentSong == null)
            {
                currentSong = playlist.First;
            }
        }
       
        #endregion
       
        #region Step 10b: Removing Songs (5 points)
       
        /// <summary>
        /// Remove a specific song from the playlist
        /// Time Complexity: O(n) due to search operation
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="song">Song to remove</param>
        /// <returns>True if song was found and removed</returns>
        public bool RemoveSong(Song song)
        {
            if (song == null) return false;

            // find the node first so we can update "current" safely
            var node = playlist.Find(song);
            if (node == null) return false;

            // if we're removing the current song, move current to a neighbor
            if (node == currentSong)
            {
                // prefer moving to the next song if available, else go to previous
                currentSong = node.Next ?? node.Previous;
            }

            // now remove the value from the list
            var removed = playlist.Remove(song);

            // if list became empty, clear current
            if (playlist.Count == 0) currentSong = null;

            return removed;
        }
       
        /// <summary>
        /// Remove song at a specific position
        /// Time Complexity: O(n) for position-based removal
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="position">Position to remove (0-based)</param>
        /// <returns>True if song was removed successfully</returns>
        public bool RemoveSongAt(int position)
        {
            if (position < 0 || position >= TotalSongs)
                return false;

            // walk to the node so we can check if it's the current one
            int i = 0;
            var node = playlist.First;
            while (node != null && i < position)
            {
                node = node.Next;
                i++;
            }
            if (node == null) return false;

            // if removing the current song, shift current pointer first
            if (node == currentSong)
            {
                currentSong = node.Next ?? node.Previous;
            }

            // remove by index
            playlist.RemoveAt(position);

            if (playlist.Count == 0) currentSong = null;
            return true;
        }
       
        #endregion
       
        #region Step 10c: Navigation (5 points)
       
        /// <summary>
        /// Move to the next song in the playlist
        /// Time Complexity: O(1) due to doubly linked list Next pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>True if moved to next song, false if at end</returns>
        public bool Next()
        {
            // move forward if we can
            if (currentSong != null && currentSong.Next != null)
            {
                currentSong = currentSong.Next;
                return true;
            }
            return false;
        }
       
        /// <summary>
        /// Move to the previous song in the playlist
        /// Time Complexity: O(1) due to doubly linked list Previous pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>True if moved to previous song, false if at beginning</returns>
        public bool Previous()
        {
            // move backward if we can
            if (currentSong != null && currentSong.Previous != null)
            {
                currentSong = currentSong.Previous;
                return true;
            }
            return false; 
        }
       
        /// <summary>
        /// Jump directly to a song at a specific position
        /// Time Complexity: O(n) for position-based access
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <param name="position">Position to jump to (0-based)</param>
        /// <returns>True if jump was successful</returns>
        public bool JumpToSong(int position)
        {
            if (position < 0 || position >= TotalSongs)
                return false;

            // walk from the head to that position
            int i = 0;
            var node = playlist.First;
            while (node != null && i < position)
            {
                node = node.Next;
                i++;
            }
            if (node == null) return false;

            currentSong = node; 
            return true;
        }
       
        #endregion
       
        #endregion
       
        #region Step 11: Display and Basic Management (10 points)
       
        /// <summary>
        /// Display the entire playlist with current song highlighted
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        public void DisplayPlaylist()
        {
            Console.WriteLine($"\n=== Playlist: {Name} ===");
            Console.WriteLine($"Total songs: {TotalSongs}\n");

            if (!HasSongs)
            {
                Console.WriteLine("(empty playlist)");
                return;
            }

            // walk nodes so we can mark the "current" one
            int index = 1; // numbering starts at 1
            var node = playlist.First;
            while (node != null)
            {
                // mark the current with ► 
                string marker = (node == currentSong) ? "► " : "  ";
                Console.WriteLine($"{marker}{index}. {node.Data}");
                node = node.Next;
                index++;
            }

            // to show total duration
            var total = GetTotalDuration();
            Console.WriteLine($"\nTotal duration: {total:hh\\:mm\\:ss}\n");
        }
       
        /// <summary>
        /// Display details of the currently selected song
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        public void DisplayCurrentSong()
        {
            if (currentSong == null)
            {
                Console.WriteLine("No song selected.");
                return;
            }

            // show detailed info and position like "Song 3 of 10"
            int pos = GetCurrentPosition();
            Console.WriteLine($"Now Playing ({pos} of {TotalSongs}):");
            Console.WriteLine(currentSong.Data.ToDetailedString());
        }
       
        /// <summary>
        /// Get the currently selected song
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>Currently selected song, or null if no song selected</returns>
        public Song? GetCurrentSong()
        {
            // return the data for the current node (or null)
            return currentSong?.Data;
        }
       
        #endregion
       
        #region Helper Methods for Students
       
        /// <summary>
        /// Get the position of the current song (1-based for display)
        /// Useful for showing "Song X of Y" information
        /// </summary>
        /// <returns>Position of current song, or 0 if no current song</returns>
        public int GetCurrentPosition()
        {
            if (currentSong == null) return 0;
           
            int position = 1;
            var current = playlist.First;
            while (current != null && current != currentSong)
            {
                position++;
                current = current.Next;
            }
            return current == currentSong ? position : 0;
        }
       
        /// <summary>
        /// Calculate total duration of all songs in the playlist
        /// Demonstrates traversal and aggregate operations
        /// </summary>
        /// <returns>Total duration as TimeSpan</returns>
        public TimeSpan GetTotalDuration()
        {
            TimeSpan total = TimeSpan.Zero;
            foreach (var song in playlist)
            {
                total = total.Add(song.Duration);
            }
            return total;
        }
       
        #endregion
    }
}
