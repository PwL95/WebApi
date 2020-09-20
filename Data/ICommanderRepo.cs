using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        Task<bool> SaveChanges(CancellationToken cancellationToken);

        Task<IEnumerable<Command>> GetAllCommands(CancellationToken cancellationToken);

        Task<Command> GetCommandById(int id, CancellationToken cancellationToken);

        Task CreateCommand(Command command, CancellationToken cancellationToken);

        Task UpdateCommand(Command command, CancellationToken cancellationToken);
        
    }
}