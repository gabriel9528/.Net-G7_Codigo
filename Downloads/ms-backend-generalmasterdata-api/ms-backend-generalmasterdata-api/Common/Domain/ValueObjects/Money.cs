using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        private Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, Currency currency)
        {
            return new Money(amount, currency);
        }

        public static Money Dollars(decimal amount)
        {
            return new Money(amount, Currency.USD);
        }

        public static Money Soles(decimal amount)
        {
            return new Money(amount, Currency.PEN);
        }

        public static Money Euros(decimal amount)
        {
            return new Money(amount, Currency.EUR);
        }

        public Money Add(Money money)
        {
            decimal total = Amount + money.Amount;
            return new Money(total, money.Currency);
        }

        public Money Subtract(Money money)
        {
            decimal total = Amount - money.Amount;
            return new Money(total, money.Currency);
        }

        public bool GreaterOrEqualThan(decimal amount)
        {
            return Amount >= amount;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
