namespace MyPoli.BusinessLogic.Implementation.Account
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AreCredentialsInvalid { get; set; }
    }
}
