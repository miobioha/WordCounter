using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WordCounter.Core.Tests
{
  [TestFixture]
  public class StringExtensionTests
  {
    [Test]
    public void CountDistinctWords_ShouldThrowException_WhenSentenceIsNull()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => StringExtension.CountDistinctWords(null));
      Assert.AreEqual(exception.ParamName, "str");
    }

    [TestCase("")]
    [TestCase("!")]
    [TestCase(" ")]
    [TestCase("      ")] //3 Tabs with 1 white space.
    [TestCase("   ... '''' ,,,,, ")]
    public void CountDistinctWords_ShouldReturnEmptyList_WhenSentenceHasNoWord(string input)
    {
      var result = input.CountDistinctWords();
      CollectionAssert.IsEmpty(result);
    }

    [TestCase("It's", "it's")]
    [TestCase(".... ,,,,\nIt's", "it's")]
    [TestCase("It's::::;;;;", "it's")]
    [TestCase(";;;;;'''''(It's)\"\"\"\"", "it's")]
    public void CountDistinctWords_ShouldReturnListWithOneWord_WhenSentenceHasJustOneWordWithPunctuations(string input, string word)
    {
      var result = input.CountDistinctWords();
      Assert.IsTrue(result.Count == 1 && result[word] == 1);
    }

    [TestCase("This is a statement, and so is this", 6)]
    [TestCase("WPF wpf WpF, wPF, wPf,;", 1)]
    [TestCase("Test the word count algorithm with NUnit", 7)]
    [TestCase("This 'this' (THIS) \"ThiS\"", 1)]
    [TestCase("hello - world", 2)]
    [TestCase("hello-world", 1)]
    [TestCase("Reach me at hello@world.com", 4)]
    public void CountDistinctWords_ShoulGiveExpectedDistinctCountInList_WhenGivenASentence(string input, int expected)
    {
      var actual = input.CountDistinctWords().Count;
      Assert.AreEqual(expected, actual);
    }

    [TestCase("\"hello,word\", \"hello, word\", \"hello word,,,, \"", 5)]
    public void CountDistinctWords_ShoulGiveExpectedTotalCountInList_WhenGivenASentence(string input, int expected)
    {
      var actual = GetWordCount(input.CountDistinctWords());
      Assert.AreEqual(expected, actual);
    }

    [TestCase("This is a statement, and so is this", "this", 2)]
    [TestCase("This is a statement, and so is this", "is", 2)]
    [TestCase("This is a statement, and so is this", "a", 1)]
    [TestCase("This is a statement, and so is this", "statement", 1)]
    [TestCase("This is a statement, and so is this", "and", 1)]
    [TestCase("This is a statement, and so is this", "so", 1)]
    public void CountDistinctWords_ShoulGiveExpectedCountPerWord_WhenGivenASentence(string input, string word, int expected)
    {
      var result = input.CountDistinctWords();
      Assert.AreEqual(expected, result[word]);
    }

    private int GetWordCount(IDictionary<string, int> words)
    {
      return words.Sum(pair => pair.Value);
    }
  }
}
