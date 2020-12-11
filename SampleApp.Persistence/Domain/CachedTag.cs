using ProtoBuf;

using System.Collections.Generic;

namespace SampleApp.Persistence.Domain
{
    [ProtoContract]
    public class CachedTag
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public virtual ICollection<CachedCourse> Courses { get; set; } = new HashSet<CachedCourse>();
    }
}