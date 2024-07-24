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

            if(ValidCommand(player, isUp)) VacuumManager._instance.AddPlayer(player, isUp);
        }
    }
    public bool ValidCommand(Player player, bool action)
    {
        bool foundPlayer = false;

        // for Each Existing Instruction
        foreach(QueuePlayer queuePlayer in VacuumManager._instance.playerQueue)
        {
            // That Player is Already In the List
            if(queuePlayer.Player == player)
            {
                foundPlayer = true;

                // That Exact Instruction is Already In the List
                if(queuePlayer.Action == action) return false;

                // Cannot input DOWN on a Grounded player, Unless theres Already an UP Action Queued... and vice versa...
                if((action == player.isSafe) && queuePlayer.Action == action) return false;
            }
        }
        
        // That Player is Not already in the List
        if(!foundPlayer)
        {
            // Cannot input DOWN on a Grounded player... and vice versa...
            if(action == player.isSafe) return false;
        }


        // If None of these are the Case, Return True
        return true;
    }


    public IEnumerator UpdatePlayerPositions()
    {
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
    public void RemovePlayer(Player player)
    {
        player.UpdateIndex();
        players[player.ListElement] = null;
        StartCoroutine(UpdatePlayerPositions());

        //Replace with Animation
        Destroy(player.playerVisual.gameObject);
    }

    public Player GetPlayer(int index)
    {
        Player[] playerArray = FindObjectsByType<Player>(FindObjectsSortMode.None);
        foreach(Player player in playerArray)
        {
            if(player != null)
            {
                player.UpdateIndex();
                if(player.Index == index) return player;
            }
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