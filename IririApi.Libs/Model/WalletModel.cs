using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class WalletModel
    {
    }

    public class Wallet { 
        
        [Key]
        public Guid WalletId { get; set; }

        public long MemberId { get; set; }

        public string Amount { get; set; }

        public string WalletHistory { get; set; }


    }


    public class WalletHistory
    {
        [Key]
        public Guid Id { get; set; }
        public long WalletId { get; set; }

        public string TransactionType { get; set; }

        public string Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
