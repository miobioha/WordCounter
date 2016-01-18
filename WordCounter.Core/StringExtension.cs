using System;
using System.Collections.Generic;
using WordCounter.Core.Enums;

namespace WordCounter.Core
{
  public static class StringExtension
  {
    /// <summary>
    /// Counts the number of times distinct words have occured.
    /// </summary>
    /// <param name="str">The input string</param>
    /// <param name="punctuations">
    /// Surroundings of a word are trimmed by removing punctuations (based on the characters in the array).
    /// If no characters are specified the default set of punctuations is used.
    /// </param>
    /// <returns> A distinct list of words in the sentence and the number of times they have occurred.</returns>
    /// <remarks>Word compare is case insensitive for thisi method.</remarks>
    public static Dictionary<string, int> CountDistinctWords(this string str, params char[] punctuations)
    {
      return CountDistinctWordsInternal(str, WordCountOptions.CaseInsensitive, punctuations);
    }

    public static Dictionary<string, int> CountDistinctWords(this string str, WordCountOptions options, 
      params char[] punctuations)
    {
      return CountDistinctWordsInternal(str, options, punctuations);
    }

    private static Dictionary<string, int> CountDistinctWordsInternal(string str, WordCountOptions options, 
      params char[] punctuations)
    {
      str.ThrowIfNull(nameof(str));
      punctuations = punctuations.Length != 0 ? punctuations : Punctuations.Default;
      var words = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      Dictionary<string, int> distinctWords = new Dictionary<string, int>();

      for (int i = 0, j = words.Length; i < j; i++)
      {
        string word = words[i].TrimStart(punctuations).TrimEnd(punctuations);
        if (options == WordCountOptions.CaseInsensitive) { word = word.ToLowerInvariant();}
        if (word == string.Empty) { continue; }
        int count = distinctWords.ContainsKey(word) ? distinctWords[word] : 0;
        distinctWords[word] = ++count;
      }

      return distinctWords;
    }
  }
}
