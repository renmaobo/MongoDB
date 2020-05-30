using System;
namespace MongoDB.Attributes
{
    /// <summary>
    /// collection document name attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CollectionNameAttribute : Attribute
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">document name</param>
        /// <param name="describe">document describe</param>
        public CollectionNameAttribute(string name, string describe = default)
        {
            this.Name = name;
            this.Describe = describe;
        }

        /// <summary>
        /// document name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// document describe
        /// </summary>
        public string Describe { get; set; }
    }
}
