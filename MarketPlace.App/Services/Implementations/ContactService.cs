using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Contacts;
using MarketPlace.Data.Entities.Contacts;
using MarketPlace.Data.Repository;

namespace MarketPlace.App.Services.Implementations
{
    public class ContactService : IContactService
    {
        #region constructor

        private readonly IGenericRepository<ContactUs> _contactUsRepository;
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly IGenericRepository<TicketMessage> _ticketMessageRepository;    
        public ContactService(IGenericRepository<ContactUs> contactUsRepository, IGenericRepository<Ticket> ticketRepository , IGenericRepository<TicketMessage> ticketmessageRepository)
        {
            _contactUsRepository = contactUsRepository;

        }



        #endregion
        #region contact us
        public async Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId)
        {
            var newContact = new ContactUs
            {
                UserId = userId != null && userId.Value != 0 ? userId.Value : (long?)null,
                Subject = contact.Subject,
                Email = contact.Email,
                UserIp = userIp,
                Text = contact.Text,
                FullName = contact.FullName
            };
            await _contactUsRepository.AddEntity(newContact);
            await _contactUsRepository.SaveChanges();
        }

        #endregion
        #region ticket

        public async Task<AddTicketResult> AddUserTicket(AddTicketViewModel ticket, long userId)
        {
            if (string.IsNullOrEmpty(ticket.Text)) return AddTicketResult.Error;
            //add icket
            var newTicket = new Ticket
                   {
                OwnerId = userId,
                    IsReadByOwner = true,
                    TicketPriority = ticket.TicketPriority,
                    Title = ticket.Title,
                    TicketSection = ticket.TicketSection,
                    TicketState = TicketState.UnderProgress
            };
            await _ticketMessageRepository.AddEntity(newTicket);
            await _ticketMessageRepository.SaveChanges();
            //add ticket message

            var newMessage = new TicketMessage
            {
                TicketId = newTicket.Id,
                Text = ticket.Text,
                SenderId = userId
            };
            await _ticketMessageRepository.AddEntity(newMessage);
            await _ticketMessageRepository.SaveChanges();


            return AddTicketResult.Success;
        }


        #endregion
        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _contactUsRepository.DisposeAsync();
        }
        #endregion
    }
}
