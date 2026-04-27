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
using NovoForecastingSystem.Models.Enums;
using NovoForecastingSystem.Repos;

namespace NovoForecastingSystem.Views
{
    /// <summary>
    /// Interaction logic for CreateRessource.xaml
    /// </summary>
    public partial class AddResourceWindow : Window
    {

        public AddResourceWindow()
        {
            InitializeComponent();
            LoadEnum();
        }

        private void LoadEnum()
        {
            ResourceRepo ResourceRepo = new ResourceRepo();
            Phase_ComboBox.ItemsSource = Enum.GetValues(typeof(Phase));
            Role_ComboBox.ItemsSource = Enum.GetValues(typeof(JobRole));
            Email_ComboBox.ItemsSource = ResourceRepo.PrintEmail();
        }



        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Object Phase = Phase_ComboBox.SelectedItem;
                Object Role = Role_ComboBox.SelectedItem;


            }
            catch 
            {
                
            }

        }
    }
}
