using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : Game_Manager
{
    // ESC ��ư
    private bool isESC_set = false;
    public GameObject ESC_UI;

    // ������Ʈ
    Rigidbody rigid;
    Animator anime;

    // �÷��̾� �ӵ�
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float currentSpeed;

    // ���� ���� ����
    private bool canJump = true; // ���� ���� ����
    public float jumpCooldown = 5f; // ���� ��� �ð� (5��)

    // �޸��� ��ٿ� ���� ����
    public float runDuration = 5f; // �ִ� �޸��� �ð� (5��)
    public float runCooldown = 5f; // �޸��� ��ٿ� �ð� (5��)
    private float runTimer = 0f; // �޸��� �ð� Ÿ�̸�
    private float cooldownTimer = 0f; // ��ٿ� Ÿ�̸�
    private bool canRun = true; // �޸��� ���� ����

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();

        // ���콺 Ŀ�� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // �⺻ �ȱ� �ӵ��� ����
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        PlayerAnimation();
        PlayerMove();
        PlayerRot();
        PlayerJump();
        ESC_Setting();

        // �޸��� Ÿ�̸� �� ��ٿ� ������Ʈ
        UpdateRunTimers();
    }

    void PlayerMove()
    {
        // �̵� �Է�
        fPlayerMov_X = Input.GetAxis("Horizontal");
        fPlayerMov_Y = Input.GetAxis("Vertical");
        vecPlayerPos = new Vector3(fPlayerMov_X, 0, fPlayerMov_Y).normalized;

        // �޸��� �Է�
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;

        // �޸��� ���¿� ���� �ӵ� ����
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        // �̵�
        transform.Translate(vecPlayerPos * currentSpeed * Time.deltaTime, Space.Self);

        // �޸��� ���̸� Ÿ�̸Ӹ� ������Ʈ
        if (isRunning)
        {
            runTimer += Time.deltaTime;

            // 5�ʰ� ������ �޸��� ���� �� ��ٿ� ����
            if (runTimer >= runDuration)
            {
                canRun = false; // �޸��� �Ұ�
                cooldownTimer = 0f; // ��ٿ� Ÿ�̸� �ʱ�ȭ
            }
        }
    }

    void PlayerAnimation() // �÷��̾� �ִϸ��̼�
    {
        if (fPlayerMov_X != 0 || fPlayerMov_Y != 0) // �ȱ⳪ �޸��� �Է��� ���� ��
        {
            anime.SetBool("isWalk", true); // �ȱ� �ִϸ��̼�

            if (Input.GetKey(KeyCode.LeftShift) && canRun) // �޸��� �Է��� ���� ��
            {
                anime.SetBool("isRun", true); // �޸��� �ִϸ��̼�
            }
            else
            {
                anime.SetBool("isRun", false); // �ȱ⸸
            }
        }
        else
        {
            anime.SetBool("isWalk", false); // ���� ����
            anime.SetBool("isRun", false);  // �޸��� �ִϸ��̼ǵ� ��
        }
    }

    void PlayerRot()
    {
        fPlayerRot_X = Input.GetAxis("Mouse X");
        vecPlayerRotPos_X = new Vector3(0, fPlayerRot_X, 0);
        transform.Rotate(vecPlayerRotPos_X * fMouse_Sens, Space.Self);
    }

    void ESC_Setting()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isESC_set = !isESC_set; // ESC ���� ���
        }

        // ESC ���� ǥ�� �� ���콺 Ŀ�� ���� ����
        ESC_UI.SetActive(isESC_set);
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigid.AddForce(Vector3.up * fjumpForce, ForceMode.Impulse);
            canJump = false; // ���� �Ұ����ϰ� ����
            Invoke("ResetJump", jumpCooldown); // 5�� �Ŀ� ���� �����ϰ� ����
        }
    }

    // ���� ��� �ð� �� �ٽ� ���� �����ϰ� ����
    void ResetJump()
    {
        canJump = true;
    }

    // �޸��� �� ��ٿ� Ÿ�̸� ������Ʈ
    void UpdateRunTimers()
    {
        if (!canRun) // ��ٿ� ���� ��
        {
            cooldownTimer += Time.deltaTime;

            // ��ٿ��� ������ �ٽ� �޸��� �����ϰ� ����
            if (cooldownTimer >= runCooldown)
            {
                canRun = true;
                runTimer = 0f; // �޸��� Ÿ�̸� �ʱ�ȭ
            }
        }
    }
}
