using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.DTO.Contacts;

namespace MarketPlace.Applicationn.Services.Interfaces
{
	public interface IContactService : IAsyncDisposable
	{
		Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId);
	}
}
