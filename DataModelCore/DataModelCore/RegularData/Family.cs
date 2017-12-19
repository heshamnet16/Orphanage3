﻿using DataModelCore.FinancialData;
using DataModelCore.Persons;
using ImageSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace DataModelCore.RegularData
{
    [Table("Famlies")]
    public class Family
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }



        [Column("Mother_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Mother")]
        public int MotherId { get; set; }
        public virtual Mother Mother { get; set; }




        [Column("Father_Id")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Father")]
        public int FatherId { get; set; }
        public virtual Father Father { get; set; }



        [Column("Bail_ID")]
        [ForeignKey("Bail")]
        public int? BailId { get; set; }
        public virtual Bail Bail { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsBailed { get; set; }



        [Column("Address_ID")]
        [ForeignKey("PrimaryAddress")]
        public int? AddressId { get; set; }
        public virtual Address PrimaryAddress { get; set; }




        [Column("Address_ID2")]
        [ForeignKey("AlternativeAddress")]
        public int? AlternativeAddressId { get; set; }
        public virtual Address AlternativeAddress { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("isExcluded")]
        public bool IsExcluded { get; set; }

        [Column("Finncial_State")]
        public string FinncialStatus { get; set; }


        [Column("FamilyCard_Num")]
        public string FamilyCardNumber { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("IsRefugee")]
        public bool IsTheyRefugees { get; set; }


        [Column("Redidence_State")]
        public string ResidenceStatus { get; set; }


        [Column("Residence_Type")]
        public string ResidenceType { get; set; }


        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }

        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("ActingUser")]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Note { get; set; }


        [Column("FamilyCardPhoto")]
        public byte[] FamilyCardPhotoFrontData { get; set; }


        [Column("FamilyCardPhotoP2")]
        public byte[] FamilyCardPhotoBackData { get; set; }


        [NotMapped]
        public Image FamilyCardImageFace { get => FamilyCardPhotoFrontData != null ? new Image(new MemoryStream(this.FamilyCardPhotoFrontData)) : null; }


        [NotMapped]
        public Image FamilyCardImageBack { get => FamilyCardPhotoBackData != null ? new Image(new MemoryStream(this.FamilyCardPhotoBackData)) : null; }




        public virtual ICollection<Orphan> Orphans { get; set; }

    }

}
