using System;

namespace SportRental.Entities
{
    public class Client
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Phone { get; private set; }

        private Client(int id, string fullName, string phone)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
        }

        public static Client Create(int id, string fullName, string phone)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Идентификатор не может быть отрицательным.");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО клиента не может быть пустым.", nameof(fullName));
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Телефон не может быть пустым.", nameof(phone));

            return new Client(id, fullName.Trim(), phone.Trim());
        }

        public override string ToString() => FullName;
    }
}
