namespace Cooking.REST.Models.Input
{
    public class UserInput
    {
        public UserInput(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
