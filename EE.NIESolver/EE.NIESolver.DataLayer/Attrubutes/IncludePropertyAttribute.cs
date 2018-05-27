using System;

namespace EE.NIESolver.DataLayer.Attrubutes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IncludePropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public IncludePropertyAttribute(string propertyName = null)
        {
            
        }
    }
}