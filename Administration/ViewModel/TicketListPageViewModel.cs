using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Administration.Properties;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class TicketListPageViewModel
    {
        private readonly TicketRepository repository;
        private readonly TicketListPage view;

        public TicketListPageViewModel(TicketListPage view, TicketRepository repository)
        {
            this.view = view;
            this.repository = repository;
        }

        public ObservableCollection<Ticket> Tickets { get; set; }

        public void OpenEditor(Ticket ticket)
        {
            try
            {
                if (ticket != null)
                {
                    ticket = ticket.Clone();
                }

                var editor = new TicketEditorWindow(ticket);
                var result = editor.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    RetrieveData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RetrieveData()
        {
            if (Tickets == null)
            {
                Tickets = new ObservableCollection<Ticket>();
                Tickets.CollectionChanged += TicketsOnCollectionChanged;
            }

            var tickets = repository.GetTickets().ToList();

            Tickets.Clear();
            tickets.ForEach(ticket => Tickets.Add(ticket));
        }

        private void TicketsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            view.ListCount = Tickets.Count;
        }

        public void Delete(Ticket ticket)
        {
            var result = MessageBox.Show(
                string.Format(Resources.DeleteTicketConfirmatonText),
                Resources.DeleteConfirmationCaption,
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    repository.Delete(ticket);
                    RetrieveData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}