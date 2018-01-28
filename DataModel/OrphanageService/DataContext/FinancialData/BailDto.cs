using OrphanageService.DataContext.Persons;
using System;

namespace OrphanageService.DataContext.FinancialData
{
    public class BailDto
    {
        public int Id { get; set; }

        public int? GuarantorID { get; set; }

        public virtual  GuarantorDto Guarantor { get; set; }

        public decimal Amount { get; set; }

        public int AccountID { get; set; }

        public virtual AccountDto Account { get; set; }
        
        public bool IsFamilyBail { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsExpired { get; set; }

        public bool IsMonthlyBail { get; set; }
        
        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDto ActingUser { get; set; }
        
        public string Note { get; set; }
        
    }
}
