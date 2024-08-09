using System.ComponentModel.DataAnnotations;
using MarketPlace.Data.Entities.Account;
using MarketPlace.Data.Entities.Commen;

namespace MarketPlace.Data.Entities.Contacts
{
    public class TicketMessage : BaseEntity
    {
        #region properties

        public long TicketId { get; set; }

        public long SenderId { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }

        #endregion

        #region relations

        public Ticket Ticket { get; set; }

        public User Sender { get; set; }

        #endregion
    }
}
