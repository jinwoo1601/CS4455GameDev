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
    public GameObject loading_bar;
    private List<Sprite> loading_bar_sprites;


    void Start() {
    	text.text = textStrings[0];
        loading_bar_sprites = new List<Sprite>();
        for (int i = 1; i <= 5; i++)
        {
            loading_bar_sprites.Add(Resources.Load<Sprite>(string.Format("image/{0}", i)));
        }

        if (loading_bar != null)
            loading_bar.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
        	if(counter != textStrings.Length){
        		text.text = textStrings[counter];
        		images[counter - 1].enabled = false;
        		counter++;
        	} else {
                MoveToScene(level);
        	}
        }
    }


    public void MoveToScene(string scene)
    {
        loading_bar.SetActive(true);
        StartCoroutine(LoadScene(scene));
    }


    IEnumerator LoadScene(string scene)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        for (int i = 0; i < 300; i++)
        {
            if (i < 100)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[1];
            }
            else if (i >= 100 && i < 200)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[2];
            }
            else if (i >= 200)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[3];
            }
            if (i % 10 == 0)
            {
                yield return null;
            }
        }
        loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[4];
        asyncOperation.allowSceneActivation = true;
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
