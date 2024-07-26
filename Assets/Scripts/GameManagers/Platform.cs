using System;
using System.Linq;
using PalexUtilities;
using UnityEngine;
using VInspector;

[Serializable]
public class Platform : MonoBehaviour
{
    public int OrderID;
    [ReadOnly] public int ListElement;
    [ReadOnly] public GameObject platformVisual;

    void Awake()
    {
        UpdateValues();
        UpdateState();
    }

    void Update()
    {
        
    }

    public void UpdateValues()
    {
        ListElement = PlatformManager._instance.platforms.Count - OrderID -1;
        platformVisual = transform.GetChild(0).gameObject;
    }

    public void UpdateState()
    {
        PlatformElement platformElement = PlatformManager._instance.platforms[ListElement];
        if(platformElement.Platform = this)
        {
            platformElement.State = PlayerManager._instance.players[ListElement] != null;
        }

        // Replace with Animation: SetBool("State", platformElement.State)
        if(platformElement.State) platformVisual.transform.localPosition = new Vector3(0, 0, 0);
        else                      platformVisual.transform.localPosition = new Vector3(0, 1.5f, 0);
    }
}