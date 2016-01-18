Example
Input: "This is a statement, and so is this."

Output: 
this - 2
is – 2
a – 1
statement – 1
and – 1
so - 1 

The result shows the comparsion is case-insensitive therefore the provided solution will do a case-insensitive 
compare. An acceptance test that verifies the given specification is included in this solution and is executed 
with other tests. The solution is using a small standard set of punctuations that can be easily extended.

Standard Set of Punctuations = { '.', ';', ':', '(', ')', '{', '}', '\'', '\"', '\n', '!', '?', ',', '-', '[', ']', '“', '”' }