using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreNPC : MonoBehaviour
{
    [SerializeField] Shop_Manager shop_manager;
    bool OpenStore=false;
    [SerializeField] Image Fkey_Image;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenStore_F();
    }

    void OpenStore_F()
    {
        if(OpenStore==true&&Input.GetKeyDown(KeyCode.F))
        {
            shop_manager.ShopActive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OpenStore = false;
        if (shop_manager.shopActiveBool)
        {
            shop_manager.ShopActive();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            OpenStore = true;
        }
    }
}
