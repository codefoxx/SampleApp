using MediatR;
using SampleApp.Core.Domain;
using System.Collections.Generic;

namespace SampleApp.Persistence.MediatR.Commands
{
    public sealed class CreateCourseCommand : IRequest<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public float FullPrice { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public Cover Cover { get; set; }

        public bool IsBeginnerCourse => Level == 1;
    }
}