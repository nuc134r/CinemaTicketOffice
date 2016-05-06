using System.Collections.Generic;
using System.Data;
using DataAccess.Connection;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<User> GetUsers()
        {
            var executor = new CommandExecutor("dbo.BrowseUsers", connectionString);
            var result = executor.ExecuteCommand();

            result.ThrowIfException();

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                var type = UserType.User;

                switch (row["Role"].ToInt())
                {
                    case 1:
                        type = UserType.User;
                        break;
                    case 2:
                        type = UserType.Admin;
                        break;
                    case 3:
                        type = UserType.Superadmin;
                        break;
                }

                yield return new User
                {
                    Login = row["User"].ToString(),
                    Type =  type
                };
            }
        }

        public void Delete(User user)
        {
            var executor = new CommandExecutor("dbo.DeleteUser", connectionString);

            executor.SetParam("@Username", user.Login, SqlDbType.NVarChar);

            var result = executor.ExecuteCommand();
            result.ThrowIfException();
        }

        public void Create(User user)
        {
            var executor = new CommandExecutor("dbo.CreateUser", connectionString);

            executor.SetParam("@Username", user.Login, SqlDbType.NVarChar);
            executor.SetParam("@Password", user.Password, SqlDbType.NVarChar);

            var userType = (int) user.Type + 1;
            executor.SetParam("@Usertype", userType, SqlDbType.Int);

            var result = executor.ExecuteCommand();
            result.ThrowIfException();
        }
    }
}