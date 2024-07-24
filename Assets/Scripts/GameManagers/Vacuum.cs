using System.Collections;
using PalexUtilities;
using UnityEngine;
using VInspector;

public class Vacuum : MonoBehaviour
{
    public Transform restPosition;
    public VacuumVisual VacuumVisual;

    [Space(5)]

    [ReadOnly] public bool Active;
    [ReadOnly] public bool Processing;
    [Space(3)]
    [ReadOnly] public Platform targetPlatform;
    [ReadOnly] public Vector3 lastPos;
    [HideInInspector] public static Vacuum _instance;


    void Awake()
    {
        _instance = this;
    }


    [Button]
    public void NextMovement()
    {
        if(VacuumManager._instance.playerQueue.Count > 0)
        {
            QueuePlayer queuePlayer = VacuumManager._instance.playerQueue[0];

            if(queuePlayer.Action) MoveToSlot(queuePlayer.Player.ListElement);
            else                   MoveToSlot(PlatformManager._instance.GetNearestEmptyPlatform().ListElement);
        }
        else
        {
            MoveToPosition(restPosition.position);
        }
    }
    public void NextAction()
    {
        if(VacuumManager._instance.playerQueue.Count != 0)
        {
            QueuePlayer queuePlayer = VacuumManager._instance.playerQueue[0];

            if(queuePlayer.Action) StartCoroutine(SuccPlayer(queuePlayer.Player));
            else                   StartCoroutine(DepositPlayer(queuePlayer.Player));
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
        if(!VacuumVisual.Moving) lastPos = transform.position;
        transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        VacuumVisual.Moving = true;
    }


    public IEnumerator SuccPlayer(Player player)
    {
        if(Processing) yield break;
        Processing = true;

        yield return new WaitForSeconds(0.5f);
        
        PlayerManager._instance.RemovePlayer(player);
        VacuumManager._instance.playerQueue.RemoveAt(0);
        SafeZone._instance.AddPlayer(player);
        player.isSafe = true;

        yield return new WaitForSeconds(0.5f);

        NextMovement();
        Processing = false;

        yield return null;
    }
    public IEnumerator DepositPlayer(Player player)
    {
        if(Processing || player == null) yield break;
        Processing = true;

        yield return new WaitForSeconds(0.5f);

        player.isSafe = false;
        VacuumManager._instance.playerQueue.RemoveAt(0);
        SafeZone._instance.RemovePlayer(player);
        PlayerManager._instance.AddPlayer(player);

        yield return new WaitForSeconds(0.5f);

        NextMovement();
        Processing = false;

        yield return null;
    }
}
