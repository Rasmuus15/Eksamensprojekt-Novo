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
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : Window
    {
        private class DbConnector : DatabaseConnector
        {
            public string ConnectionString => connectionString;
        }

        public CreateProject()
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

                var dbConnector = new DbConnector();
                string connectionString = dbConnector.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string insertProjectQuery = "INSERT INTO PROJECT (ProjectName, Complexity) VALUES (@ProjectName, @Complexity); SELECT SCOPE_IDENTITY();";
                            int projectId = 0;

                            using (SqlCommand cmd = new SqlCommand(insertProjectQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ProjectName", projectName);
                                cmd.Parameters.AddWithValue("@Complexity", (object)complexity ?? DBNull.Value);

                                object result = cmd.ExecuteScalar();
                                if (result != null)
                                {
                                    projectId = Convert.ToInt32(result);
                                }
                            }

                            if (startDate.HasValue && projectId > 0)
                            {
                                string insertLengthQuery = "INSERT INTO PROJECT_LENGTH (StartDate, ProjectId) VALUES (@StartDate, @ProjectId);";
                                using (SqlCommand lengthCmd = new SqlCommand(insertLengthQuery, connection, transaction))
                                {
                                    lengthCmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                                    lengthCmd.Parameters.AddWithValue("@ProjectId", projectId);
                                    lengthCmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Project successfully created in database!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error saving project: " + ex.Message);
                        }
                    }
                }
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
