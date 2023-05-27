using System;
using SplashKitSDK;

namespace code
{
    public enum MenuOption
    {
        NewAccount,
        Withdraw,
        Deposit,
        Transfer,
        Print,
        Quit
    }
    public class Program
    {

        public static void Main()
        {
            Bank bank = new Bank();
            Account account = new Account("Shelby Account", 200000);
            MenuOption option = ReadUserOption();
            switch (option)
            {
                case MenuOption.NewAccount:
                    DoNewAccount(bank);
                    break;
                case MenuOption.Withdraw:
                    DoWithdraw(bank);
                    break;
                case MenuOption.Deposit:
                    DoDeposit(bank);
                    break;
                case MenuOption.Transfer:
                    DoTransfer(bank);
                    break;
                case MenuOption.Print:
                    DoPrint(bank);
                    break;
                case MenuOption.Quit:
                    Console.WriteLine("Quit");
                    break;
            }
        }


        private static MenuOption ReadUserOption()
        {
            int option;
            Console.WriteLine("Please choose option [1-5]: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("1: Create a new account");
            Console.WriteLine("2: Withdraw Money from your account");
            Console.WriteLine("3: Make a deposit to your account");
            Console.WriteLine("4: Make a transfer between accounts");
            Console.WriteLine("5: Print your account balance");
            Console.WriteLine("6: Quit");
            Console.WriteLine("-----------------------------------");
            do
            {
                option = Convert.ToInt32(Console.ReadLine());
                if (option < 1 || option > 6)
                {
                    Console.WriteLine("Please select a valid number between 1 and 5");
                }
            } while (option < 1 || option > 6);
            return (MenuOption)(option - 1);

        }
        private static void DoNewAccount(Bank bank)
        {
            Console.Write("Enter account name: ");
            string name = Console.ReadLine();
            Console.Write("Enter starting balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());
            Account newAccount = new Account(name, balance);
            bank.AddAccount(newAccount);
            Console.WriteLine($"New account {name} created with balance ${balance}");
        }

        private static Account FindAccount(Bank fromBank)
        {
            Console.Write("Enter account name: ");
            string name = Console.ReadLine();
            Account result = fromBank.GetAccount(name);
            if (result == null)
            {
                Console.WriteLine($"No account found with name {name}");
            }
            return result;
        }

        private static void DoDeposit(Bank toBank)
        {
            Account account = FindAccount(toBank);
            if (account == null) return;

            Console.WriteLine("How much do you want to deposit: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction deposit = new DepositTransaction(account, amount);
                toBank.ExecuteTransaction(deposit);
                deposit.Print();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private static void DoWithdraw(Bank fromBank)
        {
            Account account = FindAccount(fromBank);
            if (account == null) return;
            Console.WriteLine("How much do you want to withdraw: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                WithdrawTransaction withdraw = new WithdrawTransaction(account, amount);
                fromBank.ExecuteTransaction(withdraw);
                withdraw.Print();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        private static void DoTransfer(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account == null) return;
            Console.WriteLine("How much do you want to transfer: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                TransferTransaction transfer = new TransferTransaction(account, account, amount);
                bank.ExecuteTransaction(transfer);
                transfer.Print();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private static void DoPrint(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account == null) return;
            account.Print();
        }
    }
}


