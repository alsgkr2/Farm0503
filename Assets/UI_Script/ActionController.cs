using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    //[SerializeField] private float range;       //������ ���� �Ÿ�
    private bool pickupActivated = false;       //������ ���� ���ɽ� true
    //private RaycastHit hitInfo;                 //�浹ü ���� ����

    //[SerializeField] private LayerMask layerMask;

    [SerializeField] private Inventory theInventory;

    void Update()
    {
               //�������� �����Ÿ� ���� �ִ��� Ȯ��
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            ItemInfoAppear();
            if (pickupActivated)
            {
                Debug.Log(other.gameObject.GetComponent<ItemPickUp>().item.itemName + "ȹ��");   //�κ��丮 �ֱ�
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
    private void CanPickUp()        //�������� �ݴ� �Ÿ�
    {
        if (pickupActivated)
        {
            print("!");
            Debug.Log(gameObject.GetComponent<ItemPickUp>().item.itemName + "ȹ��");   //�κ��丮 �ֱ�
            theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
            Destroy(gameObject);
            ItemInfoDisappear();
        }
    }*/
}
