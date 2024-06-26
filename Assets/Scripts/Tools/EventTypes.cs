using PalexUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VInspector;

public class EventTypes : MonoBehaviour
{
    [Header("Types")]
    public bool Trigger;


    [Header("Events")]
    public bool DestroySelf;
    [Space(5)]
    public AudioClip PlaySound;
    [Space(5)]
    public bool LoadScene;
        [ShowIf("LoadScene")]
        public string Scene;
    [EndIf]


    void Awake()
    {
        //playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    void OnTriggerEnter(Collider collider)
    {
        //if(Trigger && collider.gameObject == playerMovement.gameObject) Activate();  
    }

    public void Activate()
    {
        if(LoadScene) SceneManager.LoadScene(Scene);
        if(DestroySelf) Destroy(gameObject);
    }
}
