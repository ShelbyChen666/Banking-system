using System;

public class DepositTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed = false;
    private bool _success = false;
    private bool _reversed = false;

    public bool Executed {get {return _executed;}}
    public bool Success {get {return _success;}}
    public bool Reversed {get {return _reversed;}}

    public DepositTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        if ( _executed)
        {
            throw new Exception("Cannot executed this transaction as it has executed.");
        }
        _executed = true;
        _success = _account.Deposit(_amount);
    }

    public void Rollback()
    {
        if(!_executed)
        {
            throw new Exception("This transaction has not been executed.");
        }
        if(_reversed)
        {
            throw new Exception("This transaction has been reversed.");
        }
        _account.Withdraw(_amount);
        _reversed = true;
    }

    public void Print()
    {
        if (_success)
        {
            if(_reversed)
            {
                Console.WriteLine($"{_amount} was successfully deposited to the account, but it was reversed.");
            } else{
                Console.WriteLine($"{_amount} was successfully deposited to the account, and it was not reversed.");
            }
            
        }
        else
        {
            Console.WriteLine("The deposit transaction was not successful.");
        }
        _account.Print();
    }

}