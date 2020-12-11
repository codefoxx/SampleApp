using MediatR;
using SampleApp.Core.Domain;

using System.Collections.Generic;

namespace SampleApp.Persistence.MediatR.Commands
{
    public class UpdateAuthorCommand : IRequest<Author>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}