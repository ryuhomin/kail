using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : Game_Manager
{
    // ESC 버튼
    private bool isESC_set = false;
    public GameObject ESC_UI;

    // 컴포넌트
    Rigidbody rigid;
    Animator anime;

    // 플레이어 속도
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float currentSpeed;

    // 점프 관련 변수
    private bool canJump = true; // 점프 가능 여부
    public float jumpCooldown = 5f; // 점프 대기 시간 (5초)

    // 달리기 쿨다운 관련 변수
    public float runDuration = 5f; // 최대 달리기 시간 (5초)
    public float runCooldown = 5f; // 달리기 쿨다운 시간 (5초)
    private float runTimer = 0f; // 달리기 시간 타이머
    private float cooldownTimer = 0f; // 쿨다운 타이머
    private bool canRun = true; // 달리기 가능 여부

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();

        // 마우스 커서 숨김
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 기본 걷기 속도로 설정
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        PlayerAnimation();
        PlayerMove();
        PlayerRot();
        PlayerJump();
        ESC_Setting();

        // 달리기 타이머 및 쿨다운 업데이트
        UpdateRunTimers();
    }

    void PlayerMove()
    {
        // 이동 입력
        fPlayerMov_X = Input.GetAxis("Horizontal");
        fPlayerMov_Y = Input.GetAxis("Vertical");
        vecPlayerPos = new Vector3(fPlayerMov_X, 0, fPlayerMov_Y).normalized;

        // 달리기 입력
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;

        // 달리기 상태에 따라 속도 조절
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        // 이동
        transform.Translate(vecPlayerPos * currentSpeed * Time.deltaTime, Space.Self);

        // 달리기 중이면 타이머를 업데이트
        if (isRunning)
        {
            runTimer += Time.deltaTime;

            // 5초가 지나면 달리기 종료 및 쿨다운 시작
            if (runTimer >= runDuration)
            {
                canRun = false; // 달리기 불가
                cooldownTimer = 0f; // 쿨다운 타이머 초기화
            }
        }
    }

    void PlayerAnimation() // 플레이어 애니메이션
    {
        if (fPlayerMov_X != 0 || fPlayerMov_Y != 0) // 걷기나 달리기 입력이 있을 때
        {
            anime.SetBool("isWalk", true); // 걷기 애니메이션

            if (Input.GetKey(KeyCode.LeftShift) && canRun) // 달리기 입력이 있을 때
            {
                anime.SetBool("isRun", true); // 달리기 애니메이션
            }
            else
            {
                anime.SetBool("isRun", false); // 걷기만
            }
        }
        else
        {
            anime.SetBool("isWalk", false); // 정지 상태
            anime.SetBool("isRun", false);  // 달리기 애니메이션도 끔
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
            isESC_set = !isESC_set; // ESC 상태 토글
        }

        // ESC 설정 표시 및 마우스 커서 유무 설정
        ESC_UI.SetActive(isESC_set);
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigid.AddForce(Vector3.up * fjumpForce, ForceMode.Impulse);
            canJump = false; // 점프 불가능하게 설정
            Invoke("ResetJump", jumpCooldown); // 5초 후에 점프 가능하게 만듦
        }
    }

    // 점프 대기 시간 후 다시 점프 가능하게 설정
    void ResetJump()
    {
        canJump = true;
    }

    // 달리기 및 쿨다운 타이머 업데이트
    void UpdateRunTimers()
    {
        if (!canRun) // 쿨다운 중일 때
        {
            cooldownTimer += Time.deltaTime;

            // 쿨다운이 끝나면 다시 달리기 가능하게 설정
            if (cooldownTimer >= runCooldown)
            {
                canRun = true;
                runTimer = 0f; // 달리기 타이머 초기화
            }
        }
    }
}
