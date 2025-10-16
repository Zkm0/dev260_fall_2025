## Observations (Reality vs Theory)



#### Predictions vs Results



My measured times mostly matched what I predicted in the Big-O table.  

'List.Contains()' was clearly slower than 'HashSet.Contains()' and 'Dict.ContainsKey()' as N got larger.  

Both HashSet and Dictionary stayed almost constant at around 0.001 ms, which matches the expected O(1) behavior really well.



#### Any Surprises



At first, I was surprised that the List search didn’t look that slow for small N.  

It reminded me how Big-O isn’t about the exact speed, but it’s about how performance grows as the data also grows.  

For small sets, even an O(N) loop can still run very fast. The small changes in timing (like 0.03 → 0.02 ms) are just noise/background system activity.



#### Given a Large Dataset and many membership checks



If I had a really large dataset and needed to check membership often,  

I’d definitely go with a "HashSet" or "Dictionary".  

They were way more consistent and faster across all sizes.  

Lists are still fine for small or quick operations, but once the size increases, sets and dictionaries are the better option.





It was actually interesting to see Big-O play used in real life, because sometimes the theory feels a bit abstract, but watching the stopwatch results made it click;  

it helped me connect what I’ve learned to what actually happens when the code runs. 



