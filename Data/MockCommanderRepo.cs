using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo: ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command
                {
                    Id = 0,
                    HowTo = "Boil an egg",
                    Line = "Boil Water",
                    Platform = "Kettle & Pan"
                },
                new Command
                {
                    Id = 1,
                    HowTo = "Cut bread",
                    Line = "get a knife",
                    Platform = "knife and chopping board"
                },
                new Command
                {
                    Id = 2,
                    HowTo = "Make tea",
                    Line = "place tea in cup",
                    Platform = "Kettle & Cup"
                }
            };

            return commands;
        }

        public Command GetCommandById(int Id)
        {
            
            return new Command
            {
                Id = 0,
                HowTo = "Boil an egg",
                Line = "Boil Water",
                Platform = "Kettle & Pan"
            };
        } 

    }
}