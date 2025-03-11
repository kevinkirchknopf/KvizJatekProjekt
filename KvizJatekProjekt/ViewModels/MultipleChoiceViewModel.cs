using LegyenOnIsMilliardosClassLibary.Commands;
using LegyenOnIsMilliardosClassLibary.Models;
using System.Collections.Generic;
using System.Windows;

namespace MilliardosWPF.ViewModels
{
    public class MultipleChoiceViewModel : ViewModelBase
    {
        private readonly MultipleChoice _question;
        private readonly MainScreenViewModel _mainScreenViewModel;
        private string _selectedAnswer;

        public string Kerdes_Szoveg => _question.Kerdes_Szoveg;
        public string? ImgSrc => _question.ImgSrc;
        public List<string> Valaszlehetosegek => _question.ValaszlehetosegekList;

        public string SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                _selectedAnswer = value;
                OnPropertyChanged();
            }
        }

        public RelayCommands SubmitCommand { get; }

        public MultipleChoiceViewModel(MultipleChoice question, MainScreenViewModel mainScreenViewModel)
        {
            _question = question;
            _mainScreenViewModel = mainScreenViewModel;
            _question.ScrambleAnswers();
            SubmitCommand = new RelayCommands(_ => SubmitAnswer());
        }

        private void SubmitAnswer()
        {
            if (string.IsNullOrEmpty(SelectedAnswer))
            {
                MessageBox.Show("Választampd leéé!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isCorrect = SelectedAnswer == _question.Jovalasz;
            _mainScreenViewModel.SubmitAnswer(isCorrect); 
        }
    }
}