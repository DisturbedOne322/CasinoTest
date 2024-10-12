using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPlayerButton : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _gameSettingsSO;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => 
        {
            Lobby.Instance.ConnectPlayer(GetPlayerInfo());
        });
    }

    //could make a call to the API to get account data
    private Player GetPlayerInfo() => new Player(GenerateRandomName(), _gameSettingsSO.DefaultBalance, Resources.Load<Sprite>("DefaultAvatar"));

    private string GenerateRandomName() => "Player" + UnityEngine.Random.Range(0, 999);
}
