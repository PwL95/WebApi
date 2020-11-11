using System.Data.Common;
using System.Threading;
using System.Net.Mime;
using System.Collections.Generic;
using Commander.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;    
        }

        /// <summary>
        /// Creates Command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Void</returns>
        public async Task CreateCommand(Command command, CancellationToken cancellationToken)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));
            await _context.AddAsync(command, cancellationToken);
            _ = await SaveChanges(cancellationToken);
        }

        /// <summary>
        /// Gets all commands
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of Command</returns>
        public async Task<IEnumerable<Command>> GetAllCommands(CancellationToken cancellationToken)
        {
            return await _context.Commands.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get command
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Command objects</returns>
        public async Task<Command> GetCommandById(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Commands.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            return result;
        }

        /// <summary>
        /// Saves context
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>boolean</returns>
        public async Task<bool> SaveChanges(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) >= 0;
        }

        /// <summary>
        /// Updates entity with new values
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>void</returns>
        public async Task UpdateCommand(Command command, CancellationToken cancellationToken)
        {   
            await _context.Commands.Update(command).ReloadAsync(cancellationToken);
            _ = await SaveChanges(cancellationToken);
        }

        /// <summary>
        /// Deletes 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteCommand(int id, CancellationToken cancellationToken)
        {
            var command = await GetCommandById(id, cancellationToken).ConfigureAwait(false);
            if(command == null) throw new ArgumentNullException();

            _context.Remove(command);
            _ = await SaveChanges(cancellationToken);
        }
    }
}