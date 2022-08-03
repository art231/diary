using Diary.Infrastructure.MediatR.Base;
using MediatR;

namespace Diary.Infrastructure.MediatR
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, bool>
            where TCommand : Command
    {
    }

    public interface ICommandHandler<in TCommand, TCommandResponse> : IRequestHandler<TCommand, TCommandResponse>
        where TCommand : Command<TCommandResponse>
    {
    }
}
