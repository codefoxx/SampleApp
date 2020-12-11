using AutoMapper;
using SampleApp.Core.Domain;
using SampleApp.Persistence.Domain;

namespace SampleApp.Persistence.MappingProfiles
{
    public class DomainToCachedDomainProfile : Profile
    {
        public DomainToCachedDomainProfile()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;

            CreateMap<Author, CachedAuthor>();
            CreateMap<Course, CachedCourse>();
            CreateMap<Tag, CachedTag>();
            CreateMap<Cover, CachedCover>();
        }
    }
}