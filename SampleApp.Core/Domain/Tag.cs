using System.Collections.Generic;

namespace SampleApp.Core.Domain
{
    public class Tag : BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}