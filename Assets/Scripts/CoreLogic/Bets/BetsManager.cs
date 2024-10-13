using System.Collections.Generic;

public class BetsManager
{
    private Dictionary<Player, List<Bet>> _betsDict = new();

    public void StartNewRound() => _betsDict.Clear();

    public void PlaceNewBet(Player player, Bet bet)
    {
        if(!_betsDict.ContainsKey(player))
        {
            _betsDict.Add(player, new List<Bet>());
        }

        //if the bet already exists and player wants to increase the amount
        foreach(var b in _betsDict[player])
        {
            if(b.Number == bet.Number)
            {
                b.AddAmount(bet.Amount);
                return;
            }
        }

        _betsDict[player].Add(bet);
    }

    public int FindBetAmountForNumber(Player player, int number)
    {
        if (!_betsDict.ContainsKey(player))
            return 0;

        foreach (var b in _betsDict[player])
        {
            if (b.Number == number)
                return b.Amount;
        }

        return 0;
    }

    public Dictionary<Player, List<Bet>> GetFinalBets() => _betsDict;
}

public class Bet
{
    private int _number;
    public int Number => _number;

    private int _amount;
    public int Amount => _amount;

    public Bet(int number, int amount)
    {
        _number = number;
        _amount = amount;
    }

    public void AddAmount(int amount) => _amount += amount;
}
