public class Player
{
    private string _playerName;
    public string PlayerName => _playerName;

    private bool _inGame = true;
    public bool InGame => _inGame;

    private PlayerBalance _balance;

    public Player(string name, int startBalance)
    {
        _playerName = name;

        _balance = new ();
        _balance.SetBalance(startBalance);
    }

    public void RemoveAmount(int amount)
    {
        _balance.RemoveAmount(amount);

        if(_balance.GetBalance() <= 0)
            _inGame = false;
    }

    public void AddAmount(int amount) => _balance.AddAmount(amount);
    public bool CanPlaceBet(int amount) => _balance.GetBalance() >= amount;

    private class PlayerBalance
    {
        private int _currentBalance = 0;

        public void SetBalance(int balance) => _currentBalance = balance;
        public void AddAmount(int amount) => _currentBalance = amount;
        public void RemoveAmount(int amount) => _currentBalance -= amount;
        public int GetBalance() => _currentBalance;
    }
}
