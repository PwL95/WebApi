using System.Data;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Dtos;
using Microsoft.AspNetCore.JsonPatch;

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
            var result = await _repository.GetAllCommands(cancellationToken).ConfigureAwait(false);
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
            var result = await _repository.GetCommandById(id, cancellationToken).ConfigureAwait(false);
            if(result == null) return NotFound();
            return Ok(_mapper.Map<CommandReadDto>(result));
        }

        //POST api/commands/
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandWriteDto command, CancellationToken cancellationToken)
        {
            var commandModel = _mapper.Map<Command>(command);

            await _repository.CreateCommand(commandModel, cancellationToken).ConfigureAwait(false);
            await _repository.SaveChanges(cancellationToken).ConfigureAwait(false);
            
            var commandResult = _mapper.Map<CommandReadDto>(command);
            //CreatedAtRoute returns URI of created value. ie api/commands/{id}
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandResult.Id}, commandResult);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandWriteDto commandDto, CancellationToken cancellationToken)
        {
            var command = await _repository.GetCommandById(id, cancellationToken).ConfigureAwait(false);
            if(command == null) return NotFound();
            _mapper.Map(commandDto, command);

            _repository.UpdateCommand(command);
            await _repository.SaveChanges(cancellationToken);

            return NoContent();
        }


        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateCommand(int id, [FromBody]JsonPatchDocument<CommandWriteDto> commandDoc, CancellationToken cancellationToken)
        {
            var command = await _repository.GetCommandById(id, cancellationToken).ConfigureAwait(false);
            if(command == null) return NotFound();

            var commandToPatch = _mapper.Map<CommandWriteDto>(command);
            ///ModelState does patch validiations
            commandDoc.ApplyTo(commandToPatch, ModelState);
            if(!TryValidateModel(commandToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, command);
            _repository.UpdateCommand(command);
            await _repository.SaveChanges(cancellationToken).ConfigureAwait(false);
            
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteCommand(id, cancellationToken).ConfigureAwait(false);
            await _repository.SaveChanges(cancellationToken);
            return NoContent();
        }
    }
}