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

            CreateMap<CommandWriteDto, Command>();
            CreateMap<Command, CommandWriteDto>();

            CreateMap<CommandReadDto, CommandWriteDto>();
            CreateMap<CommandWriteDto, CommandReadDto>();
        }
    }
}