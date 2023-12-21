using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LING_A3
{
    internal class Program
    {
        /* Student Name: LING LIN
         * Student ID: 301292283
         */

        static void Main(string[] args)
        {
            TestAccounts();
        }

        static void TestAccounts()
        {
            Console.WriteLine($"-------------------------------Banking Application----------------------------------------");
            Console.WriteLine($"------------------------------All account information-------------------------------------");
            Console.WriteLine($"Consumer ID       Name            Account Number         Type           Balance           ");
            Console.WriteLine($"------------------------------------------------------------------------------------------");

            Bank.AccountList.Add(new SavingsAccount("S647", "Alex Du", 222290192, 4783.98));
            Bank.AccountList.Add(new ChequingAccount("C576", "Dale Stayne", 333312312, 12894.56));

            Bank.ShowAll();

            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine("Trying to withdraw $500.00 from the following account");
            Console.WriteLine(Bank.AccountList[0].ToString());
            Bank.AccountList[0].Withdraw(500.00);
            Console.WriteLine($"{new string('-', 90)}");

            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine("Trying to deposit $-250.00 to the following account");
            Console.WriteLine(Bank.AccountList[1].ToString());
            Bank.AccountList[1].Deposit(-250.00);
            Console.WriteLine($"{new string('-', 90)}");

            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine("Trying to withdraw $10000.00 to the following account");
            Console.WriteLine(Bank.AccountList[2].ToString());
            Bank.AccountList[2].Withdraw(10000.00);
            Console.WriteLine($"{new string('-', 90)}");

            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine("Trying to deposit $5000.00 to the following account");
            Console.WriteLine(Bank.AccountList[2].ToString());
            Bank.AccountList[2].Deposit(5000.00);
            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine("Trying to deposit $7300.00 to the following account");
            Console.WriteLine(Bank.AccountList[3].ToString());
            Bank.AccountList[3].Deposit(7300.90);
            Console.WriteLine($"{new string('-', 90)}");

            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine("Trying to withdraw $45000.40 from the following account");
            Console.WriteLine(Bank.AccountList[4].ToString());
            Bank.AccountList[4].Withdraw(45000.40);
            Console.WriteLine($"{new string('-', 90)}");

            Console.WriteLine($"{new string('-', 90)}");
            Console.WriteLine("Trying to withdraw $37000.00 from the following account");
            Console.WriteLine(Bank.AccountList[5].ToString());
            Bank.AccountList[5].Withdraw(37000);
            Console.WriteLine($"{new string('-', 90)}");

            Console.WriteLine($"{new string('-', 90)}");
            Bank.ShowAll(67676767);
            Console.WriteLine($"{new string('-', 90)}");

            //Bank.ShowAll();

            Console.WriteLine($"-------------------------------Banking Application----------------------------------------");
            Console.WriteLine($"------------------------------All account information-------------------------------------");
            Console.WriteLine($"Consumer ID       Name            Account Number         Type           Balance           ");
            Console.WriteLine($"------------------------------------------------------------------------------------------");

            Bank.AccountList.Add(new SavingsAccount("S647", "Alex Du", 222290192, 4783.98));
            Bank.AccountList.Add(new ChequingAccount("C576", "Dale Stayne", 333312312, 12894.56));

            Bank.ShowAll();
        }
    }



    public class Consumer
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Consumer(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }

    }

    public abstract class Account : Consumer
    {
        public int AccountNum { get; }
        public double Balance { get; set; } = 0;

        //Constructor : Create parameterized constructor to
        //accept necessary parameters for field variables of 
        //account class and the parameters of the super class.
        public Account(int accountNum,
        string id,
        string name) : base(id, name)
        {
            AccountNum = accountNum;
        }


        public abstract void Withdraw(double amount);
        public abstract void Deposit(double amount);

        public override string ToString()
        {
            return $"{Id} {Name} {AccountNum}";
        }
    }

    public class InsufficientBalanceException : Exception
    {
        public override string Message => "Account not having enough balance to withdraw.";
    }
    public class MinimumBalanceException : Exception
    {
        public override string Message => "You must maintain minimum $3000 balance in Saving account.";
    }
    public class IncorrectAmountException : Exception
    {
        public override string Message => "You must provide positive number for amount to be deposited.";
    }
    public class OverdraftLimitException : Exception
    {
        public override string Message => "Overdraft Limit exceeded. You can only use $2000 from overdraft.";
    }
    public class AccountNotFoundException : Exception
    {
        public override string Message => "Account with given number does not exist.";
    }


    class SavingsAccount : Account
    {
        //public double Balance { get; set; }

        public SavingsAccount(string id,
            string name,
            int accountNum,
            double balance = 0.0) : base(accountNum, id, name)
        {
            this.Balance = balance;
        }

        
        public override void Withdraw(double amount)
        {
            try
            {
                if (Balance < amount)
                {
                    throw new InsufficientBalanceException();
                }
                else if (Balance - amount < 3000)
                {
                    throw new MinimumBalanceException();
                }
                else
                {
                    Balance -= amount;
                    Console.WriteLine($"Amount withdrawn successfully. Updated balance: {Balance}");
                }
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (MinimumBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public override void Deposit(double amount)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new IncorrectAmountException();
                }
                else
                {
                    Balance += amount;
                    Console.WriteLine($"Successfully deposited {amount} to the account number {AccountNum} \nUpdated balance is {Balance}");
                }
            }
            catch (IncorrectAmountException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override string ToString()
        {
            return $"{this.Id}\t{this.Name,-20}\t{this.AccountNum,-20}\t{"Saving",-20}\t{this.Balance}";
        }
    }
    class ChequingAccount : Account
    {
        //public double Balance { get; set; }
        public ChequingAccount(string id,
            string name,
            int accountNum,
            double balance = 0.0) : base(accountNum, id, name)
        {
            Balance = balance;
        }

        public override void Withdraw(double amount)
        {

            if (amount <= 0)
            {
                throw new IncorrectAmountException();
            }
            if (amount > Balance + 2000)
            {
                throw new OverdraftLimitException();
            }
            Balance -= amount;

            Console.WriteLine($"Successfully withdrawn {amount} from the account number {AccountNum}\nUpdated balance is {Balance:C}");
        }
        public override void Deposit(double amount)
        {
           

            if (amount < 0)
            {
                throw new IncorrectAmountException();
            }

            Balance += amount;
            
            Console.WriteLine($"Successfully deposited {amount} to the account number {AccountNum} \nUpdated balance is {Balance:C}");

        }
        public override string ToString()
        {
            return $"{this.Id}\t{this.Name,-20}\t{this.AccountNum,-20}\t{"Chequing",-20}\t{this.Balance}";
        }

    }


    public class Bank
    {

        public static List<Account> AccountList = new List<Account>();

        static Bank()
        {
            AccountList = new List<Account>();
            AccountList.Add(new SavingsAccount("S101", "James Finch", 222210212, 3400.90));
            AccountList.Add(new SavingsAccount("S102", "Kell Forest", 222214500, 42520.32));
            AccountList.Add(new SavingsAccount("S103", "Amy Stone", 222212000, 8273.45));
            AccountList.Add(new ChequingAccount("C104", "Kaitlin Ross", 333315002, 91230.45));
            AccountList.Add(new ChequingAccount("C105", "Adem First", 333303019, 43987.67));
            AccountList.Add(new ChequingAccount("C106", "John Doe", 333358927, 34829.76));
        }

        public static void ShowAll()
        {
            Console.WriteLine("");
            foreach (var account in AccountList)
            {
                Console.WriteLine(account.ToString());
            }
        }
        public static void ShowAll(int accountNum)
        {
            Console.WriteLine($"{accountNum}");
            Account newAccount = null;
            foreach (var account in AccountList)
            {
                if (account.AccountNum == accountNum)
                {
                    newAccount = account;
                    break;
                }
            }
            if (newAccount == null)
            {
                throw new AccountNotFoundException();
            }
            Console.WriteLine(newAccount.ToString());
        }
    }
}
   
