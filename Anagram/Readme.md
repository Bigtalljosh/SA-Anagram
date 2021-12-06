# Anagram Assignment

## The Task

Write a program that takes as argument the path to a file containing one word per line, groups the words that are anagrams to each other, and writes to the standard output each of these groups.
The groups should be separated by newlines and the words inside each group by commas.

## The Data

You can make the following assumptions about the data in the files:

- The words in the input file are ordered by size
- Production files will not fit into memory all at once (but all the words of the same size would)
- The words are not necessarily actual English words, for example, “abc” and “cba” are both considered words for the sake of this exercise.

The files provided in the `Data` folder are just sample input data to help you reason about the problem. Production files will be much bigger.

If you make other assumptions, make sure you write them down in a readme in your submission


---

## My Notes

### Initial Assumption

After reading the spec, more specifically the `Production files will not fit into memory all at once (but all the words of the same size would)` point. 
I'm currently thinking I can open the file, start reading every line until the length of the string read is longer, at which point halt, process the word list, spit out the output and begin reading again.

I'm assuming "Anagram" in this case is used loosely to mean any two (or more) entries in the data set that contain all the same letters.
I've made this assumption based on the example given that "abc" and "cba" are considered words for the sake of this test.

In this case, I think I can just order each individual word alphabetically.
So GOD and DOG both become DGO, then compare neighbouring elements and check if they're equal. If they are that's a group.

I do have an idea for a solution that should theoretically cope with whatever sized file we wanted to throw at this.
We could spin up a Durable Function, read the file into batches and use the Fan-Out Pattern to kick off another function for each batch.
Let each batch be processed independantly then Fan-In with the results of each function, aggregating the results and outputting them.
This solution would take a bit longer than the guidelines however, so I'll stick to a basic console app.



### My Chosen language

I'll be completing this assignment in C# which I consider to be my strongest.

### How to run the code

TODO : FILL ME IN ONCE COMPLETED

### Big O Analysis

Runtime complexity is O(n * t) where n is the number of words, and t is the number of anagram groups

Memory complexity is O(n*2)

### Reasons behind the data structures I have chosen

TODO : FILL IN WHEN I'VE CHOSEN DATA STRUCTURES

### What I would do with more time

TODO : FILL IN ONCE COMPLETED