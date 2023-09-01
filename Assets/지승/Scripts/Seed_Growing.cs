using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_Growing : MonoBehaviour
{
    public GameObject Fruit;

    public static Seed_Growing instance;

    public GameObject PreantsField;//�ɰ��� �ִ� ��

    public float Seed_Growing_Time = 60f;

    public bool check = false;

    private void Awake()
    {
        Seed_Growing_Time = Fruit.GetComponent<Seed_Type>().growing_Time;
         instance = this;

    }

    private void Update()
    {
        if(check == true)
        {
            Seeds_Grow();
        }
    }

    public void Seeds_Grow()
    {
        if (Seed_Growing_Time <= 0)
        {
            GameObject Fruit_Perfact = Instantiate(Fruit, transform.position, Quaternion.identity); // identity�� ���� ȸ���� ���°� �� �⺻��
            transform.parent = Fruit_Perfact.transform;
            Fruit_Perfact.transform.name = "Fruit";
            check = false;
            if (PreantsField != null)
            {
                Fruit_Perfact.transform.parent = PreantsField.transform;
            }
        }
        else
        {
            Seed_Growing_Time -= Time.deltaTime;
        }
    }
    /*IEnumerator Seeds_Grow()
    {
        yield return new WaitForSecondsRealtime(Seed_Growing_Time);
        // �۹� �ϼ�
        GameObject Fruit_Perfact = Instantiate(Fruit, transform.position, Quaternion.identity); // identity�� ���� ȸ���� ���°� �� �⺻��
        transform.parent = Fruit_Perfact.transform;
        Fruit_Perfact.transform.name = "Fruit";
        if (PreantsField != null)
        {
            Fruit_Perfact.transform.parent = PreantsField.transform;
        }
    }*/
}