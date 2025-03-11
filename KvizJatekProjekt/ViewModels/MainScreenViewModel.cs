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
            // Initialize the context
            _context = new KvizJatekContext();

            // Ensure the database is created
            _context.Database.EnsureCreated();

            // Seed the database if it's empty
            KvizJatekContext.Seed(_context);

            StartGameCommand = new RelayCommands(_ => StartGame());
            ExitCommand = new RelayCommands(_ => Application.Current.Shutdown());

            // Set initial screen
            CurrentView = new MainScreen();
        }

        private void StartGame()
        {
            // Load all questions from the database
            _questions = _context.MultipleChoices.OrderBy(x => Guid.NewGuid()).ToList();
            _currentQuestionIndex = 0;
            _score = 0;

            // Show the first question
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            if (_currentQuestionIndex < _questions.Count)
            {
                // Show the next question
                var question = _questions[_currentQuestionIndex];
                CurrentView = new MultipleChoiceView(question, this);
            }
            else
            {
                // No more questions, show the final score
                MessageBox.Show($"Game Over! Your final score is: {_score}/{_questions.Count}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                CurrentView = new MainScreen(); // Return to the main screen
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