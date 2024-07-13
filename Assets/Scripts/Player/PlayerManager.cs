using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PalexUtilities;
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
        if(Input.GetKeyDown(KeyCode.Tab)) StartCoroutine(UpdatePlayerPositions());
    }


    public void OnNumberPress(InputAction.CallbackContext context)
    {
        if(context.started) HandleNumberPress(int.Parse(context.control.name));
    }

    public void HandleNumberPress(int number)
    {
        if(number > 6) return;
        players[players.Count-1].MovePosition(PlatformManager._instance.GetPlatform(number-1).platformPos.position);
    }


    //[Button]
    public IEnumerator UpdatePlayerPositions()
    {
        Tools.ClearLogConsole();
        int PlatformCount = PlatformManager._instance.platforms.Count;
        if(PlatformCount == 0 || PlatformManager._instance == null) yield break;

        for (int i = PlatformCount-1; i >= 0; i--) 
        {
            Player player = players[i];
            if(player != null)
            {
                while(player.AvailableSlotCheck())
                {
                    player.MoveToSlot(PlatformManager._instance.GetPlatform(i+1));
                }
                player.MoveToSlot(PlatformManager._instance.GetPlatform(player.ListElement));
            }
            yield return new WaitForSeconds(0.075f);
        }
    }

    public void AddPlayers()
    {
        Player[] playerArray = FindObjectsByType<Player>(FindObjectsSortMode.None);
        List<Player> sortedPlayers = playerArray.OrderBy(p => p.Index).ToList();

        int PlatformCount = PlatformManager._instance.platforms.Count;
        players = new List<Player>(PlatformCount);

        for (int i = PlatformCount-1; i >= 0; i--) 
        {    
            if (i < sortedPlayers.Count) 
            {
                players.Add(sortedPlayers[i]);
                sortedPlayers[i].UpdateIndex();
            }
            else players.Add(null);
        }
    }
    
    public void OnValidate()
    {
        if(!Application.isPlaying || Time.fixedTime == 0) return;
        StartCoroutine(UpdatePlayerPositions());
    }
}