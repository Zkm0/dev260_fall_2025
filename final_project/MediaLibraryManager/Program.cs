using System;
using System.Collections.Generic;

namespace MediaLibraryManager
{
    // represent one media item in the library
    class MediaItem
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Type { get; set; } = "";
        public int Year { get; set; }
    }

    class Program
    {
        // core data structures

        // stores media items by unique ID for a fast lookup
        static Dictionary<string, MediaItem> mediaById =
            new Dictionary<string, MediaItem>();

        // maintains insertion order for display
        static List<MediaItem> mediaList =
            new List<MediaItem>();

        // ensures media titles remain unique
        static HashSet<string> titles =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        static void Main(string[] args)
        {
            //menu mode
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine(" Media Library Manager ");
                Console.WriteLine("1. Add media item");
                Console.WriteLine("2. View all media");
                Console.WriteLine("3. Search media by ID");
                Console.WriteLine("4. Update media item");
                Console.WriteLine("5. Delete media item");
                Console.WriteLine("6. Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddMediaItem();
                        break;

                    case "2":
                        ViewAllMedia();
                        break;

                    case "3":
                        SearchMediaById();
                        break;


                    case "4":
                        UpdateMediaItem();
                        break;

                    case "5":
                        DeleteMediaItem();
                        break;


                    case "6":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

            }
        }



        static void AddMediaItem()
        {
            //add logic

            Console.Write("Enter ID: ");
            string id = Console.ReadLine()?.Trim() ?? "";

            if (id == "" || mediaById.ContainsKey(id))
            {
                Console.WriteLine("Invalid or duplicate ID.");
                return;
            }

            Console.Write("Enter title: ");
            string title = Console.ReadLine()?.Trim() ?? "";

            if (title == "" || titles.Contains(title))
            {
                Console.WriteLine("Invalid or duplicate title.");
                return;
            }

            Console.Write("Enter type (Movie, Book, Music, etc.): ");
            string type = Console.ReadLine()?.Trim() ?? "";

            if (type == "")
            {
                Console.WriteLine("Type cannot be empty.");
                return;
            }

            Console.Write("Enter release year: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year.");
                return;
            }

            MediaItem item = new MediaItem
            {
                Id = id,
                Title = title,
                Type = type,
                Year = year
            };

            mediaById[id] = item;
            mediaList.Add(item);
            titles.Add(title);

            Console.WriteLine("Media item added successfully!");
        }



        static void ViewAllMedia()
        //view logic
        {
            if (mediaList.Count == 0)
            {
                Console.WriteLine("No media items found.");
                return;
            }

            Console.WriteLine("\n Media Library ");

            foreach (MediaItem item in mediaList)
            {
                Console.WriteLine(
                    $"ID: {item.Id} | Title: {item.Title} | Type: {item.Type} | Year: {item.Year}"
                );
            }
        }




        static void SearchMediaById()
        {
            Console.Write("Enter media ID to search: ");
            string id = Console.ReadLine()?.Trim() ?? "";

            if (id == "")
            {
                Console.WriteLine("ID cannot be empty.");
                return;
            }

            if (mediaById.TryGetValue(id, out MediaItem item))
            {
                Console.WriteLine(
                    $"ID: {item.Id} | Title: {item.Title} | Type: {item.Type} | Year: {item.Year}"
                );
            }
            else
            {
                Console.WriteLine("Media item not found.");
            }
        }



        static void DeleteMediaItem()
        {
            Console.Write("Enter media ID to delete: ");
            string id = Console.ReadLine()?.Trim() ?? "";

            if (id == "")
            {
                Console.WriteLine("ID cannot be empty.");
                return;
            }

            if (!mediaById.TryGetValue(id, out MediaItem item))
            {
                Console.WriteLine("Media item not found.");
                return;
            }

            mediaById.Remove(id);
            mediaList.Remove(item);
            titles.Remove(item.Title);

            Console.WriteLine("Media item deleted successfully!");
        }



        static void UpdateMediaItem()
        {
            Console.Write("Enter media ID to update: ");
            string id = Console.ReadLine()?.Trim() ?? "";

            if (!mediaById.TryGetValue(id, out MediaItem item))
            {
                Console.WriteLine("Media item not found.");
                return;
            }

            Console.WriteLine($"Current title: {item.Title}");
            Console.Write("New title (press Enter to keep current): ");
            string newTitle = Console.ReadLine()?.Trim() ?? "";

            if (newTitle != "" && newTitle != item.Title)
            {
                if (titles.Contains(newTitle))
                {
                    Console.WriteLine("Another item already has this title.");
                    return;
                }

                titles.Remove(item.Title);
                item.Title = newTitle;
                titles.Add(item.Title);
            }

            Console.WriteLine($"Current type: {item.Type}");
            Console.Write("New type (press Enter to keep current): ");
            string newType = Console.ReadLine()?.Trim() ?? "";
            if (newType != "")
            {
                item.Type = newType;
            }

            Console.WriteLine($"Current year: {item.Year}");
            Console.Write("New year (press Enter to keep current): ");
            string yearInput = Console.ReadLine()?.Trim() ?? "";
            if (yearInput != "")
            {
                if (int.TryParse(yearInput, out int newYear))
                {
                    item.Year = newYear;
                }
                else
                {
                    Console.WriteLine("Invalid year. Update cancelled.");
                    return;
                }
            }

            Console.WriteLine("Media item updated successfully!");
        }

    }
}
