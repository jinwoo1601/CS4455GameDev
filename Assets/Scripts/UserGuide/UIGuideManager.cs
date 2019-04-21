using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIGuideManager : MonoBehaviour, StateMachineListener
{
    protected static UIGuideManager s_Instance;
    public static UIGuideManager instance { get { return s_Instance; } }
    public GameObject panel;
    public GameObject gate;
    public Text textPad;
    public Text textPadBottom;
    public string end_word = "Now we could start our journey from the door, let's go!";
    public Light[] to_light;

    private bool enable_listener = true;


    private KeyCode[] interestedKeyCode = { KeyCode.Space, KeyCode.Space , KeyCode.W, KeyCode.S, KeyCode.A,  KeyCode.D, KeyCode.K, KeyCode.J
        , KeyCode.None, KeyCode.None, KeyCode.K, KeyCode.Mouse0, KeyCode.Mouse1 };

    private int counter = -1;

    public string[] texts = {
        "Hi, Ellen! Welcome to our journey!",
        "Before you start your journey, let me give you some tips",
        "You could use W to move forward" ,
        "You could use S to move backward" ,
        "You could use A to turn left" ,
        "You could use D to turn right" ,
        "Click K to Pull Out Your Axe",
        "Now you try to attack by clicking J",
        "Great, now try to click j multiple times",
        "You can attack at any time, even running",
        "Click K again to Take Back AXE",
        "Click left mouse to select the items",
        "You could shange your view by pressing right mouse and rotate"
    };

    public string[] inputIntructions = {
        "PRESS SPACE to CONTINUE", 
        "PRESS SPACE to CONTINUE",
        "PRESS W to MOVE",
        "PRESS S to MOVE",
        "PRESS A to MOVE",
        "PRESS D to MOVE",
        "PRESS K to ARM",
        "CLICK J to ATTACK",
        "Click J Multiple Times to get combo",
        "Click W & J to enable jump hit",
        "PRESS K Again to Take Back AXE",
        "PRESS LEFT MOUSE to CONTINUE",
        "PRESS RIGHT MOUSE to ROTATE" };

    // Start is called before the first frame update
    void Start()
    {
        s_Instance = this;
        PlayerInput.Instance.enable = false;
    }

    void FixedUpdate()
    {

        if (counter > -1 && counter < interestedKeyCode.Length && interestedKeyCode[counter] != KeyCode.None && Input.GetKeyDown(interestedKeyCode[counter]))
        {
            showNext();
            if (counter > 1)
            {
                PlayerInput.Instance.enable = true;
            }
        }
        else if (counter > -1 && counter < interestedKeyCode.Length && interestedKeyCode[counter] == KeyCode.None) {
            enable_listener = true;
        }
        

    }

    private bool showNext() {
        ++counter;
        if (counter < interestedKeyCode.Length)
        {
            textPad.text = texts[counter];
            textPadBottom.text = inputIntructions[counter];
        }
        else {
            textPadBottom.text = "";
            textPad.text = end_word;
            foreach (Light l in to_light) {
                l.intensity = 5;
            }
            gate.SetActive(false);
        }
        return counter < texts.Length && counter < inputIntructions.Length;
    }

    public void triggerStateEvent(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enable_listener)
        {
            panel.SetActive(true);

            showNext();
            enable_listener = false;
        }
        
    }

}
