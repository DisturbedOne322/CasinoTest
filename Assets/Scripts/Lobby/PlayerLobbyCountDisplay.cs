using TMPro;
using UnityEngine;

public class PlayerLobbyCountDisplay : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _gameSettingsSO;

    private Color _lobbyNotReadyColor = Color.red;
    private Color _lobbyReadyColor = Color.white;

    [SerializeField]
    private TextMeshProUGUI _playerCountText;
    // Start is called before the first frame update
    void Start()
    {
        Lobby.Instance.OnPlayerConnected += OnPlayersChanged;
        Lobby.Instance.OnPlayerDisconnected += OnPlayersChanged;
    }

    private void OnDestroy()
    {
        Lobby.Instance.OnPlayerConnected -= OnPlayersChanged;
        Lobby.Instance.OnPlayerDisconnected -= OnPlayersChanged;
    }

    private void OnPlayersChanged(Player player)
    {
        UpdatePlayerCountDisplay();
    }


    private void UpdatePlayerCountDisplay()
    {
        int count = Lobby.Instance.PlayersInLobby.Count;
        _playerCountText.text = count + "/" + _gameSettingsSO.MaxPlayers;
        _playerCountText.color = count >= _gameSettingsSO.MinPlayers ? _lobbyReadyColor : _lobbyNotReadyColor;
    }
}
