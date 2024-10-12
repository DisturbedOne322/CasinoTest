using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLobbyDisplayInfo : MonoBehaviour
{
    [SerializeField]
    private Image _playerAvatarImage;
    [SerializeField]
    private TextMeshProUGUI _playerNameText;
    [SerializeField]
    private Button _disconnectButton;
    
    private Player _player;
    public Player Player => _player;

    private void Awake()
    {
        _disconnectButton.onClick.AddListener(() =>
        {
            Lobby.Instance.DisconnectPlayer(_player);
        });
    }

    public void Initialize(Player player)
    {
        _player = player;
        _playerNameText.text = player.PlayerName;
        _playerAvatarImage.sprite = player.AvatarSprite;
    }
}
