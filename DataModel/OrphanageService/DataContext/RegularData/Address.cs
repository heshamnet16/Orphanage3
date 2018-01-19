using OrphanageService.DataContext.Persons;
using System.Collections.Generic;

namespace OrphanageService.DataContext.RegularData
{
    public class AddressDC
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Town { get; set; }

        public string Street { get; set; }

        public string HomePhone { get; set; }

        public string CellPhone { get; set; }

        public string WorkPhone { get; set; }

        string Fax { get; set; }

        string Email { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Note { get; set; }
    }
}
