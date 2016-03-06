using DataAccess.Model;

namespace Administration.Interfaces
{
    public interface IMovieEditorWindow
    {
        int SelectedAgeLimitIndex { set; }
        AgeLimit AgeLimit { get; }
    }
}