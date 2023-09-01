using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Manager : MonoBehaviour
{
    GameObject upgrade;
    public GameObject upgradeChoice;

    bool upgradeActive;

    public GameObject axeChoice;
    public GameObject shovelChoice;
    public GameObject pickChoice;
    public GameObject wateringCanChoice;

    Text workTimeUpgradeAmountText;
    Text pirodoUpgradeAmountText;
    Text furitUpgradeAmountText;

    // 0: workTime 1: pirodo 2: furit
    public int[] axeUpgradeAmount = new int[3];
    public int[] shovelUpgradeAmount = new int[3];
    public int[] pickUpgradeAmount = new int[3];
    public int[] wateringCanUpgradeAmount = new int[3];
    public int[] handUpgradeAmount = new int[3];

    Upgrade upgradeScript;

    void Start()
    {
        upgrade = GameObject.Find("ParentObect").transform.Find("Upgrade").gameObject;
        upgradeChoice = upgrade.transform.Find("UpgradeChoice").gameObject;

        axeChoice = upgradeChoice.transform.Find("AxeChoice").gameObject;
        shovelChoice = upgradeChoice.transform.Find("ShovelChoice").gameObject;
        pickChoice = upgradeChoice.transform.Find("PickChoice").gameObject;
        wateringCanChoice = upgradeChoice.transform.Find("WateringCanChoice").gameObject;

        workTimeUpgradeAmountText = axeChoice.transform.Find("AxeChoiceImage1").transform.Find("AxeChoiceButton1").transform.Find("AmountText1").GetComponent<Text>();
        pirodoUpgradeAmountText = axeChoice.transform.Find("AxeChoiceImage2").transform.Find("AxeChoiceButton2").transform.Find("AmountText2").GetComponent<Text>();
        furitUpgradeAmountText = axeChoice.transform.Find("AxeChoiceImage3").transform.Find("AxeChoiceButton3").transform.Find("AmountText3").GetComponent<Text>();

        for (int i = 0; i < 3; i++)
        {
            axeUpgradeAmount[i] = 1000;
            shovelUpgradeAmount[i] = 1000;
            pickUpgradeAmount[i] = 1000;
            wateringCanUpgradeAmount[i] = 1000;
            handUpgradeAmount[i] = 1000;
        }

        upgradeScript = GameObject.Find("Manager").GetComponent<Upgrade>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && upgradeActive == false)
        {
            upgrade.SetActive(true);
            upgradeActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.U) && upgradeActive)
        {
            upgrade.SetActive(false);
            upgradeActive = false;
        }

        if (upgradeScript.Weapon != null && upgradeScript.Weapon.name == "Dokky") WeaponUp(axeUpgradeAmount);
        else if (upgradeScript.Weapon != null && upgradeScript.Weapon.name == "Sap") WeaponUp(shovelUpgradeAmount);
        else if (upgradeScript.Weapon != null && upgradeScript.Weapon.name == "Pick") WeaponUp(pickUpgradeAmount);
        else if (upgradeScript.Weapon != null && upgradeScript.Weapon.name == "Watering") WeaponUp(wateringCanUpgradeAmount);
        else if (upgradeScript.Weapon != null && upgradeScript.Weapon.name == "Gloves_Harvest") WeaponUp(handUpgradeAmount);
    }

    private void WeaponUp(int[] amount)
    {
        workTimeUpgradeAmountText.text = "강화 금액: " + amount[0];
        pirodoUpgradeAmountText.text = "강화 금액: " + amount[1];
        furitUpgradeAmountText.text = "강화 금액: " + amount[2];
    }
}
