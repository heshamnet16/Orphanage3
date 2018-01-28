using OrphanageService.DataContext.Persons;
using System.Collections.Generic;

namespace OrphanageService.DataContext.RegularData
{
    public class NameDto
    {
        public int Id { get; set; }

        public string First { get; set; }

        public string  Father { get; set; }

        public string Last { get; set; }

        public string EnglishFirst { get; set; }

        public string EnglishFather { get; set; }

        public string  EnglishLast { get; set; }
    }
}
