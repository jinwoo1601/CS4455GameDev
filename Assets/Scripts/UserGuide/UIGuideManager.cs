using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIGuideManager : MonoBehaviour
{
    protected static UIGuideManager s_Instance;
    public static UIGuideManager instance { get { return s_Instance; } }
    public GameObject panel;
    public GameObject gate;
    public Text textPad;
    public Text textPadBottom;
    private string end_word = "I will not rest until I defeat the Goblin Queen!";
    public Light[] to_light;
    public KeyEnabler KE;

    private bool enable_listener = true;


    private KeyCode[] interestedKeyCode = { KeyCode.Space, KeyCode.Space , KeyCode.Space, KeyCode.Space, KeyCode.Space, KeyCode.K, KeyCode.J, KeyCode.None , KeyCode.None, KeyCode.K, KeyCode.Space };

    private int counter = -1;

    private string[] texts = {
        "A small blacksmith’s shop in the early morning.\n There is nothing more peaceful.\n A single bird chirps as I light the furnace fire.",
        "A knock on the door tells me my morning will soon be ruined.",
        "As the only blacksmith in Neverwear I bring in good money.\n But ever since the Goblin Queen invaded and raised taxes, \n even I have difficulty affording food.",
        "Each morning the tax collector comes. Anyone who \n refuses to pay is sent to the Goblin Queen’s dungeon \n and they are never seen again.",
        "I have the ability to fight. I’ve been working \n with blades my entire life. I must stand up for \n the freedom of my village.",
        "But first, maybe some practice swings.",
        "But first, maybe some practice swings." ,
        "But first, maybe some practice swings." ,
        "But first, maybe some practice swings." ,
        "That's probably enough practice.",
        "I will unlock my door and face the day! \n I might not be well trained but I will do my best."
    };

    private string[] inputIntructions = {
        "Press SPACE to CONTINUE", 
        "Press SPACE to CONTINUE",
        "Press SPACE to CONTINUE",
        "Press SPACE to CONTINUE",
        "Press SPACE to CONTINUE",
        "Press K to unsheathe axe",
        "Press J to ATTACK",
        "Press J quickly to combo ATTACK",
        "Press J while walking forward to JUMP ATTACK",
        "Press K to sheathe axe",
        "Press Space to interact with objects"};

    // Start is called before the first frame update
    void Start()
    {
        s_Instance = this;
        PlayerInput.Instance.enable = false;
    }

    void Update()
    {

        if (counter > -1 && counter < interestedKeyCode.Length && interestedKeyCode[counter] != KeyCode.None && Input.GetKeyDown(interestedKeyCode[counter]))
        {
            showNext();
            if (counter >= -1)
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
            //gate.SetActive(false);
        }

        if(counter == 10){
            KE.EnableKey();
        }
        return counter < texts.Length && counter < inputIntructions.Length;
    }

    public bool triggerStateEvent(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enable_listener)
        {
            panel.SetActive(true);

             
            new WaitForSecondsRealtime(8);

            showNext();
            enable_listener = false;
            return true;
        }
        else
            return false;
        
    }

}
