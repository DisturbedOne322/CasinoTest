using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetPopup : MonoBehaviour
{
    public event Action<int, int> OnBetConfirmed;

    [SerializeField]
    private GameObject _popUpParentGO;

    [SerializeField]
    private GameSettingsSO _gameSettingsSO;

    //defined to only store integers in the component properties
    [SerializeField]
    private TMP_InputField _betInputField;
    [SerializeField]
    private TextMeshProUGUI _balanceText;
    [SerializeField]
    private TextMeshProUGUI _numberText;
    [SerializeField]
    private TextMeshProUGUI _minBetText;
    [SerializeField]
    private TextMeshProUGUI _maxBetText;
    [SerializeField]
    private TextMeshProUGUI _betSumText;
    [SerializeField]
    private Button _confirmBetButton;
    [SerializeField]
    private Button _cancelBetButton;

    private int _lastNumberSelected;
    private int _betSum = 0;

    void Start()
    {
        _confirmBetButton.onClick.AddListener(() =>
        {
            OnBetConfirmed?.Invoke(_lastNumberSelected, Convert.ToInt32(_betInputField.text));
            _popUpParentGO.SetActive(false);
        });

        _cancelBetButton.onClick.AddListener(() =>
        {
            _popUpParentGO.SetActive(false);
        });

        _betInputField.onDeselect.AddListener(ProcessInputData);

        _minBetText.text = _gameSettingsSO.MinBet.ToString();
        _maxBetText.text = _gameSettingsSO.MaxBet.ToString();
    }

    public void OpenPopup(int balance, int number, int betSum)
    {
        if (!GameManager.Instance.IsBettingPhase)
            return;

        _betSum = betSum;
        _lastNumberSelected = number;

        _betSumText.text = _betSum.ToString();
        _balanceText.text = balance.ToString();
        _numberText.text = number.ToString();

        _popUpParentGO.SetActive(true);

        _betInputField.text = 0.ToString();
        _betInputField.Select();
    }

    private void ProcessInputData(string stringValue)
    {
        if (stringValue.Length == 0)
            return;

        int intendedBet = Convert.ToInt32(stringValue);

        Player player = GameManager.Instance.CurrentPlayer;
        int maxPossibleBet = Math.Min(_gameSettingsSO.MaxBet - _betSum, player.Balance);
        int minPossibleBet = _gameSettingsSO.MinBet;

        if (maxPossibleBet < minPossibleBet)
        {
            _betInputField.text = 0.ToString();
            return;
        }


        if (!player.CanPlaceBet(minPossibleBet))
        {
            _betInputField.text = 0.ToString();
            return;
        }

        int clampedBet = Mathf.Clamp(intendedBet, minPossibleBet, maxPossibleBet);
        if (!player.CanPlaceBet(clampedBet))
        {
            _betInputField.text = 0.ToString();
            return;
        }

        _betInputField.text = clampedBet.ToString();
    }
}
