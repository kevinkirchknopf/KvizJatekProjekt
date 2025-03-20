using KvizJatekProjekt;
using System.Windows;
using System.Windows.Controls;

namespace MilliardosWPF.Views
{
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void BtnOpenMainMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainScreen = new Kerdesekuc();
            Window window = new Window
            {
                Title = "Kérdések",
                Content = mainScreen,
                Width = 400,
                Height = 300
            };
            window.Show();
        }
    }
}
