﻿using OrphanageService.DataContext.RegularData;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.Persons
{
    public class MotherDto
    {
        public int Id { get; set; }

        public int NameId { get; set; }

        public virtual NameDto Name { get; set; }

        public DateTime Birthday { get; set; }

        public bool IsDead { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public int? AddressId { get; set; }

        public virtual AddressDto Address { get; set; }

        public bool IsMarried { get; set; }

        public string HusbandName { get; set; }

        public bool? HasSheOrphans { get; set; }

        public string Jop { get; set; }
  
        public decimal? Salary { get; set; }

        public string Story { get; set; }

        public long? ColorMark { get; set; }

        public string IdentityCardNumber { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDto ActingUser { get; set; }

        public string Note { get; set; }

        public string IdentityCardFaceURI { get; set; }

        public string IdentityCardBackURI { get; set; }

        public virtual IList<FamilyDto> Families { get; set; }
    }
}