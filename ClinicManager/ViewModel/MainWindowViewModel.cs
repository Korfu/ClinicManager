﻿using ClinicManager.Model;
using ClinicManager.Services;
using ClinicManager.Utilities;
using Newtonsoft.Json;
using System;
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

        private IDialogService _dialogService;
        private IPatientDataService _patientDataService;

        private void Edit(object param)
        {
            _dialogService.ShowPatientDetailDialog();
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

        public MainWindowViewModel(IDialogService dialogService,
                                      IPatientDataService patientDataService)
        {
            _dialogService = dialogService;
            _patientDataService = patientDataService;
            EditCommand = new CustomCommand(Edit,CanEdit);
            AllPatients = new ObservableCollection<PatientViewModel>();
            Messenger.Default.Register<PatientToBeDeleted>(this, DeleteSelectedPatient);
            var allPatients = _patientDataService.GetAllPatients();
            var patientViewModels = allPatients.Select(x => PatientViewModel.FromModel(x));
            foreach (var patient in patientViewModels)
            {
                AllPatients.Add(patient);
            }
        }

        private void DeleteSelectedPatient(PatientToBeDeleted patientToBeDeleted)
        {
            //AllPatients.Remove(patientToBeDeleted.PatientToBeDeletedProperty);
            _patientDataService.DeletePatient(patientToBeDeleted.PatientToBeDeletedProperty);
            _dialogService.ClosePatientsDetailDialog();
            var allPatients = _patientDataService.GetAllPatients();
            var patientViewModels = allPatients.Select(x => PatientViewModel.FromModel(x));
            AllPatients.Clear();
            foreach (var patient in patientViewModels)
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
                Messenger.Default.Send(value);
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

       
    }
}
