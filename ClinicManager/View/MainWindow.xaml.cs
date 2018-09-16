using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ClinicManager.Model;
using ClinicManager.Utilities;
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
            DataContext = ViewModelLocator.MainWindowViewModel;
        }

        private void MenuItem_SaveAll(object sender, RoutedEventArgs e)
        {
            
        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
