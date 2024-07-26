using System.Collections.Generic;
using System.Linq;
using PalexUtilities;
using UnityEngine;
using VInspector;
using VInspector.Libs;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager _instance;

    public List<PlatformElement> platforms;


    void Awake()
    {
        _instance = this;
        AddPlatforms();
    }

    void Update()
    {
        
    }


    [Button]
    public void UpdatePlatformStates()
    {
        foreach (PlatformElement platform in platforms)
            platform.Platform.UpdateState();
    }


    public void AddPlatforms()
    {
        platforms = new List<PlatformElement>();

        Platform[] platformArray = FindObjectsByType<Platform>(FindObjectsSortMode.None);
        List<Platform> sortedPlatforms = platformArray.OrderBy(p => p.OrderID).ToList();
        sortedPlatforms.Reverse();

        foreach (Platform platform in sortedPlatforms)
        {
            PlatformElement platformElement = new PlatformElement(platform, true);
            platforms.Add(platformElement);
            platform.UpdateValues();
        }
        foreach (PlatformElement platform in platforms)
            platform.Platform.UpdateValues();
    }

    public Platform GetNearestEmptyPlatform()
    {
        for (int i = platforms.Count-1; i >= 0; i--)
            if(!platforms[i].State) return platforms[i].Platform;
            
        return null;
    }
}
