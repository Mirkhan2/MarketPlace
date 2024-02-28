﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.DTO.Contacts;

namespace MarketPlace.App.Services.Interfaces
{
	public interface IContactService : IAsyncDisposable
	{
        #region contact us
        Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId);
        #endregion
        #region ticket
        Task<AddTicketResult> AddUserTicket(AddTicketViewModel ticket , long userId );

        #endregion
    }
}