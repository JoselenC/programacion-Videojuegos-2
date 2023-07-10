using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private Text rankingUsers;
    [SerializeField] private  Text rankingScore;
    private List<PlayerData> _players;
    private DataPersistManager _persistManager;
    public void Start()
    {
        _persistManager = FindObjectOfType<DataPersistManager>();
        _players = _persistManager.LoadPlayerData("playerData.json");

        var topPlayers = _players.OrderByDescending(player => player.playerScore).Take(5);

        foreach (var player in topPlayers)
        {
            rankingUsers.text += player.playerUsername + "\n";
            rankingScore.text += player.playerScore.ToString() + "\n";
        }
    }

}
