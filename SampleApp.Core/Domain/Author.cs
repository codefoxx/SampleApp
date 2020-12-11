using System.Collections.Generic;

namespace SampleApp.Core.Domain
{
    public class Author: BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}