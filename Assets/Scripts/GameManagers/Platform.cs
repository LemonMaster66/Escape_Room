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
        
    }

    void Update()
    {
        
    }

    public void UpdateValues()
    {
        platformPos = Tools.GetChildren(transform)[0];
    }
}
