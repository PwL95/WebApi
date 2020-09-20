using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile: Profile
    {
        public CommandsProfile()
        {
            //Source -> Target

            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandReadDto, Command>();

            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandReadDto, CommandCreateDto>();
            CreateMap<CommandCreateDto, CommandReadDto>();
        }
    }
}