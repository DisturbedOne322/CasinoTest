using System.Collections.Generic;

public class BetsManager
{
    private Dictionary<Player, List<Bet>> _betsList = new();

    public void StartNewRound() => _betsList.Clear();

    public void PlaceNewBets(Player player, List<Bet> bets) => _betsList.Add(player, bets);

    public Dictionary<Player, List<Bet>> GetFinalBets() => _betsList;
}

public class Bet
{
    private int _number;
    private int Number => _number;

    private int _amount;
    private int Amount => _amount;

    public Bet(int number, int amount)
    {
        _number = number;
        _amount = amount;
    }
}
