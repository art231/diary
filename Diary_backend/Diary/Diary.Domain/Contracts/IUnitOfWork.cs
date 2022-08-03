using System.Threading;
using System.Threading.Tasks;

namespace Diary.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken token);
    }
}