using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReturn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player")) {
        	BarbPlayerController.instance.setHintText("I might want to go back and buy different buffs before I fight the boss.\n Press SPACE to RETURN.");
        }
    }

    void OnTriggerStay(Collider c){
    	if(c.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space)){
    		SceneManager.LoadScene("l4.5");
    	}
    }

    //BarbPlayerController.instance.setHintText("You've already looted this chest.");
}
