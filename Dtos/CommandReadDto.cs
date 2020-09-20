namespace Commander.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }

        public string HowTo { get; set; }
        
        public string Line { get; set; }

        //Remove field to not show for Client
        // public string Platform { get; set; }
    }
}