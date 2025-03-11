using System.Windows.Controls;
using LegyenOnIsMilliardosClassLibary.Models;
using MilliardosWPF.ViewModels;

namespace MilliardosWPF.Views
{
    public partial class MultipleChoiceView : UserControl
    {
        public MultipleChoiceView(MultipleChoice question, MainScreenViewModel mainScreenViewModel)
        {
            InitializeComponent();
            DataContext = new MultipleChoiceViewModel(question, mainScreenViewModel);
        }
    }
}