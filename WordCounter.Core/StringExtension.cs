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
      return CountDistinctWords(str, WordCountOptions.CaseInsensitive, punctuations);
    }

    public static Dictionary<string, int> CountDistinctWords(this string str, WordCountOptions options,
      params char[] punctuations)
    {
      return CountDistinctWordsInternal2(str, options, punctuations);
    }

    private static Dictionary<string, int> CountDistinctWordsInternal2(string str, WordCountOptions options,
      params char[] punctuations)
    {
      str.ThrowIfNull("str");
      punctuations = punctuations.Length != 0 ? punctuations : Punctuations.Default;
      Dictionary<string, int> distinctWords = new Dictionary<string, int>();
      var pset = new HashSet<char>(punctuations);
      int start = -1;
      for (int i = 0; i < str.Length; i++)
      {
        if (char.IsWhiteSpace(str[i]))
        {
          if (start != -1)
          {
            int end = i - 1;
            CountWord(GetWord(str, start, end, pset), distinctWords, options);
            start = -1;
          }

          continue;
        }

        if (start != -1 && i == str.Length - 1)
        {
          int end = str.Length - 1;
          CountWord(GetWord(str, start, end, pset), distinctWords, options);
          start = -1;

          continue;
        }

        if (start == -1)
        { start = i; }
      }

      return distinctWords;
    }

    /// <summary>
    /// Get word from specifed string, removing any surrounding punctuations specified in the punctuation set.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="start">The start character position.</param>
    /// <param name="end">The end character position.</param>
    /// <param name="pset"></param>
    /// <returns>The extracted word if any otherwise empty string.</returns>
    private static string GetWord(string str, int start, int end, HashSet<char> pset)
    {
      while (start <= end && pset.Contains(str[start]))
      { start++; }

      while (end >= start && pset.Contains(str[end]))
      { end--; }

      if (start > end)
      { return string.Empty; }

      return str.Substring(start, end - start + 1);
    }

    private static void CountWord(string word, Dictionary<string, int> words, WordCountOptions options)
    {
      if (word == string.Empty) { return; }
      if (options == WordCountOptions.CaseInsensitive) { word = word.ToLowerInvariant(); }
      int count = words.ContainsKey(word) ? words[word] : 0;
      words[word] = ++count;
    }
  }
}
