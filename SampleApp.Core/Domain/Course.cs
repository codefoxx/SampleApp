using System;
using System.Collections.Generic;

namespace SampleApp.Core.Domain
{
    public class Course : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public float FullPrice { get; set; }

        public virtual Author Author { get; set; }

        public int AuthorId { get; set; }

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public Cover Cover { get; set; }

        public bool IsBeginnerCourse => Level == 1;
    }
}