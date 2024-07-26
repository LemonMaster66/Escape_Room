using System;
using UnityEngine;

[Serializable]
public class PlatformElement
{
    public Platform Platform;
    public bool State;

    public PlatformElement(Platform platform, bool state)
    {
        Platform = platform;
        State = state;
    }
}
