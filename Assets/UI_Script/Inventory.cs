using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    //�κ��丮 Ȱ��ȭ ����, true�� �� ī�޶� �����Ӱ� �ٸ� �Է��� ����
    public static bool inventoryActivated = false;
    public static bool SeedInventoryActivated = false;

    [SerializeField] private GameObject go_InventoryBase;       //inventory_Base �̹���
    [SerializeField] private GameObject go_SlotsParent;         //grid setting
    [SerializeField] private GameObject go_SlotsParent_2;         //grid setting
    [SerializeField] private UIManager ui_Manager;

    [SerializeField] private GameObject Seed_InventoryBase;
    [SerializeField] private GameObject Seed_SlotsParent;

    private Slot[] slots;           // ���Ե� �迭
    public Slot[] seedSlots;

    [SerializeField] private GameObject Seed_SelectImage_1;
    [SerializeField] private GameObject Seed_SelectImage_2;
    [SerializeField] private GameObject Seed_SelectImage_3;
    [SerializeField] private GameObject Seed_SelectImage_4;

    public int TapCount = 0;
    public int NameTextCount = 0;

    [SerializeField]
    private Farm_Move farm_Move;

    GameObject seedChange;

    [SerializeField] private GameObject Inven_selcet_img_1;
    [SerializeField] private GameObject Inven_selcet_img_2;

    public Text seed_NameText;
    [SerializeField] private GameObject Inven_SelcetMenu;

    [SerializeField] private GameObject Sell_ButtonObject;

    private Item Select_ItmeData;

    // [SerializeField] private Gauge Gauge;

    Shop_Manager shop_Manager;
    Pirodo pirodo;
    public Gauge Gauge;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        List<Slot> slotsList = slots.ToList();
        slots = go_SlotsParent_2.GetComponentsInChildren<Slot>();
        for(int i = 0; i < slots.Length; i++)
        {
            slotsList.Add(slots[i]);
        }
        slots = slotsList.ToArray();
        seedSlots = Seed_SlotsParent.GetComponentsInChildren<Slot>();
        Inven_selcet_img_1.SetActive(true);
        Inven_selcet_img_2.SetActive(false);
        CloseInventory();
        Seed_SelectImage_1.SetActive(true);
        Seed_SelectImage_2.SetActive(false);
        Seed_SelectImage_3.SetActive(false);
        Seed_SelectImage_4.SetActive(false);
        seed_NameText.text = "";

        shop_Manager = GameObject.Find("Shop_Manager").GetComponent<Shop_Manager>();
        pirodo = GameObject.Find("Player").GetComponent<Pirodo>();

        Sell_ButtonObject.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
        SelectSeedInventory();
        MousePoint();
    }

    public void first_Inven_Button()
    {
        GameObject first = GameObject.Find("1st_Inven");
        GameObject second = GameObject.Find("2nd_Inven");
        if(first.transform.GetSiblingIndex() < second.transform.GetSiblingIndex())
        {
            first.transform.SetSiblingIndex(second.transform.GetSiblingIndex());
        }
        Inven_selcet_img_1.SetActive(true);
        Inven_selcet_img_2.SetActive(false);

    }
    public void second_Inven_Button()
    {
        GameObject first = GameObject.Find("1st_Inven");
        GameObject second = GameObject.Find("2nd_Inven");
        if (first.transform.GetSiblingIndex() > second.transform.GetSiblingIndex())
        {
            second.transform.SetSiblingIndex(first.transform.GetSiblingIndex());
        }
        Inven_selcet_img_1.SetActive(false);
        Inven_selcet_img_2.SetActive(true);
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    //�κ��丮 ui ���� �� �ݱ�
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    //�κ��丮 ���� ���� �������� ������ ī��Ʈ �߰� or ���� �� ���Կ� ������ �̹��� �߰� �� ī��Ʈ �߰�
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.itemName == _item.itemName)
                {
                    if (slots[i].itemCount <= 0 && _count == -1) return;
                    else if (_count == -1) shop_Manager.money += 250;
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (slots[i].itemCount <= 0 && _count == -1) return;
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    //���� �����ϳ� �����ǿ��� ����ϱ� ���� �� ��Ḧ ��ȯ�� �� ���
    public void AcquireItem_recipe(Item _item, int _count)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    //�����ǿ��� �̹����� �÷��� �����Ͽ� Ȱ��ȭ �Ǿ����� ��Ÿ�� ��
    public void CheckItem_1(Item _item, GameObject clickObject, int _count = -1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    if(clickObject.tag == "Recipe2")
                    {
                        slots[i].SetSlotCount(_count);
                        //ui_Manager.Food_Image_Change_2(clickObject);
                        return;
                    }
                    else
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
    }

    //�����ǿ��� Ư�� �������� ���������� �־���� ��
    public void CheckItem_2(Item _item, int _count = -1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {

                    slots[i].SetSlotCount(_count);

                    return;

                }
            }
        }
    }

    public bool Check_Inven(Item _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    return true;
                }
            }
            else if (slots.Length - 1 == i && slots[i].item == null)
            {
                return false;
            }
        }
        return false;
    }


    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

    //���� �κ��丮 
    private void SelectSeedInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (TapCount < 3) TapCount++;
            else TapCount = 0;
            NameTextCount = 0;

            if (TapCount == 0)
            {
                Seed_SelectImage_1.SetActive(true);
                Seed_SelectImage_4.SetActive(false);
            }
            else if(TapCount == 1)
            {
                Seed_SelectImage_1.SetActive(false);
                Seed_SelectImage_2.SetActive(true);
            }
            else if (TapCount == 2)
            {
                Seed_SelectImage_2.SetActive(false);
                Seed_SelectImage_3.SetActive(true);
            }
            else if (TapCount == 3)
            {
                Seed_SelectImage_3.SetActive(false);
                Seed_SelectImage_4.SetActive(true);
            }
        }

        if (TapCount == 0)
        {
            if (seedSlots[0].item != null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = seedSlots[0].item.itemPrefab;
                if(NameTextCount == 0)
                {
                    seed_NameText.text = seedSlots[0].item.itemName;
                    NameTextCount++;
                    StopCoroutine("seedNameTextWait");
                    StartCoroutine("seedNameTextWait");
                }
            }
            else if (seedSlots[0].item == null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = null;
            }
        }
        else if (TapCount == 1)
        {
            if (seedSlots[1].item != null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = seedSlots[1].item.itemPrefab;
                if(NameTextCount == 0)
                {
                    seed_NameText.text = seedSlots[1].item.itemName;
                    NameTextCount++;
                    StopCoroutine("seedNameTextWait");
                    StartCoroutine("seedNameTextWait");
                }
                
            }
            else if (seedSlots[1].item == null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = null;
            }
        }
        else if (TapCount == 2)
        {
            if (seedSlots[2].item != null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = seedSlots[2].item.itemPrefab;
                if (NameTextCount == 0)
                {
                    seed_NameText.text = seedSlots[2].item.itemName;
                    NameTextCount++;
                    StopCoroutine("seedNameTextWait");
                    StartCoroutine("seedNameTextWait");
                }

            }
            else if (seedSlots[2].item == null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = null;
            }
        }
        else if (TapCount == 3)
        {
            if (seedSlots[3].item != null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = seedSlots[3].item.itemPrefab;
                if (NameTextCount == 0)
                {
                    seed_NameText.text = seedSlots[3].item.itemName;
                    NameTextCount++;
                    StopCoroutine("seedNameTextWait");
                    StartCoroutine("seedNameTextWait");
                }

            }
            else if (seedSlots[3].item == null)
            {
                farm_Move.plant.GetComponent<Seed_Growing>().Fruit = null;
            }
        }
    }

    IEnumerator seedNameTextWait()
    {
        yield return new WaitForSecondsRealtime(2f);
        seed_NameText.text = "";
    }

    public void Seed_Subtract()
    {
        seedSlots[TapCount].SetSlotCount(-1);
    }

    public void Seed_AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < seedSlots.Length; i++)
            {
                if (seedSlots[i].item != null && seedSlots[i].item.itemName == _item.itemName)
                {
                    if (seedSlots[i].itemCount <= 0 && _count == -1) return;
                    else if (_count == -1) shop_Manager.money += 250;
                    seedSlots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
        for (int i = 0; i < seedSlots.Length; i++)
        {
            if (seedSlots[i].item == null)
            {
                if (seedSlots[i].itemCount <= 0 && _count == -1) return;
                seedSlots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    //���� �����ϳ� �����ǿ��� ����ϱ� ���� �� ��Ḧ ��ȯ�� �� ���
    public void Seed_AcquireItem_recipe(Item _item, int _count)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < seedSlots.Length; i++)
            {
                if (seedSlots[i].item != null)
                {
                    if (seedSlots[i].item.itemName == _item.itemName)
                    {
                        seedSlots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < seedSlots.Length; i++)
        {
            if (seedSlots[i].item == null)
            {
                seedSlots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    //�����ǿ��� �̹����� �÷��� �����Ͽ� Ȱ��ȭ �Ǿ����� ��Ÿ�� ��
    public void Seed_CheckItem_1(Item _item, GameObject clickObject, int _count = -1)
    {
        for (int i = 0; i < seedSlots.Length; i++)
        {
            if (seedSlots[i].item != null)
            {
                if (seedSlots[i].item.itemName == _item.itemName)
                {
                    if (clickObject.tag == "Recipe2")
                    {
                        seedSlots[i].SetSlotCount(_count);
                        //ui_Manager.Food_Image_Change_2(clickObject);
                        return;
                    }
                    else
                    {
                        seedSlots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
    }

    //�����ǿ��� Ư�� �������� ���������� �־���� ��
    public void Seed_CheckItem_2(Item _item, int _count = -1)
    {
        for (int i = 0; i < seedSlots.Length; i++)
        {
            if (seedSlots[i].item != null)
            {
                if (seedSlots[i].item.itemName == _item.itemName)
                {

                    seedSlots[i].SetSlotCount(_count);

                    return;

                }
            }
        }
    }

    public bool Seed_Check_Inven(Item _item)
    {
        for (int i = 0; i < seedSlots.Length; i++)
        {
            if (seedSlots[i].item != null)
            {
                if (seedSlots[i].item.itemName == _item.itemName)
                {
                    return true;
                }
            }
            else if (seedSlots.Length - 1 == i && seedSlots[i].item == null)
            {
                return false;
            }
        }
        return false;
    }
    
    //�κ��丮 ����
    public void Inven_Select()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        if (clickObject.GetComponent<Image>().sprite != null)
        {
            Inven_SelcetMenu.SetActive(true);
            Inven_SelcetMenu.transform.position = clickObject.transform.position;
            Sell_ButtonObject.GetComponent<Button>().interactable = false;
            Select_ItmeData = clickObject.transform.parent.GetComponent<Slot>().item;
            if (shop_Manager.shopActiveBool)
            {
                Sell_ButtonObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    //�κ� ���� Ŭ�� �� �ش� ������ ������ ��� Ŭ�� �� �޴�â �ݱ�
    public void MousePoint()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if(Vector2.Distance(mousePos, Inven_SelcetMenu.transform.position) >= 80) Inven_SelcetMenu.SetActive(false);
        }
    }

    //������ �Ǹ� �� ���� ����
    public void Sell_Button()
    {
        if (shop_Manager.shopActiveBool)
        {
            Sell_Item(Select_ItmeData, -1);
        }
    }

    //�κ� ������ �Ǹ�
    public void Sell_Item(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.itemName == _item.itemName)
                {
                    if (slots[i].itemCount <= 0 && _count == -1) return;
                    else if (_count == -1) shop_Manager.Sell_Item_Money(_item.itemName);
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
    }

    //�κ� ������ �Ա�
    public void Eat_Button()
    {
        if (Item.ItemType.Used == Select_ItmeData.itemType)
        {
            if (Select_ItmeData.weaponType == "Plant")
            {
                Gauge.gauge_amount += 10f;
                if (Gauge.gauge_amount > 100f)
                {
                    Gauge.gauge_amount = 100f;
                }
                Gauge.gauge.GetComponent<Slider>().value = Gauge.gauge_amount;
            }

            else if (Select_ItmeData.weaponType == "Food")
            {
                Gauge.gauge_amount += 20f;
                if (Gauge.gauge_amount > 100f)
                {
                    Gauge.gauge_amount = 100f;
                }
                Gauge.gauge.GetComponent<Slider>().value = Gauge.gauge_amount;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.itemName == Select_ItmeData.itemName)
                {
                    slots[i].SetSlotCount(-1);
                    return;
                }
            }
        }
    }

}
