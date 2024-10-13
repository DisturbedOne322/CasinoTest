using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    private Button _startGameButton;

    private const string GAMEPLAY_SCENE_NAME = "GameplayScene";

    private bool _loadingScene = false;

    private void Awake()
    {
        _startGameButton = GetComponent<Button>();
        _startGameButton.onClick.AddListener(() =>
        {
            TryStartGame();
        });
    }

    private void TryStartGame()
    {
        if (_loadingScene)
            return;

        if (Lobby.Instance.IsLobbyReady())
            StartCoroutine(LoadGameplaySceneAsync());
    }

    private IEnumerator LoadGameplaySceneAsync()
    {
        _loadingScene = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GAMEPLAY_SCENE_NAME);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        _loadingScene = false;
    }
}
