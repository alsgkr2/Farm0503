using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Farm_Move : MonoBehaviour
{
    public float field_Creat_Speed = 0f; // 밭 생성 소요 시간
    [SerializeField]
    GameObject farm_Field; // 생성할 밭 오브젝트 
    public GameObject plant; // 씨앗
    [SerializeField]
    GameObject ray_Box; // 플레이어 앞에 박스를 만들어서 아래로 레이 발사
    [SerializeField]
    GameObject weapon_Field; // 괭이
    [SerializeField]
    GameObject weapon_Plant; // 맨손
    [SerializeField]
    GameObject weapon_Water; // 물뿌리개
    [SerializeField]
    GameObject create_Field_Spown; // 밭 생성할 위치 값을 가진 오브젝트
    [SerializeField]
    GameObject Spown_View; // 밭 미리보기의 위치 값
    [SerializeField]
    GameObject[] Seeds = new GameObject[10]; // 씨앗 리스트

    public bool corutine_Double_Check = false; // 밭, 씨앗, 물, 수확등 스킬 사용중에 중복으로 시전 안되게 막기위한 변수
    Quaternion field_rotation; // 미리보기 밭의 회전 값을 가질 변수
    LayerMask layer; // 밭, 씨앗 레이어 값
    public RaycastHit[] hit_Field; // 밭을 걸러내는 레이
    Renderer field_Color; // 밭 미리 보기 색상값

    // House_Spown()
    [SerializeField]
    public GameObject House_Spown_Point; // 농장 집 문 앞에서 집 내부로 이동할때 집 위치값
    public bool House_Check = false; // 집에 있을때는 도구를 들지 못하게하기 위해서 변수선언
    // Farm_Spown()
    [SerializeField]
    public GameObject Farm_Spown_Point; // 집 내부에서 농장 집 앞으로 이동할때 농장 문 위치값

    [SerializeField]
    public GameObject Shop_In_Spown; // 농장->상점 위치값
    [SerializeField]
    public GameObject Shop_Out_Spown; // 상점->농장 위치값

    // Sleep()
    [SerializeField]
    public TextMeshProUGUI day_View;
    [SerializeField]
    Camera nextDay_BlackOut;
    [SerializeField]
    GameObject sleep_Spown;
    public bool sleep_Ok = false;
    public int day_Count = 0;

    public static Farm_Move instance;

    // 밭갈기 생성되는데 소요되는 시간 변수
    public float field_Create_Time = 5.5f;
    // 씨앗 심는데 소요되는 시간 변수
    public float seed_Plant_Time = 5.5f;
    // 물뿌리는데 소요되는 시간 변수
    public float watering_Time = 3f;
    /*UI*/
    [SerializeField] private Inventory inventory;

    [SerializeField]
    GameObject select_Weapon;//현재 플레이어가 들고있는 무기

    
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        plant.GetComponent<Seed_Growing>().Fruit = null;
        field_Color = create_Field_Spown.GetComponent<Renderer>(); // 미리보기 밭 색깔 바꿔주기 위해 renderer 가져옴
        Weapon_Set_Off();
        field_rotation = farm_Field.transform.rotation; // 밭 미리보기 회전 값 넣어줌
    }

    // Update is called once per frame
    void Update()
    {
        print(seed_Plant_Time);
        Field_Create(); // 밭 생성 메서드
        Seed_plant(); // 씨앗 심기 메서드
        Spraying_Water(); // 물 뿌리기 메서드
        Sleep(); // 잠 자기 메서드
        House_Spown(); // 집으로 이동하는 메서드
        Farm_Spown(); // 농장으로 이동하ㅣ는 메서드
    }

    public void All_Weapon_Off()
    {
        weapon_Field.SetActive(false);
        weapon_Plant.SetActive(false);
        weapon_Water.SetActive(false);
        create_Field_Spown.gameObject.SetActive(false);
        Work.Instance.SetFalseWeapon();//지호 무기 꺼주기
    }

    void Field_Create() // 밭 생성 메서드
    {
        if (weapon_Field.activeSelf == false)
        {
            Movement.instance.field_Create_move = false;
        }
        if (corutine_Double_Check == false && House_Check == false) // 스킬 시전중에 도구 교체 불가능 + 집에 있을때는 도구 들기 불가능
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Work.Instance.SetFalseWeapon();//지호 무기 꺼주기
                                               // z 키 눌렀을때 괭이가 없다면 켜줌
                if (weapon_Field.activeSelf == false)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    weapon_Field.SetActive(true);
                    create_Field_Spown.gameObject.SetActive(true);

                    select_Weapon = weapon_Field; // 선택된 무기 넣어줌
                    
                    create_Field_Spown.transform.rotation = field_rotation;
                    Movement.instance.field_Create_move = true;
                }
                // z 키 눌렀을때 괭이가 있으면 꺼줌
                else if (weapon_Field.activeSelf == true)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    Movement.instance.field_Create_move = false;
                }
            }
        }
        // 밭 갈기

         //밭 미리보기의 회전값을 유니티상에 프리팹 회전 값으로 고정해둠
        create_Field_Spown.transform.position = Spown_View.transform.position;
        layer = 1 << LayerMask.NameToLayer("Field"); // 밭 레이어를 넣어서 레이를 쏨
        // 밭 을 걸러내는 레이 발사 상자모양임
        hit_Field = Physics.BoxCastAll(Spown_View.transform.position, new Vector3(0.95f, 1, 0.95f), transform.forward, transform.rotation, 0.01f, layer);
        // 괭이가 있다면
        if (weapon_Field.activeSelf == true)
        {
            // 레이(밭이 없다면)에 아무것도 걸리지 않았다면 실행
            if (hit_Field.Length == 0)
            {
                // 밭 미리보기 색상을 변경
                field_Color.material.color = Color.yellow;
                if (Input.GetMouseButtonDown(0))
                {
                    if (corutine_Double_Check == false)
                    {
                        if (Pirodo.instance.pirodo_Slider.value <= 0) // 피로도가 0이면 쓰러짐
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.Hunger_P_M(5);
                            Pirodo.instance.pirodo_Slider.value = 80;
                        }
                        else if (Pirodo.instance.hunger_Slider.value <= 0) // 허기가 0이면 쓰러짐
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.hunger_Slider.value = 5;
                            Pirodo.instance.pirodo_Slider.value = 85;
                        }
                        else
                        {
                            Pluse_Minus(select_Weapon.GetComponent<Weapon>()); // 피로도와 허기 감소시켜주는 곳
                            StartCoroutine("Create_Field");
                        }
                    }
                    else if (corutine_Double_Check == true)
                    {
                        print("이미 밭 코루틴이 실행 중입니당.");
                    }
                }
            }
            else
            {
                field_Color.material.color = Color.red;
            }
        }
    }
    
    void Seed_plant() //씨앗 심기 메서드
    {
        if(corutine_Double_Check == false && House_Check == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Work.Instance.SetFalseWeapon();//지호 무기 꺼주기
                                               // X 키 눌렀을때 손이 없다면 켜줌
                if (weapon_Plant.activeSelf == false)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    select_Weapon = weapon_Plant;
                    weapon_Plant.SetActive(true); // 씨앗 심기 무기 보여주고 아래 나머지 오브젝트들 꺼줌
                }
                // X 키 눌렀을때 손이 있으면 꺼줌
                else if (weapon_Plant.activeSelf == true)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    weapon_Plant.SetActive(false);
                }
            }
        }
        

        // 씨앗 심기
        if (weapon_Plant.activeSelf == true)
        {
            // 밭 미리보기의 회전값을 유니티상에 프리팹 회전 값으로 고정해둠
            create_Field_Spown.transform.position = Spown_View.transform.position;
            layer = 1 << LayerMask.NameToLayer("Seed");
            // 박스 모양의 레이임 사용법은(박스 시작 위치, 크기, 방향, 거리, 찾아낼 레이어) 일걸?
            RaycastHit[] hits_Seed = Physics.BoxCastAll(Spown_View.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, transform.rotation, 0.05f, layer);
          


            if (plant.GetComponent<Seed_Growing>().Fruit == null)
            {
                print("선택된 씨앗이 없습니다.");
            }
            else if (plant.GetComponent<Seed_Growing>().Fruit != null)
            {
                if (hit_Field.Length > 0)
                {
                    if (hit_Field[0].transform.Find("Fruit") == null)
                    {
                        print("씨앗심기 가능");
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (corutine_Double_Check == false)
                            {
                                if (Pirodo.instance.pirodo_Slider.value <= 0) // 피로도가 0이면 쓰러짐
                                {
                                    All_Weapon_Off();
                                    StartCoroutine("Sleeping");
                                    transform.position = sleep_Spown.transform.position;
                                    Pirodo.instance.Hunger_P_M(5);
                                    Pirodo.instance.pirodo_Slider.value = 80;
                                }
                                else if (Pirodo.instance.hunger_Slider.value <= 0) // 허기가 0이면 쓰러짐
                                {
                                    All_Weapon_Off();
                                    StartCoroutine("Sleeping");
                                    transform.position = sleep_Spown.transform.position;
                                    Pirodo.instance.hunger_Slider.value = 5;
                                    Pirodo.instance.pirodo_Slider.value = 85;
                                }
                                else
                                {
                                    Pluse_Minus(select_Weapon.GetComponent<Weapon>()); // 피로도와 허기 감소시켜주는 곳
                                    StartCoroutine("Seed_Planting");
                                }
                            }
                            else if (corutine_Double_Check == true)
                            {
                                print("이미 씨앗 코루틴이 실행 중입니당.");
                            }
                        }
                    }
                    else
                    {
                        print("씨앗이 심어져 있습니다.");
                    }
                }
            }
        }
    }

    void Spraying_Water() // 물뿌리개 메서드
    {
       if(corutine_Double_Check == false && House_Check == false)
        {
            // 물 뿌리개
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Work.Instance.SetFalseWeapon();//지호 무기 꺼주기
                                               // z 키 눌렀을때 물뿌리개가 없다면 켜줌
                if (weapon_Water.activeSelf == false)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    //sleep_Spown = weapon_Water;
                    select_Weapon = weapon_Water;
                    weapon_Water.SetActive(true);
                }
                // z 키 눌렀을때 물뿌리개가 있으면 꺼줌
                else if (weapon_Water.activeSelf == true)
                {
                    Weapon_Set_Off();
                    Disaster_Blocking.instance.Blocking_Off();
                }
            }
        }
        if (weapon_Water.activeSelf == true)
        {
            layer = 1 << LayerMask.NameToLayer("Seed");
            // 박스 모양의 레이임 사용법은(박스 시작 위치, 크기, 방향, 거리, 찾아낼 레이어) 일걸?
            RaycastHit[] hits = Physics.BoxCastAll(Spown_View.transform.position, farm_Field.transform.localScale * 1.5f, transform.forward, transform.rotation, 0.05f, layer);

            if (hits.Length >= 1)
            {
                if (Input.GetMouseButtonDown(0) && hit_Field[0].transform.GetComponent<Field_Manager>().water_Ok == false/*hit_Field[0].transform.GetComponent<Field_Manager>().Water_Count <= 500*/)
                {
                    if (corutine_Double_Check == false)
                    {
                        if (Pirodo.instance.pirodo_Slider.value <= 0) // 피로도가 0이면 쓰러짐
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.Hunger_P_M(5);
                            Pirodo.instance.pirodo_Slider.value = 80;
                        }
                        else if (Pirodo.instance.hunger_Slider.value <= 0) // 허기가 0이면 쓰러짐
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.hunger_Slider.value = 5;
                            Pirodo.instance.pirodo_Slider.value = 85;
                        }
                        else
                        {
                            Pluse_Minus(select_Weapon.GetComponent<Weapon>()); // 피로도와 허기 감소시켜주는 곳
                            StartCoroutine("Watering", hit_Field);
                        }
                    }
                    else if (corutine_Double_Check == true)
                    {
                        print("이미 물 뿌리기 코루틴이 실행 중입니다.");
                    }
                }
            }
            else
            {
                print("물을 이미 뿌렸습니다.");
            }
        }
    }

    // 매개변수로 피로도와 허기 값을 받아옴
    public void Pluse_Minus(Weapon My_Weapon)
    {
        Pirodo.instance.Pirodo_P_M(My_Weapon.pirodo + My_Weapon.Loss_pirodo_Amount); // 무기가 가진 피로도 값을 가져와서 그만큼 피로도를 줄여준다.
        Pirodo.instance.Hunger_P_M(My_Weapon.hunger); // 무기가 가진 허기를 줄여주는 값을 가져와서 그만큼 허기를 줄여준다.
    }
    void Sleep()
    {
        layer = 1 << LayerMask.NameToLayer("Bed");
        // 박스 모양의 레이임 사용법은(박스 시작 위치, 크기, 방향, 거리, 찾아낼 레이어) 일걸?
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(2, 2, 2), transform.forward, transform.rotation, 0.05f, layer);

        if (hits.Length == 1)
        {
            print("침대를 찾음");
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (corutine_Double_Check == false)
                {
                    StartCoroutine("Sleeping");
                    Pirodo.instance.Pirodo_P_M(100);
                    Pirodo.instance.Hunger_P_M(5);
                }
                else if (corutine_Double_Check == true)
                {
                }
            }
        }
    }

    void House_Spown()
    {
        layer = 1 << LayerMask.NameToLayer("Door");
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(2, 2, 2), transform.forward, transform.rotation, 0.05f, layer);
        if (hits.Length == 1)
        {
            if (Input.GetKeyDown(KeyCode.F) && hits[0].transform.name == "Farm_Door"  && Fade_In_Out.instance.isFading == false)
            {
                Fade_In_Out.instance.StartFade();
                StartCoroutine("Wait_Time_House", House_Spown_Point);

            }
            else if (Input.GetKeyDown(KeyCode.F) && hits[0].transform.name == "Shop_In_Door" && Fade_In_Out.instance.isFading == false)
            {
                Fade_In_Out.instance.StartFade();
                StartCoroutine("Wait_Time", Shop_In_Spown);
            }
            else if (Input.GetKeyDown(KeyCode.F) && hits[0].transform.name == "Shop_Out_Door" && Fade_In_Out.instance.isFading == false)
            {
                Fade_In_Out.instance.StartFade();
                StartCoroutine("Wait_Time", Shop_Out_Spown);
            }
        }
    }

    void Farm_Spown()
    {
        layer = 1 << LayerMask.NameToLayer("Door");
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(2, 2, 2), transform.forward, transform.rotation, 0.05f, layer);
        if (hits.Length == 1)
        {
            if (Input.GetKeyDown(KeyCode.F) && hits[0].transform.name == "House_Door" && Fade_In_Out.instance.isFading == false)
            {
                Fade_In_Out.instance.StartFade();
                StartCoroutine("Wait_Time", Farm_Spown_Point);
            }
        }
    }

    private void OnDrawGizmos() // 박스 레이의 범위를 씬 상에서 보여주기 위한 메서드
    {
        // 선 생상 변경
        Gizmos.color = Color.red;
        // 범위 보여주는 건데 실제 설정해둔 박스 크기랑 다르니까 주의하셈요
        Gizmos.DrawWireCube(Spown_View.transform.position, new Vector3(2, 1, 2));
    }

    public bool RayCast_Hit(LayerMask layerMask)
    {
        RaycastHit[] hits = Physics.BoxCastAll(Spown_View.transform.position, farm_Field.transform.localScale * 1.5f, transform.forward, transform.rotation, 0.05f, layerMask);
        if(hits.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Weapon_Set_Off()
    {
        weapon_Plant.SetActive(false); // 씨앗 심기 무기 보여주고 아래 나머지 오브젝트들 꺼줌
        weapon_Field.SetActive(false);
        create_Field_Spown.SetActive(false);
        weapon_Water.SetActive(false);
    }

    // 물 뿌리기 코루틴
    IEnumerator Watering(RaycastHit[] raycastHit)
    {

        corutine_Double_Check = true;
        Movement.instance.move_Switch = 1; // 일단은 해놓는건데 1은 캐릭터 움직임을 못하도록 막는거임
        Movement.instance.animator.SetBool("stay", false); // 가만히 있는 애니메이션 중지
        Movement.instance.animator.SetBool("Water", true); // 씨앗 심는 애니메이션 실행
        yield return new WaitForSecondsRealtime(watering_Time - select_Weapon.GetComponent<Weapon>().Loss_Work_Time);
        Movement.instance.animator.SetBool("Water", false);
        Movement.instance.animator.SetBool("stay", true);
        Movement.instance.move_Switch = 0;
        //raycastHit[0].transform.GetComponent<Field_Manager>().Water_Full(500f);
        raycastHit[0].transform.GetComponent<Field_Manager>().Water_Full(true);
        corutine_Double_Check = false;
    }
    // 씨앗 심기 코루틴
    IEnumerator Seed_Planting()
    {
        corutine_Double_Check = true;
        Movement.instance.move_Switch = 1; // 일단은 해놓는건데 1은 캐릭터 움직임을 못하도록 막는거임
        Movement.instance.animator.SetBool("seed_plant", true); // 씨앗 심는 애니메이션 실행
        Movement.instance.animator.SetBool("stay", false); // 가만히 있는 애니메이션 중지
        yield return new WaitForSecondsRealtime(seed_Plant_Time);
        GameObject plantt = Instantiate(plant, new Vector3(hit_Field[0].transform.position.x, 0, hit_Field[0].transform.position.z), Quaternion.identity); // 씨앗 생성
        plantt.GetComponent<Seed_Growing>().PreantsField = hit_Field[0].transform.gameObject;
        plantt.GetComponent<Seed_Growing>().check = true;
        plantt.transform.SetParent(hit_Field[0].transform);

        hit_Field[0].transform.GetComponent<Field_Manager>().Seed_True = true;
        plantt.transform.name = "Fruit";
        Movement.instance.animator.SetBool("seed_plant", false);
        Movement.instance.animator.SetBool("stay", true);
        Movement.instance.move_Switch = 0;
        corutine_Double_Check = false;

        inventory.Seed_Subtract();
    }

    // 밭 
    IEnumerator Create_Field()
    {
        corutine_Double_Check = true;
        // 밭 생성 코루틴
        Movement.instance.move_Switch = 1;
        Movement.instance.animator.SetBool("field_create", true);
        Movement.instance.animator.SetBool("stay", false);
        ParticleManager.instance.PlayParticle("CreateFiled", create_Field_Spown.transform);//파티클 생성
        yield return new WaitForSecondsRealtime(field_Create_Time - select_Weapon.GetComponent<Weapon>().Loss_Work_Time);

        //          생성할 오브젝트, 생성 위치(x, y, z), 회전값
        Instantiate(farm_Field, new Vector3(create_Field_Spown.transform.position.x, create_Field_Spown.transform.position.y, create_Field_Spown.transform.position.z), field_rotation);
        ParticleManager.instance.PlayParticle("CreateFiled", create_Field_Spown.transform);//파티클 중지
        Movement.instance.animator.SetBool("field_create", false);
        Movement.instance.animator.SetBool("stay", true);
        Movement.instance.move_Switch = 0;
        corutine_Double_Check = false;
    }
    
    public void Start_Sleeping()
    {
        StartCoroutine("Sleeping");
    }
    IEnumerator Sleeping()
    {

        //Fade_In_Out fade_IO = FindObjectOfType<Fade_In_Out>();
        //fade_IO.Fada_In_Out_Start(false);
        yield return new WaitForSecondsRealtime(0.2f);
        sleep_Ok = true;
        corutine_Double_Check = true;
        Movement.instance.move_Switch = 1;
        GetComponent<Rigidbody>().useGravity = false;
        Movement.instance.animator.SetBool("Sleep", true);
        Movement.instance.animator.SetBool("stay", false);
        Fade_In_Out.instance.StartFade();
        GetComponent<Rigidbody>().MovePosition(sleep_Spown.transform.position);
        yield return new WaitForSecondsRealtime(3f);
        day_Count += 1;
        day_View.text = day_Count + " Day";

        GameObject[] Field = GameObject.FindGameObjectsWithTag("Field");

        foreach (GameObject f in Field)
        {
            f.GetComponent<Field_Manager>().water_Ok = false; // 자고 일어났을때 모든 밭의 물없음 으로 설정
        }
        
        Movement.instance.animator.SetBool("Sleep", false);
        Movement.instance.animator.SetBool("stay", true);
        GetComponent<Rigidbody>().useGravity = true;
        Movement.instance.move_Switch = 0;
        corutine_Double_Check = false;
        sleep_Ok = false;
    }
    IEnumerator Wait_Time(GameObject vector3)
    {
        yield return new WaitForSecondsRealtime(1f);
        transform.position = vector3.transform.position;
        Camera_Rotate.instance.distance = 10f;
        House_Check = false;

    }
    IEnumerator Wait_Time_House(GameObject vector3)
    {
        House_Check = true;
        yield return new WaitForSecondsRealtime(1f);
        transform.position = vector3.transform.position;
        Camera_Rotate.instance.distance = 1f;

    }

    public void DayNightCount()        // 일정 밤되면 카운트 증가
    {
        day_Count += 1;
        print("밤으로 day 증가");
    }
}
