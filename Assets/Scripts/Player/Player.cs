using System;
using PalexUtilities;
using UnityEngine;
using VInspector;

[Serializable]
public class Player : MonoBehaviour
{
    public string  Name = "Name";
    public int     Index = 0;
    public int     Health = 5;
    public bool    Grounded = true;

    [Space(8)]

    public GameObject playerVisualPrefab;
    [Space(5)]
    public PlayerVisual playerVisual;
    public Platform currentPlatform;


    void Awake()
    {
        gameObject.name = Name;
        SpawnPlayerVisual();
    }
    void SpawnPlayerVisual()
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

    public void MoveToEmptySpot()
    {

    }

    [Button]
    public bool AvailableSlotCheck()
    {
        Tools.ClearLogConsole();
        UpdateIndex();

        // Check if the next index is out of bounds
        int platformCount = PlatformManager._instance.platforms.Count;
        int nextIndex = platformCount - Index;
        if (nextIndex < 0 || nextIndex >= platformCount)
        {
            Debug.Log("Out of Bounds");
            return false;
        }

        // Check if the next spot is empty
        bool isEmpty = PlayerManager._instance.players[nextIndex] == null;
        Debug.Log(isEmpty ? "Empty" : "Taken");

        return isEmpty;
    }


    public void UpdateIndex()
    {
        int newIndex = PlayerManager._instance.players.IndexOf(this);
        Index = PlatformManager._instance.platforms.Count - newIndex -1;
    }
}
