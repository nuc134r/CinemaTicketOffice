namespace DataAccess.Model
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public User Clone()
        {
            return new User
            {
                Login = Login,
                Password = Password,
                Type = Type
            };
        }
    }
}