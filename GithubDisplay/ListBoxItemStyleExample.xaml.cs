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
    /// Interaction logic for ListBoxItemStyleExample.xaml
    /// </summary>
    public partial class ListBoxItemStyleExample : Window, INotifyPropertyChanged
    {

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; FieldChanged(); }
        }

        private ObservableCollection<string> data = new ObservableCollection<string>();
        public ObservableCollection<string> Data
        {
            get { return data; }
            set { data = value; FieldChanged(); }
        }


        public void FieldChanged([CallerMemberName] string fieldName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fieldName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ListBoxItemStyleExample()
        {
            InitializeComponent();
            parentPanel.DataContext = this;
            Enumerable.Range(1, 10).ToList().ForEach(i => Data.Add(i.ToString()));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Enabled = !Enabled;
        }
    }
}
