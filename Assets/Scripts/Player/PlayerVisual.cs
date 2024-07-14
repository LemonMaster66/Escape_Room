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


    void Awake()
    {
        
    }

    void Update()
    {
        if(originPlayerTransform != null)
        {
            // Smooth Position
            Vector3 targetPosition = Vector3.Lerp(transform.position, originPlayerTransform.position, positionSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);

            // Smooth Rotation
            float movementX = (transform.position.x - originPlayerTransform.position.x) * 200;
            Quaternion targetRotation = Quaternion.Euler(TiltTransform.eulerAngles.x, Mathf.Clamp(movementX, -90, 90), TiltTransform.eulerAngles.z);
            TiltTransform.rotation = Quaternion.Slerp(TiltTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
