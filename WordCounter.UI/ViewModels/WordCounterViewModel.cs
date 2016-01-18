using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using WordCounter.Core;

namespace WordCounter.UI.ViewModels
{
  public delegate Dictionary<string, int> WordCountDelegate(string str);

  public class WordCounterViewModel : BindableBase
  {
    private string _input;
    private bool _isBusy;
    private IDictionary<string, int> _wordCount;
    internal Func<string, Dictionary<string, int>> WordCounterFunc;
    private readonly WordCountDelegate _countWordsDelegate;

    public WordCounterViewModel(WordCountDelegate countWordsDelegate)
    {
      _countWordsDelegate = countWordsDelegate;
      RunCommand =
        DelegateCommand.FromAsyncHandler(RunAsync, () => !string.IsNullOrEmpty(Input) && !IsBusy)
          .ObservesProperty(() => Input)
          .ObservesProperty(() => IsBusy);
    }

    public DelegateCommand RunCommand { get; set; }

    public IDictionary<string, int> WordCount
    {
      get { return _wordCount; }
      set { SetProperty(ref _wordCount, value); }
    }

    public string Input
    {
      get { return _input ; }
      set { this.SetProperty(ref _input, value); }
    }

    public bool IsBusy
    {
      get { return _isBusy; }
      set { this.SetProperty(ref _isBusy, value); }
    }

    private async Task RunAsync()
    {
      IsBusy = true;
      WordCount = await Task.Run(() => _countWordsDelegate(Input));
      IsBusy = false;
    }
  }
} 
