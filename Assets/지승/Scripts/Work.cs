using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Work : MonoBehaviour
{
    public TextMeshProUGUI Grow_Timer_Text;

    public static Work Instance;

    [SerializeField] Animator ani;
    [SerializeField] Transform WorkBoxPos;//���۹� ĳ�� ������ �߽�
    [SerializeField] Vector3 interaction_Range;//���۹� ĳ�� ����
    //���� ���� �ɸ��� �ð�
    [SerializeField] float cut_Tree_Time;
    //��Ȯ�ϱ� �ɸ��� �ð�
    [SerializeField] float cut_Fruit_Time;
    //���� ���� �ɸ��� �ð�
    [SerializeField] float cut_grass_Time;
    //ray�Ǵ� ��Ÿ�
    [SerializeField] float _maxDistance;
    //���õ� ������Ʈ
    [SerializeField] GameObject Select_Obj;    
    //���õ� ����
    [SerializeField] GameObject Select_Seed;
    //��ⱸ ����
    [SerializeField] GameObject[] Weapons;//0 �Ǽ� 1 ���� 2 ��  layer 8  64  128
    //���� ��� �ִ� ��ⱸ
    [SerializeField] GameObject Select_Weapon;
    [SerializeField] LayerMask layer;//2^���ⷹ�̾

    bool corutine_Check = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //print(layer.value);
        Interction();
        ChoiceWepon();

        Check_Fruit();


    }

    void ChoiceWepon()//���� layer�� 2^������ ���̾�
    {
        if (Farm_Move.instance.corutine_Double_Check == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Farm_Move.instance.All_Weapon_Off();
                // Farm_Move.instance.Weapon_Set_Off();
                //��
                SetFalseWeapon();
                Select_Weapon = Weapons[0];
                Select_Weapon.SetActive(true);
                layer = 512;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Farm_Move.instance.All_Weapon_Off();

                // Farm_Move.instance.Weapon_Set_Off();
                //����
                SetFalseWeapon();
                Select_Weapon = Weapons[1];
                Select_Weapon.SetActive(true);
                layer = 1024;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Farm_Move.instance.All_Weapon_Off();

                //Farm_Move.instance.Weapon_Set_Off();
                //��
                SetFalseWeapon();
                Select_Weapon = Weapons[2];
                Select_Weapon.SetActive(true);
                layer = 2048;
            }
        }
    }

    public void SetFalseWeapon()
    {
        foreach (GameObject w in Weapons)
        {
            w.SetActive(false);
        }
        Select_Weapon = null;
    }

    void Interction()
    {
        if (Select_Weapon != null)
        {
            Select_Obj = Find_Any_Interaction(Select_Weapon);

        }

        if (Input.GetMouseButtonDown(0) && Select_Obj != null)
        {
            if (corutine_Check == false)
            {
                switch (Select_Obj.layer)//<-������ ���̾�  
                {
                    case 10:
                        print("���� ����� ���� �̴�");
                        if (Select_Weapon == Weapons[1])
                        {
                            //���� ���̾�

                            //���� ���� �ִϸ��̼�
                            ani.SetBool("CutTree", true);
                            //�ִϸ��̼ǳ�����
                            StartCoroutine("Ani_End", "CutTree");
                        }

                        break;

                    case 9:
                        print("���� ����� �� �̴�");
                        //��Ȯ ���̾�
                        if (Select_Weapon == Weapons[0])
                        {
                            //��Ȯ ���̾�

                            //��Ȯ �ִϸ��̼�
                            ani.SetBool("harvest", true);
                            //�ִϸ��̼ǳ�����
                            StartCoroutine("Ani_End", "harvest");

                        }
                        break;

                    case 11:
                        print("���� ����� �� �̴�");
                        //�������� ���̾�
                        if (Select_Weapon == Weapons[2])
                        {
                            //�������� ���̾�

                            //�������� �ִϸ��̼�
                            ani.SetBool("cut_grass", true);
                            //�ִϸ��̼ǳ�����
                            StartCoroutine("Ani_End", "cut_grass");
                        }
                        break;

                    default:
                        break;

                }
            }
        }
    }

    GameObject Find_Any_Interaction(GameObject Weapon)
    {

        RaycastHit[] hits = Physics.BoxCastAll(transform.position, interaction_Range / 1.5f, transform.forward, transform.rotation, 0.5f, layer);


        if (hits != null)
        {

            foreach (RaycastHit hit in hits)
            {
                print(hit.transform.name);
                if (hit.transform.gameObject.layer == Select_Weapon.layer)
                {
                    return hit.transform.gameObject;

                }
                else
                {
                    return null;
                }

            }
            return null;
        }
        else
        {
            print("�ֺ��� �ƹ��͵� ����");
            return null;
        }
    }


    IEnumerator Ani_End(string aniName)
    {
        Movement.instance.move_Switch = 1;
        corutine_Check = true;
        switch (aniName)
        {
            case "CutTree":
                Farm_Move.instance.corutine_Double_Check = true;
                //���� ���� �ִϸ��̼�
                yield return new WaitForSeconds(cut_Tree_Time-Select_Weapon.GetComponent<Weapon>().Loss_Work_Time);
                Destroy(Select_Obj);
                //�ִϸ��̼ǳ�����
                ani.SetBool("CutTree", false);
                Farm_Move.instance.corutine_Double_Check = false;
                if (Pirodo.instance.pirodo_Slider.value <= 0) // �Ƿε��� 0�̸� ������
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.Hunger_P_M(5);
                    Pirodo.instance.pirodo_Slider.value = 80;
                }
                else if (Pirodo.instance.hunger_Slider.value <= 0) // ��Ⱑ 0�̸� ������
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.hunger_Slider.value = 5;
                    Pirodo.instance.pirodo_Slider.value = 85;
                }
                else
                {
                    Farm_Move.instance.Pluse_Minus(Select_Weapon.GetComponent<Weapon>());
                }
                break;

            case "harvest":
                //��Ȯ�ϴ� �ִϸ��̼�
                Farm_Move.instance.corutine_Double_Check = true;
                yield return new WaitForSeconds(cut_Fruit_Time - Select_Weapon.GetComponent<Weapon>().Loss_Work_Time);
                Select_Obj.GetComponent<Perfact_Grow>().Cuting(Select_Weapon.GetComponent<Weapon>());
                ParticleManager.instance.PlayParticle("Harvest", Select_Obj.transform);
                Select_Obj.GetComponentInParent<Field_Manager>().Seed_True = false;
                Select_Obj.GetComponentInParent<Field_Manager>().water_Ok = false;

                Destroy(Select_Obj);
                //�ִϸ��̼ǳ�����
                ani.SetBool("harvest", false);
                Farm_Move.instance.corutine_Double_Check = false;
                if (Pirodo.instance.pirodo_Slider.value <= 0) // �Ƿε��� 0�̸� ������
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.Hunger_P_M(5);
                    Pirodo.instance.pirodo_Slider.value = 80;
                }
                else if (Pirodo.instance.hunger_Slider.value <= 0) // ��Ⱑ 0�̸� ������
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.hunger_Slider.value = 5;
                    Pirodo.instance.pirodo_Slider.value = 85;
                }
                else
                {
                    Farm_Move.instance.Pluse_Minus(Select_Weapon.GetComponent<Weapon>());
                }
                break;

            case "cut_grass":
                //�ܵ� �ڸ���
                Farm_Move.instance.corutine_Double_Check = true;
                yield return new WaitForSeconds(cut_grass_Time - Select_Weapon.GetComponent<Weapon>().Loss_Work_Time);

                Destroy(Select_Obj);
                //�ִϸ��̼ǳ�����
                ani.SetBool("cut_grass", false);
                Farm_Move.instance.corutine_Double_Check = false;

                Select_Obj.GetComponentInParent<GassManager>().count--;

                if (Pirodo.instance.pirodo_Slider.value <= 0) // �Ƿε��� 0�̸� ������
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.Hunger_P_M(5);
                    Pirodo.instance.pirodo_Slider.value = 80;
                }
                else if (Pirodo.instance.hunger_Slider.value <= 0) // ��Ⱑ 0�̸� ������
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.hunger_Slider.value = 5;
                    Pirodo.instance.pirodo_Slider.value = 85;
                }
                else
                {
                    Farm_Move.instance.Pluse_Minus(Select_Weapon.GetComponent<Weapon>());
                }
                
                break;

            default:
                break;

        }
        Movement.instance.move_Switch = 0;
        corutine_Check = false;
        yield return null;
    }

    public void Check_Fruit_Grow_Time(GameObject Fruit)
    {
        if(Select_Obj!=null )
        {
            float Timeleft = 0;//���� �ð�
            //Timeleft=Fruit.GetComponent<���½ð��� �ִ� ��ũ��Ʈ>().�ڶ�µ� ���½ð�; <-������Ʈ�� �־��ּ�
        }
    }

    /*�����*/
    private Color _rayColor = Color.blue;

    private void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;

        Gizmos.DrawWireCube(transform.position, interaction_Range);
    }


    /*UI �۹��� ������ ���� �� ���� �۹��� ���� ������ ����*/
    void Check_Fruit()
    {
        LayerMask  layer_Seed = 1 << LayerMask.NameToLayer("Seed");
        Collider[] hits_Seed = Physics.OverlapBox(transform.position, interaction_Range / 2f, Quaternion.identity, layer_Seed);


        if (hits_Seed!=null &&hits_Seed.Length != 0)
        {
            Grow_Timer_Text.gameObject.SetActive(true);

            if (Select_Seed==null)
            {
                Select_Seed = hits_Seed[0].gameObject;
                Grow_Timer_Text.transform.position = new Vector3(Select_Seed.transform.position.x, Select_Seed.transform.position.y, Select_Seed.transform.position.z);
            }

            if(Select_Seed.GetComponent<Seed_Growing>().Seed_Growing_Time<=0)
            {
                Grow_Timer_Text.text = "";
            }
            else
            {
                Grow_Timer_Text.text = ((int)(Select_Seed.GetComponent<Seed_Growing>().Seed_Growing_Time)).ToString();

            }
        }
        else
        {
            Select_Seed = null;
            Grow_Timer_Text.gameObject.SetActive(false);
        }
    }
}