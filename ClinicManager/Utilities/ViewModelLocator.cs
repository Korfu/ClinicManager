using ClinicManager.ViewModel;

namespace ClinicManager
{
    public static class ViewModelLocator
    {
        public static MainWindowViewModel MainWindowViewModel = new MainWindowViewModel();
        public static PatientDetailViewModel PatientDetailViewModel = new PatientDetailViewModel();
    }
}