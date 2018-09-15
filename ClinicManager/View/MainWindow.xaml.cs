using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ClinicManager.ViewModel;
using Newtonsoft.Json;

namespace ClinicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Patient> AllPatients { get; set;  }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            MainManuEditButton.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PatientDetailView detailView = new PatientDetailView();
            detailView.Patient = ((Patient) PatientsListBox.SelectedItem);
            detailView.ShowDialog();
        }

        private void MenuItem_Edit(object sender, RoutedEventArgs e)
        {
            PatientDetailView detailView = new PatientDetailView();
            detailView.Patient = ((Patient) PatientsListBox.SelectedItem);
            detailView.ShowDialog();
        }

        private void MenuItem_SaveAll(object sender, RoutedEventArgs e)
        {
            var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                DateFormatString = "dd/MM/yyyy"
            });
            File.Delete("samplePatients.json");
            using (var streamWriter = new StreamWriter(File.OpenWrite("samplePatients.json")))
            {
                jsonSerializer.Serialize(streamWriter, PatientsListBox.Items);
            }
        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
