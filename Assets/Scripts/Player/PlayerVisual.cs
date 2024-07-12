using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [Header("Origin Player")]
    public Player originPlayer;
    public Transform originPlayerTransform;

    public Transform TiltTransform;
    public Transform ShakeTransform;


    [Header("Properties")]
    public float positionSpeed = 30;
    public float rotationSpeed = 20;


    [Header("Details")]
    public GameObject HeadObj;

    private Vector3 rotationDelta;
    private Vector3 movementDelta;


    void Awake()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, originPlayerTransform.position, positionSpeed * Time.deltaTime);
    }
}
