using System.Collections.Generic;
using MarketPlace.Data.Entities.Contacts;

namespace MarketPlace.Data.DTO.Contacts
{
    public class TicketDetailDTO
    {
        public Ticket Ticket { get; set; }

        public List<TicketMessage> TicketMessages { get; set; }
    }
}
