using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Contacts;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class TicketController : UserBaseController
    {
        #region constructor
        private readonly IContactService _contactService;
        public TicketController(IContactService contactService)
        {
            _contactService = contactService;
        }

        #endregion

        #region list
        [HttpGet("tickets")]
        public async Task<IActionResult> Index(FilterTicketDTO filter)
        {
            filter.UserId = User.GetUserId();
            filter.FilterTicketState = FilterTicketState.NotDeleted;
            filter.OrderBy = FilterTicketOder.CreateDate_DES;

            return View(await _contactService.FilterTickets(filter));
        }

        #endregion

        #region add ticket
        [HttpGet("add-ticket")]
        public async Task<IActionResult> addTicket()
        {
            return View();
        }
        [HttpPost("add-ticket"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTicket(AddTicketDTO ticket)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.AddUserTicket(ticket, User.GetUserId());
                switch (result)
                {
                    case AddTicketResult.Error:
                        TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                        break;
                    case AddTicketResult.Success:
                        TempData[SuccessMessage] = "تیکت شما با موفقیت ثبت شد";
                        TempData[InfoMessage] = "پاسخ شما به زودی ارسال خواهد شد";
                        return RedirectToAction("Index");

                }
            }
            return View(ticket);
        }
        #endregion

        #region show ticket detail
        [HttpGet("tickets/{ticketId}")]
         public async Task<IActionResult> TicketDetail(long ticketId)
        {
            var ticket = await _contactService.GetTicketForShow(ticketId , User.GetUserId());
            if (ticket == null) return NotFound();
    
            return View();
        }
        #endregion

    }
}
