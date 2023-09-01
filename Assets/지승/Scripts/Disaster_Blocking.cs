using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disaster_Blocking : MonoBehaviour
{
    [SerializeField]
    GameObject rain_View;
    [SerializeField]
    GameObject hurricane_View;

    [SerializeField]
    GameObject rain_Blocking; // �� ������ �ִ� ������Ʈ
    [SerializeField]
    GameObject hurricane_Blocking; // �㸮������ ������ �ִ� ������Ʈ

    int switch_Button = 0;
    
    public static Disaster_Blocking instance;

    private void Awake()
    {
        instance = this;
        Blocking_Off(); // �⺻������ �� ����
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �κ��丮���� ���õǴ� �ڵ尡 ��¥������ �׳� Ű�Է����� �������°ɷ� ����� ����
        if (Input.GetKeyDown(KeyCode.Alpha7)) // �� �����ִ� ������Ʈ ��ġ
        {
            Farm_Move.instance.All_Weapon_Off(); // �ٸ� ������ �� ����
            if (switch_Button == 1)// -> �̹� �� ������Ʈ �ε� �ٽ� ������� ����
            {
                switch_Button = 0; // -> �ƹ��͵� �ƴ� ���� �־ ��, �㸮���� �Ѵ� �ȳ����� ��
                rain_View.SetActive(false); // Ű ���������� �̸����� ������
            }
            else if(switch_Button == 0 || switch_Button == 2) // 0 -> �ƹ��͵� �ƴ� ���� / 2 -> �㸮���� / �㸮�����̰ų� �ƹ��͵� �ƴ� ���ڰ� ������ �� ������Ʈ ���� �־���
            {
                switch_Button = 1;
                hurricane_View.SetActive(false); // �㸮���� ������Ʈ ����
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) // �㸮���� �����ִ� ������Ʈ ��ġ
        {
            Farm_Move.instance.All_Weapon_Off(); // �ٸ� ������ �� ����
            if (switch_Button == 2) // -> �̹� �㸮���� ������Ʈ �ε� �ٽ� ������� ����
            {
                switch_Button = 0; // -> �ƹ��͵� �ƴ� ���� �־ ��, �㸮���� �Ѵ� �ȳ����� ��
                hurricane_View.SetActive(false); // �㸮���� ������Ʈ ����
            }
            else if (switch_Button == 0 || switch_Button == 1) // 0 -> �ƹ��͵� �ƴ� ���� / 1 -> �� / ��ų� �ƹ��͵� �ƴ� ���ڰ� ������ �㸮���� ������Ʈ ���� �־���
            {
                switch_Button = 2;
                rain_View.SetActive(false); // �� ������Ʈ ����
            }
        }

        if (switch_Button == 1) // -> �� ������Ʈ �϶� ����
        {
            Choice_Key(rain_View, rain_Blocking); // -> �Լ�(�� �����ִ� ������Ʈ �̸�����,  �񸷾��ִ� ��ġ�� ������Ʈ)
        }

        else if (switch_Button == 2)// -> �㸮���� ������Ʈ �϶� ����
        { 
            Choice_Key(hurricane_View, hurricane_Blocking); // -> �Լ�(�㸮���� �����ִ� ������Ʈ �̸�����,  �㸮���θ����ִ� ��ġ�� ������Ʈ)

        }
    }
    
    public void Blocking_Off()
    {
        rain_View.SetActive(false);
        hurricane_View.SetActive(false);
        switch_Button = 0;
    }

    [System.Obsolete]
    public void Choice_Key(GameObject blocking_View, GameObject blocking)
    {
        blocking_View.SetActive(true); // ��, �㸮���� �� �� �޾ƿ� ������Ʈ ����

        if (blocking_View.activeSelf == true) // �̸����� ������Ʈ�� ���ִٸ� ����
        {
            if (Input.GetMouseButtonDown(0) && Farm_Move.instance.hit_Field.Length >= 1) // ���콺�� ������ ���� �ִٸ� ����
            {
                Install_Blocking(blocking); // �Լ�(��, �㸮���� �� ��ġ�� ������Ʈ)
            }
        }
    }

    // ��, �㸮������ ���� �� �ִ� ������Ʈ ��ġ�ϴ� �Լ�
    [System.Obsolete]
    public void Install_Blocking(GameObject blocking)
    {
        // �� ������Ʈ �ڽĿ� �κ��丮���� �޾ƿ� ������Ʈ�� ������ ����
        if (Farm_Move.instance.hit_Field[0].transform.FindChild(blocking.name.ToString() + "(Clone)") == null)
        {
            // ������Ʈ ��ġ�� ��ġ�� �� ��ġ��
            GameObject Prefab = Instantiate(blocking, new Vector3(Farm_Move.instance.hit_Field[0].transform.position.x, 0.35f, Farm_Move.instance.hit_Field[0].transform.position.z), Quaternion.identity);
            // �������� �� �ڽ����� �־ ���� ������� �Բ� ������� ��
            Prefab.transform.SetParent(Farm_Move.instance.hit_Field[0].transform);
        }
        else
        {

        }
    }
}
