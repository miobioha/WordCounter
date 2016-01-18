using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Utility;

namespace WordCounter.AcceptanceTests
{
  [Binding]
  public class WordCountingSteps
  {
    private string _sentence;

    [Given(@"a sentence: '(.*)'")]
    public void GivenASentence(string sentence)
    {
      _sentence = sentence;
    }

    [When(@"the program is run")]
    public void WhenTheProgramIsRun()
    {
      Window window = WordCountingFeature.Window;
      window.WaitWhileBusy(); 
      var textBox = window.Get<TextBox>("InputTextBox");
      textBox.BulkText = _sentence;
      window.WaitWhileBusy();
      Button button = window.Get<Button>("RunButton");
      button.Click();
      window.WaitWhileBusy();
      Retry.For(() => button.Enabled, enabled => !enabled, TimeSpan.FromSeconds(15));
    }

    [Then(@"I am returned a distinct list of words in the sentence and the number of times they have occurred:")]
    public void ThenIAmReturnedADistinctListOfWordsInTheSentenceAndTheNumberOfTimesTheyHaveOccurred(Table table)
    {
      Window window = WordCountingFeature.Window;
      var listView = window.Get<ListView>("WordCountListView");
      window.WaitWhileBusy();
      
      var wordCounts = (from row in listView.Rows
        let word = row.Cells["Word"].Text
        let count = row.Cells["Count"].Text
        select new WordCount {Word = word, Count = count}).ToList();

      table.CompareToSet(wordCounts);
    }
  }

  public class WordCount
  {
    public string Word { get; set; }
    public string Count { get; set; }
  }
}
