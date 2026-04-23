using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Models.Enums;

namespace NovoForecastingSystem.Views
{
    /// <summary>
    /// Interaction logic for EditProject.xaml
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        public Project CurrentProject { get; private set; }

        public EditProjectWindow()
        {
            InitializeComponent();
            LoadEnums();
        }

        public EditProjectWindow(Project project) : this()
        {
            CurrentProject = project;
            LoadProjectData();
        }

        private void LoadProjectData()
        {
            if (CurrentProject != null)
            {
                ProjectNameTextBox.Text = CurrentProject.ProjectName;
                ComplexityComboBox.SelectedItem = CurrentProject.Complexity;
                PhaseComboBox.SelectedItem = CurrentProject.Phase;
            }
        }

        private void LoadEnums()
        {
            ComplexityComboBox.ItemsSource = Enum.GetValues<Complexity>();
            PhaseComboBox.ItemsSource = Enum.GetValues<NovoForecastingSystem.Models.Enums.Phase>();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProject != null)
            {
                CurrentProject.ProjectName = ProjectNameTextBox.Text;

                if (ComplexityComboBox.SelectedItem is Complexity complexity)
                {
                    CurrentProject.Complexity = complexity;
                }

                if (PhaseComboBox.SelectedItem is NovoForecastingSystem.Models.Enums.Phase phase)
                {
                    CurrentProject.Phase = phase;
                }
            }

            this.DialogResult = true;
            this.Close();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
