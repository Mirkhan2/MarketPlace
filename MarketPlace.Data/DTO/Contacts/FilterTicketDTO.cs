
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarketPlace.Data.Entities.Contacts;

namespace MarketPlace.Data.DTO.Contacts
{
    public class FilterTicketDTO
    {
        #region properties

        public string Title { get; set; }

        public long? UserId { get; set; }
        public FilterTicketState? FilterTicketState { get; set; }
        
        public TicketSection? TicketSection { get; set; }

        public TicketPriority? TicketPriority { get; set; }
        public FilterTicketOder OrderBy { get; set; }
        public List<Ticket> Tickets { get; set; }

        #endregion

    }
    public enum FilterTicketState
    {
        All,
        NotDeleted,
        Deleted
    }
    public enum FilterTicketOder
    {
        CreateDate_DES,
        CreateDate_ASC
    }
}
