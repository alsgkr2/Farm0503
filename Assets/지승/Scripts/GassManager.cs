using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GassManager : MonoBehaviour
{


    [SerializeField] float ReProduceTime;//잔디 재생성 시간
    [SerializeField] Transform[] Area;
    [SerializeField] GameObject Gass;


    public static GassManager instance;

    public int MaxgrassCount;// 최대잔디 개수
    public float curTime = 0;
    public float LossFruitTime;//열매를 잃는데 걸리는 시간/       잔디가 max개 있때 해당 시간이 지나면 열매가 사라진다
    bool isRunCoroutine;
    public int count = 0;
    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine("Rerroduce");

    }

    private void Update()
    {
        if (MaxgrassCount > count && isRunCoroutine == false)
        {
            StartCoroutine("Rerroduce");
        }
        
        if(MaxgrassCount <= count)//현제 잔디가 많음
        {
            Full_Grass();
          
        }
    }

    IEnumerator Rerroduce()
    {

        curTime = 0;
        count = 0;
        // 자식 오브젝트 중 Grass_02(Clone) 프리팹 개수 세기
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Grass_02(Clone)")
            {
                count++;
            }
        }

        if (MaxgrassCount > count)
        {
            isRunCoroutine = true; ;

            yield return new WaitForSeconds(ReProduceTime);
            isRunCoroutine = false;
            float posx = Random.Range(Area[0].position.x, Area[1].position.x);
            float posz = Random.Range(Area[0].position.z, Area[1].position.z);

            GameObject grass = Instantiate(Gass, new Vector3(posx, 0, posz), Quaternion.identity);
            grass.transform.SetParent(transform);
            StartCoroutine("Rerroduce");
        }

    }

    [System.Obsolete]
    void Full_Grass()//
    {
       

        curTime += Time.deltaTime;

        if (curTime >= LossFruitTime)
        {
           

            if (transform.GetComponentInChildren<Perfact_Grow>()!=null)
            {      
                transform.GetComponentInChildren<Perfact_Grow>().DownFruitAmount(1, "Grass");
                print("작물 1개 줄여듬");
            }

            curTime = 0;

        }

    }


}
