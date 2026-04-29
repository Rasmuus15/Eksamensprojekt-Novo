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
using NovoForecastingSystem.Models.Enums;
using NovoForecastingSystem.ViewModels;



namespace NovoForecastingSystem.Views
{
    /// <summary>
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow()
        {
            InitializeComponent();
            ComplexityComboBox.ItemsSource = Enum.GetValues(typeof(Complexity));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            //lukker vindue efter projekt er oprettet
            this.Close();
        }

    }
}
