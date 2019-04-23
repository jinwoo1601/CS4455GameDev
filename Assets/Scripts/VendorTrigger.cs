using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorTrigger : MonoBehaviour
{
    public void TriggerVendorMenu()
    {
        Debug.Log("trigger vendor");
        Vendor.Instance.OpenStore();
    }

    public void EndVendorMenu()
    {
        Vendor.Instance.CloseStore();
    }
}
