using System;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    public string  Name = "Name";
    public int     Index = 0;
    public int     Health = 5;
    public bool    Grounded = true;

    [Space(8)]

    public GameObject playerVisualPrefab;
    [Space(5)]
    public PlayerVisual playerVisual;
    public Platform currentPlatform;


    void Awake()
    {
        gameObject.name = Name;
    }
    // void SpawnPlayerVisual()
    // {
    //     GameObject visualObj = Instantiate(playerVisualPrefab, transform.position, Quaternion.identity, transform.parent);
    //     playerVisual = visualObj.GetComponent<PlayerVisual>();
    // }


    public void MovePosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
