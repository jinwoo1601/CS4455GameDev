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
    //public CanvasGroup PurchaseButtonsCanvasGroup;
    //public CanvasGroup SellButtonsCanvasGroup;
    public CanvasGroup BuffItemPanel;
    public CanvasGroup SellItemPanel;

    private string buttonName;
    private Buff.BuffType curBuff;

    public Button BrowseItem1Button;
    public Button BrowseItem2Button;
    public Button BrowseItem3Button;
    public Button BrowseItem4Button;
    //public Button BrowseItem5Button;
    public Button Sell1Button;
    public Button Sell2Button;
    public Button ExitStoreButton;

    public GameObject speedPrefab;
    public GameObject attackPrefab;
    public GameObject luckPrefab;
    public GameObject revivePrefab;

    int buffNumber;

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
        SellItemPanel.alpha = 0.0f;
        SellItemPanel.blocksRaycasts = false;
        SellItemPanel.interactable = false;
        BrowseItem1Button.GetComponentInChildren<Text>().text = Buff.BuffType.speed.ToString();
        BrowseItem2Button.GetComponentInChildren<Text>().text = Buff.BuffType.attack.ToString();
        BrowseItem3Button.GetComponentInChildren<Text>().text = Buff.BuffType.luck.ToString();
        BrowseItem4Button.GetComponentInChildren<Text>().text = Buff.BuffType.revive.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.buffs.Count == 2)
        {
            Sell1Button.GetComponentInChildren<Text>().text = "Refund " + PlayerData.buffs[0].ToString();
            Sell2Button.GetComponentInChildren<Text>().text = "Refund " + PlayerData.buffs[1].ToString();
            Sell1Button.GetComponent<CanvasGroup>().alpha = 1.0f;
            Sell1Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Sell1Button.GetComponent<CanvasGroup>().interactable = true;
            Sell2Button.GetComponent<CanvasGroup>().alpha = 1.0f;
            Sell2Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Sell2Button.GetComponent<CanvasGroup>().interactable = true;
        }
        else if (PlayerData.buffs.Count == 1)
        {
            if (PlayerData.buffAuroras[0] != null)
            {
                Sell1Button.GetComponentInChildren<Text>().text = "Refund " + PlayerData.buffs[0].ToString();
                Sell2Button.GetComponentInChildren<Text>().text = "You only have one buff";
                Sell1Button.GetComponent<CanvasGroup>().alpha = 1.0f;
                Sell1Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Sell1Button.GetComponent<CanvasGroup>().interactable = true;
                Sell2Button.GetComponent<CanvasGroup>().alpha = 0.5f;
                Sell2Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
                Sell2Button.GetComponent<CanvasGroup>().interactable = false;
            } else if (PlayerData.buffAuroras[1] != null)
            {
                Sell2Button.GetComponentInChildren<Text>().text = "Refund " + PlayerData.buffs[1].ToString();
                Sell1Button.GetComponentInChildren<Text>().text = "You only have one buff";
                Sell1Button.GetComponent<CanvasGroup>().alpha = 0.5f;
                Sell1Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
                Sell1Button.GetComponent<CanvasGroup>().interactable = false;
                Sell2Button.GetComponent<CanvasGroup>().alpha = 1.0f;
                Sell2Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Sell2Button.GetComponent<CanvasGroup>().interactable = true;
            }
        } else
        {
            Sell1Button.GetComponent<CanvasGroup>().alpha = 0.5f;
            Sell1Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Sell1Button.GetComponent<CanvasGroup>().interactable = false;
            Sell2Button.GetComponent<CanvasGroup>().alpha = 0.5f;
            Sell2Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Sell2Button.GetComponent<CanvasGroup>().interactable = false;
        }
        if (PlayerData.buffs.Count > 1)
        {
            BrowseItem1Button.GetComponent<CanvasGroup>().alpha = 0.5f;
            BrowseItem1Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
            BrowseItem1Button.GetComponent<CanvasGroup>().interactable = false;
            BrowseItem2Button.GetComponent<CanvasGroup>().alpha = 0.5f;
            BrowseItem2Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
            BrowseItem2Button.GetComponent<CanvasGroup>().interactable = false;
            BrowseItem3Button.GetComponent<CanvasGroup>().alpha = 0.5f;
            BrowseItem3Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
            BrowseItem3Button.GetComponent<CanvasGroup>().interactable = false;
            BrowseItem4Button.GetComponent<CanvasGroup>().alpha = 0.5f;
            BrowseItem4Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
            BrowseItem4Button.GetComponent<CanvasGroup>().interactable = false;
        }
        else
        {
            if (PlayerData.buffs.Contains(Buff.BuffType.speed))
            {
                BrowseItem1Button.GetComponent<CanvasGroup>().alpha = 0.5f;
                BrowseItem1Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
                BrowseItem1Button.GetComponent<CanvasGroup>().interactable = false;
            } else
            {
                BrowseItem1Button.GetComponent<CanvasGroup>().alpha = 1.0f;
                BrowseItem1Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                BrowseItem1Button.GetComponent<CanvasGroup>().interactable = true;
            }
            if (PlayerData.buffs.Contains(Buff.BuffType.attack))
            {
                BrowseItem2Button.GetComponent<CanvasGroup>().alpha = 0.5f;
                BrowseItem2Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
                BrowseItem2Button.GetComponent<CanvasGroup>().interactable = false;
            } else
            {
                BrowseItem2Button.GetComponent<CanvasGroup>().alpha = 1.0f;
                BrowseItem2Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                BrowseItem2Button.GetComponent<CanvasGroup>().interactable = true;
            }
            if (PlayerData.buffs.Contains(Buff.BuffType.luck))
            {
                BrowseItem3Button.GetComponent<CanvasGroup>().alpha = 0.5f;
                BrowseItem3Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
                BrowseItem3Button.GetComponent<CanvasGroup>().interactable = false;
            } else
            {
                BrowseItem3Button.GetComponent<CanvasGroup>().alpha = 1.0f;
                BrowseItem3Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                BrowseItem3Button.GetComponent<CanvasGroup>().interactable = true;
            }
            if (PlayerData.buffs.Contains(Buff.BuffType.revive))
            {
                BrowseItem4Button.GetComponent<CanvasGroup>().alpha = 0.5f;
                BrowseItem4Button.GetComponent<CanvasGroup>().blocksRaycasts = false;
                BrowseItem4Button.GetComponent<CanvasGroup>().interactable = false;
            } else
            {
                BrowseItem4Button.GetComponent<CanvasGroup>().alpha = 1.0f;
                BrowseItem4Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                BrowseItem4Button.GetComponent<CanvasGroup>().interactable = true;
            }
        }
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
        FillBuffInfo(buffType, false);
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
        CreateBuff(curBuff);

        //use buttonName and switch statements to determine what buff
        //has been purchased
        BarbPlayerController.instance.setHintText("Buff Purchased!");

        BuffItemPanel.alpha = 0.0f;
        BuffItemPanel.blocksRaycasts = false;
        BuffItemPanel.interactable = false;
        VendorMenuPanel.alpha = 1.0f;
        VendorMenuPanel.blocksRaycasts = true;
        VendorMenuPanel.interactable = true;
    }

    public void SellBuff()
    {
        int buffPrice = Buff.GetBuffPrice(PlayerData.buffs[buffNumber]);
        PlayerData.coinCount += buffPrice / 2;
        BarbPlayerController.instance.removeBuff(PlayerData.buffs[buffNumber]);
        PlayerData.buffs.RemoveAt(buffNumber);
        Destroy(PlayerData.buffAuroras[buffNumber]);
        PlayerData.buffAuroras.RemoveAt(buffNumber);
        BarbPlayerController.instance.setHintText("Buff Refunded!");
        SellItemPanel.alpha = 0.0f;
        SellItemPanel.blocksRaycasts = false;
        SellItemPanel.interactable = false;
        VendorMenuPanel.alpha = 1.0f;
        VendorMenuPanel.blocksRaycasts = true;
        VendorMenuPanel.interactable = true;
    }

    public void BackToStore()
    {
        BuffItemPanel.alpha = 0.0f;
        BuffItemPanel.blocksRaycasts = false;
        BuffItemPanel.interactable = false;
        SellItemPanel.alpha = 0.0f;
        SellItemPanel.blocksRaycasts = false;
        SellItemPanel.interactable = false;
        VendorMenuPanel.alpha = 1.0f;
        VendorMenuPanel.blocksRaycasts = true;
        VendorMenuPanel.interactable = true;
    }

    public void SellItem(int buffNumber)
    {
        this.buffNumber = buffNumber;
        VendorMenuPanel.alpha = 0.0f;
        VendorMenuPanel.blocksRaycasts = false;
        VendorMenuPanel.interactable = false;
        SellItemPanel.alpha = 1.0f;
        SellItemPanel.blocksRaycasts = true;
        SellItemPanel.interactable = true;
        FillBuffInfo(PlayerData.buffs[buffNumber], true);
    }

    public void CreateBuff(Buff.BuffType type)
    {
        //if (PlayerData.buffs.Count > 1)
        //{
        //    BarbPlayerController.instance.removeBuff(PlayerData.buffs[0]);
        //    PlayerData.buffs.RemoveAt(0);
        //    Destroy(PlayerData.buffAuroras[0]);
        //    PlayerData.buffAuroras.RemoveAt(0);
        //}

        Vector3 offset = new Vector3(0f, -0.03f, 0f);
        Vector3 instantiatePos = BarbPlayerController.instance.transform.position + offset;
        GameObject buffPrefab;
        switch (type)
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
        PlayerData.buffs.Add(type);
        PlayerData.buffAuroras.Add(buffPrefab);
        BarbPlayerController.instance.addBuff(curBuff);
    }

    // Once we implement the buffs, this is how we fill in the menu with appropriate texts
    private void FillBuffInfo(Buff.BuffType type, bool isSale)
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

        
        if (!isSale)
        {
            BuffItemPanel.transform.GetChild(0).GetComponent<Text>().text = Buff.GetBuffDescription(type);
            BuffItemPanel.transform.GetChild(1).GetComponent<Text>().text = "Price: " + Buff.GetBuffPrice(type).ToString();
        } else
        {
            SellItemPanel.transform.GetChild(0).GetComponent<Text>().text = Buff.GetBuffDescription(type);
            SellItemPanel.transform.GetChild(1).GetComponent<Text>().text = "Refund: " + (Buff.GetBuffPrice(type)/2).ToString();
        }
    }
}
