using System;
using System.Collections.Generic;

namespace OrphanageV3.Services.Interfaces
{
    public interface IAutoCompleteService
    {
        IList<string> EnglishNameStrings { get; set; }
        IList<string> ArabicNameStrings { get; set; }
        IList<string> SicknessNames { get; set; }
        IList<string> DoctorsNames { get; set; }
        IList<string> MedicenNames { get; set; }
        IList<string> EducationReasons { get; set; }
        IList<string> EducationSchools { get; set; }
        IList<string> EducationStages { get; set; }
        IList<string> BirthPlaces { get; set; }
        IList<string> OrphanStories { get; set; }

        IList<string> Countries { get; set; }
        IList<string> Cities { get; set; }
        IList<string> Towns { get; set; }
        IList<string> Streets { get; set; }

        void LoadData();

        event EventHandler DataLoaded;
    }
}