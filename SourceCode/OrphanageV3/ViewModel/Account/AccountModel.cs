using OrphanageV3.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Account
{
    public class AccountModel
    {
        [ShowInChooser(Order = 0)]
        public int Id { get; set; }

        [ShowInChooser(Order = 1)]
        public string AccountName { get; set; }

        public string Currency { get; set; }

        [ShowInChooser(Order = 2)]
        public string CurrencyShortcut { get; set; }

        [ShowInChooser(Order = 3)]
        public decimal Amount { get; set; }

        public bool CanNotBeNegative { get; set; }

        public string Note { get; set; }

        public DateTime RegDate { get; set; }

        public int UserName { get; set; }

        public int BailsCount { get; set; }

        public int GuarantorsCount { get; set; }
    }
}