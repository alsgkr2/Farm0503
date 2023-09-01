using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Farm_Move : MonoBehaviour
{
    public float field_Creat_Speed = 0f; // �� ���� �ҿ� �ð�
    [SerializeField]
    GameObject farm_Field; // ������ �� ������Ʈ 
    public GameObject plant; // ����
    [SerializeField]
    GameObject ray_Box; // �÷��̾� �տ� �ڽ��� ���� �Ʒ��� ���� �߻�
    [SerializeField]
    GameObject weapon_Field; // ����
    [SerializeField]
    GameObject weapon_Plant; // �Ǽ�
    [SerializeField]
    GameObject weapon_Water; // ���Ѹ���
    [SerializeField]
    GameObject create_Field_Spown; // �� ������ ��ġ ���� ���� ������Ʈ
    [SerializeField]
    GameObject Spown_View; // �� �̸������� ��ġ ��
    [SerializeField]
    GameObject[] Seeds = new GameObject[10]; // ���� ����Ʈ

    public bool corutine_Double_Check = false; // ��, ����, ��, ��Ȯ�� ��ų ����߿� �ߺ����� ���� �ȵǰ� �������� ����
    Quaternion field_rotation; // �̸����� ���� ȸ�� ���� ���� ����
    LayerMask layer; // ��, ���� ���̾� ��
    public RaycastHit[] hit_Field; // ���� �ɷ����� ����
    Renderer field_Color; // �� �̸� ���� ����

    // House_Spown()
    [SerializeField]
    public GameObject House_Spown_Point; // ���� �� �� �տ��� �� ���η� �̵��Ҷ� �� ��ġ��
    public bool House_Check = false; // ���� �������� ������ ���� ���ϰ��ϱ� ���ؼ� ��������
    // Farm_Spown()
    [SerializeField]
    public GameObject Farm_Spown_Point; // �� ���ο��� ���� �� ������ �̵��Ҷ� ���� �� ��ġ��

    [SerializeField]
    public GameObject Shop_In_Spown; // ����->���� ��ġ��
    [SerializeField]
    public GameObject Shop_Out_Spown; // ����->���� ��ġ��

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

    // �簥�� �����Ǵµ� �ҿ�Ǵ� �ð� ����
    public float field_Create_Time = 5.5f;
    // ���� �ɴµ� �ҿ�Ǵ� �ð� ����
    public float seed_Plant_Time = 5.5f;
    // ���Ѹ��µ� �ҿ�Ǵ� �ð� ����
    public float watering_Time = 3f;
    /*UI*/
    [SerializeField] private Inventory inventory;

    [SerializeField]
    GameObject select_Weapon;//���� �÷��̾ ����ִ� ����

    
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        plant.GetComponent<Seed_Growing>().Fruit = null;
        field_Color = create_Field_Spown.GetComponent<Renderer>(); // �̸����� �� ���� �ٲ��ֱ� ���� renderer ������
        Weapon_Set_Off();
        field_rotation = farm_Field.transform.rotation; // �� �̸����� ȸ�� �� �־���
    }

    // Update is called once per frame
    void Update()
    {
        print(seed_Plant_Time);
        Field_Create(); // �� ���� �޼���
        Seed_plant(); // ���� �ɱ� �޼���
        Spraying_Water(); // �� �Ѹ��� �޼���
        Sleep(); // �� �ڱ� �޼���
        House_Spown(); // ������ �̵��ϴ� �޼���
        Farm_Spown(); // �������� �̵��ϤӴ� �޼���
    }

    public void All_Weapon_Off()
    {
        weapon_Field.SetActive(false);
        weapon_Plant.SetActive(false);
        weapon_Water.SetActive(false);
        create_Field_Spown.gameObject.SetActive(false);
        Work.Instance.SetFalseWeapon();//��ȣ ���� ���ֱ�
    }

    void Field_Create() // �� ���� �޼���
    {
        if (weapon_Field.activeSelf == false)
        {
            Movement.instance.field_Create_move = false;
        }
        if (corutine_Double_Check == false && House_Check == false) // ��ų �����߿� ���� ��ü �Ұ��� + ���� �������� ���� ��� �Ұ���
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Work.Instance.SetFalseWeapon();//��ȣ ���� ���ֱ�
                                               // z Ű �������� ���̰� ���ٸ� ����
                if (weapon_Field.activeSelf == false)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    weapon_Field.SetActive(true);
                    create_Field_Spown.gameObject.SetActive(true);

                    select_Weapon = weapon_Field; // ���õ� ���� �־���
                    
                    create_Field_Spown.transform.rotation = field_rotation;
                    Movement.instance.field_Create_move = true;
                }
                // z Ű �������� ���̰� ������ ����
                else if (weapon_Field.activeSelf == true)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    Movement.instance.field_Create_move = false;
                }
            }
        }
        // �� ����

         //�� �̸������� ȸ������ ����Ƽ�� ������ ȸ�� ������ �����ص�
        create_Field_Spown.transform.position = Spown_View.transform.position;
        layer = 1 << LayerMask.NameToLayer("Field"); // �� ���̾ �־ ���̸� ��
        // �� �� �ɷ����� ���� �߻� ���ڸ����
        hit_Field = Physics.BoxCastAll(Spown_View.transform.position, new Vector3(0.95f, 1, 0.95f), transform.forward, transform.rotation, 0.01f, layer);
        // ���̰� �ִٸ�
        if (weapon_Field.activeSelf == true)
        {
            // ����(���� ���ٸ�)�� �ƹ��͵� �ɸ��� �ʾҴٸ� ����
            if (hit_Field.Length == 0)
            {
                // �� �̸����� ������ ����
                field_Color.material.color = Color.yellow;
                if (Input.GetMouseButtonDown(0))
                {
                    if (corutine_Double_Check == false)
                    {
                        if (Pirodo.instance.pirodo_Slider.value <= 0) // �Ƿε��� 0�̸� ������
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.Hunger_P_M(5);
                            Pirodo.instance.pirodo_Slider.value = 80;
                        }
                        else if (Pirodo.instance.hunger_Slider.value <= 0) // ��Ⱑ 0�̸� ������
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.hunger_Slider.value = 5;
                            Pirodo.instance.pirodo_Slider.value = 85;
                        }
                        else
                        {
                            Pluse_Minus(select_Weapon.GetComponent<Weapon>()); // �Ƿε��� ��� ���ҽ����ִ� ��
                            StartCoroutine("Create_Field");
                        }
                    }
                    else if (corutine_Double_Check == true)
                    {
                        print("�̹� �� �ڷ�ƾ�� ���� ���Դϴ�.");
                    }
                }
            }
            else
            {
                field_Color.material.color = Color.red;
            }
        }
    }
    
    void Seed_plant() //���� �ɱ� �޼���
    {
        if(corutine_Double_Check == false && House_Check == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Work.Instance.SetFalseWeapon();//��ȣ ���� ���ֱ�
                                               // X Ű �������� ���� ���ٸ� ����
                if (weapon_Plant.activeSelf == false)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    select_Weapon = weapon_Plant;
                    weapon_Plant.SetActive(true); // ���� �ɱ� ���� �����ְ� �Ʒ� ������ ������Ʈ�� ����
                }
                // X Ű �������� ���� ������ ����
                else if (weapon_Plant.activeSelf == true)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    weapon_Plant.SetActive(false);
                }
            }
        }
        

        // ���� �ɱ�
        if (weapon_Plant.activeSelf == true)
        {
            // �� �̸������� ȸ������ ����Ƽ�� ������ ȸ�� ������ �����ص�
            create_Field_Spown.transform.position = Spown_View.transform.position;
            layer = 1 << LayerMask.NameToLayer("Seed");
            // �ڽ� ����� ������ ������(�ڽ� ���� ��ġ, ũ��, ����, �Ÿ�, ã�Ƴ� ���̾�) �ϰ�?
            RaycastHit[] hits_Seed = Physics.BoxCastAll(Spown_View.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, transform.rotation, 0.05f, layer);
          


            if (plant.GetComponent<Seed_Growing>().Fruit == null)
            {
                print("���õ� ������ �����ϴ�.");
            }
            else if (plant.GetComponent<Seed_Growing>().Fruit != null)
            {
                if (hit_Field.Length > 0)
                {
                    if (hit_Field[0].transform.Find("Fruit") == null)
                    {
                        print("���ѽɱ� ����");
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (corutine_Double_Check == false)
                            {
                                if (Pirodo.instance.pirodo_Slider.value <= 0) // �Ƿε��� 0�̸� ������
                                {
                                    All_Weapon_Off();
                                    StartCoroutine("Sleeping");
                                    transform.position = sleep_Spown.transform.position;
                                    Pirodo.instance.Hunger_P_M(5);
                                    Pirodo.instance.pirodo_Slider.value = 80;
                                }
                                else if (Pirodo.instance.hunger_Slider.value <= 0) // ��Ⱑ 0�̸� ������
                                {
                                    All_Weapon_Off();
                                    StartCoroutine("Sleeping");
                                    transform.position = sleep_Spown.transform.position;
                                    Pirodo.instance.hunger_Slider.value = 5;
                                    Pirodo.instance.pirodo_Slider.value = 85;
                                }
                                else
                                {
                                    Pluse_Minus(select_Weapon.GetComponent<Weapon>()); // �Ƿε��� ��� ���ҽ����ִ� ��
                                    StartCoroutine("Seed_Planting");
                                }
                            }
                            else if (corutine_Double_Check == true)
                            {
                                print("�̹� ���� �ڷ�ƾ�� ���� ���Դϴ�.");
                            }
                        }
                    }
                    else
                    {
                        print("������ �ɾ��� �ֽ��ϴ�.");
                    }
                }
            }
        }
    }

    void Spraying_Water() // ���Ѹ��� �޼���
    {
       if(corutine_Double_Check == false && House_Check == false)
        {
            // �� �Ѹ���
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Work.Instance.SetFalseWeapon();//��ȣ ���� ���ֱ�
                                               // z Ű �������� ���Ѹ����� ���ٸ� ����
                if (weapon_Water.activeSelf == false)
                {
                    Disaster_Blocking.instance.Blocking_Off();
                    Weapon_Set_Off();
                    //sleep_Spown = weapon_Water;
                    select_Weapon = weapon_Water;
                    weapon_Water.SetActive(true);
                }
                // z Ű �������� ���Ѹ����� ������ ����
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
            // �ڽ� ����� ������ ������(�ڽ� ���� ��ġ, ũ��, ����, �Ÿ�, ã�Ƴ� ���̾�) �ϰ�?
            RaycastHit[] hits = Physics.BoxCastAll(Spown_View.transform.position, farm_Field.transform.localScale * 1.5f, transform.forward, transform.rotation, 0.05f, layer);

            if (hits.Length >= 1)
            {
                if (Input.GetMouseButtonDown(0) && hit_Field[0].transform.GetComponent<Field_Manager>().water_Ok == false/*hit_Field[0].transform.GetComponent<Field_Manager>().Water_Count <= 500*/)
                {
                    if (corutine_Double_Check == false)
                    {
                        if (Pirodo.instance.pirodo_Slider.value <= 0) // �Ƿε��� 0�̸� ������
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.Hunger_P_M(5);
                            Pirodo.instance.pirodo_Slider.value = 80;
                        }
                        else if (Pirodo.instance.hunger_Slider.value <= 0) // ��Ⱑ 0�̸� ������
                        {
                            All_Weapon_Off();
                            StartCoroutine("Sleeping");
                            transform.position = sleep_Spown.transform.position;
                            Pirodo.instance.hunger_Slider.value = 5;
                            Pirodo.instance.pirodo_Slider.value = 85;
                        }
                        else
                        {
                            Pluse_Minus(select_Weapon.GetComponent<Weapon>()); // �Ƿε��� ��� ���ҽ����ִ� ��
                            StartCoroutine("Watering", hit_Field);
                        }
                    }
                    else if (corutine_Double_Check == true)
                    {
                        print("�̹� �� �Ѹ��� �ڷ�ƾ�� ���� ���Դϴ�.");
                    }
                }
            }
            else
            {
                print("���� �̹� �ѷȽ��ϴ�.");
            }
        }
    }

    // �Ű������� �Ƿε��� ��� ���� �޾ƿ�
    public void Pluse_Minus(Weapon My_Weapon)
    {
        Pirodo.instance.Pirodo_P_M(My_Weapon.pirodo + My_Weapon.Loss_pirodo_Amount); // ���Ⱑ ���� �Ƿε� ���� �����ͼ� �׸�ŭ �Ƿε��� �ٿ��ش�.
        Pirodo.instance.Hunger_P_M(My_Weapon.hunger); // ���Ⱑ ���� ��⸦ �ٿ��ִ� ���� �����ͼ� �׸�ŭ ��⸦ �ٿ��ش�.
    }
    void Sleep()
    {
        layer = 1 << LayerMask.NameToLayer("Bed");
        // �ڽ� ����� ������ ������(�ڽ� ���� ��ġ, ũ��, ����, �Ÿ�, ã�Ƴ� ���̾�) �ϰ�?
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(2, 2, 2), transform.forward, transform.rotation, 0.05f, layer);

        if (hits.Length == 1)
        {
            print("ħ�븦 ã��");
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

    private void OnDrawGizmos() // �ڽ� ������ ������ �� �󿡼� �����ֱ� ���� �޼���
    {
        // �� ���� ����
        Gizmos.color = Color.red;
        // ���� �����ִ� �ǵ� ���� �����ص� �ڽ� ũ��� �ٸ��ϱ� �����ϼ���
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
        weapon_Plant.SetActive(false); // ���� �ɱ� ���� �����ְ� �Ʒ� ������ ������Ʈ�� ����
        weapon_Field.SetActive(false);
        create_Field_Spown.SetActive(false);
        weapon_Water.SetActive(false);
    }

    // �� �Ѹ��� �ڷ�ƾ
    IEnumerator Watering(RaycastHit[] raycastHit)
    {

        corutine_Double_Check = true;
        Movement.instance.move_Switch = 1; // �ϴ��� �س��°ǵ� 1�� ĳ���� �������� ���ϵ��� ���°���
        Movement.instance.animator.SetBool("stay", false); // ������ �ִ� �ִϸ��̼� ����
        Movement.instance.animator.SetBool("Water", true); // ���� �ɴ� �ִϸ��̼� ����
        yield return new WaitForSecondsRealtime(watering_Time - select_Weapon.GetComponent<Weapon>().Loss_Work_Time);
        Movement.instance.animator.SetBool("Water", false);
        Movement.instance.animator.SetBool("stay", true);
        Movement.instance.move_Switch = 0;
        //raycastHit[0].transform.GetComponent<Field_Manager>().Water_Full(500f);
        raycastHit[0].transform.GetComponent<Field_Manager>().Water_Full(true);
        corutine_Double_Check = false;
    }
    // ���� �ɱ� �ڷ�ƾ
    IEnumerator Seed_Planting()
    {
        corutine_Double_Check = true;
        Movement.instance.move_Switch = 1; // �ϴ��� �س��°ǵ� 1�� ĳ���� �������� ���ϵ��� ���°���
        Movement.instance.animator.SetBool("seed_plant", true); // ���� �ɴ� �ִϸ��̼� ����
        Movement.instance.animator.SetBool("stay", false); // ������ �ִ� �ִϸ��̼� ����
        yield return new WaitForSecondsRealtime(seed_Plant_Time);
        GameObject plantt = Instantiate(plant, new Vector3(hit_Field[0].transform.position.x, 0, hit_Field[0].transform.position.z), Quaternion.identity); // ���� ����
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

    // �� 
    IEnumerator Create_Field()
    {
        corutine_Double_Check = true;
        // �� ���� �ڷ�ƾ
        Movement.instance.move_Switch = 1;
        Movement.instance.animator.SetBool("field_create", true);
        Movement.instance.animator.SetBool("stay", false);
        ParticleManager.instance.PlayParticle("CreateFiled", create_Field_Spown.transform);//��ƼŬ ����
        yield return new WaitForSecondsRealtime(field_Create_Time - select_Weapon.GetComponent<Weapon>().Loss_Work_Time);

        //          ������ ������Ʈ, ���� ��ġ(x, y, z), ȸ����
        Instantiate(farm_Field, new Vector3(create_Field_Spown.transform.position.x, create_Field_Spown.transform.position.y, create_Field_Spown.transform.position.z), field_rotation);
        ParticleManager.instance.PlayParticle("CreateFiled", create_Field_Spown.transform);//��ƼŬ ����
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
            f.GetComponent<Field_Manager>().water_Ok = false; // �ڰ� �Ͼ���� ��� ���� ������ ���� ����
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

    public void DayNightCount()        // ���� ��Ǹ� ī��Ʈ ����
    {
        day_Count += 1;
        print("������ day ����");
    }
}
