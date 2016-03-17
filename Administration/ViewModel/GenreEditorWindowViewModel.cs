using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class GenreEditorWindowViewModel
    {
        private readonly bool createMode;
        private readonly MovieRepository repository;

        public GenreEditorWindowViewModel(Genre genre, MovieRepository repository)
        {
            if (genre == null)
            {
                createMode = true;
                genre = new Genre();
            }

            this.repository = repository;
            Genre = genre;
        }

        public Genre Genre { get; set; }

        public void Save()
        {
            repository.Save(Genre, !createMode);
        }
    }
}