﻿using System.Collections.Generic;
namespace CurrencyChart.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}