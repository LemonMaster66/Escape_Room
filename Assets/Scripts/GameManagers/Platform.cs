using System;
using PalexUtilities;
using UnityEngine;

[Serializable]
public class Platform : MonoBehaviour
{
    public int OrderID;
    [HideInInspector] public Transform platformPos;

    void Awake()
    {
        platformPos = Tools.GetChildren(transform)[0];
    }

    void Update()
    {
        
    }
}
