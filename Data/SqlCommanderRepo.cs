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

        public async Task<IEnumerable<Command>> GetAllCommands(CancellationToken cancellationToken)
        {
            return await _context.Commands.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Command> GetCommandById(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Commands.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            return result;
        }
    }

}