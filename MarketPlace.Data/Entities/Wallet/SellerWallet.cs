using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.Entities.Commen;
using MarketPlace.Data.Entities.Store;

namespace MarketPlace.Data.Entities.Wallet
{
    public class SellerWallet : BaseEntity
    {
        #region properties
        public long SellerId { get; set; }
        public int Price { get; set; }
        public TransactionType TransactionType { get; set; }


        #endregion

        #region relations
        public Seller Seller { get; set; }
        public ICollection<SellerWallet> SellerWallets { get; set; }

        #endregion
    }
    public enum TransactionType
    {
        Deposit,
        Withrawal
    }
}
