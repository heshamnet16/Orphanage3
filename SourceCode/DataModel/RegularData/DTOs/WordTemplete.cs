using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageDataModel.RegularData.DTOs
{
    [Table("WordFiles")]
    public class WordTemplete
    {
        /// <summary>
        /// the name of the word template file
        /// </summary>
        ///
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TemplateName { get; set; }

        public byte[] Data { get; set; }

        public bool IsFamily { get; set; }

        public int MaxOrphansCount { get; set; }

        public bool UseBookmarks { get; set; }

        public bool UseCustomBoolean { get; set; }

        public string BooleanYesString { get; set; }

        public string BooleanNoString { get; set; }

        public bool UseCustomImageSize { get; set; }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public bool IsDefault { get; set; }

        public virtual WordPage WordPageData { get; set; }
    }
}