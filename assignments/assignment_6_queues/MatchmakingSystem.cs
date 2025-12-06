using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// TODO: Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Use switch statement on mode to select correct queue
            // Don't forget to call player.JoinQueue()!


            // select correct queue using switch
            Queue<Player> queue = mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => casualQueue
            };

            // prevent duplicate entries
            if (queue.Any(p => p.Username.Equals(player.Username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($" {player.Username} is already in the {mode} queue.");
                return;
            }

            // to track join time and add
            player.JoinQueue(mode);
            queue.Enqueue(player);
            Console.WriteLine($" {player.Username} joined {mode} (skill {player.SkillRating}).");
        }

        

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Different logic needed for each mode
            // Remember to check queue count first!

            // pick the correct queue for this mode
            Queue<Player> queue = mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => casualQueue
            };

            // need at least two players to form a 1v1
            if (queue.Count < 2)
            {
                Console.WriteLine(" Not enough players in this queue to create a match yet.");
                return null;
            }

            // CASUAL; fastest
            if (mode == GameMode.Casual)
            {
                // take the first two players in line
                var p1 = queue.Dequeue();
                var p2 = queue.Dequeue();

                // leaving the queue 
                p1.LeaveQueue();
                p2.LeaveQueue();

                // create the match
                var match = new Match(p1, p2, mode);
                Console.WriteLine($" CASUAL match: {p1.Username} (S{p1.SkillRating}) vs {p2.Username} (S{p2.SkillRating})");
                return match;
            }

            // to search inside the queue for Ranked/QuickPlay 
            // Convert queue to list so we can inspect 
            var list = queue.ToList();

            // remove picked players from queue while preserving others' FIFO
            Match BuildMatchAndRequeue(Player a, Player b)
            {
                // rebuild queue without the two matched players 
                queue.Clear();
                foreach (var p in list)
                {
                    if (!ReferenceEquals(p, a) && !ReferenceEquals(p, b))
                        queue.Enqueue(p);
                }

                a.LeaveQueue();
                b.LeaveQueue();

                var made = new Match(a, b, mode);
                Console.WriteLine($" {mode} match: {a.Username} (S{a.SkillRating}) vs {b.Username} (S{b.SkillRating})");
                return made;
            }

            // RANKED: must be within ±2 skill
            if (mode == GameMode.Ranked)
            {
                // try all pairs in queue order (i < j) and pick the first valid one
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (CanMatchInRanked(list[i], list[j]))  
                        {
                            return BuildMatchAndRequeue(list[i], list[j]);
                        }
                    }
                }

                Console.WriteLine(" No suitable ranked pair found (need ±2 skill).");
                return null;
            }

            // QUICKPLAY: prefer good skill match, else go fast if busy 
            if (mode == GameMode.QuickPlay)
            {
                // 1) first try for a skill match (same rule as Ranked)
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (CanMatchInRanked(list[i], list[j]))
                        {
                            return BuildMatchAndRequeue(list[i], list[j]);
                        }
                    }
                }

                // 2) if none found and the queue is getting long, prioritize speed
                if (list.Count > 4)
                {
                    return BuildMatchAndRequeue(list[0], list[1]); // first two in line
                }

                // 3) else, wait for a better pair or more players
                Console.WriteLine(" QuickPlay is waiting for a good pair (or more players).");
                return null;
            }

            // 
            return null;
        }

        
        /// <summary>
        /// TODO: Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            // TODO: Implement this method
            // Hint: Very straightforward - simulate, record, display

            if (match == null) return;

            //simulate the match outcome(calls Match.SimulateOutcome)
            match.SimulateOutcome();

           // record this match in the history list
            matchHistory.Add(match);

            //update total match count for system stats
            totalMatches++;

            //print result summary
            Console.WriteLine($" {match}");
        }

        /// <summary>
        /// TODO: Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            // TODO: Implement this method
            // Hint: Loop through each queue and display formatted information

            Console.WriteLine("\n Current Queue Status");
            Console.WriteLine("-------------------------");

            // print each queue one by one
            PrintQueue("Casual", casualQueue);
            PrintQueue("Ranked", rankedQueue);
            PrintQueue("QuickPlay", quickPlayQueue);
            Console.WriteLine();
        }

        // so we don't repeat the same printing logic 3 times.
       //Shows position, username, skill, and how long they've been waiting.
        private void PrintQueue(string label, Queue<Player> queue)
        {
            Console.WriteLine($"\n {label} Queue ({queue.Count} players):");

            if (queue.Count == 0)
            {
                Console.WriteLine(" (empty)");
                return;
            }

            int pos = 1;
            foreach (var p in queue)
            {
                //  returns a wait like "12s" or "1m 05s"
                string wait = p.GetQueueTime();
                Console.WriteLine($"  {pos,2}. {p.Username}  (Skill {p.SkillRating}) - {wait}");
                pos++;
            }
        }

   

        /// <summary>
        /// TODO: Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            // TODO: Implement this method
            // Hint: Combine player info with match history filtering
            Console.WriteLine("\n Player Profile");
            Console.WriteLine("------------------");

            //basic info (username, skill, W/L, etc)
            Console.WriteLine(player.ToDetailedString());

            //  queue status
            string queueLine;
            if (player.JoinedQueue == DateTime.MinValue)
            {
                queueLine = "Queue: not currently queued";
            }
            else
            {
                // wait duration
                var wait = DateTime.Now - player.JoinedQueue;
                string waitText = wait.TotalMinutes < 1
                    ? $"{wait.Seconds}s"
                    : $"{wait.Minutes}m {wait.Seconds}s";

                // quick estimate depending on queue type
                var eta = GetQueueEstimate(player.PreferredMode);
                queueLine = $"Queue: {player.PreferredMode} - waiting {waitText} - ETA: {eta}";
            }
            Console.WriteLine(queueLine);

            //  recent match history (last 3 matches)
            Console.WriteLine("\nRecent matches:");
            var recent = matchHistory
                .Where(m => ReferenceEquals(m.Player1, player) || ReferenceEquals(m.Player2, player))
                .OrderByDescending(m => m.MatchTime)
                .Take(3)
                .ToList();

            if (recent.Count == 0)
            {
                Console.WriteLine("  (no matches yet)");
            }
            else
            {
                foreach (var m in recent)
                {
                    // Use Match.GetSummary() if available, fallback if needed
                    string summary = m.GetSummary();
                    if (string.IsNullOrWhiteSpace(summary))
                    {
                        var opponent = ReferenceEquals(m.Player1, player) ? m.Player2 : m.Player1;
                        var result = m.Winner == null ? "unprocessed"
                                     : ReferenceEquals(m.Winner, player) ? "won" : "lost";
                        summary = $"{m.MatchTime:MM/dd HH:mm} | vs {opponent.Username} | {m.Mode} | {result}";
                    }
                    Console.WriteLine($"  • {summary}");
                }
            }

            Console.WriteLine(); 
        }
       

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Check queue counts and apply mode-specific logic

            // pick the correct queue
            Queue<Player> q = mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => casualQueue
            };

          
            if (q.Count >= 2) return "No wait";
            if (q.Count == 1) return "Short wait";

            // empty queue
            string baseAnswer = "Long wait";

            // ranked 
            if (mode == GameMode.Ranked)
            {
                var list = q.ToList(); 
                if (list.Count >= 2)
                {
                    int min = list.Min(p => p.SkillRating);
                    int max = list.Max(p => p.SkillRating);
                    int spread = max - min;

                    if (spread <= 2) return "No wait";
                    if (spread <= 4) return "Short wait";
                    return "Long wait";
                }
                return baseAnswer;
            }

            // quickplay
            if (mode == GameMode.QuickPlay)
            {
                if (q.Count >= 5) return "No wait";
                if (q.Count >= 2) return "Short wait";
                return "Long wait";
            }

            // casual
            return baseAnswer;
        }

        

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
    }
}