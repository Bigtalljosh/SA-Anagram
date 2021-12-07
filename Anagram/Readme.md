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

I do have an idea for a solution that should theoretically cope with whatever sized file we wanted to throw at this.
We could spin up a Durable Function, read the file into batches and use the Fan-Out Pattern to kick off another function for each batch.
Let each batch be processed independantly then Fan-In with the results of each function, aggregating the results and outputting them.
This solution would take a bit longer than the guidelines however, so I'll stick to a basic console app.

Also working on the assumption that the spec says the words are in length order so I do not need to order them.
On a true production service I would not make this assumption and order them myself and check the length before searching for anagrams.

### My Chosen language

I'll be completing this assignment in C# which I consider to be my strongest.

### How to run the code

If you have .NET installed you can run the following: 

#### Building the Project

From the solution directory run:

`dotnet build`

#### Testing the Project

`dotnet test` - Run tests from the command line. There are both Integration and Unit tests in this project.

#### Running from source

`dotnet run Data\example1.txt` 

#### Running from Executable

To run the application, create a folder and pop the executable and the datafile in the same directory.
Use the following command to start the app: 
`./Anagram.Runner.exe example2.txt`

#### Notes

Running in debug mode will output files for each length of word and their anagrams. This was handy for me during debugging.

### Big O Analysis

It's been a long time since I had to compute this, I spent a little time trying to brush myself up on this topic and I think the below is accurate.

Runtime complexity is O(n * t) where n is the number of words, and t is the number of anagram groups. 
This is because the complexity scales with the number of words in the dictionary and then as the application runs and more unique entries are found the groups increase.

Memory complexity is O(n*2) where n is again the number of words.
This is because as words are processed, not only will they be in memory from the data file, but also now in memory for my data structure to sort them into anagram groups.

### Reasons behind the data structures I have chosen

I've used a dictionary as this utilises a hash set under the hood preventing duplicates being entered.
I've also used a simple list to store the anagrams, I chose not to use an array as I do not know ahead of time how big the array needs to be.
So the overhead of creating a new array each iteration, or allocating a wildly large array to try and compentsate would not be very efficient.

### What I would do with more time

With more time I would adjust the loop which reads in all the words of a same length at a time.
I went down this path as I made the assumption that the words are all sorted in order. I think for a production service when
I would not be able to make this assumption and the loop falls apart in practicality. 

I would ensure my file reader had some safe guarding in place, checking file names, extensions, existance etc.
And also write tests for these use cases. 

I'd include some benchmarks in the project. I did actually perform some rudimentary benchmarking myself before submission.
This resulted in me actually stripping out a lot of logging from the solution. 
It seems there's a huge overhead with the build in Microsoft.Extensions.Logging provider when logging to a console.
I was able to improve runtime of the app from ~3.5 minutes to ~20 seconds.
I do think it's likely an issue with logging to a console specifically. I think logging to a file would be more efficient as it would 
batch up the logs and flush them at intervals.