using System.Collections.Generic;
using System.Linq;
using PalexUtilities;
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



    public Platform GetNearestEmptyPlatform()
    {
        for (int i = platforms.Count-1; i >= 0; i--)
        {
            Player player = PlayerManager._instance.players[i];
            if(player == null) return Tools.GetKey(platforms, i);
        }
        return null;
    }


    public void AddPlatforms()
    {
        platforms = new SerializedDictionary<Platform, bool>();

        Platform[] platformArray = FindObjectsByType<Platform>(FindObjectsSortMode.None);
        List<Platform> sortedPlatforms = platformArray.OrderBy(p => p.OrderID).ToList();
        sortedPlatforms.Reverse();

        foreach (Platform platform in sortedPlatforms)
        {
            platforms.Add(platform, true);
            platform.UpdateValues();
        }
    }
}
