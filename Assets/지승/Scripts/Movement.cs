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
    public int move_Switch = 0; // 0 -> ������, 1 -> �ȿ�����, 2 -> ��, �Ʒ��θ� �̵� ����
    public bool field_Create_move = false; // �� ���鶧 ȸ�� �Ұ����ϰ� ����� ���� ����
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
            Vector3 direction = new Vector3(x, 0f, z).normalized; // �̵� ���� ���� ��� �� ����ȭ �񽺵��� �̵��Ҷ� �ӵ��� �����ϰ� ��

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
                    // ĳ���� �ִϸ��̼� ȸ��
                    Quaternion newRotation = Quaternion.LookRotation(move);

                    rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.deltaTime);
                }
                else if (field_Create_move == true)
                {

                }
            }

            // �����̰� ������
            if (x != 0 || z != 0)
            {
                // �ٰ�������
                if (runMode == true)
                {
                    animator.SetBool("run", true);
                    animator.SetBool("walk", false);
                    animator.SetBool("stay", false);
                    //rb.transform.Translate(new Vector3(x, 0, z) * runSpeed * Time.deltaTime);
                    rb.MovePosition(transform.position + (move * runSpeed * Time.deltaTime));
                }
                // �ٰ����� ������
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
            print("��ų ������ �������� �ʽ��ϴ�.");
        }
        else if (move_Switch == 2) // ���̸� ������� �� ��, ��, ��, ��θ� �����̰� ��
        {

        }
    }

    
    void RunMode()
    {
        // �������� ������
        if (x == 0 && z == 0)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            animator.SetBool("stay", true);
        }
        // �޸��� ��弳��
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

