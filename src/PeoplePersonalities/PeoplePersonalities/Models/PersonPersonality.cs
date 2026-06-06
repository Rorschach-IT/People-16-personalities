using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PeoplePersonalities.Models
{
    [BsonIgnoreExtraElements]
    public class PersonPersonality
    {
        [Required(ErrorMessage = "Pole imię jest wymagane.")]
        [Display(Name = "Imię")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+(-[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+)*$",
            ErrorMessage = "Imię może zawierać tylko litery, polskie znaki i pojedynczy myślnik między członami.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Pole nazwisko jest wymagane.")]
        [Display(Name = "Nazwisko")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+(-[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+)*$",
            ErrorMessage = "Nazwisko może zawierać tylko litery, polskie znaki i pojedynczy myślnik między członami.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Pole typ osobowości jest wymagane.")]
        [Display(Name = "Typ osobowości")]
        [RegularExpression("^(INTJ|INTP|ENTJ|ENTP|INFJ|INFP|ENFJ|ENFP|ISTJ|ISFJ|ESTJ|ESFJ|ISTP|ISFP|ESTP|ESFP)$",
            ErrorMessage = "Podaj poprawny typ osobowości MBTI.")]
        public string Type { get; set; } = null!;
    }
}