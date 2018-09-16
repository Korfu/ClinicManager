using ClinicManager.Services;
using ClinicManager.ViewModel;

namespace ClinicManager.Utilities
{
    public static class ViewModelLocator
    {
        static DialogService _dialogService = new DialogService();
        static PatientDataService _patientDataService = new PatientDataService();

        public static MainWindowViewModel MainWindowViewModel = new MainWindowViewModel(_dialogService, _patientDataService);
        public static PatientDetailViewModel PatientDetailViewModel = new PatientDetailViewModel(_dialogService, _patientDataService);
    }
}