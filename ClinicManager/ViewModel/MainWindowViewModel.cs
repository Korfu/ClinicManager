using ClinicManager.Model;
using ClinicManager.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace ClinicManager.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ICommand EditCommand { get; set; }

        private void Edit(object param)
        {
            PatientDetailView view = new PatientDetailView();
            ViewModelLocator.PatientDetailViewModel.selectedPatient = selectedPatient;
            //view.
            //private static string GetPhotoForUser(PatientViewModel patient)
            //{
            //    return patient.InsuranceNumber.Last() % 2 == 0 ? "Photos/male.png" : "Photos/female.png";
            //}
            view.ShowDialog();
        }

        private bool CanEdit(object param)
        {
            if (selectedPatient != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public MainWindowViewModel()
        {
            EditCommand = new CustomCommand(Edit,CanEdit);
            AllPatients = new ObservableCollection<PatientViewModel>();
            var allPatients = LoadFromFile();
            foreach (var patient in allPatients)
            {
                AllPatients.Add(patient);
            }
        }

        public ObservableCollection<PatientViewModel> AllPatients { get; set; }

        private PatientViewModel _selectedPatient;
        public PatientViewModel selectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                _selectedPatient = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("selectedPatient"));
                }
            }
        }

        private ObservableCollection<PatientViewModel> _patients;
        public ObservableCollection<PatientViewModel> patients
        {
            get
            {
                return _patients;
            }
            set
            {
                _patients = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("patients"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private PatientViewModel[] LoadFromFile()
        {
            var allText = File.ReadAllText("samplePatients.json");
            var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                DateFormatString = "dd/MM/yyyy"
            });
            var patients = jsonSerializer.Deserialize<List<PatientViewModel>>(new JsonTextReader(new StringReader(allText)));
            for (var i = 0; i < patients.Count; i++)
            {
                var patient = patients[i];
                patient.Photo = GetPhotoForUser(patient);
            }

            return patients.ToArray();
        }

        private static string GetPhotoForUser(PatientViewModel patient)
        {
            return patient.InsuranceNumber.Last() % 2 == 0 ? "Photos/male.png" : "Photos/female.png";
        }
    }
}
