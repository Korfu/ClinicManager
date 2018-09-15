using ClinicManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            AllPatients = new ObservableCollection<PatientViewModel>();
            var allPatients = LoadFromFile();
            foreach (var patient in allPatients)
            {
                AllPatients.Add(patient);
            }
        }


        public ObservableCollection<PatientViewModel> AllPatients { get; set; }

        private PatientViewModel selectedPatient;
        public PatientViewModel SelectedPatient
        {
            get
            {
                return selectedPatient;
            }
            set
            {
                selectedPatient = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedPatient"));
                }
            }
        }

        private ObservableCollection<PatientViewModel> patients;
        public ObservableCollection<PatientViewModel> Patients
        {
            get
            {
                return patients;
            }
            set
            {
                patients = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Patients"));
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
