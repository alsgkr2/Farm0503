using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    //cooking_panel
    public bool cooking_state;
    public GameObject Cooking_Recipe;

    private bool log_state;
    public GameObject log;

    //������ �����̹���
    public Sprite corn_change_img;
    public Sprite cabbage_change_img;
    public Sprite coleslaw_change_img;
    public Sprite tomato_change_img;
    public Sprite salad_change_img;
    public Sprite food4_change_img;
    
    //������ �⺻�̹���
    public Sprite corn_img;
    public Sprite cabbage_img;
    public Sprite coleslaw_img;
    public Sprite tomato_img;
    public Sprite salad_img;
    public Sprite food4_img;

    //������ Ȱ��ȭ(?)Ȯ��
    private bool Recipe1_corn_state;
    private bool Recipe1_cabbage_state;
    private bool Recipe3_tomato_state;
    private bool Recipe3_cabbage_state;

    private bool Recipe2_food1_state;
    private bool Recipe2_food2_state;

    int recipe2_food1_used_count = 0;
    int recipe2_food2_used_count = 0;

    public Text Food1_Text_Count;
    public Text Food2_Text_Count;

    private Sprite food3;

    public Camera point;
    private RaycastHit hit;

    [SerializeField] private Inventory theInventory;


    // Start is called before the first frame update
    void Start()
    {
        cooking_state = false;
        Cooking_Recipe.SetActive(false);
        Recipe1_corn_state = true;
        Recipe1_cabbage_state = true;
        Recipe2_food1_state = true;
        Recipe2_food2_state = true;
        log_state = false;
        log.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //cooking_panel Ȱ��ȭ �� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (cooking_state == true)
            {
                Cooking_Recipe.SetActive(false);
                cooking_state = false;
            }
            else if (cooking_state == false)
            {
                Cooking_Recipe.SetActive(true);
                cooking_state = true;
            }
        }
        /*
        //�����ǿ��� food1�� food2�� Ȱ��ȭ(?)�Ǿ��� �� food3�� Ȱ��ȭ(?)
        if (cooking_state == true)
        {
            //������ 2
            if (Recipe2_food1_state == false && Recipe2_food2_state == false)
            {
                GameObject.Find("�Ұ��").GetComponent<Image>().sprite = food4_change_img;
            }
            else if (Recipe2_food1_state == true && Recipe2_food2_state == true || Recipe2_food1_state == true && Recipe2_food2_state == false || Recipe2_food1_state == false && Recipe2_food2_state == true)
            {
                GameObject.Find("�Ұ��").GetComponent<Image>().sprite = food4_img;
            }
        }
        */
    }

    //�������� ��� Ŭ����
    public void Click_Food_recipe_1()
    { 
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        //1�� ������
        {
            //1�� ��� �̹����� ����� �� �÷��� �ٲٸ� 2����ᰡ �÷���� ����� Ȱ��ȭ
            if (clickObject.GetComponent<Image>().sprite == corn_img && theInventory.Check_Inven(clickObject.GetComponent<ItemPickUp>().item))
            {
                theInventory.CheckItem_1(clickObject.GetComponent<ItemPickUp>().item, clickObject);
                clickObject.GetComponent<Image>().sprite = corn_change_img;
                if (GameObject.Find("Food_cabbage").GetComponent<Image>().sprite == cabbage_change_img) GameObject.Find("Food_coleslaw").GetComponent<Image>().sprite = coleslaw_change_img;
            }
            //1�� ��� �̹����� �÷��� �� ������� �ٲٸ� ����� ���� ��Ȱ��ȭ
            else if (clickObject.GetComponent<Image>().sprite == corn_change_img)
            {
                theInventory.AcquireItem(clickObject.GetComponent<ItemPickUp>().item);
                clickObject.GetComponent<Image>().sprite = corn_img;
                if (GameObject.Find("Food_coleslaw").GetComponent<Image>().sprite == coleslaw_change_img) GameObject.Find("Food_coleslaw").GetComponent<Image>().sprite = coleslaw_img;
            }

            //2�� ��� �̹����� ����� �� �÷��� �ٲٸ� 1����ᰡ �÷���� ����� Ȱ��ȭ
            if (clickObject.GetComponent<Image>().sprite == cabbage_img && theInventory.Check_Inven(clickObject.GetComponent<ItemPickUp>().item))
            {
                theInventory.CheckItem_1(clickObject.GetComponent<ItemPickUp>().item, clickObject);
                clickObject.GetComponent<Image>().sprite = cabbage_change_img;
                if (GameObject.Find("Food_corn").GetComponent<Image>().sprite == corn_change_img) GameObject.Find("Food_coleslaw").GetComponent<Image>().sprite = coleslaw_change_img;
            }
            //2�� ��� �̹����� �÷��� �� ������� �ٲٸ� ����� ���� ��Ȱ��ȭ
            else if (clickObject.GetComponent<Image>().sprite == cabbage_change_img)
            {
                theInventory.AcquireItem(clickObject.GetComponent<ItemPickUp>().item);
                clickObject.GetComponent<Image>().sprite = cabbage_img;
                if (GameObject.Find("Food_coleslaw").GetComponent<Image>().sprite == coleslaw_change_img) GameObject.Find("Food_coleslaw").GetComponent<Image>().sprite = coleslaw_img;
            }

            //����� �̹����� �÷��� ��
            if (clickObject.GetComponent<Image>().sprite == coleslaw_change_img)
            {
                theInventory.AcquireItem(clickObject.GetComponent<ItemPickUp>().item, 2);
                GameObject.Find("Food_corn").GetComponent<Image>().sprite = corn_img;
                GameObject.Find("Food_cabbage").GetComponent<Image>().sprite = cabbage_img;
                clickObject.GetComponent<Image>().sprite = coleslaw_img;
            }
        }
    }

    /*
    public void Click_Food_recipe_2()       //ui���� 3��° �� ���� ������
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        //2�� ������
        {
            if (clickObject.GetComponent<Image>().sprite == corn_img)                                      //1�� ��� �̹����� ����� ��
            {
                if(theInventory.Check_Inven(clickObject.GetComponent<ItemPickUp>().item))
                {
                    if (recipe2_food1_used_count == 0 || recipe2_food1_used_count == 1)
                    {
                        theInventory.CheckItem_2(clickObject.GetComponent<ItemPickUp>().item);
                        Food1_Text_Count.text = (recipe2_food1_used_count + 1).ToString();
                        recipe2_food1_used_count += 1;
                    }
                    else if (recipe2_food1_used_count == 2)
                    {
                        Food1_Text_Count.text = (recipe2_food1_used_count + 1).ToString();
                        theInventory.CheckItem_1(clickObject.GetComponent<ItemPickUp>().item, clickObject);
                    }
                }
            }
            else if (clickObject.GetComponent<Image>().sprite == corn_change_img)                           //1�� ��� �̹����� �÷��� ��
            {
                theInventory.AcquireItem_recipe(clickObject.GetComponent<ItemPickUp>().item, recipe2_food1_used_count + 1);
                Food_Image_Change_2(clickObject);
                recipe2_food1_used_count = 0;
                Food1_Text_Count.text = "";
            }



            if (clickObject.GetComponent<Image>().sprite == cabbage_img)                                      //2�� ��� �̹����� ����� ��
            {
                if (theInventory.Check_Inven(clickObject.GetComponent<ItemPickUp>().item))
                {
                    if (recipe2_food2_used_count == 0)
                    {
                        theInventory.CheckItem_2(clickObject.GetComponent<ItemPickUp>().item);
                        Food2_Text_Count.text = (recipe2_food2_used_count + 1).ToString();
                        recipe2_food2_used_count += 1;
                    }
                    else if (recipe2_food2_used_count == 1)
                    {
                        Food2_Text_Count.text = (recipe2_food2_used_count + 1).ToString();
                        theInventory.CheckItem_1(clickObject.GetComponent<ItemPickUp>().item, clickObject);
                    }
                }   
            }
            else if (clickObject.GetComponent<Image>().sprite == cabbage_change_img)                           //2�� ��� �̹����� �÷��� ��
            {
                theInventory.AcquireItem_recipe(clickObject.GetComponent<ItemPickUp>().item, recipe2_food2_used_count + 1);
                Food_Image_Change_2(clickObject);
                recipe2_food2_used_count = 0;
                Food2_Text_Count.text = "";
            }

            if (clickObject.GetComponent<Image>().sprite == food4_change_img)                                 //3�� ��� �̹����� �÷��� ��
            {
                theInventory.AcquireItem(clickObject.GetComponent<ItemPickUp>().item);
                GameObject.Find("������").GetComponent<Image>().sprite = corn_img;
                Recipe2_food1_state = true;
                GameObject.Find("�������").GetComponent<Image>().sprite = cabbage_img;
                Recipe2_food2_state = true;
            }
        }
    }
    */

    public void Click_Food_recipe_3()           //ui���� 2��° ������
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        //2�� ������
        {
            if (clickObject.GetComponent<Image>().sprite == tomato_img && theInventory.Check_Inven(clickObject.GetComponent<ItemPickUp>().item))                                      //1�� ��� �̹����� ����� ��
            {
                theInventory.CheckItem_1(clickObject.GetComponent<ItemPickUp>().item, clickObject);
                clickObject.GetComponent<Image>().sprite = tomato_change_img;
                if (GameObject.Find("Food_cabbage_3").GetComponent<Image>().sprite == cabbage_change_img) GameObject.Find("Food_salad").GetComponent<Image>().sprite = salad_change_img;
            }
            else if (clickObject.GetComponent<Image>().sprite == tomato_change_img)                           //1�� ��� �̹����� �÷��� ��
            {
                theInventory.AcquireItem(clickObject.GetComponent<ItemPickUp>().item);
                clickObject.GetComponent<Image>().sprite = tomato_img;
                if (GameObject.Find("Food_salad").GetComponent<Image>().sprite == salad_change_img) GameObject.Find("Food_salad").GetComponent<Image>().sprite = salad_img;
            }
            if (clickObject.GetComponent<Image>().sprite == cabbage_img && theInventory.Check_Inven(clickObject.GetComponent<ItemPickUp>().item))                                      //2�� ��� �̹����� ����� ��
            {
                theInventory.CheckItem_1(clickObject.GetComponent<ItemPickUp>().item, clickObject);
                clickObject.GetComponent<Image>().sprite = cabbage_change_img;
                if (GameObject.Find("Food_tomato").GetComponent<Image>().sprite == tomato_change_img) GameObject.Find("Food_salad").GetComponent<Image>().sprite = salad_change_img;
            }
            else if (clickObject.GetComponent<Image>().sprite == cabbage_change_img)                           //2�� ��� �̹����� �÷��� ��
            {
                theInventory.AcquireItem(clickObject.GetComponent<ItemPickUp>().item);
                clickObject.GetComponent<Image>().sprite = cabbage_img;
                if (GameObject.Find("Food_salad").GetComponent<Image>().sprite == salad_change_img) GameObject.Find("Food_salad").GetComponent<Image>().sprite = salad_img;
            }

            if (clickObject.GetComponent<Image>().sprite == salad_change_img)                                 //3�� ��� �̹����� �÷��� ��
            {
                theInventory.AcquireItem(clickObject.GetComponent<ItemPickUp>().item);
                GameObject.Find("Food_tomato").GetComponent<Image>().sprite = tomato_img;
                GameObject.Find("Food_cabbage_3").GetComponent<Image>().sprite = cabbage_img;
                clickObject.GetComponent<Image>().sprite = salad_img;
            }
        }
    }



    public void Food_Plus()
    {
        GameObject foodObject = EventSystem.current.currentSelectedGameObject;

        if (foodObject.name == "food_test1")
        {
            theInventory.AcquireItem(foodObject.GetComponent<ItemPickUp>().item);
        }
        if (foodObject.name == "food_test2")
        {
            theInventory.AcquireItem(foodObject.GetComponent<ItemPickUp>().item);
        }
        if (foodObject.name == "food_test3")
        {
            theInventory.AcquireItem(foodObject.GetComponent<ItemPickUp>().item);
        }
    }

    public void Seed_Plus(int seed)
    {
        GameObject seedObject = EventSystem.current.currentSelectedGameObject;

        if (seedObject.name == "BuyButton" && seed == 1)
        {
            theInventory.Seed_AcquireItem(seedObject.transform.Find("Seed1").GetComponent<ItemPickUp>().item);
        }
        else if (seedObject.name == "BuyButton" && seed == 2)
        {
            theInventory.Seed_AcquireItem(seedObject.transform.Find("Seed2").GetComponent<ItemPickUp>().item);
        }
        else if (seedObject.name == "BuyButton" && seed == 3)
        {
            theInventory.Seed_AcquireItem(seedObject.transform.Find("Seed3").GetComponent<ItemPickUp>().item);
        }
    }


    //2�� �����ǿ� ���� �̹��� ����
    //public void Food_Image_Change_2(GameObject _clickObject)
    //{
    //    if (_clickObject.GetComponent<Image>().sprite == corn_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = corn_change_img;
    //        Recipe2_food1_state = false;
    //    }
    //    else if (_clickObject.GetComponent<Image>().sprite == corn_change_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = corn_img;
    //        Recipe2_food1_state = true;
    //    }
    //    if (_clickObject.GetComponent<Image>().sprite == cabbage_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = cabbage_change_img;
    //        Recipe2_food2_state = false;
    //    }
    //    else if (_clickObject.GetComponent<Image>().sprite == cabbage_change_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = cabbage_img;
    //        Recipe2_food2_state = true;
    //    }
    //}

    ////public void Food_Image_Change_3(GameObject _clickObject)
    //{
    //    if (_clickObject.GetComponent<Image>().sprite == tomato_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = tomato_change_img;
    //        Recipe3_tomato_state = false;
    //    }
    //    else if (_clickObject.GetComponent<Image>().sprite == tomato_change_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = tomato_img;
    //        Recipe3_tomato_state = true;
    //    }
    //    if (_clickObject.GetComponent<Image>().sprite == cabbage_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = cabbage_change_img;
    //        Recipe3_cabbage_state = false;
    //    }
    //    else if (_clickObject.GetComponent<Image>().sprite == cabbage_change_img)
    //    {
    //        _clickObject.GetComponent<Image>().sprite = cabbage_img;
    //        Recipe3_cabbage_state = true;
    //    }

    //}



    //������ 1 ���� �α� ����
    public void Hover_corn()
    {
        GameObject hoverObject = GameObject.Find("Food_corn").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }
    public void Hover_cabbage()
    {
        GameObject hoverObject = GameObject.Find("Food_cabbage").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }
    public void Hover_coleslaw()
    {
        GameObject hoverObject = GameObject.Find("Food_coleslaw").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }
    public void Hover_tomato()
    {
        GameObject hoverObject = GameObject.Find("Food_tomato").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }
    public void Hover_salad()
    {
        GameObject hoverObject = GameObject.Find("Food_salad").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }

    //������ 2 ���� �α� ����
    public void Recipe2_Hover_food1()
    {
        GameObject hoverObject = GameObject.Find("������").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }
    public void Recipe2_Hover_food2()
    {
        GameObject hoverObject = GameObject.Find("�������").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }
    public void Recipe2_Hover_food3()
    {
        GameObject hoverObject = GameObject.Find("�Ұ��").gameObject;

        log.SetActive(true);
        log_state = true;

        GameObject.Find("Text_Log").GetComponent<Text>().text = hoverObject.GetComponentInChildren<Text>().text;
    }
    

    public void Exit_food()
    {
        GameObject.Find("Text_Log").GetComponent<Text>().text = "";
        log.SetActive(false);
        log_state = false;
    }


    public void Item_Plus(int itemNum)
    {
        GameObject itemObject = EventSystem.current.currentSelectedGameObject;

        //���� 1~3
        if (itemObject.name == "BuyButton" && itemNum == 1)
        {
            theInventory.Seed_AcquireItem(itemObject.transform.Find("Seed1").GetComponent<ItemPickUp>().item);
        }
        else if (itemObject.name == "BuyButton" && itemNum == 2)
        {
            theInventory.Seed_AcquireItem(itemObject.transform.Find("Seed2").GetComponent<ItemPickUp>().item);
        }
        else if (itemObject.name == "BuyButton" && itemNum == 3)
        {
            theInventory.Seed_AcquireItem(itemObject.transform.Find("Seed3").GetComponent<ItemPickUp>().item);
        }

        //���۹� 4~6
        else if (itemObject.name == "BuyButton" && itemNum == 4)
        {
            theInventory.AcquireItem(itemObject.transform.Find("TomatoItem").GetComponent<ItemPickUp>().item);
        }
        else if (itemObject.name == "BuyButton" && itemNum == 5)
        {
            theInventory.AcquireItem(itemObject.transform.Find("ConItem").GetComponent<ItemPickUp>().item);
        }
        else if (itemObject.name == "BuyButton" && itemNum == 6)
        {
            theInventory.AcquireItem(itemObject.transform.Find("CabbageItem").GetComponent<ItemPickUp>().item);
        }

        //���� 7~8
        else if (itemObject.name == "BuyButton" && itemNum == 7)
        {
            theInventory.AcquireItem(itemObject.transform.Find("ColeslawItem").GetComponent<ItemPickUp>().item);
        }
        else if (itemObject.name == "BuyButton" && itemNum == 8)
        {
            theInventory.AcquireItem(itemObject.transform.Find("SaladItem").GetComponent<ItemPickUp>().item);
        }
    }

}
