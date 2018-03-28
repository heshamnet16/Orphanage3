using System;

namespace OrphanageV3.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ShowInChooser : Attribute
    {
        public int Order { get; set; }
    }
}