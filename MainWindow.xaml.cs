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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyBib
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        public ComboBox ComboWombo = new ComboBox();
        
        public TextBlock Combo_txt = new TextBlock();
        public MainWindow()
        {
            InitializeComponent();
            ComboWombo.Width = 269;
            ComboWombo.Height = 25;


            ComboWombo.IsEditable = true;
            ComboWombo.IsReadOnly = true;
            ComboWombo.Text = "Select A Format";

            ComboWombo.Items.Add("MLA8");
            
            ComboWombo.Items.Add("APA");
            MainGrid.Children.Add(ComboWombo);
            Canvas.SetTop(ComboWombo, 213);
            Canvas.SetLeft(ComboWombo, 123);

        }

        private void CreateCit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
