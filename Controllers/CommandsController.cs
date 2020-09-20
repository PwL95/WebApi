using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController: ControllerBase
    {
        private readonly ICommanderRepo _repository;
        
        /// <summary>
        /// Interface of commands
        /// </summary>
        /// <param name="reposiotry"></param>
        public CommandsController(ICommanderRepo reposiotry)
        {
            _repository = reposiotry;
        }

        /// <summary>
        /// Get all commands
        /// </summary>
        /// <returns></returns>
        //GET api/commands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Command>>> GetAllCommands(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllCommands(cancellationToken);
            return Ok(result);
        }  

        //GET api/commands/{id}
        /// <summary>
        /// Get command by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Command>> GetCommandById(int id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetCommandById(id, cancellationToken);
            return Ok(result);
        }
    }
}