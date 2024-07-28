using System;
using System.Collections.Generic;
using PalexUtilities;
using UnityEngine;
using VInspector;

[Serializable]
public class Player : MonoBehaviour
{
    public string  Name = "Name";
    public int     Index = 0;
    public int     Health = 9;
    public bool    isSafe = false;

    [Space(5)]

    [ReadOnly] public int ListIndex = 0;
    [ReadOnly] public int ListElement = 0;

    [Space(8)]

    public GameObject playerVisualPrefab;
    [Space(5)]
    public PlayerVisual playerVisual;
    public Platform currentPlatform;


    void Awake()
    {
        gameObject.name = Name;
    }
    public void SpawnPlayerVisual()
    {
        GameObject visualObj = Instantiate(playerVisualPrefab, transform.position, Quaternion.identity, transform.parent);
        playerVisual = visualObj.GetComponent<PlayerVisual>();

        playerVisual.originPlayer = this;
        playerVisual.originPlayerTransform = transform;
    }


    public void MovePosition(Vector3 pos)
    {
        transform.position = pos;
    }


    public bool AvailableSlotCheck()
    {
        UpdateIndex();

        List<Player> currentList = !isSafe ? PlayerManager._instance.players : SafeZone._instance.safePlayers;

        // Check if the next index is out of bounds
        int platformCount = currentList.Count;
        int nextIndex = platformCount - ListIndex;
        if (nextIndex < 0 || nextIndex >= platformCount) return false;

        // Check if the next spot is empty
        bool isEmpty = currentList[nextIndex] == null;
        return isEmpty;
    }

    public void MoveToSlot(Platform platform)
    {
        List<Player> currentList = !isSafe ? PlayerManager._instance.players : SafeZone._instance.safePlayers;
        int ListIndex = currentList.IndexOf(this);

        if(AvailableSlotCheck())
        {
            currentList[ListIndex] = null;
            currentList[ListIndex+1] = this;
        }
        
        UpdateIndex();
        currentPlatform = platform;
        MovePosition(platform.transform.position);
    }


    public void UpdateIndex()
    {
        List<Player> currentList = !isSafe ? PlayerManager._instance.players : SafeZone._instance.safePlayers;
        ListElement = currentList.IndexOf(this);
        if(ListElement == -1) return;

        ListIndex = PlatformManager._instance.platforms.Count - ListElement -1;
        currentPlatform = PlatformManager._instance.platforms[ListElement].Platform;
    }
}
