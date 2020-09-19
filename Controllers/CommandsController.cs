using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var result = _repository.GetAllCommands();
            return Ok(result);
        }  

        //GET api/commands/{id}
        /// <summary>
        /// Get command by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var result = _repository.GetCommandById(id);
            return Ok(result);
        }
    }
}