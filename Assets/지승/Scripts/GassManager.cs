using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GassManager : MonoBehaviour
{


    [SerializeField] float ReProduceTime;//�ܵ� ����� �ð�
    [SerializeField] Transform[] Area;
    [SerializeField] GameObject Gass;


    public static GassManager instance;

    public int MaxgrassCount;// �ִ��ܵ� ����
    public float curTime = 0;
    public float LossFruitTime;//���Ÿ� �Ҵµ� �ɸ��� �ð�/       �ܵ� max�� �ֶ� �ش� �ð��� ������ ���Ű� �������
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
        
        if(MaxgrassCount <= count)//���� �ܵ� ����
        {
            Full_Grass();
          
        }
    }

    IEnumerator Rerroduce()
    {

        curTime = 0;
        count = 0;
        // �ڽ� ������Ʈ �� Grass_02(Clone) ������ ���� ����
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
                print("�۹� 1�� �ٿ���");
            }

            curTime = 0;

        }

    }


}
