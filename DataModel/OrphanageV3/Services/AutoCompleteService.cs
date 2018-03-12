using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public class AutoCompleteService : IAutoCompleteService
    {
        private readonly IApiClient _apiClient;

        public event EventHandler DataLoaded;

        public IList<string> EnglishNameStrings { get; set; }
        public IList<string> ArabicNameStrings { get; set ; }
        public IList<string> SicknessNames { get ; set ; }
        public IList<string> DoctorsNames { get ; set; }
        public IList<string> MedicenNames { get ; set; }
        public IList<string> EducationReasons { get ; set ; }
        public IList<string> EducationSchools { get; set; }
        public IList<string> EducationStages { get ; set ; }
        public IList<string> BirthPlaces { get ; set ; }
        public IList<string> OrphanStories { get ; set; }

        private bool EducationLoaded = false;
        private bool HealthLoaded = false;
        private bool NamesLoaded = false;
        private bool OrphanDataLoaded = false;

        public AutoCompleteService(IApiClient apiClient)
        {
            _apiClient = apiClient;
            EnglishNameStrings = new List<string>();
            ArabicNameStrings = new List<string>();
            SicknessNames = new List<string>();
            DoctorsNames = new List<string>();
            MedicenNames = new List<string>();
            EducationReasons = new List<string>();
            EducationSchools = new List<string>();
            EducationStages = new List<string>();
            BirthPlaces = new List<string>();
            OrphanStories = new List<string>();
            GetAutoCompleteStrings();
        }
        
        private async void GetAutoCompleteStrings()
        {
            var engFirstNamesTask = _apiClient.AutoCompletesController_GetEnglishFirstNamesAsync();
            var engFatherNamesTask = _apiClient.AutoCompletesController_GetEnglishFatherNamesAsync();
            var engLastNamesTask = _apiClient.AutoCompletesController_GetEnglishLastNamesAsync();
            var ArabicFirstNamesTask = _apiClient.AutoCompletesController_GetFirstNamesAsync();
            var ArabicFatherNamesTask = _apiClient.AutoCompletesController_GetFatherNamesAsync();
            var ArabicLastNamesTask = _apiClient.AutoCompletesController_GetLastNamesAsync();
            var SicknessNamesTask = _apiClient.AutoCompletesController_GetSicknessNamesAsync();
            var BirthPlacesTask = _apiClient.AutoCompletesController_GetOrphansPlacesOfBirthAsync();
            var MedicensNamesTask = _apiClient.AutoCompletesController_GetMedicensAsync();
            var EducationReasonsTask = _apiClient.AutoCompletesController_GetEducationReasonsAsync();
            var EducationSchoolsTask = _apiClient.AutoCompletesController_GetEducationSchoolsAsync();
            var EducationStagesTask = _apiClient.AutoCompletesController_GetEducationStagesAsync();

            var engFirstList = await engFirstNamesTask;
            foreach (var firstN in engFirstList)
                if (!EnglishNameStrings.Contains(firstN) && firstN != null && firstN.Length > 0)
                    EnglishNameStrings.Add(firstN);

            var engFatherList = await engFatherNamesTask;
            foreach (var FatherN in engFatherList)
                if (!EnglishNameStrings.Contains(FatherN) && FatherN != null && FatherN.Length > 0)
                    EnglishNameStrings.Add(FatherN);

            var emgLastList = await engLastNamesTask;
            foreach (var lastN in emgLastList)
                if (!EnglishNameStrings.Contains(lastN) && lastN != null && lastN.Length > 0)
                    EnglishNameStrings.Add(lastN);

            var FirstList = await ArabicFirstNamesTask;

            foreach (var firstN in FirstList)
                if (!ArabicNameStrings.Contains(firstN) && firstN != null && firstN.Length > 0)
                    ArabicNameStrings.Add(firstN);

            var FatherList = await ArabicFatherNamesTask;
            foreach (var FatherN in FatherList)
                if (!ArabicNameStrings.Contains(FatherN) && FatherN != null && FatherN.Length > 0)
                    ArabicNameStrings.Add(FatherN);

            var LastList = await ArabicLastNamesTask;
            foreach (var lastN in LastList)
                if (!ArabicNameStrings.Contains(lastN) && lastN != null && lastN.Length > 0)
                    ArabicNameStrings.Add(lastN);

            NamesLoaded = true;

            var SicknessList = await SicknessNamesTask;
            foreach (var sickness in SicknessList)
            {
                if (sickness == null) continue;
                var sickNs = sickness.Split(new char[] { ';' });
                foreach (var sickname in sickNs)
                {
                    if (!SicknessNames.Contains(sickname) && sickname != null && sickname.Length > 0)
                    {
                        SicknessNames.Add(sickname);
                    }
                }
            }
            var MedicenList = await MedicensNamesTask;
            foreach (var medicensString in MedicenList)
            {
                if (medicensString == null) continue;
                var medicensArray = medicensString.Split(new char[] { ';' });
                foreach (var medicen in medicensArray)
                {
                    if (!MedicenNames.Contains(medicen) && medicen != null && medicen.Length > 0)
                        MedicenNames.Add(medicen);
                }
            }

            HealthLoaded = true; 

            var EducationReasonsList = await EducationReasonsTask;
            foreach (var reason in EducationReasonsList)
                if (!EducationReasons.Contains(reason) && reason != null && reason.Length > 0)
                    EducationReasons.Add(reason);

            var EducationSchoolsList = await EducationSchoolsTask;
            foreach (var school in EducationSchoolsList)
                if (!EducationSchools.Contains(school) && school != null && school.Length > 0)
                    EducationSchools.Add(school);

            var EducationStagesList = await EducationStagesTask;
            foreach (var stage in EducationStagesList)
                if (!EducationStages.Contains(stage) && stage != null && stage.Length > 0)
                    EducationStages.Add(stage);
            EducationLoaded = true;

            var BirthPlacesList = await BirthPlacesTask;
            foreach (var birthplace in BirthPlacesList)
                if (!BirthPlaces.Contains(birthplace) && birthplace != null && birthplace.Length > 0)
                    BirthPlaces.Add(birthplace);
            OrphanDataLoaded = true;

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public void LoadData()
        {
            if (NamesLoaded && EducationLoaded && OrphanDataLoaded && HealthLoaded)
                DataLoaded?.Invoke(this, new EventArgs());
            else
                GetAutoCompleteStrings();
        }
    }
}
