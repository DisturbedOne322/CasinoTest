using UnityEngine;

public class Player
{
    private Sprite _avatarSprite;
    public Sprite AvatarSprite => _avatarSprite;

    private string _playerName;
    public string PlayerName => _playerName;

    private bool _inGame = true;
    public bool InGame => _inGame;

    private PlayerBalance _balance;
    public int Balance => _balance.GetBalance();

    public Player(string name, int startBalance, Sprite avatar)
    {
        _playerName = name;

        _balance = new ();
        _balance.SetBalance(startBalance);

        _avatarSprite = avatar;
    }

    public void RemoveAmount(int amount)
    {
        _balance.RemoveAmount(amount);

        if(_balance.GetBalance() <= 0)
            _inGame = false;
    }

    public void AddAmount(int amount) => _balance.AddAmount(amount);
    public bool CanPlaceBet(int amount) => Balance >= amount;

    private class PlayerBalance
    {
        private int _currentBalance = 0;

        public void SetBalance(int balance) => _currentBalance = balance;
        public void AddAmount(int amount) => _currentBalance = amount;
        public void RemoveAmount(int amount) => _currentBalance -= amount;
        public int GetBalance() => _currentBalance;
    }
}
