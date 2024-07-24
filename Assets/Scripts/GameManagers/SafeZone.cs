using System.Collections.Generic;
using PalexUtilities;
using UnityEngine;
using VInspector;

public class SafeZone : MonoBehaviour
{
    public List<Player> safePlayers;
    
    [HideInInspector] public static SafeZone _instance;

    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        
    }

    public void UpdatePlayerPositions()
    {
        Tools.ClearLogConsole();
        int PlatformCount = PlatformManager._instance.platforms.Count;
        if(PlatformCount == 0 || PlatformManager._instance == null) return;

        for (int i = PlatformCount-1; i >= 0; i--) 
        {
            Player player = safePlayers[i];
            if(player != null)
            {
                while(player.AvailableSlotCheck())
                    player.MoveToSlot(Tools.GetKey(PlatformManager._instance.platforms, i+1));
            }
        }
    }
    

    [Button]
    public void AddPlayer(Player player)
    {
        int PlatformCount = PlatformManager._instance.platforms.Count;

        for (int i = PlatformCount-1; i >= 0; i--) 
        {    
            if (safePlayers[i] == null) 
            {
                safePlayers[i] = player;
                player.UpdateIndex();
                //player.SpawnPlayerVisual();
                return;
            }
        }
    }
    [Button]
    public void RemovePlayer(Player player)
    {
        int ListIndex = safePlayers.LastIndexOf(player);
        safePlayers[ListIndex] = null;
        UpdatePlayerPositions();

        //Replace with Animation
        if(player.playerVisual != null) Destroy(player.playerVisual.gameObject);
    }
}
