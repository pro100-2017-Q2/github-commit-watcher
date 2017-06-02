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
    /// Interaction logic for GridCells.xaml
    /// </summary>
    public partial class GridCells : Window
    {
        public GridCells()
        {
            InitializeComponent();

            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; i < 50; i++)
                {
                    MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }
            
        }
    }
}
