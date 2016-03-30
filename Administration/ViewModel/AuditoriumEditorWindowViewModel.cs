using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class AuditoriumEditorWindowViewModel
    {
        private readonly bool createMode;
        private readonly ShowtimeRepository repository;

        public AuditoriumEditorWindowViewModel(Auditorium auditorium, ShowtimeRepository repository)
        {
            if (auditorium == null)
            {
                createMode = true;
                auditorium = new Auditorium();
            }

            Auditorium = auditorium;
            this.repository = repository;
        }

        public Auditorium Auditorium { get; set; }

        public void Save()
        {
            repository.Save(Auditorium, !createMode);
        }
    }
}