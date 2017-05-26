using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MenuButtonExample.xaml
    /// </summary>
    public partial class MenuButtonExample : Window
    {
        public MenuButtonExample()
        {
            InitializeComponent();
        }
        
        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as MenuItem).IsSubmenuOpen = true;
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as MenuItem).IsSubmenuOpen = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as MenuItem).Header.ToString());
        }
    }
}
