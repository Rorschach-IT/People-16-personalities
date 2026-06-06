namespace PeoplePersonalities.Models
{
    public class HomeIndexViewModel
    {
        public List<PersonPersonality> Items { get; set; } = new();
        public List<string> Types { get; set; } = new();
        public string? SelectedType { get; set; }
    }
}