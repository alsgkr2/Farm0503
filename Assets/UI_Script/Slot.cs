using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    public int itemCount;

    [SerializeField] private Text text_Count;
    [SerializeField] private GameObject go_CountImage;      //������ ���� ui�� ��Ȱ��ȭ/Ȱ��ȭ�� ���� ����


    //������ �̹����� ������ ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //�κ��� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Equipment)    //���� �������� ���Ⱑ �ƴ� �� ������ ���� ui Ȱ��ȭ
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        SetColor(1);
    }

    //�ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)      //�������� �ϳ��� ���� �� �ش� ���� �ʱ�ȭ
        {
            ClearSlot();
            GameObject.Find("Inven_SelcetMenu").SetActive(false);
        }
    }

    //�ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}