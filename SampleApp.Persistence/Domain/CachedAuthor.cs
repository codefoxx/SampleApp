using ProtoBuf;

using System.Collections.Generic;

namespace SampleApp.Persistence.Domain
{
    [ProtoContract]
    public class CachedAuthor
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public ICollection<CachedCourse> Courses { get; set; }
    }
}