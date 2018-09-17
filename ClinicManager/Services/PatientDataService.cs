using ClinicManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Services
{
    public class PatientDataService : IPatientDataService
    {
        public List<Patient> GetAllPatients()
        {
            return LoadFromFile().ToList();
        }

        public void DeletePatient(Patient toBeDeleted)
        {
            var currentList = LoadFromFile().ToList();
            var matchingOne = currentList.Single(x => x.InsuranceNumber == toBeDeleted.InsuranceNumber);
            currentList.Remove(matchingOne);
            Save(currentList);
        }

        public void UpdatePatient(Patient toBeUpdated)
        {
            var currentList = LoadFromFile().ToList();
            var matchingOne = currentList.Single(x => x.InsuranceNumber == toBeUpdated.InsuranceNumber);
            int indexOf = currentList.IndexOf(matchingOne);
            currentList.Insert(indexOf, toBeUpdated);
            currentList.Remove(matchingOne);
            Save(currentList);

        }

        private Patient[] LoadFromFile()
        {
            var allText = File.ReadAllText("samplePatients.json");
            var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                DateFormatString = "dd/MM/yyyy"
            });
            var patients = jsonSerializer.Deserialize<List<Patient>>(new JsonTextReader(new StringReader(allText)));
            for (var i = 0; i < patients.Count; i++)
            {
                var patient = patients[i];
                patient.Photo = GetPhotoForUser(patient);
            }

            return patients.ToArray();
        }

        private void Save(List<Patient> patientsList)
        {
            var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                DateFormatString = "dd/MM/yyyy"
            });
            File.Delete("samplePatients.json");
            using (var streamWriter = new StreamWriter(File.OpenWrite("samplePatients.json")))
            {
                jsonSerializer.Serialize(streamWriter, patientsList);
            }
        }

        private static string GetPhotoForUser(Patient patient)
        {
            return patient.InsuranceNumber.Last() % 2 == 0 ? "Photos/male.png" : "Photos/female.png";
        }
    }

    public interface IPatientDataService
    {
        List<Patient> GetAllPatients();
        void DeletePatient(Patient toBeDeleted);
        void UpdatePatient(Patient toBeUpdated);
    }
}
