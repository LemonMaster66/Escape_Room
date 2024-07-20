using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PalexUtilities;
using UnityEngine;
using VInspector;
using VInspector.Libs;

public class VacuumManager : MonoBehaviour
{
    public SerializedDictionary<Player, bool> playerQueue;

    [HideInInspector] public static VacuumManager _instance;


    void Awake()
    {
        _instance = this;
    }


    public void UpdateVacuum()
    {
        Vacuum Vacuum = Vacuum._instance;
        Vacuum.Active = true;
        Vacuum.NextMovement();
    }

    public void AddPlayer(Player player, bool state)
    {
        // If that Player is Already In the Dictionary
        if(playerQueue.ContainsKey(player))
        {
            int playerIndex = playerQueue.Keys.ToList().IndexOf(player);
            bool value = Tools.GetValue(playerQueue, playerIndex);
            
            if(value != state) playerQueue.Remove(player);
        }
        else playerQueue.Add(player, state);
        UpdateVacuum();
    }
}
