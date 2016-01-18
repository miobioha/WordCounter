@WordCounting
Feature: WordCounting
	As an author
  I want to know the number of times each word appears in a sentence
  So that I can make sure I'm not repeating myself

Scenario: Count Distinct Words
	Given a sentence: 'This is a statement, and so is this.'
	When the program is run
	Then I am returned a distinct list of words in the sentence and the number of times they have occurred:
    | Word        | Count |
    | this        | 2     |
    | is          | 2     |
    | a           | 1     |
    | statement   | 1     |
    | and         | 1     |
    | so          | 1     |
