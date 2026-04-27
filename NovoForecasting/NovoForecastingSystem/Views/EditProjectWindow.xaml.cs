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

namespace NovoForecastingSystem.Views
{
    /// <summary>
    /// Interaction logic for EditProject.xaml
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        public EditProjectWindow()
        {
            InitializeComponent();
            PhaseComboBox.ItemsSource = Enum.GetValues(typeof(Models.Enums.Phase));
            ComplexityComboBox.ItemsSource = Enum.GetValues(typeof(Models.Enums.Complexity));
        }
    }
}
