using OrphanageDataModel.RegularData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.Persons
{
    [Table("Fathers")]
    public class Father
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int NameId { get; set; }

        public virtual Name Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("Dieday")]
        public DateTime DateOfDeath { get; set; }

        [Column("DeathCertificate_Photo")]
        public byte[] DeathCertificatePhotoData { get; set; }

        [Column("Photo")]
        public byte[] PhotoData { get; set; }

        public string Jop { get; set; }

        public string Story { get; set; }

        [Column("DeathResone")]
        public string DeathReason { get; set; }

        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        private string _IdentityCardNumber;

        [Column("IdentityCard_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
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
        public Image PersonalPhoto { get => PhotoData != null ? Image.FromStream(new MemoryStream(this.PhotoData)) : null; }

        [NotMapped]
        public Image DeathCertificateImage { get => DeathCertificatePhotoData != null ? Image.FromStream(new MemoryStream(this.DeathCertificatePhotoData)) : null; }

        [NotMapped]
        public string PersonalPhotoURI { get; set; }

        [NotMapped]
        public string DeathCertificateImageURI { get; set; }

        public virtual ICollection<Family> Families { get; set; }
    }
}