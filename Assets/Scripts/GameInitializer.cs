using System.Collections.Generic;

public class GameInitializer
{
    private const int DEFAULT_PLAYER_BALANCE = 1000;

    public List<Player> InitializePlayers(int amount)
    {
        List<Player> players = new List<Player>();
        for (int i = 0; i < amount; i++)
            players.Add(new Player(GenerateRandomName(), 1000, null));

        return players;
    }

    private string GenerateRandomName() => "Player" + UnityEngine.Random.Range(0, 999);
}
