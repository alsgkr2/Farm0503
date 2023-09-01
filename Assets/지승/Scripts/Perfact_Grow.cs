
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perfact_Grow : MonoBehaviour
{
  

    [SerializeField]
    GameObject perfact_Fruit;

    public static Perfact_Grow Instance;

    public int Fruit_Amount;//��Ȯ�� ��� ���� ����
    public int MaxAmount;//�ִ�� ���� �� �ִ� ���� ����
    private void Start()
    {
        Instance = this;
        MaxAmount = 3;
        Fruit_Amount = Random.Range(1, MaxAmount);
    }

    private void OnDestroy()
    {
        //Cuting();
    }

    public void Cuting(Weapon weapon)
    {

        for (int i = 0; i < Fruit_Amount+ weapon.Plus_Furit_Amount; i++)
        {
            GameObject F = Instantiate(perfact_Fruit, new Vector3(transform.position.x, transform.position.y + 0.2f * i, transform.position.z), Quaternion.identity);
            F.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);

        }
        print("������ �����");
    }

    [System.Obsolete]
    public void DownFruitAmount(int count=0,string What="")//��Ȯ���� ���ڸ� �ٿ��ִ� ��
    {

        switch (What)
        {
            case "Grass":
                Fruit_Amount -= count;
                break;

            case "Rain":
                if (transform.parent.FindChild("Rain_Blocking(Clone)") == null)
                {
                    Fruit_Amount -= count;
                }
                //transform.parent.GetComponent<Field_Manager>().Water_Full(500f);
                transform.parent.GetComponent<Field_Manager>().water_Ok = true;

                break;

            case "Hurricane":
                if (transform.parent.FindChild("Hurricane_Blocking(Clone)")==null)
                {
                    Destroy(transform.parent);
                }
                break;

            case "Bird":
                Fruit_Amount -= count;
                break;

            default:
                break;
        }

        Fruit_Amount -= count;

    }

}