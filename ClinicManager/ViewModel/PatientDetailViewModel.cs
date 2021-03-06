﻿using ClinicManager.Model;
using ClinicManager.Services;
using ClinicManager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClinicManager.ViewModel
{
    public class PatientDetailViewModel : INotifyPropertyChanged

    {
        public ICommand DeleteCommand { get; set; }
        private PatientDataService _patientDataService;
        private DialogService _dialogService;

        private void Delete(object param)
        {
            Messenger.Default.Send(new PatientToBeDeleted() { PatientToBeDeletedProperty = selectedPatient.Model});
        }

        private bool CanDelete(object param)
        {
            if (selectedPatient != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICommand UpdateCommand { get; set; }

        private void Update(object param)
        {
            _patientDataService.UpdatePatient(selectedPatient.ToModel());
            _dialogService.ClosePatientsDetailDialog();
        }

        private bool CanUpdate(object param)
        {
            if (selectedPatient != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public PatientDetailViewModel(DialogService dialogService,
                                      PatientDataService patientDataService)
        {
            DeleteCommand = new CustomCommand(Delete, CanDelete);
            UpdateCommand = new CustomCommand(Update, CanUpdate);
            _dialogService = dialogService;
            _patientDataService = patientDataService;
            Messenger.Default.Register<PatientViewModel>(this, SetSelectedPatient);
        }

        private void SetSelectedPatient(PatientViewModel selectedPatientMessage)
        {
            selectedPatient = selectedPatientMessage;
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
