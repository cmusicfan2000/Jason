using Jason.ViewModels.WorshipServices;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Jason.Views.WorshipServiceParts
{
    public sealed partial class SongView : UserControl
    {
        public SongViewModel ViewModel => DataContext as SongViewModel;

        public SongView()
        {
            this.InitializeComponent();
        }

        private void ListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {

            IEnumerable<string> items = e.Items.Select(x => x.ToString());

        }
    }
}
