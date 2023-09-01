using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ : MonoBehaviour
{
    public GameObject player; // 카메라가 따라다닐 대상의 Transform 컴포넌트

    public float offsetX=0f;
    public float offsetY=25;
    public float offsetZ=-35f;

    Vector3 cameraPosition;

    public float follwSpeed;


    private void FixedUpdate()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, follwSpeed * Time.deltaTime);


    }
}
