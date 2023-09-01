using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumBerSlot : MonoBehaviour
{
    [SerializeField] private GameObject N_Slot_1_SelctImage;
    [SerializeField] private GameObject N_Slot_2_SelctImage;
    [SerializeField] private GameObject N_Slot_3_SelctImage;
    [SerializeField] private GameObject N_Slot_4_SelctImage;
    [SerializeField] private GameObject N_Slot_5_SelctImage;
    [SerializeField] private GameObject N_Slot_6_SelctImage;
    [SerializeField] private GameObject N_Slot_7_SelctImage;
    [SerializeField] private GameObject N_Slot_8_SelctImage;

    //[SerializeField] private Inventory inven;

    // Start is called before the first frame update
    void Start()
    {
        N_Slot_1_SelctImage.SetActive(true);
        N_Slot_2_SelctImage.SetActive(false);
        N_Slot_3_SelctImage.SetActive(false);
        N_Slot_4_SelctImage.SetActive(false);
        N_Slot_5_SelctImage.SetActive(false);
        N_Slot_6_SelctImage.SetActive(false);
        N_Slot_7_SelctImage.SetActive(false);
        N_Slot_8_SelctImage.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        //Number_Slot의 이미지 활성화 및 비활성화(현재 들고 있는 도구 표시를 위함)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            N_Slot_1_SelctImage.SetActive(true);
            N_Slot_2_SelctImage.SetActive(false);
            N_Slot_3_SelctImage.SetActive(false);
            N_Slot_4_SelctImage.SetActive(false);
            N_Slot_5_SelctImage.SetActive(false);
            N_Slot_6_SelctImage.SetActive(false);
            N_Slot_7_SelctImage.SetActive(false);
            N_Slot_8_SelctImage.SetActive(false);

            //inven.seed_NameText.text = "맨 손";
            //StartCoroutine("SlotNameTextWait");
        }
       

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            N_Slot_1_SelctImage.SetActive(false);
            N_Slot_2_SelctImage.SetActive(true);
            N_Slot_3_SelctImage.SetActive(false);
            N_Slot_4_SelctImage.SetActive(false);
            N_Slot_5_SelctImage.SetActive(false);
            N_Slot_6_SelctImage.SetActive(false);
            N_Slot_7_SelctImage.SetActive(false);
            N_Slot_8_SelctImage.SetActive(false);

            //inven.seed_NameText.text = "도 끼";
            //StartCoroutine("SlotNameTextWait");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            N_Slot_1_SelctImage.SetActive(false);
            N_Slot_2_SelctImage.SetActive(false);
            N_Slot_3_SelctImage.SetActive(true);
            N_Slot_4_SelctImage.SetActive(false);
            N_Slot_5_SelctImage.SetActive(false);
            N_Slot_6_SelctImage.SetActive(false);
            N_Slot_7_SelctImage.SetActive(false);
            N_Slot_8_SelctImage.SetActive(false);

            //inven.seed_NameText.text = "삽";
            //StartCoroutine("SlotNameTextWait");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            N_Slot_1_SelctImage.SetActive(false);
            N_Slot_2_SelctImage.SetActive(false);
            N_Slot_3_SelctImage.SetActive(false);
            N_Slot_4_SelctImage.SetActive(true);
            N_Slot_5_SelctImage.SetActive(false);
            N_Slot_6_SelctImage.SetActive(false);
            N_Slot_7_SelctImage.SetActive(false);
            N_Slot_8_SelctImage.SetActive(false);

            //inven.seed_NameText.text = "곡 괭 이";
            //StartCoroutine("SlotNameTextWait");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            N_Slot_1_SelctImage.SetActive(false);
            N_Slot_2_SelctImage.SetActive(false);
            N_Slot_3_SelctImage.SetActive(false);
            N_Slot_4_SelctImage.SetActive(false);
            N_Slot_5_SelctImage.SetActive(true);
            N_Slot_6_SelctImage.SetActive(false);
            N_Slot_7_SelctImage.SetActive(false);
            N_Slot_8_SelctImage.SetActive(false);

            //inven.seed_NameText.text = "씨앗 심기";
            //StartCoroutine("SlotNameTextWait");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            N_Slot_1_SelctImage.SetActive(false);
            N_Slot_2_SelctImage.SetActive(false);
            N_Slot_3_SelctImage.SetActive(false);
            N_Slot_4_SelctImage.SetActive(false);
            N_Slot_5_SelctImage.SetActive(false);
            N_Slot_6_SelctImage.SetActive(true);
            N_Slot_7_SelctImage.SetActive(false);
            N_Slot_8_SelctImage.SetActive(false);

            //inven.seed_NameText.text = "물뿌리개";
            //StartCoroutine("SlotNameTextWait");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            N_Slot_1_SelctImage.SetActive(false);
            N_Slot_2_SelctImage.SetActive(false);
            N_Slot_3_SelctImage.SetActive(false);
            N_Slot_4_SelctImage.SetActive(false);
            N_Slot_5_SelctImage.SetActive(false);
            N_Slot_6_SelctImage.SetActive(false);
            N_Slot_7_SelctImage.SetActive(true);
            N_Slot_8_SelctImage.SetActive(false);

            //inven.seed_NameText.text = "비 막 이";
            //StartCoroutine("SlotNameTextWait");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            N_Slot_1_SelctImage.SetActive(false);
            N_Slot_2_SelctImage.SetActive(false);
            N_Slot_3_SelctImage.SetActive(false);
            N_Slot_4_SelctImage.SetActive(false);
            N_Slot_5_SelctImage.SetActive(false);
            N_Slot_6_SelctImage.SetActive(false);
            N_Slot_7_SelctImage.SetActive(false);
            N_Slot_8_SelctImage.SetActive(true);

            //inven.seed_NameText.text = "태풍막이";
            //StartCoroutine("SlotNameTextWait");
        }
    }
    /*IEnumerator SlotNameTextWait()
    {
        yield return new WaitForSecondsRealtime(2f);
        inven.seed_NameText.text = "";
    }*/
}
