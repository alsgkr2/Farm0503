using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    //[SerializeField] private float range;       //아이템 습득 거리
    private bool pickupActivated = false;       //아이템 습득 가능시 true
    //private RaycastHit hitInfo;                 //충돌체 정보 저장

    //[SerializeField] private LayerMask layerMask;

    [SerializeField] private Inventory theInventory;

    void Update()
    {
               //아이템이 사정거리 내에 있는지 확인
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            ItemInfoAppear();
            if (pickupActivated)
            {
                Debug.Log(other.gameObject.GetComponent<ItemPickUp>().item.itemName + "획득");   //인벤토리 넣기
                theInventory.AcquireItem(other.gameObject.GetComponent<ItemPickUp>().item);
                Destroy(other.gameObject);
                ItemInfoDisappear();
            }
        }
        else ItemInfoDisappear();

    }
    /*
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else ItemInfoDisappear();
    }
    */

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        //CanPickUp();
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
    }
    /*
    private void CanPickUp()        //아이템을 줍는 거리
    {
        if (pickupActivated)
        {
            print("!");
            Debug.Log(gameObject.GetComponent<ItemPickUp>().item.itemName + "획득");   //인벤토리 넣기
            theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
            Destroy(gameObject);
            ItemInfoDisappear();
        }
    }*/
}
