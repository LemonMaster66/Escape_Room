using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using VInspector;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _instance;

    public List<Player> players;

    void Awake()
    {
        _instance = this;

        AddPlayers();
    }

    void Update()
    {
        
    }


    public void OnNumberPress(InputAction.CallbackContext context)
    {
        if(context.started) HandleNumberPress(int.Parse(context.control.name));
    }

    public void HandleNumberPress(int number)
    {
        if(number > 6) return;
        players[players.Count-1].MovePosition(PlatformManager._instance.GetPlatform(number-1).platformPos.position + Vector3.up * 0.5f);
    }


    public void UpdatePlayerPositions()
    {
        
    }

    public void AddPlayers()
    {
        Player[] playerArray = FindObjectsByType<Player>(FindObjectsSortMode.None);
        List<Player> sortedPlayers = playerArray.OrderBy(p => p.Index).ToList();

        int PlatformCount = PlatformManager._instance.platforms.Count;
        players = new List<Player>(PlatformCount);

        for (int i = PlatformCount-1; i >= 0; i--) 
        {    
            if (i < sortedPlayers.Count) players.Add(sortedPlayers[i]);
            else                         players.Add(null);
        }
    }
}