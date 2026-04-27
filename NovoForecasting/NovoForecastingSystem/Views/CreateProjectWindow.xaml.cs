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
using Microsoft.Data.SqlClient;



namespace NovoForecastingSystem.Views
{
    /// <summary>
    /// Interaction logic for CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {

        public CreateProjectWindow()
        {
            InitializeComponent();
        }
        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string projectName = ProjectNameTextBox.Text;
                DateTime? startDate = StartDatePicker.SelectedDate;
                string complexity = (ComplexityComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

                if (string.IsNullOrWhiteSpace(projectName))
                {
                    MessageBox.Show("Please enter a project name.");
                    return;
                }

                var repo = new Repos.ProjectRepo();
                repo.CreateProject(projectName, complexity, startDate);

                MessageBox.Show("Project successfully created in database!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
