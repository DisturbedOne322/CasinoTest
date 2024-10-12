using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private BetPopup _betPopup;

    private BetsManager _betsManager;

    // Start is called before the first frame update
    void Start()
    {
        var board = GameObject.FindObjectsByType<BoardNumber>(FindObjectsSortMode.None).ToList();
        foreach (BoardNumber number in board) 
        {
            number.OnButtonClicked += Number_OnButtonClicked;
        }

        _betPopup.OnBetConfirmed += _betPopup_OnBetConfirmed;
    }

    public void Initialize(BetsManager betsManager) => _betsManager = betsManager;

    private void _betPopup_OnBetConfirmed(int number, int amount)
    {
        if (amount == 0)
            return;

        var player = GameManager.Instance.CurrentPlayer;

        player.RemoveAmount(amount);
        _betsManager.PlaceNewBet(player, new Bet(number, amount));
    }

    private void Number_OnButtonClicked(int number)
    {
        Player player = GameManager.Instance.CurrentPlayer;
        _betPopup.OpenPopup(player.Balance, number, _betsManager.FindBetAmountForNumber(player, number));
    }
}
