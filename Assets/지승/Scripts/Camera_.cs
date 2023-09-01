using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ : MonoBehaviour
{
    public GameObject player; // ī�޶� ����ٴ� ����� Transform ������Ʈ

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
