using DataAccess.Model;

namespace Administration.Editors
{
    public partial class TicketEditorWindow
    {
        public TicketEditorWindow(Ticket ticket)
        {
            InitializeComponent();

            DataContext = ticket;
        }
    }
}