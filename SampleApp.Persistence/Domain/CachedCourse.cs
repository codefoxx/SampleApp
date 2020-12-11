using ProtoBuf;

using System.Collections.Generic;

namespace SampleApp.Persistence.Domain
{
    [ProtoContract]
    public class CachedCourse
    {
        [ProtoMember(1)]
        public int Id { get; set; }


        [ProtoMember(2)] 
        public string Name { get; set; }
        

        [ProtoMember(3)]
        public string Description { get; set; }

        [ProtoMember(4)]
        public int Level { get; set; }

        [ProtoMember(5)]
        public float FullPrice { get; set; }

        [ProtoMember(6)]
        public CachedAuthor Author { get; set; }

        [ProtoMember(7)]
        public int AuthorId { get; set; }

        [ProtoMember(8)]
        public ICollection<CachedTag> Tags { get; set; }

        [ProtoMember(9)]
        public CachedCover Cover { get; set; }

        [ProtoMember(10)]
        public bool IsBeginnerCourse { get; }
    }
}