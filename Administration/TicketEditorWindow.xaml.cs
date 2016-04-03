using DataAccess.Model;

namespace Administration
{
    public partial class TicketEditorWindow
    {
        private Ticket ticket;

        public TicketEditorWindow(Ticket ticket)
        {
            InitializeComponent();

            this.ticket = ticket;
        }
    }
}