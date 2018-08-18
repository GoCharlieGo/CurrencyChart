using System;
using System.Collections.Generic;
namespace CurrencyChart.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int SallerId { get; set; }
        public int BuyerId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Member Member { get; set; }
    }
}