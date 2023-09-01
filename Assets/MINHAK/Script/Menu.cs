using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuObjects;
    public GameObject guideObjects;
    Text guideText;
    Image guideImage;
    public int guidePage;

    public GameObject inventory;
    public GameObject Cooking;

    public bool menuActiveBool;
    public bool currentScreenMode;
    

    void Start()
    {
        menuObjects = GameObject.Find("ParentObect").transform.Find("MenuObjects").gameObject;
        guideObjects = GameObject.Find("Canvas").transform.Find("GuideObjects").gameObject;
        inventory = GameObject.Find("Inventory").transform.Find("inventory_Base").gameObject;
        Cooking = GameObject.Find("Canvas").transform.Find("Cooking_Recipe").gameObject;
        guideText = guideObjects.transform.Find("GuideText").GetComponent<Text>();
        guideImage = guideObjects.transform.Find("GuideImage").GetComponent<Image>();
        currentScreenMode = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActiveBool) 
            {
                menuObjects.SetActive(false);
                menuActiveBool = false;
            }
            else if(menuActiveBool == false)
            {
                menuObjects.SetActive(true);
                menuActiveBool = true;
            }
        }

        if (guideObjects.activeSelf)
        {
            if(guidePage == 1)
            {
                guideText.text = "���� â�Դϴ�. 1~8������ ������ �׿� �´� ���ڸ� ������ ������ �� �ֽ��ϴ�." +
                    "\n��� ������ ������ �� ���콺 �� Ŭ�� �Ǵ� �� Ŭ������ ����� �� �ֽ��ϴ�." +
                    "\n 1��(�Ǽ�): ���ѽɱ� " +
                    "\n 2��(����): �������� " +
                    "\n 3��(��): �������� " +
                    "\n 4��(���): �� ���ٱ� " +
                    "\n 5��: ���� �ɱ� " +
                    "\n 6��(���Ѹ���): ���ֱ� " +
                    "\n 7��: ? " +
                    "\n 8��: ?";
                guideImage.rectTransform.localPosition = new Vector3(40, -492, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 980);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 130);
            }
            else if(guidePage == 2)
            {
                guideText.text = "���� â�Դϴ�. �ִ� 4ĭ���� ������ TapŰ�� ������ ���� ������ �ٲ� �� �ֽ��ϴ�.";
                guideImage.rectTransform.localPosition = new Vector3(782.3f, -507, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            }
            else if (guidePage == 3)
            {
                inventory.SetActive(true);
                guideText.text = "�κ��丮 â�Դϴ�. EŰ�� ������ ���� ų �� �ֽ��ϴ�. 1, 2�������� ������ �� �������� �ִ� 8ĭ���� �ֽ��ϴ�. " +
                    "\n���۹�, ���� ���� ���� �� ������ ������ �ִٸ� �������� �Ǹ� �� ���� �ֽ��ϴ�.";
                guideImage.rectTransform.localPosition = new Vector3(-703, 50, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 350);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 750);
            }
            else if (guidePage == 4)
            {
                inventory.SetActive(false);
                Cooking.SetActive(true);
                guideText.text = "�丮 â�Դϴ�. ���Ĵ뿡�� FŰ�� ������ ���� ų �� �ֽ��ϴ�. ���Ĵ�� �� ������ �ֽ��ϴ�." +
                    "\n�� ��Ḧ Ŭ���ѵ� Ȱ��ȭ�� ���ľ������� �����ϸ� �׿� �´� ������ ���� �� �ֽ��ϴ�.";
                guideImage.rectTransform.localPosition = new Vector3(0, 0, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1000);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 650);
            }
            else if (guidePage == 5)
            {
                Cooking.SetActive(false);
                guideText.text = "�̿��� ��� ��ȣ�ۿ�Ű�� FŰ �Դϴ�. �̻� ���� ������ ��ġ�ڽ��ϴ�." +
                    "\n��ſ� ���� �ǽñ⸦ �ٶ��ϴ�. ���� ������Ű�� ������ ó������ ���ư��ϴ�";
                guideImage.rectTransform.localPosition = new Vector3(0, 0, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            }
        }
    }
}
