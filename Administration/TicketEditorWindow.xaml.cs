using DataAccess.Model;

namespace Administration
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