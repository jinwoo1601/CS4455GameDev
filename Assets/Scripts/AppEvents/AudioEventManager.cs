using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;
    
    public AudioClip[] FootstepAudio;
    public AudioClip[] mFootstepAudio;
    public AudioClip[] attackAudio;
    public AudioClip mDeathAudio;
    public AudioClip coinAudio;


    private UnityAction<Vector3> FootstepEventListener;
    private UnityAction<Vector3> mFootstepEventListener;
    private UnityAction<Vector3> attackEventListener;
    private UnityAction<Vector3> mDeathEventListener;
    private UnityAction<Vector3> coinEventListener;

    void Awake()
    {

        FootstepEventListener = new UnityAction<Vector3>(FootstepEventHandler);
        mFootstepEventListener = new UnityAction<Vector3>(mFootstepEventHandler);
        attackEventListener = new UnityAction<Vector3>(attackEventHandler);
        mDeathEventListener = new UnityAction<Vector3>(mDeathEventHandler);
        coinEventListener = new UnityAction<Vector3>(coinEventHandler);
    }


    // Use this for initialization
    void Start()
    {


        			
    }


    void OnEnable()
    {
        EventManager.StartListening<FootstepEvent, Vector3>(FootstepEventListener);
        EventManager.StartListening<mFootstepEvent, Vector3>(mFootstepEventListener);
        EventManager.StartListening<attackEvent, Vector3>(attackEventListener);
        EventManager.StartListening<mDeathEvent, Vector3>(mDeathEventListener);
        EventManager.StartListening<coinEvent, Vector3>(coinEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<FootstepEvent, Vector3>(FootstepEventListener);
        EventManager.StopListening<mFootstepEvent, Vector3>(mFootstepEventListener);
        EventManager.StopListening<attackEvent, Vector3>(attackEventListener);
        EventManager.StopListening<mDeathEvent, Vector3>(mDeathEventListener);
        EventManager.StopListening<coinEvent, Vector3>(coinEventListener);
    }




    void FootstepEventHandler(Vector3 pos) {


        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.FootstepAudio[Random.Range(0, FootstepAudio.Length)];

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }



    void mFootstepEventHandler(Vector3 pos)
    {

        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.mFootstepAudio[Random.Range(0, mFootstepAudio.Length)];

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


    void mDeathEventHandler(Vector3 pos)
    {

        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.mDeathAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void coinEventHandler(Vector3 pos)
    {

        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.coinAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }
}
