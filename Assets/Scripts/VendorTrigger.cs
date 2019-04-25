using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorTrigger : MonoBehaviour
{
    public static bool isOpen;

    public void TriggerVendorMenu()
    {
        Debug.Log("trigger vendor");
        isOpen = true;
        Vendor.Instance.OpenStore();
    }

    public void EndVendorMenu()
    {
        isOpen = false;
        Vendor.Instance.CloseStore();
    }
}
