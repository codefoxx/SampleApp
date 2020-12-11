using MediatR;
using SampleApp.Core.Domain;
using System.Collections.Generic;

namespace SampleApp.Persistence.MediatR.Queries
{
    public class GetCoursesWithAuthorsQuery : PaginationQuery, IRequest<IList<Course>>
    {
      
    }
}