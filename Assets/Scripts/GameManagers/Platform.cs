using System;
using System.Linq;
using PalexUtilities;
using UnityEngine;

[Serializable]
public class Platform : MonoBehaviour
{
    public int OrderID;
    public int ListElement;
    [HideInInspector] public Transform platformPos;

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateValues()
    {
        platformPos = Tools.GetChildren(transform)[0];
        ListElement = PlatformManager._instance.platforms.Keys.ToList().IndexOf(this);
    }
}
