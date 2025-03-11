using KvizJatekProjekt.Data;
using LegyenOnIsMilliardosClassLibary.Commands;
using LegyenOnIsMilliardosClassLibary.Models;
using MilliardosWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MilliardosWPF.ViewModels
{
    public class MainScreenViewModel : ViewModelBase
    {
        private readonly KvizJatekContext _context;
        private object _currentView;
        private List<MultipleChoice> _questions;
        private int _currentQuestionIndex;
        private int _score;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public RelayCommands StartGameCommand { get; }
        public RelayCommands ExitCommand { get; }

        public MainScreenViewModel()
        {
           
            _context = new KvizJatekContext();

            _context.Database.EnsureCreated();

            KvizJatekContext.Seed(_context);

            StartGameCommand = new RelayCommands(_ => StartGame());
            ExitCommand = new RelayCommands(_ => Application.Current.Shutdown());

         
            CurrentView = new MainScreen();
        }

        private void StartGame()
        {
           
            _questions = _context.MultipleChoices.OrderBy(x => Guid.NewGuid()).ToList();
            _currentQuestionIndex = 0;
            _score = 0;

          
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            if (_currentQuestionIndex < _questions.Count)
            {
               
                var question = _questions[_currentQuestionIndex];
                CurrentView = new MultipleChoiceView(question, this);
            }
            else
            {
                
                MessageBox.Show($" {_score}/{_questions.Count}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                CurrentView = new MainScreen(); 
            }
        }

        public void SubmitAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                _score++;
            }

            // Move to the next question
            _currentQuestionIndex++;
            ShowNextQuestion();
        }
    }
}