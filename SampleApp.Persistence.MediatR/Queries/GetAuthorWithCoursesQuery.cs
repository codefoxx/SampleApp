using MediatR;
using SampleApp.Core.Domain;
using System.Collections.Generic;

namespace SampleApp.Persistence.MediatR.Queries
{
    public class GetAuthorWithCoursesQuery : IRequest<Author>
    {
        public int AuthorId { get; set; }

        public GetAuthorWithCoursesQuery(int authorId)
        {
            AuthorId = authorId;
        }
    }
}