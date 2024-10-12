using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayoutProcessor
{
    public event Action<string> OnBetProcessed;
    public event Action OnProcessingFinished;

    private const float MESSAGE_DISPLAY_TIME_TOTAL = 3f;

    public IEnumerator ProcessPayouts(int winningNumber, Dictionary<Player, List<Bet>> finalBets, float payoutMultiplier)
    {
        int totalBetOnWinningNumber = CalculateTotalBetsOnWinningNumber(finalBets, winningNumber);

        foreach (var kv in finalBets)
        {
            string message = "";

            Player player = kv.Key;
            foreach (var bet in kv.Value)
            {
                if (bet.Number == winningNumber)
                {
                    var moneyWon = CalculatePlayerPayout(bet.Amount, totalBetOnWinningNumber, payoutMultiplier);
                    player.AddAmount(moneyWon);
                    message += $"{player.PlayerName} bet {bet.Amount} on {bet.Number} and WON {moneyWon}!\n";
                }
                else
                    message += $"{player.PlayerName} bet {bet.Amount} on {bet.Number} and LOST!\n";
            }

            OnBetProcessed?.Invoke(message);
            yield return GameManager.Instance.StartCoroutine(RunTimer());
        }

        ProcessCasinoPayout(finalBets, winningNumber);
        yield return GameManager.Instance.StartCoroutine(RunTimer());

        OnProcessingFinished?.Invoke();
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

    // General method that uses a condition delegate
    private int CalculateTotalBets(Dictionary<Player, List<Bet>> finalBets, Func<Bet, bool> condition)
    {
        int total = 0;
        foreach (var kv in finalBets)
        {
            foreach (var bet in kv.Value)
            {
                if (condition(bet))
                    total += bet.Amount;
            }
        }
        return total;
    }

    private int CalculateTotalBetsOnWinningNumber(Dictionary<Player, List<Bet>> finalBets, int winningNumber)
    {
        return CalculateTotalBets(finalBets, bet => bet.Number == winningNumber);
    }

    private int CalculateTotalBetsCasinoBanked(Dictionary<Player, List<Bet>> finalBets, int winningNumber)
    {
        return CalculateTotalBets(finalBets, bet => bet.Number != winningNumber);
    }

    private int CalculatePlayerPayout(int bet, int totalBet, float payoutMultiplier)
    {
        float share = bet * 1f / totalBet;
        return Mathf.FloorToInt(share * totalBet * payoutMultiplier);
    }

    private void ProcessCasinoPayout(Dictionary<Player, List<Bet>> finalBets, int winningNumber)
    {
        int totalCasinoBank = CalculateTotalBetsCasinoBanked(finalBets, winningNumber);

        if (totalCasinoBank == 0)
            return;

        string message = "Casino won in total " + totalCasinoBank;
        OnBetProcessed?.Invoke(message);
    }
}
