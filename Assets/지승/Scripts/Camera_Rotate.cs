using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotate : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 타겟 오브젝트(Transform)
    public float distance = 10.0f; // 카메라와 타겟 사이의 거리
    public float xSpeed = 250.0f; // 카메라 회전 속도
    public float ySpeed = 120.0f;
    public float yMinLimit = -20.0f; // 카메라의 y축 회전 제한
    public float yMaxLimit = 80.0f;
    public float distanceMin = 5.0f; // 카메라와 타겟 사이의 최소 거리
    public float distanceMax = 15.0f; // 카메라와 타겟 사이의 최대 거리
    public float zoomSpeed = 2.0f; // 줌인/아웃 속도

    private float x = 0.0f; // 카메라의 x축 회전값
    private float y = 0.0f; // 카메라의 y축 회전값
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
        if (Input.GetMouseButton(1)) // 마우스 우클릭 버튼을 누르면
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel"); // 마우스 휠 버튼을 누르면
        distance -= scroll * zoomSpeed; // 거리값(distance)을 마우스 휠의 스크롤 값에 따라 조정함
        distance = Mathf.Clamp(distance, distanceMin, distanceMax); // 최소/최대 거리값을 벗어나지 않도록 함

        //확대 축소


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

