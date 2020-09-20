using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        Task<IEnumerable<Command>> GetAllCommands(CancellationToken cancellationToken);

        Task<Command> GetCommandById(int id, CancellationToken cancellationToken);

    }
}