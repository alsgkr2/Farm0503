using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Work : MonoBehaviour
{
    public TextMeshProUGUI Grow_Timer_Text;

    public static Work Instance;

    [SerializeField] Animator ani;
    [SerializeField] Transform WorkBoxPos;//농작물 캐는 범위의 중심
    [SerializeField] Vector3 interaction_Range;//농작물 캐는 범위
    //나무 베기 걸리는 시간
    [SerializeField] float cut_Tree_Time;
    //수확하기 걸리는 시간
    [SerializeField] float cut_Fruit_Time;
    //잡초 제거 걸리는 시간
    [SerializeField] float cut_grass_Time;
    //ray되는 사거리
    [SerializeField] float _maxDistance;
    //선택된 오브젝트
    [SerializeField] GameObject Select_Obj;    
    //선택된 씨앗
    [SerializeField] GameObject Select_Seed;
    //농기구 모음
    [SerializeField] GameObject[] Weapons;//0 맨손 1 도끼 2 낫  layer 8  64  128
    //현제 들고 있는 농기구
    [SerializeField] GameObject Select_Weapon;
    [SerializeField] LayerMask layer;//2^무기레이어값

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

    void ChoiceWepon()//여기 layer는 2^무기의 레이어
    {
        if (Farm_Move.instance.corutine_Double_Check == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Farm_Move.instance.All_Weapon_Off();
                // Farm_Move.instance.Weapon_Set_Off();
                //손
                SetFalseWeapon();
                Select_Weapon = Weapons[0];
                Select_Weapon.SetActive(true);
                layer = 512;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Farm_Move.instance.All_Weapon_Off();

                // Farm_Move.instance.Weapon_Set_Off();
                //도끼
                SetFalseWeapon();
                Select_Weapon = Weapons[1];
                Select_Weapon.SetActive(true);
                layer = 1024;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Farm_Move.instance.All_Weapon_Off();

                //Farm_Move.instance.Weapon_Set_Off();
                //낫
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
                switch (Select_Obj.layer)//<-무기의 레이어  
                {
                    case 10:
                        print("현제 무기는 도끼 이다");
                        if (Select_Weapon == Weapons[1])
                        {
                            //나무 레이어

                            //나무 베는 애니메이션
                            ani.SetBool("CutTree", true);
                            //애니메이션끝날때
                            StartCoroutine("Ani_End", "CutTree");
                        }

                        break;

                    case 9:
                        print("현제 무기는 손 이다");
                        //수확 레이어
                        if (Select_Weapon == Weapons[0])
                        {
                            //수확 레이어

                            //수확 애니메이션
                            ani.SetBool("harvest", true);
                            //애니메이션끝날때
                            StartCoroutine("Ani_End", "harvest");

                        }
                        break;

                    case 11:
                        print("현제 무기는 낫 이다");
                        //잡초제거 레이어
                        if (Select_Weapon == Weapons[2])
                        {
                            //잡초제거 레이어

                            //잡초제거 애니메이션
                            ani.SetBool("cut_grass", true);
                            //애니메이션끝날때
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
            print("주변에 아무것도 없다");
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
                //나무 베는 애니메이션
                yield return new WaitForSeconds(cut_Tree_Time-Select_Weapon.GetComponent<Weapon>().Loss_Work_Time);
                Destroy(Select_Obj);
                //애니메이션끝날때
                ani.SetBool("CutTree", false);
                Farm_Move.instance.corutine_Double_Check = false;
                if (Pirodo.instance.pirodo_Slider.value <= 0) // 피로도가 0이면 쓰러짐
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.Hunger_P_M(5);
                    Pirodo.instance.pirodo_Slider.value = 80;
                }
                else if (Pirodo.instance.hunger_Slider.value <= 0) // 허기가 0이면 쓰러짐
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
                //수확하는 애니메이션
                Farm_Move.instance.corutine_Double_Check = true;
                yield return new WaitForSeconds(cut_Fruit_Time - Select_Weapon.GetComponent<Weapon>().Loss_Work_Time);
                Select_Obj.GetComponent<Perfact_Grow>().Cuting(Select_Weapon.GetComponent<Weapon>());
                ParticleManager.instance.PlayParticle("Harvest", Select_Obj.transform);
                Select_Obj.GetComponentInParent<Field_Manager>().Seed_True = false;
                Select_Obj.GetComponentInParent<Field_Manager>().water_Ok = false;

                Destroy(Select_Obj);
                //애니메이션끝날때
                ani.SetBool("harvest", false);
                Farm_Move.instance.corutine_Double_Check = false;
                if (Pirodo.instance.pirodo_Slider.value <= 0) // 피로도가 0이면 쓰러짐
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.Hunger_P_M(5);
                    Pirodo.instance.pirodo_Slider.value = 80;
                }
                else if (Pirodo.instance.hunger_Slider.value <= 0) // 허기가 0이면 쓰러짐
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
                //잔디 자르기
                Farm_Move.instance.corutine_Double_Check = true;
                yield return new WaitForSeconds(cut_grass_Time - Select_Weapon.GetComponent<Weapon>().Loss_Work_Time);

                Destroy(Select_Obj);
                //애니메이션끝날때
                ani.SetBool("cut_grass", false);
                Farm_Move.instance.corutine_Double_Check = false;

                Select_Obj.GetComponentInParent<GassManager>().count--;

                if (Pirodo.instance.pirodo_Slider.value <= 0) // 피로도가 0이면 쓰러짐
                {
                    SetFalseWeapon();
                    Farm_Move.instance.All_Weapon_Off();
                    Farm_Move.instance.Start_Sleeping();
                    Pirodo.instance.Hunger_P_M(5);
                    Pirodo.instance.pirodo_Slider.value = 80;
                }
                else if (Pirodo.instance.hunger_Slider.value <= 0) // 허기가 0이면 쓰러짐
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
            float Timeleft = 0;//남은 시간
            //Timeleft=Fruit.GetComponent<남는시간이 있는 스크립트>().자라는데 남는시간; <-업데이트에 넣어주쇼
        }
    }

    /*기즈모*/
    private Color _rayColor = Color.blue;

    private void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;

        Gizmos.DrawWireCube(transform.position, interaction_Range);
    }


    /*UI 작물에 가까히 갔을 때 현제 작물의 상태 가지고 오기*/
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