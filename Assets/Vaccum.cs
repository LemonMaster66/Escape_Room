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



    [Button]
    public void NextMovement()
    {
        if(playerQueue.Count > 0)
        {
            Player player = Tools.GetKey(playerQueue, 0);
            bool Action = Tools.GetValue(playerQueue, 0);

            MoveToSlot(player.ListElement);

            if(Action) MoveToSlot(player.ListElement);
            else       MoveToSlot(PlatformManager._instance.GetNearestEmptyPlatform().ListElement);
        }
        else
        {
            MoveToPosition(restPosition.position);
        }
    }
    public void NextAction()
    {
        if(playerQueue.Count != 0)
        {
            Player player = Tools.GetKey(playerQueue, 0);
            bool Action = Tools.GetValue(playerQueue, 0);

            if(Action) StartCoroutine(SuccPlayer(player));
            else       StartCoroutine(DepositPlayer(player));
        }
    }


    public void MoveToSlot(int Index)
    {
        if(Index > PlatformManager._instance.platforms.Count) return;

        targetPlatform = Tools.GetKey(PlatformManager._instance.platforms, Index);
        MoveToPosition(new Vector3(targetPlatform.platformPos.position.x, 3, 0));
    }
    public void MoveToPosition(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        vaccumVisual.Moving = true;
    }


    public IEnumerator SuccPlayer(Player player)
    {
        yield return new WaitForSeconds(0.5f);
        
        PlayerManager._instance.RemovePlayer(player);
        playerQueue.Remove(player);

        yield return new WaitForSeconds(0.5f);

        NextMovement();

        yield return null;
    }
    public IEnumerator DepositPlayer(Player player)
    {
        yield return new WaitForSeconds(0.5f);

        PlayerManager._instance.AddPlayer(player);
        playerQueue.Remove(player);

        yield return new WaitForSeconds(0.5f);

        NextMovement();

        yield return null;
    }
}
