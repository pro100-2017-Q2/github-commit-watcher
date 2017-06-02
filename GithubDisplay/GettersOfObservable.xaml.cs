using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GithubDisplay
{
    /// <summary>
    /// Interaction logic for GettersOfObservable.xaml
    /// </summary>
    public partial class GettersOfObservable : Window, INotifyPropertyChanged
    {
        private ObservableCollection<string> allItems = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> AllItems
        {
            get { return allItems; }
            set {
                allItems = value;
                NotifyFieldChanged();
                NotifyFieldChanged("FilteredItems");
            }
        }

        void NotifyFieldChanged([CallerMemberName] string fieldName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fieldName));
        }

        public IEnumerable<string> FilteredItems
        {
            get
            {
                return AllItems.TakeWhile((s, i) => i % 2 == 0);
            }
        }



        public GettersOfObservable()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AllItems.Add("Testing");
        }
    }
}
