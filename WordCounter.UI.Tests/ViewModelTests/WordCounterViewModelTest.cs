using System.Threading;
using NUnit.Framework;
using WordCounter.Core;
using WordCounter.UI.ViewModels;

namespace WordCounter.UI.Tests.ViewModelTests
{
  [TestFixture]
  public class WordCounterViewModelTest
  {
    [TestCase("", false)]
    [TestCase(null, false)]
    [TestCase("hello world", true)]
    public void RunCommand_ShouldReturnExpectedCanExecuteResult_WhenGivenAnInputString(string input, bool expected)
    {
      // Arrange
      var target = new WordCounterViewModel(s => s.CountDistinctWords()) {Input = input};

      // Act
      bool actual = target.RunCommand.CanExecute();

      // Assert    
      Assert.AreEqual(expected, actual);
    }

    [Test]
    public async void RunCommand_ShouldSetListOfWordCount_WhenGivenAValidInputString()
    {
      // Arrange
      var target = new WordCounterViewModel(str => str.CountDistinctWords())
      {
        WordCount = null,
        Input = "hello world"
      };

      // Act
      await target.RunCommand.Execute();
    
      // Assert    
      Assert.IsNotNull(target.WordCount);
    }

    [Test]
    public async void RunCommand_ShouldDisableRunCommand_WhenProcessingInputString()
    {
      // Arrange
      var target = new WordCounterViewModel(str =>
      {
        Thread.Sleep(1000);
        return str.CountDistinctWords();
      })
      {
        WordCount = null,
        Input = "hello world"
      };

      // Act
      var task = target.RunCommand.Execute();
      bool canExecute = target.RunCommand.CanExecute();
      await task;

      // Assert    
      Assert.IsFalse(canExecute);
    }

    [Test]
    public async void RunCommand_ShouldEnableRunCommand_WhenProcessingIsComplete()
    {
      // Arrange
      var target = new WordCounterViewModel(str =>
      {
        Thread.Sleep(1000);
        return str.CountDistinctWords();
      })
      {
        WordCount = null,
        Input = "hello world"
      };

      // Act
      await target.RunCommand.Execute();
      bool canExecute = target.RunCommand.CanExecute();

      // Assert    
      Assert.IsTrue(canExecute);
    }
  }
}
