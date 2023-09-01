using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Move()
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float runSpeed = 10f;
    [SerializeField]
    float cameraMaxSpeed = 0f;
    [SerializeField]
    Vector3 startMouse_pos;
    [SerializeField]
    Vector3 moveMouse_pos;
    [SerializeField]
    float x = 0f;
    [SerializeField]
    float z = 0f;
    public int move_Switch = 0; // 0 -> 움직임, 1 -> 안움직임, 2 -> 위, 아래로만 이동 가능
    public bool field_Create_move = false; // 밭 만들때 회전 불가능하게 만들기 위한 변수
    Vector3 move;

    //RunMode()
    bool runMode = false;
    float rotateSpeed = 3f;

    // CameraView()
    [SerializeField]
    Transform playerCenterCamera_FirstPos;
    [SerializeField]
    Transform playerCenterCamera;


    // Sleep()
    

    public static Movement instance;
    public Animator animator;
    Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playerCenterCamera_FirstPos = playerCenterCamera.transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RunMode();
    }

    private void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (move_Switch == 0)
        {
            Vector3 direction = new Vector3(x, 0f, z).normalized; // 이동 방향 벡터 계산 후 정규화 비스듬히 이동할때 속도를 일정하게 함

           // transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            move.Set(direction.x, 0, direction.z);
            if (x == 0f && z == 0f)
            {
                return;
            }
            else
            {
                if (field_Create_move == false)
                {
                    // 캐릭터 애니메이션 회전
                    Quaternion newRotation = Quaternion.LookRotation(move);

                    rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.deltaTime);
                }
                else if (field_Create_move == true)
                {

                }
            }

            // 움직이고 있을때
            if (x != 0 || z != 0)
            {
                // 뛰고있을때
                if (runMode == true)
                {
                    animator.SetBool("run", true);
                    animator.SetBool("walk", false);
                    animator.SetBool("stay", false);
                    //rb.transform.Translate(new Vector3(x, 0, z) * runSpeed * Time.deltaTime);
                    rb.MovePosition(transform.position + (move * runSpeed * Time.deltaTime));
                }
                // 뛰고있지 않을때
                else
                {
                    animator.SetBool("run", false);
                    animator.SetBool("walk", true);
                    animator.SetBool("stay", false);
                    rb.MovePosition(transform.position + (move * moveSpeed * Time.deltaTime));

                    // rb.transform.Translate(new Vector3(x, 0, z) * moveSpeed * Time.deltaTime);
                }
            }
        }
        else if (move_Switch == 1)
        {
            print("스킬 실행중 움직이지 않습니다.");
        }
        else if (move_Switch == 2) // 괭이를 들고있을 때 상, 하, 좌, 우로만 움직이게 함
        {

        }
    }

    
    void RunMode()
    {
        // 움직이지 않을때
        if (x == 0 && z == 0)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            animator.SetBool("stay", true);
        }
        // 달리기 모드설정
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (runMode == true)
            {
                runMode = false;
            }
            else
            {
                runMode = true;
            }
        }
    }

    
}

