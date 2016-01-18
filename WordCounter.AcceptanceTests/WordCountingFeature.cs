using TechTalk.SpecFlow;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace WordCounter.AcceptanceTests
{
  [Binding]
  public partial class WordCountingFeature
  {
    public static Application WpfApplication;
    public static Window Window;

    [BeforeFeature("WordCounting")]
    public static void BeforeWordCountingFeature()
    {
      WpfApplication = Application.Launch("WordCounter.UI\\WordCounter.UI.exe");
      Window = WpfApplication.GetWindow("MainWindow");
    }

    [AfterFeature("WordCounting")]
    public static void AfterWordCountingFeature()
    {
      WpfApplication.Close();
      WpfApplication = null;
      Window = null;
    }
  }
}
