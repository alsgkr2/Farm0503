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
        //Number_Slot�� �̹��� Ȱ��ȭ �� ��Ȱ��ȭ(���� ��� �ִ� ���� ǥ�ø� ����)
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

            //inven.seed_NameText.text = "�� ��";
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

            //inven.seed_NameText.text = "�� ��";
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

            //inven.seed_NameText.text = "��";
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

            //inven.seed_NameText.text = "�� �� ��";
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

            //inven.seed_NameText.text = "���� �ɱ�";
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

            //inven.seed_NameText.text = "���Ѹ���";
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

            //inven.seed_NameText.text = "�� �� ��";
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

            //inven.seed_NameText.text = "��ǳ����";
            //StartCoroutine("SlotNameTextWait");
        }
    }
    /*IEnumerator SlotNameTextWait()
    {
        yield return new WaitForSecondsRealtime(2f);
        inven.seed_NameText.text = "";
    }*/
}
