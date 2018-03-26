using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true,Inherited =false)]
    public class ShowInChooser : Attribute
    {
        public int Order { get; set; }
    }
}
