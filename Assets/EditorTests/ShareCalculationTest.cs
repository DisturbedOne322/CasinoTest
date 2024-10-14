using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShareCalculationTests
{
    private PayoutProcessor _payoutProcessor;

    [SetUp]
    public void SetUp()
    {
        _payoutProcessor = new PayoutProcessor(new GameObject().AddComponent<MonoTest>());
    }

    [Test]
    public void ShareTestHalf()
    {
        int player1Bet = 100;
        int player2Bet = 100;

        int total = player2Bet + player1Bet;

        Assert.AreEqual(0.5f, _payoutProcessor.CalculateSharePercent(player1Bet, total));
        Assert.AreEqual(0.5f, _payoutProcessor.CalculateSharePercent(player2Bet, total));
    }

    [Test]
    public void ShareTestAll()
    {
        int player1Bet = 200;
        int player2Bet = 0;

        int total = player2Bet + player1Bet;

        Assert.AreEqual(1, _payoutProcessor.CalculateSharePercent(player1Bet, total));
        Assert.AreEqual(0, _payoutProcessor.CalculateSharePercent(player2Bet, total));
    }

    [Test]
    public void ShareTestQuarter()
    {
        int player1Bet = 300;
        int player2Bet = 100;

        int total = player2Bet + player1Bet;

        Assert.AreEqual(0.75f, _payoutProcessor.CalculateSharePercent(player1Bet, total));
        Assert.AreEqual(0.25f, _payoutProcessor.CalculateSharePercent(player2Bet, total));
    }

    [Test]
    public void ShareTestThird()
    {
        int player1Bet = 200;
        int player2Bet = 100;

        int total = player2Bet + player1Bet;

        Assert.AreEqual(true, Mathf.Approximately(1 - 1 / 3f, _payoutProcessor.CalculateSharePercent(player1Bet, total)));
        Assert.AreEqual(true, Mathf.Approximately(1 / 3f, _payoutProcessor.CalculateSharePercent(player2Bet, total)));
    }
}
