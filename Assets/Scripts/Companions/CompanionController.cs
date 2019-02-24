using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    public GameObject Player;
    public bool following = false;

    NavMeshAgent navMeshAgent;
    DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        if (dialogueTrigger == null)
            Debug.Log("Couldn't find dialogue trigger");


    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.Log("Couldn't find NavMeshAgent");
    }

    void Update()
    {
        Vector3 playerPos = Player.transform.position;
        if (following && Vector3.Distance(transform.position, playerPos) > 5)
        {
            Vector3 range = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
            navMeshAgent.SetDestination(Player.transform.position + range);
        }
    }


    public void Picked()
    {
        Debug.Log("picked");
        following = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!following && other.CompareTag("Player"))
        {
            Debug.Log("trigger enter");
            dialogueTrigger.TriggerDialogue();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!following && other.CompareTag("Player"))
        {
            Debug.Log("trigger exit");
            dialogueTrigger.EndDialogue();
        }
    }
}
