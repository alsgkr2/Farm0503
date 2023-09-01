using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject Weapon;

    Upgrade_Manager upgrade_Manager;
    Shop_Manager shop_Manager;

    private void Start()
    {
        upgrade_Manager = GameObject.Find("Upgrade_Manager").GetComponent<Upgrade_Manager>();
        shop_Manager = GameObject.Find("Shop_Manager").GetComponent<Shop_Manager>();
    }

    public void Upgrade_WorkTime()//작업시간 줄여주기
    {
        if (Weapon.name == "Dokky" && shop_Manager.money >= upgrade_Manager.axeUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.axeUpgradeAmount[0]);
        else if (Weapon.name == "Sap" && shop_Manager.money >= upgrade_Manager.shovelUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.shovelUpgradeAmount[0]);
        else if (Weapon.name == "Pick" && shop_Manager.money >= upgrade_Manager.pickUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.pickUpgradeAmount[0]);
        else if (Weapon.name == "Watering" && shop_Manager.money >= upgrade_Manager.wateringCanUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.wateringCanUpgradeAmount[0]);
        else if (Weapon.name == "Gloves_Harvest" && shop_Manager.money >= upgrade_Manager.handUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.handUpgradeAmount[0]);
    }

    public void Upgrade_Pirodo()//피로도 숫자 줄여주기
    {
        if (Weapon.name == "Dokky" && shop_Manager.money >= upgrade_Manager.axeUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.axeUpgradeAmount[1]);
        else if (Weapon.name == "Sap" && shop_Manager.money >= upgrade_Manager.shovelUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.shovelUpgradeAmount[1]);
        else if (Weapon.name == "Pick" && shop_Manager.money >= upgrade_Manager.pickUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.pickUpgradeAmount[1]);
        else if (Weapon.name == "Watering" && shop_Manager.money >= upgrade_Manager.wateringCanUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.wateringCanUpgradeAmount[1]);
        else if (Weapon.name == "Gloves_Harvest" && shop_Manager.money >= upgrade_Manager.handUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.handUpgradeAmount[1]);
    }

    public void Upgrade_Furit()//획득 열매 숫자 증가시켜주기
    {
        if (Weapon.name == "Dokky" && shop_Manager.money >= upgrade_Manager.axeUpgradeAmount[2]) Upgrade_Furit_Amount(ref upgrade_Manager.axeUpgradeAmount[2]);
        else if (Weapon.name == "Sap" && shop_Manager.money >= upgrade_Manager.shovelUpgradeAmount[2]) Upgrade_Furit_Amount(ref upgrade_Manager.shovelUpgradeAmount[2]);
        else if (Weapon.name == "Pick" && shop_Manager.money >= upgrade_Manager.pickUpgradeAmount[2]) Upgrade_Furit_Amount(ref upgrade_Manager.pickUpgradeAmount[2]);
        else if (Weapon.name == "Watering" && shop_Manager.money >= upgrade_Manager.wateringCanUpgradeAmount[2]) Upgrade_Furit_Amount(ref upgrade_Manager.wateringCanUpgradeAmount[2]);
        else if (Weapon.name == "Gloves_Harvest" && shop_Manager.money >= upgrade_Manager.handUpgradeAmount[2]) Upgrade_Furit_Amount(ref upgrade_Manager.handUpgradeAmount[2]);
    }

    public void Select_Weapon(GameObject Weapon)
    {
        this.Weapon = Weapon;
    }

    /// <summary>
    /// 선택한 장비에 따라 비용만큼 돈 감소, 필요 비용 2배, 장비 스탯상승(작업시간)
    /// </summary>
    /// <param name="amount"></param>
    private void Upgrade_WorkTime_Amount(ref int amount)
    {
        shop_Manager.money -= amount;
        amount *= 2;
        Weapon.GetComponent<Weapon>().Loss_Work_Time += 1;
    }

    /// <summary>
    /// 선택한 장비에 따라 비용만큼 돈 감소, 필요 비용 2배, 장비 스탯상승(피로도)
    /// </summary>
    /// <param name="amount"></param>
    private void Upgrade_Pirodo_Amount(ref int amount)
    {
        shop_Manager.money -= amount;
        amount *= 2;
        Weapon.GetComponent<Weapon>().Loss_pirodo_Amount += 1;
    }

    /// <summary>
    /// 선택한 장비에 따라 비용만큼 돈 감소, 필요 비용 2배, 장비 스탯상승(획득 아이템 숫자)
    /// </summary>
    /// <param name="amount"></param>
    private void Upgrade_Furit_Amount(ref int amount)
    {
        shop_Manager.money -= amount;
        amount *= 2;
        Weapon.GetComponent<Weapon>().Plus_Furit_Amount += 1;
    }
}
