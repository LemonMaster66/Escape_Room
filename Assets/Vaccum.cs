using System.Collections;
using PalexUtilities;
using UnityEngine;
using VInspector;

public class Vaccum : MonoBehaviour
{
    public Transform restPosition;
    public VaccumVisual vaccumVisual;

    [Space(5)]

    [ReadOnly] public Platform targetPlatform;
    public SerializedDictionary<Player, bool> playerQueue;


    [HideInInspector] public static Vaccum _instance;


    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        
    }



    public void NextMovement()
    {
        Player player = Tools.GetKey(playerQueue, 0);
        
        if(player != null)
        {
            MoveToSlot(player.Index);
        }
        else
        {
            MoveToPosition(restPosition.position);
        }
    }


    [Button]
    public void MoveToSlot(int Index)
    {
        if(Index > PlatformManager._instance.platforms.Count) return;

        targetPlatform = Tools.GetKey(PlatformManager._instance.platforms, Index);
        MoveToPosition(new Vector3(targetPlatform.platformPos.position.x, 3, 0));
    }
    public void MoveToPosition(Vector3 pos)
    {
        transform.position = pos;
        vaccumVisual.Moving = true;
    }


    public IEnumerator SuccPlayer(Player player)
    {
        yield return new WaitForSeconds(0.5f);

        //targetPlayer = player;
        PlayerManager._instance.RemovePlayer(player);

        yield return null;
    }
}
