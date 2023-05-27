using System;

public class TransferTransaction
{
    private Account _toAccount;
    private Account _fromAccount;
    private decimal _amount;
    private DepositTransaction _theDeposit;
    private WithdrawTransaction _theWithdraw;

    private bool _executed = false;
    private bool _success = false;
    private bool _reversed = false;

    public bool Executed {get {return _executed;}}
    public bool Success {get {return _success;}}
    public bool Reversed {get {return _reversed;}}

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        _amount = amount;
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _theDeposit = new DepositTransaction(toAccount, amount);
        _theWithdraw = new WithdrawTransaction(fromAccount, amount);
    }

    public void Execute()
    {
        if ( _executed)
        {
            throw new Exception("Cannot executed this transaction as it has executed.");
        }
        _theWithdraw.Execute();
        if (_theWithdraw.Success)
        {
            _theDeposit.Execute();
            if(!_theDeposit.Success)
            {
                _theWithdraw.Rollback();
                _success = false;
            }
        }
        _executed = true;
        _success = true;
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
        if(_theWithdraw.Success)
        {
            _theWithdraw.Rollback();
        }
        if(_theDeposit.Success)
        {
            _theDeposit.Rollback();
        }
        _reversed = true;
    }

    public void Print()
    {
        if (_success)
        {
            Console.WriteLine($"Transferred ${_amount} from Jake’s Account to My Account");
        }
        else
        {
            Console.WriteLine("The transfer transaction was not successful.");
        }
        _fromAccount.Print();
    }

}