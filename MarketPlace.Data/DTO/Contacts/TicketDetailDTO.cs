using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.Entities.Account;
using MarketPlace.Data.Entities.Contacts;

namespace MarketPlace.Data.DTO.Contacts
{
    public class TicketDetailDTO
    {
        public Ticket Ticket { get; set; }

        public List<TicketMessage> TicketMessages   { get; set; }
    }
}
