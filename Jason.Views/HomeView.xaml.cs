using Jason.ViewModels.WorshipServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Jason.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        public HomeView()
        {
            this.InitializeComponent();
        }
    }

    public class WorshipServicePartTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SongTemplate { get; set; }
        public DataTemplate GenericTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is SongViewModel)
                return SongTemplate;
            else
                return GenericTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
            => SelectTemplateCore(item);
    }
}