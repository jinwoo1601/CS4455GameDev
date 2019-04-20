using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;
    
    public AudioClip[] FootstepAudio;
    public AudioClip[] attackAudio;


    private UnityAction<Vector3> FootstepEventListener;
    private UnityAction<Vector3> attackEventListener;

    void Awake()
    {

        FootstepEventListener = new UnityAction<Vector3>(FootstepEventHandler);
        attackEventListener = new UnityAction<Vector3>(attackEventHandler);
    }


    // Use this for initialization
    void Start()
    {


        			
    }


    void OnEnable()
    {
        EventManager.StartListening<attackEvent, Vector3>(attackEventListener);
        EventManager.StartListening<FootstepEvent, Vector3>(FootstepEventListener);

    }

    void OnDisable()
    {
        EventManager.StopListening<attackEvent, Vector3>(attackEventListener);
        EventManager.StopListening<FootstepEvent, Vector3>(FootstepEventListener);
    }




    void FootstepEventHandler(Vector3 pos) {


        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.FootstepAudio[Random.Range(0, FootstepAudio.Length)];

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();


    }

    void attackEventHandler(Vector3 pos)
    {


        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.attackAudio[Random.Range(0, attackAudio.Length)];

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();


    }
}
