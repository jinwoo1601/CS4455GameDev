using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;
    
    public AudioClip[] FootstepAudio;


    private UnityAction<Vector3> FootstepEventListener;

    void Awake()
    {
        

        FootstepEventListener = new UnityAction<Vector3>(FootstepEventHandler);
    }


    // Use this for initialization
    void Start()
    {


        			
    }


    void OnEnable()
    {
        
        EventManager.StartListening<FootstepEvent, Vector3>(FootstepEventListener);

    }

    void OnDisable()
    {
        EventManager.StopListening<FootstepEvent, Vector3>(FootstepEventListener);
    }




    void FootstepEventHandler(Vector3 pos) {


        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.FootstepAudio[Random.Range(0, FootstepAudio.Length)];

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();


    }
}
