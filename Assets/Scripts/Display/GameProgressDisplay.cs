using TMPro;
using UnityEngine;

public class GameProgressDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _progressText;

    private RoundManager _roundManager;
    private PayoutProcessor _payoutProcessor;

    public void Initialize(RoundManager roundManager, PayoutProcessor payoutProcessor)
    {
        _roundManager = roundManager;
        _payoutProcessor = payoutProcessor;

        _roundManager.OnPlayerTurnChanged += Instance_OnPlayerTurnChanged;
        _roundManager.OnTurnsEnded += Instance_OnTurnsEnded;

        _payoutProcessor.OnBetProcessed += _payoutProcessor_OnBetProcessed;
    }

    private void OnDestroy()
    {
        _roundManager.OnPlayerTurnChanged -= Instance_OnPlayerTurnChanged;
        _roundManager.OnTurnsEnded -= Instance_OnTurnsEnded;

        _payoutProcessor.OnBetProcessed -= _payoutProcessor_OnBetProcessed;
    }

    private void _payoutProcessor_OnBetProcessed(string message) => _progressText.text = message;


    private void Instance_OnTurnsEnded()
    {
        _progressText.text = "Spinning...";
    }

    private void Instance_OnPlayerTurnChanged(Player player)
    {
        if (player.InGame)
            _progressText.text = player.PlayerName + "'s turn...";
        else
            _progressText.text = player.PlayerName + " isn't in game anymore";
    }
}
