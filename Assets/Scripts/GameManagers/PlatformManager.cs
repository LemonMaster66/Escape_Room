using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VInspector;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager _instance;

    public SerializedDictionary<Platform, bool> platforms;


    void Awake()
    {
        _instance = this;
        AddPlatforms();
    }

    void Update()
    {
        
    }


    public Platform GetPlatform(int index)
    {
        return platforms.ElementAt(index).Key;
    }
    public bool GetPlatformState(int index)
    {
        return platforms.ElementAt(index).Value;
    }


    public void AddPlatforms()
    {
        platforms = new SerializedDictionary<Platform, bool>();

        Platform[] platformArray = FindObjectsByType<Platform>(FindObjectsSortMode.None);
        List<Platform> sortedPlatforms = platformArray.OrderBy(p => p.OrderID).ToList();
        sortedPlatforms.Reverse();

        foreach (Platform platform in sortedPlatforms) platforms.Add(platform, true);
    }
}
