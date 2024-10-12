using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameBoard))]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    [SerializeField]
    private Roulette _roulette;
    [SerializeField]    
    private GameBoard _board;
    [SerializeField]
    private GameProgressDisplay _progressDisplay;

    [SerializeField]
    private GameSettingsSO _gameSettings;
    [SerializeField]
    private Button _endTurnButton;

    private BetsManager _betsManager;
    private RoundManager _roundManager;
    private PayoutProcessor _payoutProcessor;

    private bool _isBettingPhase = true;
    public bool IsBettingPhase => _isBettingPhase;

    public Player CurrentPlayer => _roundManager.CurrentPlayer;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(this);
    }

    void Start()
    {
        _betsManager = new ();
        _roundManager = new(_endTurnButton, _betsManager);
        _payoutProcessor = new();

        _board.Initialize(_betsManager);
        _progressDisplay.Initialize(_roundManager, _payoutProcessor);

        _roundManager.OnTurnsEnded += OnTurnsEnded;
        _roulette.OnSpinEnd += OnSpinEnd;
        _payoutProcessor.OnProcessingFinished += _payoutProcessor_OnProcessingFinished;

        StartRound();
    }

    private void OnDestroy()
    {
        _roundManager.OnTurnsEnded -= OnTurnsEnded;
        _roulette.OnSpinEnd -= OnSpinEnd;
        _payoutProcessor.OnProcessingFinished -= _payoutProcessor_OnProcessingFinished;
    }

    private void _payoutProcessor_OnProcessingFinished()
    {
        StartRound();
    }

    private void OnTurnsEnded()
    {
        _isBettingPhase = false;
        _roulette.Spin();
    }

    private void OnSpinEnd(int winningNumber)
    {
        StartCoroutine(_payoutProcessor.ProcessPayouts(winningNumber, _betsManager.GetFinalBets(), _gameSettings.PayoutMultiplier));
    }

    private void StartRound()
    {
        _isBettingPhase = true;
        _betsManager.StartNewRound();
        _roundManager.StartRound(Lobby.Instance.PlayersInLobby);
    }
}
