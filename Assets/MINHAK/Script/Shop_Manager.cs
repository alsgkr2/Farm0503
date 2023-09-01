using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop_Manager : MonoBehaviour
{
    GameObject shop;
    GameObject seedTapObjects;
    GameObject foodTapObjects;
    GameObject plantTapObjects;
    GameObject itemSelectImageObjects;

    Text moneyText;
    /// <summary>
    /// 0: �������� 1: ����߾� 2: �丶�侾 3: �丶�� 4: ������ 5: ����� 6: �ݽ��ο� 7: ������
    /// </summary>
    public Text[] itemCountText;

    public Sprite defaultSeed;
    public Sprite choiceSeed;

    public bool shopActiveBool;

    /// <summary>
    /// 0: ���� 1: ���۹� 2: ����
    /// </summary>
    public int currentTap;

    public int money;

    GameObject[] items;

    /// <summary>
    /// 0: Default 1: seed 2: seed2 3: seed3 4: tomato 5: con 6: cabbage 7: coleslaw 8: salad
    /// </summary>
    public int currentChoice;

    //��ǰ ���� ����ü 
    public struct stuffCount
    {
        public int[] seed; //����
        public int tomato; //�丶��
        public int con; //������
        public int cabbage; //�����
        public int coleslaw; //�ݽ��ο�
        public int salad; //������
    }
    public stuffCount sc;

    public struct stuffMoney
    {
        public int[] seedMoney; //����
        public int tomatoMoney; //�丶��
        public int conMoney; //������
        public int cabbageMoney; //�����
        public int coleslawMoney; //�ݽ��ο�
        public int saladMoney; //������
    }
    public stuffMoney sm;

    void Start()
    {
        shop = GameObject.Find("ParentObect").transform.Find("ShopObjects").gameObject;

        seedTapObjects = shop.transform.Find("SeedTapObjects").gameObject;
        foodTapObjects = shop.transform.Find("FoodTapObjects").gameObject;
        plantTapObjects = shop.transform.Find("PlantTapObjects").gameObject;

        itemSelectImageObjects = shop.transform.Find("ItemSelectImageObjects").gameObject;

        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();

        items = new GameObject[6];
        for (int i = 0; i < items.Length; i++) items[i] = itemSelectImageObjects.transform.Find("ItemSelectImage" + i).gameObject;

        sc.seed = new int[3];
        for (int i = 0; i < sc.seed.Length; i++) sc.seed[i] = 5000;
        sc.tomato = 5000;
        sc.con = 5000;
        sc.cabbage = 5000;
        sc.coleslaw = 5000;
        sc.salad = 5000;

        money = 50000;

        sm.seedMoney = new int[3];
        for (int i = 0; i < sc.seed.Length; i++) sm.seedMoney[i] = 500;
        sm.tomatoMoney = 1000;
        sm.conMoney = 1000;
        sm.cabbageMoney = 1000;
        sm.coleslawMoney = 2000;
        sm.saladMoney = 2000;
    }

    void Update()
    {
        moneyText.text = "���� �ݾ�: " + money.ToString() + "��";

        //������ Ȱ��ȭ ���ִ� ���
        if (shopActiveBool)
        {
            itemCountText[0].text = "������ �� \n���: " + sc.seed[0] + "��" + "\n����: " + sm.seedMoney[0] + "��";
            itemCountText[1].text = "����� �� \n���: " + sc.seed[1] + "��" + "\n����: " + sm.seedMoney[1] + "��";
            itemCountText[2].text = "�丶�� �� \n���: " + sc.seed[2] + "��" + "\n����: " + sm.seedMoney[2] + "��";
            itemCountText[3].text = "�丶�� \n���: " + sc.tomato + "��" + "\n����: " + sm.tomatoMoney + "��";
            itemCountText[4].text = "������ \n���: " + sc.con + "��" + "\n����: " + sm.conMoney + "��";
            itemCountText[5].text = "����� \n���: " + sc.cabbage + "��" + "\n����: " + sm.cabbageMoney + "��";
            itemCountText[6].text = "�ݽ��ο� \n���: " + sc.coleslaw + "��" + "\n����: " + sm.coleslawMoney + "��";
            itemCountText[7].text = "������ \n���: " + sc.salad + "��" + "\n����: " + sm.saladMoney + "��";
        }

        //Q������ ���� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Q)) ShopActive();

        if(currentTap == 0)
        {
            seedTapObjects.SetActive(true);
            plantTapObjects.SetActive(false);
            foodTapObjects.SetActive(false);
        }
        else if(currentTap == 1)
        {
            seedTapObjects.SetActive(false);
            plantTapObjects.SetActive(true);
            foodTapObjects.SetActive(false);
        }
        else if (currentTap == 2)
        {
            seedTapObjects.SetActive(false);
            plantTapObjects.SetActive(false);
            foodTapObjects.SetActive(true);
        }
    }

    /// <summary>
    /// ������ ��ư Ŭ��
    /// </summary>
    public void ItemClick(int num)
    {
        for (int i = 1; i < items.Length + 1; i++)
        {
            if(num == i && currentChoice == i)
            {
                items[i - 1].gameObject.SetActive(false);
                currentChoice = 0;
                return;
            }
        }
        for (int i = 0; i < items.Length; i++) items[i].gameObject.SetActive(false);
        if (num != 0)
        {
            items[num - 1].gameObject.SetActive(true);
            if (currentTap == 0) currentChoice = num;
            else if (currentTap == 1) currentChoice = num + 3;
            else if (currentTap == 2) currentChoice = num + 6;
        }
        else currentChoice = 0;
    }

    /// <summary>
    /// ���� Ȱ��ȭ
    /// </summary>
    public void ShopActive()
    {
        if (shopActiveBool == false)
        {
            shop.SetActive(true);
            shopActiveBool = true;
        }
        else if (shopActiveBool)
        {
            shop.SetActive(false);
            shopActiveBool = false;
        }
    }

    /// <summary>
    /// �Ǹ� �����ۿ� ���� �� ȹ�� 
    /// </summary>
    /// <param name="itemName"></param>
    public void Sell_Item_Money(string itemName)
    {
        // ����
        if (itemName == "������ ����") money += (sm.seedMoney[0] / 2);
        else if (itemName == "����� ����") money += (sm.seedMoney[1] / 2);
        else if (itemName == "�丶�� ����") money += (sm.seedMoney[2] / 2);
        // ���۹�
        else if (itemName == "Tomato") money += (sm.tomatoMoney / 2);
        else if (itemName == "Corn") money += (sm.conMoney / 2);
        else if (itemName == "Cabbage") money += (sm.cabbageMoney / 2);
        // ����
        else if (itemName == "Coleslaw") money += (sm.coleslawMoney / 2);
        else if (itemName == "Salad") money += (sm.saladMoney / 2);
    }
}