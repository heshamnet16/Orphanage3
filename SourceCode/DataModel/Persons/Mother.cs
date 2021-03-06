﻿using OrphanageDataModel.RegularData;
using OrphanageDataModel.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.Persons
{
    [Table("Mothers")]
    public class Mother
    {
        public Mother()
        {
            RegDate = DateTime.Now;
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_Id")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int NameId { get; set; }

        public virtual Name Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [BirthdayValidator]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool IsDead { get; set; }

        [Column("Dieday")]
        [DateOfDeathValidator]
        public DateTime? DateOfDeath { get; set; }

        [Column("IdentityCard_Photo")]
        public byte[] IdentityCardPhotoFaceData { get; set; }

        [Column("IdentityCard_Photo2")]
        public byte[] IdentityCardPhotoBackData { get; set; }

        [Column("Address_ID")]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool IsMarried { get; set; }

        public string HusbandName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("IsOwnOrphans")]
        public bool? HasSheOrphans { get; set; }

        public string Jop { get; set; }

        public decimal? Salary { get; set; }

        public string Story { get; set; }

        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        private string _IdentityCardNumber;

        [Column("IdentityCard_ID")]
        //[Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MinLength(10, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MaxLength(11, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string IdentityCardNumber
        {
            get { return _IdentityCardNumber; }
            set
            {
                if (value != null && value.Length == 10)
                {
                    _IdentityCardNumber = "0" + value;
                }
                else
                {
                    _IdentityCardNumber = value;
                }
                if (value != null && value.Length == 0)
                    _IdentityCardNumber = null;
            }
        }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public DateTime RegDate { get; set; }

        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int UserId { get; set; }

        public virtual User ActingUser { get; set; }

        public string Note { get; set; }

        [NotMapped]
        public Image IdentityCardFace { get => IdentityCardPhotoFaceData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoFaceData)) : null; }

        [NotMapped]
        public Image IdentityCardBack { get => IdentityCardPhotoBackData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoBackData)) : null; }

        [NotMapped]
        public string IdentityCardFaceURI { get; set; }

        [NotMapped]
        public string IdentityCardBackURI { get; set; }

        public virtual ICollection<Family> Families { get; set; }
    }
}