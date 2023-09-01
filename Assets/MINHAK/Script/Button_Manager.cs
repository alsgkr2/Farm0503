using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Manager : MonoBehaviour
{
    Shop_Manager shop_Manager;
    Menu menu;
    UIManager uIManager;
    Upgrade_Manager upgrade_Manager;
    Gauge gauge;

    void Start()
    {
        shop_Manager = GameObject.Find("Shop_Manager").GetComponent<Shop_Manager>();
        menu = GameObject.Find("Menu_Manager").GetComponent<Menu>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        upgrade_Manager = GameObject.Find("Upgrade_Manager").GetComponent<Upgrade_Manager>();
        gauge = GameObject.Find("UIManager").GetComponent<Gauge>();
    }

    void Update()
    {

    }

    /// <summary>
    /// 씨앗 탭 버튼
    /// </summary>
    public void SeedTapButton()
    {
        shop_Manager.currentTap = 0;
        shop_Manager.ItemClick(0);
    }

    /// <summary>
    /// 음식 탭 버튼
    /// </summary>
    public void FoodTapButton()
    {
        shop_Manager.currentTap = 2;
        shop_Manager.ItemClick(0);
    }

    /// <summary>
    /// 작물 탭 버튼
    /// </summary>
    public void PlantTapButton()
    {
        shop_Manager.currentTap = 1;
        shop_Manager.ItemClick(0);
    }


    /// <summary>
    /// 물품 구매 버튼
    /// </summary>
    public void BuyButton()
    {
        // 씨앗 1~3
        if (shop_Manager.currentChoice == 1 && shop_Manager.money >= 500 && shop_Manager.sc.seed[0] > 0)
        {
            shop_Manager.money -= shop_Manager.sm.seedMoney[0];
            --shop_Manager.sc.seed[0];
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }
        else if (shop_Manager.currentChoice == 2 && shop_Manager.money >= 500 && shop_Manager.sc.seed[1] > 0)
        {
            shop_Manager.money -= shop_Manager.sm.seedMoney[1];
            --shop_Manager.sc.seed[1];
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }
        else if (shop_Manager.currentChoice == 3 && shop_Manager.money >= 500 && shop_Manager.sc.seed[2] > 0)
        {
            shop_Manager.money -= shop_Manager.sm.seedMoney[2];
            --shop_Manager.sc.seed[2];
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }

        // 농작물 4~6
        else if (shop_Manager.currentChoice == 4 && shop_Manager.money >= 500 && shop_Manager.sc.tomato > 0)
        {
            shop_Manager.money -= shop_Manager.sm.tomatoMoney;
            --shop_Manager.sc.tomato;
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }
        else if (shop_Manager.currentChoice == 5 && shop_Manager.money >= 500 && shop_Manager.sc.con > 0)
        {
            shop_Manager.money -= shop_Manager.sm.conMoney;
            --shop_Manager.sc.con;
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }
        else if (shop_Manager.currentChoice == 6 && shop_Manager.money >= 500 && shop_Manager.sc.cabbage > 0)
        {
            shop_Manager.money -= shop_Manager.sm.cabbageMoney;
            --shop_Manager.sc.cabbage;
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }

        // 음식 7~9
        else if (shop_Manager.currentChoice == 7 && shop_Manager.money >= 500 && shop_Manager.sc.coleslaw > 0)
        {
            shop_Manager.money -= shop_Manager.sm.coleslawMoney;
            --shop_Manager.sc.coleslaw;
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }
        else if (shop_Manager.currentChoice == 8 && shop_Manager.money >= 500 && shop_Manager.sc.salad > 0)
        {
            shop_Manager.money -= shop_Manager.sm.saladMoney;
            --shop_Manager.sc.salad;
            uIManager.Item_Plus(shop_Manager.currentChoice);
        }
    }

    /// <summary>
    /// 물품 판매 버튼
    /// </summary>
    /* 인벤에서 판매하는 기능으로 인해 비활성화
    public void SellButton()
    {
        GameObject seedObject = EventSystem.current.currentSelectedGameObject;

        if (shop_Manager.currentChoice == 1 && seedObject.name == "SellButton")
        {
            inventory.Seed_AcquireItem(seedObject.transform.Find("Seed1").GetComponent<ItemPickUp>().item, -1);
        }
        else if (shop_Manager.currentChoice == 2 && seedObject.name == "SellButton")
        {
            inventory.Seed_AcquireItem(seedObject.transform.Find("Seed2").GetComponent<ItemPickUp>().item, -1);
        }
        else if (shop_Manager.currentChoice == 3 && seedObject.name == "SellButton")
        {
            inventory.Seed_AcquireItem(seedObject.transform.Find("Seed3").GetComponent<ItemPickUp>().item, -1);
        }
        else if (shop_Manager.currentChoice == 4 && seedObject.name == "SellButton")
        {
            inventory.AcquireItem(seedObject.transform.Find("TomatoItem").GetComponent<ItemPickUp>().item, -1);
        }
        else if (shop_Manager.currentChoice == 5 && seedObject.name == "SellButton")
        {
            inventory.AcquireItem(seedObject.transform.Find("ConItem").GetComponent<ItemPickUp>().item, -1);
        }
        else if (shop_Manager.currentChoice == 6 && seedObject.name == "SellButton")
        {
            inventory.AcquireItem(seedObject.transform.Find("CabbageItem").GetComponent<ItemPickUp>().item, -1);
        }
    }
    */

    /// <summary>
    /// 게임종료 버튼
    /// </summary>
    public void GameEndButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// 해상도, 화면 모드 버튼
    /// </summary>
    public void SetResolution()
    {
        int width = 1980;
        int height = 1080;

        if(menu.currentScreenMode)
        {
            Screen.SetResolution(width, height, false);
            menu.currentScreenMode = false;
        }
        else if (menu.currentScreenMode == false)
        {
            Screen.SetResolution(width, height, true);
            menu.currentScreenMode = true;
        }
    }

    /// <summary>
    /// 강화 취소 버튼
    /// </summary>
    public void UpgradeCancelButton()
    {
        upgrade_Manager.upgradeChoice.SetActive(false);
        upgrade_Manager.axeChoice.SetActive(false);
        upgrade_Manager.shovelChoice.SetActive(false);
        upgrade_Manager.pickChoice.SetActive(false);
        upgrade_Manager.wateringCanChoice.SetActive(false);
    }

    /// <summary>
    /// 도끼 강화 선택 버튼
    /// </summary>
    public void AxeUpgradeButton()
    {
        upgrade_Manager.upgradeChoice.SetActive(true);
        upgrade_Manager.axeChoice.SetActive(true);
    }

    /// <summary>
    /// 도끼 강화 선택 버튼
    /// </summary>
    public void shovelUpgradeButton()
    {
        upgrade_Manager.upgradeChoice.SetActive(true);
        upgrade_Manager.shovelChoice.SetActive(true);
    }

    /// <summary>
    /// 도끼 강화 선택 버튼
    /// </summary>
    public void pickUpgradeButton()
    {
        upgrade_Manager.upgradeChoice.SetActive(true);
        upgrade_Manager.pickChoice.SetActive(true);
    }

    /// <summary>
    /// 도끼 강화 선택 버튼
    /// </summary>
    public void wateringCanUpgradeButton()
    {
        upgrade_Manager.upgradeChoice.SetActive(true);
        upgrade_Manager.wateringCanChoice.SetActive(true);
    }

    /// <summary>
    /// 게임 설명 버튼
    /// </summary>
    public void GuideButton()
    {
        menu.menuObjects.SetActive(false);
        menu.menuActiveBool = false;
        menu.guideObjects.SetActive(true);
        menu.guidePage++;
    }
    
    /// <summary>
    /// 게임 설명 다음 페이지 버튼
    /// </summary>
    public void GuideNextPageButton()
    {
        if (menu.guidePage == 5) menu.guidePage = 0;
        menu.guidePage++;
    }

    /// <summary>
    /// 게임 설명 종료 버튼
    /// </summary>
    public void GuideEndButton()
    {
        menu.menuObjects.SetActive(true);
        menu.menuActiveBool = true;
        menu.inventory.SetActive(false);
        menu.Cooking.SetActive(false);
        menu.guideObjects.SetActive(false);
        menu.guidePage = 0;
    }

    /// <summary>
    /// 게임 일시정지 버튼
    /// </summary>
    public void GameStopButton()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            gauge.gauge_amount_float = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gauge.gauge_amount_float = 0.02f;
        }
    }
}
