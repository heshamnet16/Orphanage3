using OrphanageDataModel.RegularData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.Persons
{
    [Table("BondsMen")]
    public class Caregiver
    {
        public Caregiver()
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

        [Column("Address_ID")]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        private string _IdentityCardNumber;

        [Column("IdentityCard_ID")]
        [MinLength(10, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MaxLength(11, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string IdentityCardId
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

        [Column("IdentityCard_Photo")]
        public byte[] IdentityCardPhotoFaceData { get; set; }

        [Column("IdentityCard_Photo2")]
        public byte[] IdentityCardPhotoBackData { get; set; }

        public string Jop { get; set; }

        public decimal? Income { get; set; }

        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        public byte[] FingerPrint { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public DateTime RegDate { get; set; }

        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int UserId { get; set; }

        public virtual User ActingUser { get; set; }

        public string Note { get; set; }

        [NotMapped]
        public Image IdentityCardImageFace { get { return IdentityCardPhotoFaceData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoFaceData)) : null; } }

        [NotMapped]
        public Image IdentityCardImageBack { get { return IdentityCardPhotoBackData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoBackData)) : null; } }

        [NotMapped]
        public string IdentityCardImageFaceURI { get; set; }

        [NotMapped]
        public string IdentityCardImageBackURI { get; set; }

        public virtual ICollection<Orphan> Orphans { get; set; }
    }
}