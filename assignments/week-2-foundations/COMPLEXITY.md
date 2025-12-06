Complexity Predictions



| Structure        | Operation                    | Big-O (Avg) | One-sentence rationale |

|------------------|------------------------------|-------------|------------------------|

| Array            | Access by index              | O(1)        | You can jump to any element right away using its position.|

| Array            | Search (unsorted)            | O(N)        | You might have to look through every item to find it. |

| List<T>          | Add at end                   | O(1) amort. | Usually quick, but sometimes it resizes the array (need of extra space) behind the scenes. |

| List<T>          | Insert at index              | O(N)        | Items have to move to make more space. |

| Stack<T>         | Push / Pop / Peek            | O(1)        | Works only at one end, at the the top, so it makes each action quick and simple |

| Queue<T>         | Enqueue / Dequeue / Peek     | O(1)        | Adds/removes at the ends, so it’s constant time. |

| Dictionary<K,V>  | Add / Lookup / Remove        | O(1) avg    | Uses a hash table so it can find/update things instantly. |

| HashSet<T>       | Add / Contains / Remove      | O(1) avg    | Uses a hash table like Dictionary, making lookups quick. |







