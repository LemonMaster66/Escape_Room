using DG.Tweening;
using PalexUtilities;
using UnityEngine;

public class VaccumVisual : MonoBehaviour
{
    public Vaccum originVaccum;
    public Transform ShakeTransform;


    [Header("Properties")]
    public float positionSpeed = 30;
    public bool Moving;



    void Awake()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, originVaccum.transform.position, positionSpeed * Time.deltaTime);

        if(transform.position == originVaccum.transform.position && Moving)
        {
            Moving = false;
            transform.DOPunchPosition(new Vector3(0.25f, 0, 0), 0.35f, 15, 0.8f);

            originVaccum.NextAction();
        }
    }
}
