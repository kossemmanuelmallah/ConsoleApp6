using System;

namespace SportRental.Entities
{
    public class Rental
    {
        public int Id { get; private set; }
        public int ClientId { get; private set; }
        public int TariffId { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public decimal TotalAmount { get; private set; }

        private Rental(int id, int clientId, int tariffId, DateTime issueDate,
                       DateTime? returnDate, decimal totalAmount)
        {
            Id = id;
            ClientId = clientId;
            TariffId = tariffId;
            IssueDate = issueDate;
            ReturnDate = returnDate;
            TotalAmount = totalAmount;
        }

        public static Rental Create(int id, int clientId, int tariffId, DateTime issueDate,
                                    DateTime? returnDate, decimal totalAmount)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Идентификатор не может быть отрицательным.");
            if (clientId <= 0)
                throw new ArgumentOutOfRangeException(nameof(clientId), "Выберите клиента.");
            if (tariffId <= 0)
                throw new ArgumentOutOfRangeException(nameof(tariffId), "Выберите тариф.");
            if (returnDate.HasValue && returnDate.Value < issueDate)
                throw new ArgumentException("Дата возврата не может быть раньше даты выдачи.", nameof(returnDate));
            if (totalAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalAmount), "Сумма не может быть отрицательной.");

            return new Rental(id, clientId, tariffId, issueDate, returnDate, totalAmount);
        }
    }
}