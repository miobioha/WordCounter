using System.Configuration;
using Microsoft.Practices.Unity;
using Prism.Unity;
using WordCounter.Core;
using WordCounter.UI.ViewModels;

namespace WordCounter.UI
{
  public class Bootstrapper : UnityBootstrapper
  {
    protected override void ConfigureContainer()
    {
      base.ConfigureContainer();

      var punctuationSet = ConfigurationManager.AppSettings["PUNCTUATION_SET"];
      
      char[] pset = Punctuations.Default;
      if (punctuationSet != null)
      {
        pset = punctuationSet.ToCharArray();
      }

      Container.RegisterType<WordCountDelegate>(
        new InjectionFactory(_ => new WordCountDelegate(s => s.CountDistinctWords(pset))));
    }
  }
}
