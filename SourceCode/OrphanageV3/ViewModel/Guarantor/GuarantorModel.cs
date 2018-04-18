using OrphanageV3.Attributes;
using System;

namespace OrphanageV3.ViewModel.Guarantor
{
    public class GuarantorModel
    {
        [ShowInChooser(Order = 0)]
        public int Id { get; set; }

        [ShowInChooser(Order = 1)]
        public string FullName { get; set; }

        public string FullAddress { get; set; }

        [ShowInChooser(Order = 2)]
        public string AccountName { get; set; }

        public long? ColorMark { get; set; }

        [ShowInChooser(Order = 3)]
        public int OrphansCount { get; set; }

        [ShowInChooser(Order = 4)]
        public int FamiliesCount { get; set; }

        [ShowInChooser(Order = 5)]
        public int BailsCount { get; set; }

        public bool IsSupportingOnlyFamilies { get; set; }

        public bool IsPayingMonthly { get; set; }

        public bool IsStillPaying { get; set; }

        public DateTime RegDate { get; set; }

        public string UserName { get; set; }

        public string Note { get; set; }
    }
}