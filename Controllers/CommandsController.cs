using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Dtos;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController: ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Interface of commands
        /// </summary>
        /// <param name="reposiotry"></param>
        public CommandsController(ICommanderRepo reposiotry, IMapper mapper)
        {
            _repository = reposiotry;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all commands
        /// </summary>
        /// <returns></returns>
        //GET api/commands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllCommands(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(result));
        }  

        //GET api/commands/{id}
        /// <summary>
        /// Get command by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name="GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetCommandById(id, cancellationToken);
            if(result == null) return NotFound();
            return Ok(_mapper.Map<CommandReadDto>(result));
        }

        //POST api/commands/
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto command, CancellationToken cancellationToken)
        {
            var commandModel = _mapper.Map<Command>(command);

            await _repository.CreateCommand(commandModel, cancellationToken);
            await _repository.SaveChanges(cancellationToken);
            
            var commandResult = _mapper.Map<CommandReadDto>(command);
            //CreatedAtRoute returns URI of created value. ie api/commands/{id}
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandResult.Id}, commandResult);
        }
    }
}