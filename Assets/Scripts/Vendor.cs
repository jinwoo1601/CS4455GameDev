using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Vendor : MonoBehaviour
{

    public static Vendor Instance { get; private set; }

    public CanvasGroup MainCanvasGroup;
    public CanvasGroup VendorMenuPanel;
    public CanvasGroup PurchaseButtonsCanvasGroup;
    public CanvasGroup SellButtonsCanvasGroup;
    public CanvasGroup BuffItemPanel;

    private string buttonName;

    public Button BrowseItem1Button;
    public Button BrowseItem2Button;
    public Button BrowseItem3Button;
    public Button BrowseItem4Button;
    public Button BrowseItem5Button;
    public Button Sell1Button;
    public Button Sell2Button;
    public Button ExitStoreButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MainCanvasGroup = GetComponent<CanvasGroup>();
        if (MainCanvasGroup == null)
            Debug.Log("Couldn't find canvas group for vendor menu");
        BuffItemPanel.alpha = 0.0f;
        BuffItemPanel.blocksRaycasts = false;
        BuffItemPanel.interactable = false;
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
        MainCanvasGroup.alpha = 0.0f;
        MainCanvasGroup.blocksRaycasts = false;
        MainCanvasGroup.interactable = false;
    }

    public void BrowseItem()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        VendorMenuPanel.alpha = 0.0f;
        VendorMenuPanel.blocksRaycasts = false;
        VendorMenuPanel.interactable = false;
        BuffItemPanel.alpha = 1.0f;
        BuffItemPanel.blocksRaycasts = true;
        BuffItemPanel.interactable = true;
        FillBuffInfo(buttonName);
    }

    public void PurchaseBuff()
    {
        //maybe add a confirmation pop-up message
        //TODO: implement buff and modify player here
        //use buttonName and switch statements to determine what buff
        //has been purchased
        BuffItemPanel.alpha = 0.0f;
        BuffItemPanel.blocksRaycasts = false;
        BuffItemPanel.interactable = false;
        VendorMenuPanel.alpha = 1.0f;
        VendorMenuPanel.blocksRaycasts = true;
        VendorMenuPanel.interactable = true;
    }

    public void BackToStore()
    {
        BuffItemPanel.alpha = 0.0f;
        BuffItemPanel.blocksRaycasts = false;
        BuffItemPanel.interactable = false;
        VendorMenuPanel.alpha = 1.0f;
        VendorMenuPanel.blocksRaycasts = true;
        VendorMenuPanel.interactable = true;
    }

    public void SellItem1()
    {

    }

    public void SellItem2()
    {

    }

    // Once we implement the buffs, this is how we fill in the menu with appropriate texts
    private void FillBuffInfo(string buttonName)
    {
        // example of how to tell which buff is being purchased
        switch(buttonName)
        {
            case "Attack Buff":
                break;
        }
        BuffItemPanel.transform.GetChild(0).GetComponent<Text>().text = "This is where buff description appears";
        BuffItemPanel.transform.GetChild(1).GetComponent<Text>().text = "This is where buff price appears";
    }
}
