using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tornado : MonoBehaviour
{
    private float td_Sp = 1f;         //???????? ????
    private float td_Area_x;
    private float td_Area_z;
    private bool td_On_Move = false;
    public float td_Move_Time = 1f;          //???????? ???????? ????

    public int td_Rd_Count = 0;         //???????? ????????
    public float td_Play_Time = 4f;     //???????? ????????

    private Rigidbody td_Rigidbody;
    private SphereCollider td_Collider;
    public ParticleSystem td_ptc;
    public AudioSource td_sd;

    GameObject farm_tomato;
    private bool farm_Angle_check = true;

    void TornadoUpdate()
    {
        this.td_Play_Time -= Time.deltaTime;
        if (this.td_Play_Time <= 0)
        {
            switch (this.td_Rd_Count)
            {
                case 2:
                    td_Rigidbody.transform.localEulerAngles = new Vector3(-90, 0, 0);       //???? ????
                    transform.position = new Vector3(td_Area_x, td_Area_z, 0);              //?????? ???? ??????
                    td_Rigidbody.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(-90, 0, 0));


                    td_ptc.Play();
                    td_sd.Play();
                    td_Rd_Count = 0;       //???????? ????
                    td_Play_Time = 15f;
                    td_On_Move = true;
                    break;
                default:
                    td_ptc.Stop();
                    td_sd.Stop();
                    td_On_Move = false;
                    td_Rd_Count = Random.Range(0, 10); // 5;            //???????? ????
                    td_Play_Time = 30f;
                    break;
            }
        }
    }




    void TornadoMove()
    {
        if (td_On_Move == true)
        {
            this.td_Move_Time -= Time.deltaTime;
            if (this.td_Move_Time <= 0)
            {
                this.td_Move_Time = 3f;
                td_Area_x = Random.Range(-1, 1);     //x???? ????
                td_Area_z = Random.Range(-1, 1);     //z???? ????
            }

            td_Rigidbody.transform.localEulerAngles = new Vector3(-90, 0, 0);   //???????? ???? ????
            td_Rigidbody.velocity = new Vector3(td_Area_x, 0, td_Area_z);       //????
            td_Rigidbody.AddForce(td_Rigidbody.velocity * td_Sp);               //???????? ??

            OnTriggerEnter(td_Collider);        //?????? ????

        }
        else
        {
            td_Area_x = 0f;                                           
            td_Area_z = 0f;
            td_Rigidbody.velocity = new Vector3(0, 0, 0);             //???? ??????  
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        td_Collider.radius = 25f;       //???????? ?????? ????

        List<int> fmReset = new List<int>();
        fmReset.Add(Random.Range(0, 3));

        if (other.tag == "Farm")
        {
            

            if (fmReset[0] == 2)
            {
                if (farm_Angle_check == true)
                {

                    farm_tomato.transform.localEulerAngles = new Vector3(0, 0, 90);
                }
            }
            else if (fmReset[0] == 1)
            {
                Destroy(other.gameObject);
            }
            
        }

        if (other.tag == "Item")
        {
        if (fmReset[0] == 1)
            {
                Destroy(other.gameObject);
            }
        }

        fmReset.Clear();
    }


    void Start()
    {
        td_ptc.Stop();
        td_sd.Stop();

        farm_tomato = GameObject.FindGameObjectWithTag("Farm");
        farm_Angle_check = true;

        td_Rigidbody = GetComponent<Rigidbody>();
        td_Collider = GetComponent<SphereCollider>();

        

    }


    void Update()
    {
        TornadoUpdate();
        TornadoMove();
    }
}
