﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Applicationn.Services.Interfaces;
using MarketPlace.DataLayerr.DTO.Contacts;
using MarketPlace.DataLayerr.Entities.Contacts;
using MarketPlace.DataLayerr.Repository;

namespace MarketPlace.Applicationn.Services.Implementations
{
	public class ContactService : IContactService
	{
		#region constructor

		private readonly IGenericRepository<ContactUs> _contactUsRepository;
        public ContactService(IGenericRepository<ContactUs> contactUsRepository)
        {
			_contactUsRepository = contactUsRepository;
            
        }

        #endregion
        #region contact us
        public async Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId)
		{
			var newContact = new ContactUs
			{
				UserId = userId != null && userId.Value != 0 ? userId.Value : (long?) null ,
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

		#region dispose
		public async ValueTask DisposeAsync()
		{
			await _contactUsRepository.DisposeAsync();
		}
		#endregion
	}
}
