namespace DataAccess
{
    public static class ConnectionStringBuilder
    {
        private const string NotTrustedTemplate = "Server={0};Database={1};User Id={2};Password={3};";
        private const string TrustedTemplate = "Server={0};Database={1};Trusted_Connection=True;";

        public static string Build(string server, string database, string user, string password)
        {
            return string.Format(NotTrustedTemplate, server, database, user, password);
        }

        public static string Build(string server, string database)
        {
            return string.Format(TrustedTemplate, server, database);
        }
    }
}