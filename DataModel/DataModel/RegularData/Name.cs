using OrphanageDataModel.Persons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrphanageDataModel.RegularData
{
    [Table("Names")]
    public class Name
    {

        [Column("ID",TypeName = "int")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string First { get; set; }

        public string  Father { get; set; }

        public string Last { get; set; }


        [Column("EName")]
        public string EnglishFirst { get; set; }

        [Column("EFather")]
        public string EnglishFather { get; set; }

        [Column("ELast")]
        public string  EnglishLast { get; set; }



        public virtual ICollection<Caregiver> Caregivers { get; set; }

        public virtual ICollection<Father> Fathers { get; set; }

        public virtual ICollection<Mother> Mothers { get; set; }

        public virtual ICollection<Orphan> Orphans { get; set; }

        public virtual ICollection<Guarantor> Guarantors { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
