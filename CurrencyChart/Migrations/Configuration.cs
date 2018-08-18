namespace CurrencyChart.Migrations
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CurrencyChart.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CurrencyChart.DAL.CurrencyChartContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CurrencyChart.DAL.CurrencyChartContext context)
        {
            var currencies = new List<Currency>
            {
            new Currency{Name="BYN"},
            new Currency{Name="RUB"},
            new Currency{Name="USD"},
            new Currency{Name="EUR"}
            };
            currencies.ForEach(s => context.Currencies.AddOrUpdate(n => n.Name, s));
            context.SaveChanges();

            var members = new List<Member>
            {
            new Member{Name="MemberOne"},
            new Member{Name="MemberTwo"},
            new Member{Name="MemberThree"},
            };
            members.ForEach(s => context.Members.AddOrUpdate(n => n.Name, s));
            context.SaveChanges();

            var transactions = new List<Transaction>
            {
            new Transaction{
                CurrencyId = currencies.Single(c => c.Name == "USD").Id,
                SallerId = members.Single(m1 => m1.Name == "MemberOne").Id,
                BuyerId = members.Single(m2 => m2.Name == "MemberTwo").Id,
                Price =100, Count=2, Date=DateTime.Now },
            new Transaction{
                CurrencyId = currencies.Single(c => c.Name == "EUR").Id,
                SallerId = members.Single(m1 => m1.Name == "MemberTwo").Id,
                BuyerId = members.Single(m2 => m2.Name == "MemberThree").Id,
                Price =50, Count=3, Date=DateTime.Now },
            new Transaction{
                CurrencyId = currencies.Single(c => c.Name == "BYN").Id,
                SallerId = members.Single(m1 => m1.Name == "MemberThree").Id,
                BuyerId = members.Single(m2 => m2.Name == "MemberOne").Id,
                Price =200, Count=1, Date=DateTime.Now },
            };
            foreach (var t in transactions)
            {
                var transactionDb = context.Transactions.SingleOrDefault(c => c.Currency.Id == t.CurrencyId &&
                         c.Buyer.Id == t.BuyerId &&
                         c.Saller.Id == t.SallerId);
                if (transactionDb == null)
                {
                    context.Transactions.Add(t);
                }
            }
            context.SaveChanges();
        }
    }
}
