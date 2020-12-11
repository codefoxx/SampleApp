using MediatR;

namespace SampleApp.Persistence.MediatR.Commands
{
    public class RemoveAuthorCommand : IRequest<Unit>
    {
        public int AuthorId { get; set; }
    }
}