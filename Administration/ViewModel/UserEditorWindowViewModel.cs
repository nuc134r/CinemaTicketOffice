using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class UserEditorWindowViewModel
    {
        private readonly UserRepository repository;
        private readonly Editors.UserEditorWindow view;

        public UserEditorWindowViewModel(Editors.UserEditorWindow view, User user, UserRepository repository)
        {
            if (user == null)
            {
                CreateMode = true;
                user = new User();
            }

            this.view = view;
            this.repository = repository;

            User = user;

            if (CreateMode) return;

            view.SetPasswordDots();
            view.Set(User.Type);
        }

        public bool CreateMode { get; set; }

        public bool NotCreateMode
        {
            get { return !CreateMode; }
        }

        public User User { get; set; }

        public void Save()
        {
            User.Password = view.GetPassword();
            User.Type = view.GetUserType();

            repository.Create(User);
        }
    }
}