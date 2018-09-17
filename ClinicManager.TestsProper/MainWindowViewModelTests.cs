using ClinicManager.Services;
using ClinicManager.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ClinicManager.Tests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        public class MockDataService : IPatientDataService
        {
            public void DeletePatient(Patient toBeDeleted)
            {
                throw new NotImplementedException();
            }

            public List<Patient> GetAllPatients()
            {
                return new List<Patient>()
                {
                    new Patient()
                };
            }

            public void UpdatePatient(Patient toBeUpdated)
            {
                throw new NotImplementedException();
            }
        }

        public class MockDialogService : IDialogService
        {
            public bool Was_ShowPatientDetailDialog_Called = false;

            public void ClosePatientsDetailDialog()
            {

            }

            public void ShowPatientDetailDialog()
            {
                Was_ShowPatientDetailDialog_Called = true;

            }
        }

        [Test]
        public void EditCommand_DisplaysNewDialog()
        {
            //arrange
            var mockDialgoService = new MockDialogService();
            var mockDataService = new MockDataService();
            var sut = new MainWindowViewModel(mockDialgoService, mockDataService);

            //act
            sut.EditCommand.Execute(null);

            //assert
            Assert.IsTrue(mockDialgoService.Was_ShowPatientDetailDialog_Called);
        }

    }
}
