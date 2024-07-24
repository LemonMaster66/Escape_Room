using System;
using UnityEngine;

[Serializable]
public class QueuePlayer
{
    public Player Player;
    public bool Action;

    public QueuePlayer(Player player, bool action)
    {
        Player = player;
        Action = action;
    }
}
