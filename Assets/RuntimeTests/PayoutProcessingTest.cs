using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PayoutProcessingTest
{
    private PayoutProcessor _payoutProcessor;

    [SetUp]
    public void SetUp()
    {
        _payoutProcessor = new PayoutProcessor(new GameObject().AddComponent<MonoTest>());
    }

    [UnityTest]
    public IEnumerator PayoutProcess1Winner1Bet()
    {
        var player1 = new Player("Player1", 1000, null);
        var player2 = new Player("Player2", 1000, null);
        var finalBets = new Dictionary<Player, List<Bet>>
        {
            { player1, new List<Bet> { new Bet(3, 100) } },
            { player2, new List<Bet> { new Bet(4, 200) } }
        };

        yield return _payoutProcessor.ProcessPayouts(finalBets, 3);

        int totalBet = 100;
        int totalLost = 200;

        int expectedPlayer1Balance = 1000 + _payoutProcessor.CalculatePlayerPayout(finalBets[player1][0].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player1][0].Amount, totalBet),
                                           totalLost); ;
        int expectedPlayer2Balance = 1000;

        // Assert: Check that the correct payout is made
        Assert.AreEqual(expectedPlayer1Balance, player1.Balance);
        Assert.AreEqual(expectedPlayer2Balance, player2.Balance);
    }

    [UnityTest]
    public IEnumerator PayoutProcess0Winners1Bet()
    {
        var player1 = new Player("Player1", 500, null);
        var player2 = new Player("Player2", 0, null);
        var player3 = new Player("Player3", 1000, null);

        var finalBets = new Dictionary<Player, List<Bet>>
        {
            { player1, new List<Bet> { new Bet(3, 100) } },
            { player2, new List<Bet> { new Bet(4, 200) } },
            { player3, new List<Bet> { new Bet(3, 200) } }
        };

        // Act: Call the public method that uses the private ones
        yield return _payoutProcessor.ProcessPayouts(finalBets, 0);

        int expectedPlayer1Balance = 500;
        int expectedPlayer2Balance = 0;
        int expectedPlayer3Balance = 1000;

        // Assert: Check that the correct payout is made
        Assert.AreEqual(expectedPlayer1Balance, player1.Balance);
        Assert.AreEqual(expectedPlayer2Balance, player2.Balance);
        Assert.AreEqual(expectedPlayer3Balance, player3.Balance);
    }

    [UnityTest]
    public IEnumerator PayoutProcess2Winners1Bet()
    {
        var player1 = new Player("Player1", 500, null);
        var player2 = new Player("Player2", 0, null);
        var player3 = new Player("Player3", 1000, null);

        var finalBets = new Dictionary<Player, List<Bet>>
        {
            { player1, new List<Bet> { new Bet(3, 400) } },
            { player2, new List<Bet> { new Bet(4, 100) } },
            { player3, new List<Bet> { new Bet(3, 100) } }
        };

        // Act: Call the public method that uses the private ones
        yield return _payoutProcessor.ProcessPayouts(finalBets, 3);

        int totalBet = 400 + 100;
        int totalLost = 100;

        int expectedPlayer1Balance = 500 + _payoutProcessor.CalculatePlayerPayout(finalBets[player1][0].Amount, 
                                           _payoutProcessor.CalculateSharePercent(finalBets[player1][0].Amount, totalBet),
                                           totalLost);
        int expectedPlayer2Balance = 0;
        int expectedPlayer3Balance = 1000 + _payoutProcessor.CalculatePlayerPayout(finalBets[player3][0].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player3][0].Amount, totalBet),
                                           totalLost);

        // Assert: Check that the correct payout is made
        Assert.AreEqual(expectedPlayer1Balance, player1.Balance);
        Assert.AreEqual(expectedPlayer2Balance, player2.Balance);
        Assert.AreEqual(expectedPlayer3Balance, player3.Balance);
    }

    [UnityTest]
    public IEnumerator PayoutProcess3Winners1Bet()
    {
        var player1 = new Player("Player1", 500, null);
        var player2 = new Player("Player2", 0, null);
        var player3 = new Player("Player3", 1000, null);

        var finalBets = new Dictionary<Player, List<Bet>>
        {
            { player1, new List<Bet> { new Bet(4, 400) } },
            { player2, new List<Bet> { new Bet(4, 100) } },
            { player3, new List<Bet> { new Bet(4, 100) } }
        };

        // Act: Call the public method that uses the private ones
        yield return _payoutProcessor.ProcessPayouts(finalBets, 4);

        int totalBet = 400 + 100 + 100;

        int expectedPlayer1Balance = 500 + _payoutProcessor.CalculatePlayerPayout(finalBets[player1][0].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player1][0].Amount, totalBet),
                                           0);
        int expectedPlayer2Balance = 0 +  _payoutProcessor.CalculatePlayerPayout(finalBets[player2][0].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player2][0].Amount, totalBet),
                                           0);
        int expectedPlayer3Balance = 1000 + _payoutProcessor.CalculatePlayerPayout(finalBets[player2][0].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player2][0].Amount, totalBet),
                                           0);

        // Assert: Check that the correct payout is made
        Assert.AreEqual(expectedPlayer1Balance, player1.Balance);
        Assert.AreEqual(expectedPlayer2Balance, player2.Balance);
        Assert.AreEqual(expectedPlayer3Balance, player3.Balance);
    }

    [UnityTest]
    public IEnumerator PayoutProcess1Winner2Bets()
    {
        var player1 = new Player("Player1", 500, null);
        var player2 = new Player("Player2", 0, null);
        var player3 = new Player("Player3", 1000, null);

        var finalBets = new Dictionary<Player, List<Bet>>
        {
            { player1, new List<Bet> { new Bet(4, 400), new Bet(5,500) } },
            { player2, new List<Bet> { new Bet(5, 100) } },
            { player3, new List<Bet> { new Bet(6, 100) } }
        };

        // Act: Call the public method that uses the private ones
        yield return _payoutProcessor.ProcessPayouts(finalBets, 4);

        int totalBet = 400;
        int totalBetLost = 500 + 100 + 100;

        int expectedPlayer1Balance = 500 + _payoutProcessor.CalculatePlayerPayout(finalBets[player1][0].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player1][0].Amount, totalBet),
                                           totalBetLost);
        int expectedPlayer2Balance = 0;
        int expectedPlayer3Balance = 1000;

        // Assert: Check that the correct payout is made
        Assert.AreEqual(expectedPlayer1Balance, player1.Balance);
        Assert.AreEqual(expectedPlayer2Balance, player2.Balance);
        Assert.AreEqual(expectedPlayer3Balance, player3.Balance);
    }

    [UnityTest]
    public IEnumerator PayoutProcess2Winners2Bets()
    {
        var player1 = new Player("Player1", 500, null);
        var player2 = new Player("Player2", 0, null);
        var player3 = new Player("Player3", 1000, null);

        var finalBets = new Dictionary<Player, List<Bet>>
        {
            { player1, new List<Bet> { new Bet(4, 400), new Bet(5,500) } },
            { player2, new List<Bet> { new Bet(5, 100), new Bet(4, 1000) } },
            { player3, new List<Bet> { new Bet(6, 100) } }
        };

        // Act: Call the public method that uses the private ones
        yield return _payoutProcessor.ProcessPayouts(finalBets, 4);

        int totalBet = 400 + 1000;
        int totalBetLost = 500 + 100 + 100;

        int expectedPlayer1Balance = 500 + _payoutProcessor.CalculatePlayerPayout(finalBets[player1][0].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player1][0].Amount, totalBet),
                                           totalBetLost);
        int expectedPlayer2Balance = 0 + _payoutProcessor.CalculatePlayerPayout(finalBets[player2][1].Amount,
                                           _payoutProcessor.CalculateSharePercent(finalBets[player2][1].Amount, totalBet),
                                           totalBetLost);
        int expectedPlayer3Balance = 1000;

        // Assert: Check that the correct payout is made
        Assert.AreEqual(expectedPlayer1Balance, player1.Balance);
        Assert.AreEqual(expectedPlayer2Balance, player2.Balance);
        Assert.AreEqual(expectedPlayer3Balance, player3.Balance);
    }
}
