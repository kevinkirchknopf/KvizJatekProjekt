using System.Windows;
using MilliardosWPF.ViewModels;

namespace MilliardosWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainScreenViewModel(); // Set MainScreen as initial view
        }
    }
}
