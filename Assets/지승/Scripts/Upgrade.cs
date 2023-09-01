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

    public void Upgrade_WorkTime()//�۾��ð� �ٿ��ֱ�
    {
        if (Weapon.name == "Dokky" && shop_Manager.money >= upgrade_Manager.axeUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.axeUpgradeAmount[0]);
        else if (Weapon.name == "Sap" && shop_Manager.money >= upgrade_Manager.shovelUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.shovelUpgradeAmount[0]);
        else if (Weapon.name == "Pick" && shop_Manager.money >= upgrade_Manager.pickUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.pickUpgradeAmount[0]);
        else if (Weapon.name == "Watering" && shop_Manager.money >= upgrade_Manager.wateringCanUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.wateringCanUpgradeAmount[0]);
        else if (Weapon.name == "Gloves_Harvest" && shop_Manager.money >= upgrade_Manager.handUpgradeAmount[0]) Upgrade_WorkTime_Amount(ref upgrade_Manager.handUpgradeAmount[0]);
    }

    public void Upgrade_Pirodo()//�Ƿε� ���� �ٿ��ֱ�
    {
        if (Weapon.name == "Dokky" && shop_Manager.money >= upgrade_Manager.axeUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.axeUpgradeAmount[1]);
        else if (Weapon.name == "Sap" && shop_Manager.money >= upgrade_Manager.shovelUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.shovelUpgradeAmount[1]);
        else if (Weapon.name == "Pick" && shop_Manager.money >= upgrade_Manager.pickUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.pickUpgradeAmount[1]);
        else if (Weapon.name == "Watering" && shop_Manager.money >= upgrade_Manager.wateringCanUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.wateringCanUpgradeAmount[1]);
        else if (Weapon.name == "Gloves_Harvest" && shop_Manager.money >= upgrade_Manager.handUpgradeAmount[1]) Upgrade_Pirodo_Amount(ref upgrade_Manager.handUpgradeAmount[1]);
    }

    public void Upgrade_Furit()//ȹ�� ���� ���� ���������ֱ�
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
    /// ������ ��� ���� ��븸ŭ �� ����, �ʿ� ��� 2��, ��� ���Ȼ��(�۾��ð�)
    /// </summary>
    /// <param name="amount"></param>
    private void Upgrade_WorkTime_Amount(ref int amount)
    {
        shop_Manager.money -= amount;
        amount *= 2;
        Weapon.GetComponent<Weapon>().Loss_Work_Time += 1;
    }

    /// <summary>
    /// ������ ��� ���� ��븸ŭ �� ����, �ʿ� ��� 2��, ��� ���Ȼ��(�Ƿε�)
    /// </summary>
    /// <param name="amount"></param>
    private void Upgrade_Pirodo_Amount(ref int amount)
    {
        shop_Manager.money -= amount;
        amount *= 2;
        Weapon.GetComponent<Weapon>().Loss_pirodo_Amount += 1;
    }

    /// <summary>
    /// ������ ��� ���� ��븸ŭ �� ����, �ʿ� ��� 2��, ��� ���Ȼ��(ȹ�� ������ ����)
    /// </summary>
    /// <param name="amount"></param>
    private void Upgrade_Furit_Amount(ref int amount)
    {
        shop_Manager.money -= amount;
        amount *= 2;
        Weapon.GetComponent<Weapon>().Plus_Furit_Amount += 1;
    }
}
