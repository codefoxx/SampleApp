namespace SampleApp.Core.Domain
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; }
    }
}