using KvizJatekProjekt.Data;
using LegyenOnIsMilliardosClassLibary.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KvizJatekProjekt
{
    public partial class Kerdesekuc : UserControl
    {
        private ObservableCollection<MultipleChoice> Questions { get; set; } = new();

        public Kerdesekuc()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            try
            {
                using var context = new KvizJatekContext();
                Questions = new ObservableCollection<MultipleChoice>(context.MultipleChoices.ToList());
                QuestionsGrid.ItemsSource = Questions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a kérdések betöltésekor: {ex.Message}");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    var newQuestion = new MultipleChoice
                    {
                        Kerdes_Szoveg = TxtKerdes.Text,
                        Valaszlehetosegek = TxtValaszok.Text,
                        Jovalasz = TxtJovalasz.Text
                    };

                    using (var context = new KvizJatekContext())
                    {
                        context.MultipleChoices.Add(newQuestion);
                        context.SaveChanges();
                        Questions.Add(newQuestion);
                    }
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt a hozzáadás során: {ex.Message}");
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsGrid.SelectedItem is MultipleChoice selectedQuestion)
            {
                try
                {
                    using (var context = new KvizJatekContext())
                    {
                        context.MultipleChoices.Remove(selectedQuestion);
                        context.SaveChanges();
                        Questions.Remove(selectedQuestion);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt a törlés során: {ex.Message}");
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new KvizJatekContext())
                {
                    foreach (var question in Questions)
                    {
                        var dbQuestion = context.MultipleChoices.Find(question.Id);
                        if (dbQuestion != null)
                        {
                            context.Entry(dbQuestion).CurrentValues.SetValues(question);
                        }
                    }
                    context.SaveChanges();
                    MessageBox.Show("Sikeres mentés!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}");
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(TxtKerdes.Text)) return false;
            if (string.IsNullOrWhiteSpace(TxtValaszok.Text)) return false;
            if (string.IsNullOrWhiteSpace(TxtJovalasz.Text)) return false;

            return true;
        }

        private void ClearFields()
        {
            TxtKerdes.Clear();
            TxtValaszok.Clear();
            TxtJovalasz.Clear();
        }
    }
}