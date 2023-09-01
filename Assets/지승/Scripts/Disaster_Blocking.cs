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
    GameObject rain_Blocking; // 비를 막을수 있는 오브젝트
    [SerializeField]
    GameObject hurricane_Blocking; // 허리케인을 막을수 있는 오브젝트

    int switch_Button = 0;
    
    public static Disaster_Blocking instance;

    private void Awake()
    {
        instance = this;
        Blocking_Off(); // 기본적으로 다 꺼둠
    }

    // Update is called once per frame
    void Update()
    {
        // 아직 인벤토리에서 선택되는 코드가 안짜여져서 그냥 키입력으로 가져오는걸로 만들어 놓음
        if (Input.GetKeyDown(KeyCode.Alpha7)) // 비 막아주는 오브젝트 설치
        {
            Farm_Move.instance.All_Weapon_Off(); // 다른 도구들 다 꺼줌
            if (switch_Button == 1)// -> 이미 비 오브젝트 인데 다시 눌를경우 실행
            {
                switch_Button = 0; // -> 아무것도 아닌 숫자 넣어서 비, 허리케인 둘다 안나오게 함
                rain_View.SetActive(false); // 키 누를때마다 미리보기 지워줌
            }
            else if(switch_Button == 0 || switch_Button == 2) // 0 -> 아무것도 아닌 숫자 / 2 -> 허리케인 / 허리케인이거나 아무것도 아닌 숫자가 있으면 비 오브젝트 숫자 넣어줌
            {
                switch_Button = 1;
                hurricane_View.SetActive(false); // 허리케인 오브젝트 꺼줌
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) // 허리케인 막아주는 오브젝트 설치
        {
            Farm_Move.instance.All_Weapon_Off(); // 다른 도구들 다 꺼줌
            if (switch_Button == 2) // -> 이미 허리케인 오브젝트 인데 다시 눌를경우 실행
            {
                switch_Button = 0; // -> 아무것도 아닌 숫자 넣어서 비, 허리케인 둘다 안나오게 함
                hurricane_View.SetActive(false); // 허리케인 오브젝트 꺼줌
            }
            else if (switch_Button == 0 || switch_Button == 1) // 0 -> 아무것도 아닌 숫자 / 1 -> 비 / 비거나 아무것도 아닌 숫자가 있으면 허리케인 오브젝트 숫자 넣어줌
            {
                switch_Button = 2;
                rain_View.SetActive(false); // 비 오브젝트 꺼줌
            }
        }

        if (switch_Button == 1) // -> 비 오브젝트 일때 실행
        {
            Choice_Key(rain_View, rain_Blocking); // -> 함수(비 막아주는 오브젝트 미리보기,  비막아주는 설치할 오브젝트)
        }

        else if (switch_Button == 2)// -> 허리케인 오브젝트 일때 실행
        { 
            Choice_Key(hurricane_View, hurricane_Blocking); // -> 함수(허리케인 막아주는 오브젝트 미리보기,  허리케인막아주는 설치할 오브젝트)

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
        blocking_View.SetActive(true); // 비, 허리케인 둘 중 받아온 오브젝트 켜줌

        if (blocking_View.activeSelf == true) // 미리보기 오브젝트가 켜있다면 실행
        {
            if (Input.GetMouseButtonDown(0) && Farm_Move.instance.hit_Field.Length >= 1) // 마우스를 누르고 밭이 있다면 실행
            {
                Install_Blocking(blocking); // 함수(비, 허리케인 중 설치할 오브젝트)
            }
        }
    }

    // 비, 허리케인을 막을 수 있는 오브젝트 설치하는 함수
    [System.Obsolete]
    public void Install_Blocking(GameObject blocking)
    {
        // 밭 오브젝트 자식에 인벤토리에서 받아온 오브젝트가 없으면 실행
        if (Farm_Move.instance.hit_Field[0].transform.FindChild(blocking.name.ToString() + "(Clone)") == null)
        {
            // 오브젝트 설치할 위치는 밭 위치임
            GameObject Prefab = Instantiate(blocking, new Vector3(Farm_Move.instance.hit_Field[0].transform.position.x, 0.35f, Farm_Move.instance.hit_Field[0].transform.position.z), Quaternion.identity);
            // 프리팹을 밭 자식으로 넣어서 밭이 사라질때 함께 사라지게 함
            Prefab.transform.SetParent(Farm_Move.instance.hit_Field[0].transform);
        }
        else
        {

        }
    }
}
