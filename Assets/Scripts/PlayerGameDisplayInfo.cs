using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameDisplayInfo : MonoBehaviour
{
    private Player _player;
    public Player Player => _player;
    [SerializeField]
    private Image _playerAvatarImage;
    [SerializeField]
    private TextMeshProUGUI _playerName;
    [SerializeField]
    private TextMeshProUGUI _playerBalance;

    public void Initialize(Player player)
    {
        _player = player;

        _playerName.text = player.PlayerName;
        _playerBalance.text = player.Balance.ToString();

        _playerAvatarImage.sprite = player.AvatarSprite;
    }

    public void UpdateBalance(int newBalance)
    {
        _playerBalance.text = newBalance.ToString();
    }
}
