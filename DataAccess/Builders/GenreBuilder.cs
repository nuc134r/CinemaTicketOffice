using DataAccess.Model;

namespace DataAccess.Builders
{
    public class GenreBuilder
    {
        private int id;
        private string name;

        public static GenreBuilder Create()
        {
            return new GenreBuilder();
        }

        public GenreBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public GenreBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public Genre Please()
        {
            return new Genre(id, name);
        }
    }
}