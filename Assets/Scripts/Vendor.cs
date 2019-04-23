using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Vendor : MonoBehaviour
{
    //private Buff[] inventory;
    private bool seller;

    //public static Vendor Instance { get; private set; }

    public CanvasGroup MainCanvasGroup;
    public CanvasGroup VendorMenuPanel;
    public CanvasGroup OptionsButtonsCanvasGroup;
    public CanvasGroup NextButtonCanvasGroup;

    public Button Purchase1Button;
    public Button Purchase2Button;
    public Button EndDialogueButton;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(this);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        MainCanvasGroup = GetComponent<CanvasGroup>();
        if (MainCanvasGroup == null)
            Debug.Log("Couldn't find canvas group for vendor menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStore()
    {
        Debug.Log("show store menu");
        MainCanvasGroup.alpha = 1.0f;
        MainCanvasGroup.blocksRaycasts = true;
        MainCanvasGroup.interactable = true;
    }

    public void CloseStore()
    {
        Debug.Log("close store menu");
        MainCanvasGroup.alpha = 1.0f;
        MainCanvasGroup.blocksRaycasts = true;
        MainCanvasGroup.interactable = true;
    }

    public void PurchaseItem1()
    {

    }

    public void PurchaseItem2()
    {

    }

    public void SellItem1()
    {

    }

    public void SellItem2()
    {

    }
}
