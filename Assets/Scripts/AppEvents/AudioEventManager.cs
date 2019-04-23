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
    public AudioClip stanceAudio;
    public AudioClip drawAudio;
    public AudioClip sheathAudio;
    public AudioClip[] DeathAudio;
    public AudioClip mDeathAudio;

    public AudioClip[] crateAudio;
    public AudioClip coinAudio;
    public AudioClip keyAudio;
    public AudioClip treasureAudio;
    public AudioClip glassAudio;
    public AudioClip doorAudio;


    private UnityAction<Vector3> FootstepEventListener;
    private UnityAction<Vector3> mFootstepEventListener;
    private UnityAction<Vector3> attackEventListener;
    private UnityAction<Vector3> stanceEventListener;
    private UnityAction<Vector3> drawEventListener;
    private UnityAction<Vector3> sheathEventListener;
    private UnityAction<Vector3> DeathEventListener;
    private UnityAction<Vector3> mDeathEventListener;

    private UnityAction<Vector3> crateEventListener;
    private UnityAction<Vector3> coinEventListener;
    private UnityAction<Vector3> keyEventListener;
    private UnityAction<Vector3> treasureEventListener;
    private UnityAction<Vector3> glassEventListener;
    private UnityAction<Vector3> doorEventListener;

    void Awake()
    {

        FootstepEventListener = new UnityAction<Vector3>(FootstepEventHandler);
        mFootstepEventListener = new UnityAction<Vector3>(mFootstepEventHandler);
        attackEventListener = new UnityAction<Vector3>(attackEventHandler);
        drawEventListener = new UnityAction<Vector3>(drawEventHandler);
        sheathEventListener = new UnityAction<Vector3>(sheathEventHandler);
        stanceEventListener = new UnityAction<Vector3>(stanceEventHandler);
        DeathEventListener = new UnityAction<Vector3>(DeathEventHandler);
        mDeathEventListener = new UnityAction<Vector3>(mDeathEventHandler);

        crateEventListener = new UnityAction<Vector3>(crateEventHandler);
        coinEventListener = new UnityAction<Vector3>(coinEventHandler);
        keyEventListener = new UnityAction<Vector3>(keyEventHandler);
        treasureEventListener = new UnityAction<Vector3>(treasureEventHandler);
        glassEventListener = new UnityAction<Vector3>(glassEventHandler);
        doorEventListener = new UnityAction<Vector3>(doorEventHandler);
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
        EventManager.StartListening<drawEvent, Vector3>(drawEventListener);
        EventManager.StartListening<sheathEvent, Vector3>(sheathEventListener);
        EventManager.StartListening<stanceEvent, Vector3>(stanceEventListener);
        EventManager.StartListening<DeathEvent, Vector3>(DeathEventListener);
        EventManager.StartListening<mDeathEvent, Vector3>(mDeathEventListener);

        EventManager.StartListening<crateEvent, Vector3>(crateEventListener);
        EventManager.StartListening<coinEvent, Vector3>(coinEventListener);
        EventManager.StartListening<keyEvent, Vector3>(keyEventListener);
        EventManager.StartListening<treasureEvent, Vector3>(treasureEventListener);
        EventManager.StartListening<glassEvent, Vector3>(glassEventListener);
        EventManager.StartListening<doorEvent, Vector3>(doorEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<FootstepEvent, Vector3>(FootstepEventListener);
        EventManager.StopListening<mFootstepEvent, Vector3>(mFootstepEventListener);
        EventManager.StopListening<attackEvent, Vector3>(attackEventListener);
        EventManager.StopListening<drawEvent, Vector3>(drawEventListener);
        EventManager.StopListening<sheathEvent, Vector3>(sheathEventListener);
        EventManager.StopListening<stanceEvent, Vector3>(stanceEventListener);
        EventManager.StopListening<DeathEvent, Vector3>(DeathEventListener);
        EventManager.StopListening<mDeathEvent, Vector3>(mDeathEventListener);

        EventManager.StopListening<crateEvent, Vector3>(crateEventListener);
        EventManager.StopListening<coinEvent, Vector3>(coinEventListener);
        EventManager.StopListening<keyEvent, Vector3>(keyEventListener);
        EventManager.StopListening<treasureEvent, Vector3>(treasureEventListener);
        EventManager.StopListening<glassEvent, Vector3>(glassEventListener);
        EventManager.StopListening<doorEvent, Vector3>(doorEventListener);
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

    void drawEventHandler(Vector3 pos)
    {
        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.drawAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void sheathEventHandler(Vector3 pos)
    {
        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.sheathAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void stanceEventHandler(Vector3 pos)
    {
        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.stanceAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void DeathEventHandler(Vector3 pos)
    {
        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.DeathAudio[Random.Range(0, DeathAudio.Length)];

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

    void crateEventHandler(Vector3 pos)
    {

        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.crateAudio[Random.Range(0, crateAudio.Length)];

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

    void keyEventHandler(Vector3 pos)
    {

        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.keyAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void treasureEventHandler(Vector3 pos)
    {

        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.treasureAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void glassEventHandler(Vector3 pos)
    {
        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.glassAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void doorEventHandler(Vector3 pos)
    {
        EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

        snd.audioSrc.clip = this.doorAudio;

        snd.audioSrc.minDistance = 5f;
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }
}
