using OrphanageV3.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Bail
{
    public class BailModel
    {
        [ShowInChooser(Order = 0)]
        public int Id { get; set; }

        [ShowInChooser(Order = 1)]
        public string GuarantorName { get; set; }

        public string CurrencyName { get; set; }

        [ShowInChooser(Order = 4)]
        public string CurrencyShortcut { get; set; }

        [ShowInChooser(Order = 2)]
        public decimal Amount { get; set; }

        [ShowInChooser(Order = 3)]
        public string AccountName { get; set; }

        public int OrphansCount { get; set; }

        public int FamiliesCount { get; set; }

        [ShowInChooser(Order = 5)]
        public bool IsFamilyBail { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsExpired { get; set; }

        public bool IsMonthlyBail { get; set; }

        public DateTime RegDate { get; set; }

        public string UserName { get; set; }

        public string Note { get; set; }
    }
}