using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Vendor : MonoBehaviour
{

    public static Vendor Instance { get; private set; }

    public CanvasGroup MainCanvasGroup;
    public CanvasGroup VendorMenuPanel;
    public CanvasGroup PurchaseButtonsCanvasGroup;
    public CanvasGroup SellButtonsCanvasGroup;
    public CanvasGroup BuffItemPanel;

    private string buttonName;
    private Buff.BuffType curBuff;

    public Button BrowseItem1Button;
    public Button BrowseItem2Button;
    public Button BrowseItem3Button;
    public Button BrowseItem4Button;
    public Button BrowseItem5Button;
    public Button Sell1Button;
    public Button Sell2Button;
    public Button ExitStoreButton;

    public GameObject speedPrefab;
    public GameObject attackPrefab;
    public GameObject luckPrefab;
    public GameObject revivePrefab;

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

    public void BrowseItem(int type)
    {
        Buff.BuffType buffType = (Buff.BuffType)type;
        curBuff = buffType;
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        VendorMenuPanel.alpha = 0.0f;
        VendorMenuPanel.blocksRaycasts = false;
        VendorMenuPanel.interactable = false;
        BuffItemPanel.alpha = 1.0f;
        BuffItemPanel.blocksRaycasts = true;
        BuffItemPanel.interactable = true;
        FillBuffInfo(buffType);
    }

    public void PurchaseBuff()
    {
        int buffPrice = Buff.GetBuffPrice(curBuff);

        if (PlayerData.coinCount < buffPrice)
        {
            BarbPlayerController.instance.setHintText("You need more gold to buy this buff!");
            return;
        } else
        {
            PlayerData.coinCount -= buffPrice;
        }

        //maybe add a confirmation pop-up message
        //TODO: implement buff and modify player here
        Vector3 offset = new Vector3(0f, -0.03f, 0f);
        Vector3 instantiatePos = BarbPlayerController.instance.transform.position + offset;
        GameObject buffPrefab;
        switch (curBuff)
        {
            case Buff.BuffType.luck:
                buffPrefab = Instantiate(luckPrefab, instantiatePos, Quaternion.identity);
                break;
            case Buff.BuffType.attack:
                buffPrefab = Instantiate(attackPrefab, instantiatePos, Quaternion.identity);
                break;
            case Buff.BuffType.revive:
                buffPrefab = Instantiate(revivePrefab, instantiatePos, Quaternion.identity);
                break;
            case Buff.BuffType.speed:
                buffPrefab = Instantiate(speedPrefab, instantiatePos, Quaternion.identity);
                break;
            default:
                buffPrefab = null;
                break;
        }

        buffPrefab.transform.SetParent(BarbPlayerController.instance.transform);
        buffPrefab.transform.localPosition = offset;
        

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
    private void FillBuffInfo(Buff.BuffType type)
    {
        // example of how to tell which buff is being purchased
        //switch (type)
        //{
        //    case Buff.BuffType.luck:
        //        break;
        //    case Buff.BuffType.attack:
        //        break;
        //    case Buff.BuffType.revive:
        //        break;
        //    case Buff.BuffType.speed:
        //        break;
        //}

        BuffItemPanel.transform.GetChild(0).GetComponent<Text>().text = Buff.GetBuffDescription(type);
        BuffItemPanel.transform.GetChild(1).GetComponent<Text>().text = Buff.GetBuffPrice(type).ToString();
    }
}
