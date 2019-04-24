using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public string[] textStrings;
    public Text text;
    private int counter = 1;
    public RawImage[] images;
    public string level;

    void Start() {
    	text.text = textStrings[0];
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
        	if(counter != textStrings.Length){
        		text.text = textStrings[counter];
        		images[counter - 1].enabled = false;
        		counter++;
        	} else {
        		SceneManager.LoadScene(level);
        	}
        }
    }
}
