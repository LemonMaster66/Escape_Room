using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PalexUtilities;
using UnityEngine;
using VInspector;
using VInspector.Libs;

public class VacuumManager : MonoBehaviour
{
    public List<QueuePlayer> playerQueue;

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
        // Check If that Player is Already In the List
        foreach(QueuePlayer queuePlayer in playerQueue)
        {
            if(queuePlayer != null)
                if(queuePlayer.Player == player && queuePlayer.Action == state) return;
        }

        QueuePlayer newQueuePlayer = new QueuePlayer(player, state);
        playerQueue.Add(newQueuePlayer);

        UpdateVacuum();
    }
}
