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
    public List<Player> playerStorage;

    void Awake()
    {
        _instance = this;

        AddAllPlayers();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) StartCoroutine(UpdatePlayerPositions());
    }


    public void OnPlayerToggle(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            bool isUp = int.TryParse(context.control.name, out int number);
            Player player = GetPlayer(int.Parse(context.action.name)-1);

            Tools.ClearLogConsole();
            Debug.Log(player + ": " + (isUp ? "Up" : "Down"));


        }
    }


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
                    player.MoveToSlot(Tools.GetKey(PlatformManager._instance.platforms, i+1));
                }
                player.MoveToSlot(Tools.GetKey(PlatformManager._instance.platforms, player.ListElement));
            }
            yield return new WaitForSeconds(0.075f);
        }
    }


    public void AddAllPlayers()
    {
        Player[] playerArray = FindObjectsByType<Player>(FindObjectsSortMode.None);
        List<Player> sortedPlayers = playerArray.OrderBy(p => p.Index).ToList();
        sortedPlayers.Reverse();

        int PlatformCount = PlatformManager._instance.platforms.Count;
        players = new List<Player>(PlatformCount);

        for (int i = PlatformCount-1; i >= 0; i--) players.Add(null);
        for (int i = PlatformCount-1; i >= 0; i--)
            if (i < sortedPlayers.Count) AddPlayer(sortedPlayers[i]);
    }

    [Button]
    public void AddPlayer(Player player)
    {
        int PlatformCount = PlatformManager._instance.platforms.Count;

        for (int i = PlatformCount-1; i >= 0; i--) 
        {    
            if (players[i] == null) 
            {
                players[i] = player;
                player.UpdateIndex();
                player.MovePosition(Tools.GetKey(PlatformManager._instance.platforms, player.ListElement).platformPos.position);
                player.SpawnPlayerVisual();
                return;
            }
        }
    }
    [Button]
    public void RemovePlayer(Player player)
    {
        int ListIndex = players.LastIndexOf(player);
        players[ListIndex] = null;
        StartCoroutine(UpdatePlayerPositions());

        //Replace with Animation
        Destroy(player.playerVisual.gameObject);
    }

    public Player GetPlayer(int index)
    {
        Player[] playerArray = FindObjectsByType<Player>(FindObjectsSortMode.None);
        foreach(Player player in playerArray)
        {
            player.UpdateIndex();
            if(player != null && player.Index == index) return player;
        }
        return null;
    }

    
    [OnValueChanged("players")]
    public void OnPlayersChange()
    {
        if(!Application.isPlaying || Time.fixedTime == 0) return;
        StartCoroutine(UpdatePlayerPositions());
    }
}