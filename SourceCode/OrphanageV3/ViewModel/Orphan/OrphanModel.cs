﻿using System;
using System.Drawing;

namespace OrphanageV3.ViewModel.Orphan
{
    public class OrphanModel
    {
        //private Image _Photo = null;
        //private IApiClient _apiClient = Program.Factory.Resolve<IApiClient>();

        public int Id { get; set; }
        public int FatherID { get; set; }
        public int MotherID { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public string FatherFullName { get; set; }

        public string MotherFatherName { get; set; }
        public string MotherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherFullName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Birthplace { get; set; }

        public int Age { get; set; }

        public bool IsBailed { get; set; }

        public bool IsExcluded { get; set; }

        public decimal ColorMark { get; set; }

        public DateTime RegDate { get; set; }

        public int Weight { get; set; }

        public int Tallness { get; set; }

        public string IdentityCardNumber { get; set; }

        public int FootSize { get; set; }

        public string Story { get; set; }

        public string StudyStage { get; set; }

        public string StudyReasons { get; set; }

        public bool IsSick { get; set; }

        public string CaregiverFatherName { get; set; }
        public string CaregiverLastName { get; set; }
        public string CaregiverFirstName { get; set; }
        public string CaregiverFullName { get; set; }

        public string ConsanguinityToCaregiver { get; set; }

        public string FacePhotoURI { get; set; }

        public Image Photo
        {
            get; set;
        }
    }
}