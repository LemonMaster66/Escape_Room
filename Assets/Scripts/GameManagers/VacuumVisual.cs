using DG.Tweening;
using PalexUtilities;
using UnityEngine;

public class VacuumVisual : MonoBehaviour
{
    public Vacuum originVacuum;
    public Transform ShakeTransform;


    [Header("Properties")]
    public float positionSpeed = 20;
    public bool Moving;



    void Awake()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, originVacuum.transform.position, positionSpeed * Time.deltaTime);

        if(transform.position == originVacuum.transform.position && Moving)
        {
            if(transform.position.x == originVacuum.restPosition.position.x)
            {
                originVacuum.Active = false;
                originVacuum.targetPlatform = null;
            }

            if(originVacuum.lastPos != originVacuum.transform.position)
                transform.DOPunchPosition(new Vector3(0.25f, 0, 0), 0.35f, 15, 0.8f);

            Moving = false;
            originVacuum.NextAction();
        }
    }
}
