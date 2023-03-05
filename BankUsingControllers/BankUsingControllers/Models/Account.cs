namespace BankUsingControllers.Models
{
    public class Account
    {
        public int Number { get; }
        public string HolderName { get; set; }
        public double Balance { get; set; }

        public Account(int number, string holderName, double balance)
        {
            Number = number;
            HolderName = holderName;
            Balance = balance;
        }

    }
}
