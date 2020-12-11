using System;

namespace SampleApp.Core.Domain
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>, IEquatable<BaseEntity<TKey>>
    {
        protected BaseEntity(TKey id)
        {
            if (Equals(id, default(TKey)))
            {
                throw new ArgumentException("The ID cannot be the type's default value.", "id");
            }

            Id = id;
        }

        // EF requires an empty constructor
        protected BaseEntity()
        {
        }

        public TKey Id { get; set; }

        public bool Equals(BaseEntity<TKey> other)
        {
            return other != null && Id.Equals(other.Id);
        }

        // For simple entities, this may suffice
        // As Evans notes earlier in the course, equality of Entities is frequently not a simple operation
        public override bool Equals(object otherObject)
        {
            return otherObject is BaseEntity<TKey> entity
                ? Equals(entity)
                : base.Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}