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
                guideText.text = "도구 창입니다. 1~8번까지 있으며 그에 맞는 숫자를 누르면 선택할 수 있습니다." +
                    "\n모든 도구는 선택한 후 마우스 우 클릭 또는 좌 클릭으로 사용할 수 있습니다." +
                    "\n 1번(맨손): 씨앗심기 " +
                    "\n 2번(도끼): 나무베기 " +
                    "\n 3번(삽): 잡초제거 " +
                    "\n 4번(곡괭이): 땅 가꾸기 " +
                    "\n 5번: 씨앗 심기 " +
                    "\n 6번(물뿌리게): 물주기 " +
                    "\n 7번: ? " +
                    "\n 8번: ?";
                guideImage.rectTransform.localPosition = new Vector3(40, -492, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 980);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 130);
            }
            else if(guidePage == 2)
            {
                guideText.text = "씨앗 창입니다. 최대 4칸까지 있으며 Tap키를 누르면 선택 씨앗을 바꿀 수 있습니다.";
                guideImage.rectTransform.localPosition = new Vector3(782.3f, -507, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            }
            else if (guidePage == 3)
            {
                inventory.SetActive(true);
                guideText.text = "인벤토리 창입니다. E키를 누르면 끄고 킬 수 있습니다. 1, 2페이지가 있으며 각 페이지당 최대 8칸까지 있습니다. " +
                    "\n농작물, 음식 등을 먹을 수 있으며 상점에 있다면 아이템을 판매 할 수도 있습니다.";
                guideImage.rectTransform.localPosition = new Vector3(-703, 50, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 350);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 750);
            }
            else if (guidePage == 4)
            {
                inventory.SetActive(false);
                Cooking.SetActive(true);
                guideText.text = "요리 창입니다. 음식대에서 F키를 누르면 끄고 킬 수 있습니다. 음식대는 집 좌측에 있습니다." +
                    "\n각 재료를 클릭한뒤 활성화된 음식아이콘을 선택하면 그에 맞는 음식을 만들 수 있습니다.";
                guideImage.rectTransform.localPosition = new Vector3(0, 0, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1000);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 650);
            }
            else if (guidePage == 5)
            {
                Cooking.SetActive(false);
                guideText.text = "이외의 모든 상호작용키는 F키 입니다. 이상 게임 설명을 마치겠습니다." +
                    "\n즐거운 게임 되시기를 바랍니다. 다음 페이지키를 누르면 처음으로 돌아갑니다";
                guideImage.rectTransform.localPosition = new Vector3(0, 0, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
                guideImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            }
        }
    }
}
