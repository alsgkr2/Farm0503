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
    /// 0: 옥수수씨 1: 양배추씨 2: 토마토씨 3: 토마토 4: 옥수수 5: 양배추 6: 콜슬로우 7: 샐러드
    /// </summary>
    public Text[] itemCountText;

    public Sprite defaultSeed;
    public Sprite choiceSeed;

    public bool shopActiveBool;

    /// <summary>
    /// 0: 씨앗 1: 농작물 2: 음식
    /// </summary>
    public int currentTap;

    public int money;

    GameObject[] items;

    /// <summary>
    /// 0: Default 1: seed 2: seed2 3: seed3 4: tomato 5: con 6: cabbage 7: coleslaw 8: salad
    /// </summary>
    public int currentChoice;

    //물품 개수 구조체 
    public struct stuffCount
    {
        public int[] seed; //씨앗
        public int tomato; //토마토
        public int con; //옥수수
        public int cabbage; //양배추
        public int coleslaw; //콜슬로우
        public int salad; //샐러드
    }
    public stuffCount sc;

    public struct stuffMoney
    {
        public int[] seedMoney; //씨앗
        public int tomatoMoney; //토마토
        public int conMoney; //옥수수
        public int cabbageMoney; //양배추
        public int coleslawMoney; //콜슬로우
        public int saladMoney; //샐러드
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
        moneyText.text = "소지 금액: " + money.ToString() + "원";

        //상점이 활성화 되있는 경우
        if (shopActiveBool)
        {
            itemCountText[0].text = "옥수수 씨 \n재고: " + sc.seed[0] + "개" + "\n가격: " + sm.seedMoney[0] + "원";
            itemCountText[1].text = "양배추 씨 \n재고: " + sc.seed[1] + "개" + "\n가격: " + sm.seedMoney[1] + "원";
            itemCountText[2].text = "토마토 씨 \n재고: " + sc.seed[2] + "개" + "\n가격: " + sm.seedMoney[2] + "원";
            itemCountText[3].text = "토마토 \n재고: " + sc.tomato + "개" + "\n가격: " + sm.tomatoMoney + "원";
            itemCountText[4].text = "옥수수 \n재고: " + sc.con + "개" + "\n가격: " + sm.conMoney + "원";
            itemCountText[5].text = "양배추 \n재고: " + sc.cabbage + "개" + "\n가격: " + sm.cabbageMoney + "원";
            itemCountText[6].text = "콜슬로우 \n재고: " + sc.coleslaw + "개" + "\n가격: " + sm.coleslawMoney + "원";
            itemCountText[7].text = "샐러드 \n재고: " + sc.salad + "개" + "\n가격: " + sm.saladMoney + "원";
        }

        //Q누를시 상점 활성화
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
    /// 아이템 버튼 클릭
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
    /// 상점 활성화
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
    /// 판매 아이템에 따른 돈 획득 
    /// </summary>
    /// <param name="itemName"></param>
    public void Sell_Item_Money(string itemName)
    {
        // 씨앗
        if (itemName == "옥수수 씨앗") money += (sm.seedMoney[0] / 2);
        else if (itemName == "양배추 씨앗") money += (sm.seedMoney[1] / 2);
        else if (itemName == "토마토 씨앗") money += (sm.seedMoney[2] / 2);
        // 농작물
        else if (itemName == "Tomato") money += (sm.tomatoMoney / 2);
        else if (itemName == "Corn") money += (sm.conMoney / 2);
        else if (itemName == "Cabbage") money += (sm.cabbageMoney / 2);
        // 음식
        else if (itemName == "Coleslaw") money += (sm.coleslawMoney / 2);
        else if (itemName == "Salad") money += (sm.saladMoney / 2);
    }
}