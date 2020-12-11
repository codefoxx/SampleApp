using AutoMapper;
using SampleApp.Core.Domain;
using SampleApp.Persistence.MediatR.Commands;

namespace SampleApp.Persistence.MediatR.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;


            CreateMap<CreateCourseCommand, Course>();
        }
    }
}