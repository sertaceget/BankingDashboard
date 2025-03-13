using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingDashboard.Application.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int Type { get; set; }
        public Guid AccountId { get; set; }
        public Guid? TargetAccountId { get; set; }
    }
}
