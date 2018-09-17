using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Services
{
    public class DialogService : IDialogService
    {
        private PatientDetailView _patientsDetialView;

        public void ShowPatientDetailDialog()
        {
            _patientsDetialView = new PatientDetailView();
            _patientsDetialView.ShowDialog();
        }
         public void ClosePatientsDetailDialog()
        {
            _patientsDetialView.Close();
        }


    }

    public interface IDialogService
    {
        void ShowPatientDetailDialog();
        void ClosePatientsDetailDialog();
    }
}
