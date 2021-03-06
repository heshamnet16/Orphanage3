﻿using OrphanageV3.Attributes;
using System;
using System.Drawing;

namespace OrphanageV3.ViewModel.Father
{
    public class FatherModel
    {
        public int Id { get; set; }

        [ShowInChooser(Order = 0)]
        public string FullName { get; set; }

        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }

        [ShowInChooser(Order = 1)]
        public string WifeName { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public Image Photo { get; set; }

        public string IdentityCardNumber { get; set; }

        public string Jop { get; set; }

        public string DeathReason { get; set; }

        public long? ColorMark { get; set; }

        public string Story { get; set; }

        public DateTime RegDate { get; set; }

        public string UserName { get; set; }

        public string Notes { get; set; }

        public string PersonalPhotoURI { get; set; }

        public string DeathCertificatePhotoURI { get; set; }

        [ShowInChooser(Order = 2)]
        public int OrphansCount { get; set; }
    }
}