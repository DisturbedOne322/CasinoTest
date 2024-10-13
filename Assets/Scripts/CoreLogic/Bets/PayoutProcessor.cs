using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayoutProcessor
{
    public event Action<string> OnBetProcessed;
    public event Action OnProcessingFinished;

    private const float MESSAGE_DISPLAY_TIME_TOTAL = 3f;

    public IEnumerator ProcessPayouts(Dictionary<Player, List<Bet>> finalBets, int winningNumber)
    {
        (int totalBetOnWinningNumber, int totalBetsLost) = CalculateWinAndLostBetsSum(finalBets, winningNumber);


        yield return ProcessAllPlayersPayouts(finalBets, totalBetOnWinningNumber, totalBetsLost, winningNumber);
        

        if(totalBetOnWinningNumber == 0)
        {
            string message = "- Casino won in total " + totalBetsLost;
            OnBetProcessed?.Invoke(message);
            yield return GameManager.Instance.StartCoroutine(RunTimer());
        }

        OnProcessingFinished?.Invoke();
    }


    private IEnumerator ProcessAllPlayersPayouts(Dictionary<Player, List<Bet>> finalBets, int totalBetOnWinningNumber, int totalBetsLost, int winningNumber)
    {
        foreach (var kv in finalBets)
        {
            OnBetProcessed?.Invoke(ProcessPlayerBets(kv.Key, kv.Value, totalBetOnWinningNumber, totalBetsLost, winningNumber));
            yield return GameManager.Instance.StartCoroutine(RunTimer());
        }
    }

    private string ProcessPlayerBets(Player player, List<Bet> bets, int totalBetOnWinningNumber, int totalBetsLost, int winningNumber)
    {
        string message = $"- {player.PlayerName} bet on numbers: ";
        foreach (var bet in bets)
        {
            if (bet.Number == winningNumber)
            {
                float share = CalculateSharePercent(bet.Amount, totalBetOnWinningNumber);
                var moneyWon = CalculatePlayerPayout(bet.Amount, share, totalBetsLost);
                player.AddAmount(moneyWon);
                return $"- {player.PlayerName} bet {bet.Amount} on {bet.Number} and WON {moneyWon}! \n" +
                    $"({String.Format("{0:0.00}", share)} multiplier from the {totalBetsLost} winnings + initial bet of {bet.Amount})";
            }
            else
                message += $"{bet.Number} ";
        }

        return message + "and LOST!";
    }

    private IEnumerator RunTimer()
    {
        float timeLeft = MESSAGE_DISPLAY_TIME_TOTAL;
        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
    }

    private (int,int) CalculateWinAndLostBetsSum(Dictionary<Player, List<Bet>> finalBets, int winningNumber)
    {
        int totalWin = 0;
        int totalLost = 0;
        foreach (var kv in finalBets)
        {
            foreach (var bet in kv.Value)
            {
                if (bet.Number == winningNumber)
                    totalWin += bet.Amount;
                else
                    totalLost += bet.Amount;
            }
        }
        return (totalWin, totalLost);
    }

    private float CalculateSharePercent(int playerBet, int totalBet) => playerBet * 1f / totalBet;
    private int CalculatePlayerPayout(int playerBet, float share, int totalBetsLost) => Mathf.FloorToInt(playerBet + share * totalBetsLost);
}
