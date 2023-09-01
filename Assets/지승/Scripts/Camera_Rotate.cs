using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotate : MonoBehaviour
{
    public Transform target; // ī�޶� ����ٴ� Ÿ�� ������Ʈ(Transform)
    public float distance = 10.0f; // ī�޶�� Ÿ�� ������ �Ÿ�
    public float xSpeed = 250.0f; // ī�޶� ȸ�� �ӵ�
    public float ySpeed = 120.0f;
    public float yMinLimit = -20.0f; // ī�޶��� y�� ȸ�� ����
    public float yMaxLimit = 80.0f;
    public float distanceMin = 5.0f; // ī�޶�� Ÿ�� ������ �ּ� �Ÿ�
    public float distanceMax = 15.0f; // ī�޶�� Ÿ�� ������ �ִ� �Ÿ�
    public float zoomSpeed = 2.0f; // ����/�ƿ� �ӵ�

    private float x = 0.0f; // ī�޶��� x�� ȸ����
    private float y = 0.0f; // ī�޶��� y�� ȸ����
    public float offsetX = 0f;
    public float offsetY = 25;
    public float offsetZ = -35f;
    public float minCameraHeight = 0f;
    Vector3 cameraPosition;
    
    public static Camera_Rotate instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1)) // ���콺 ��Ŭ�� ��ư�� ������
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel"); // ���콺 �� ��ư�� ������
        distance -= scroll * zoomSpeed; // �Ÿ���(distance)�� ���콺 ���� ��ũ�� ���� ���� ������
        distance = Mathf.Clamp(distance, distanceMin, distanceMax); // �ּ�/�ִ� �Ÿ����� ����� �ʵ��� ��

        //Ȯ�� ���


        Quaternion newRotation = Quaternion.Euler(y, x, 0);
        Vector3 newPosition = newRotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        transform.rotation = newRotation;
        transform.position = newPosition;
    }
   

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}

