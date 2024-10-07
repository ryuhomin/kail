using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // 마우스 감도
    protected float fMouse_Sens = 5.0f;

    // 점프 세기
    protected float fjumpForce = 5.0f;

    // 플레이어 속도
    protected float fPlayer_Speed = 5.0f;
    protected float fPlayer_RunSpeed = 12.0f;

    // 이동
    protected float fPlayerMov_X = 0;
    protected float fPlayerMov_Y = 0;

    // 마우스 회전
    protected float fPlayerRot_X = 0;
    protected float fPlayerRot_Y = 0;

    //플레이어 좌표
    protected Vector3 vecPlayerPos = Vector3.zero;
    protected Vector3 vecPlayerRotPos_X = Vector3.zero;
    protected Vector3 vecPlayerRotPos_Y = Vector3.zero;

    // 플레이어의 Transform (카메라에 붙어있는 경우)
    public Transform playerTransform;
    public Transform cameraTransform;

    void Update()
    {
        // 마우스 입력 처리
        HandleMouseMovement();
    }

    void HandleMouseMovement()
    {
        // 마우스 입력을 받아 회전 값 계산
        fPlayerRot_X -= Input.GetAxis("Mouse Y") * fMouse_Sens;  // 상하 회전
        fPlayerRot_Y += Input.GetAxis("Mouse X") * fMouse_Sens;  // 좌우 회전

        // 상하 회전 각도를 제한 (카메라가 너무 위아래로 회전하지 않도록)
        fPlayerRot_X = Mathf.Clamp(fPlayerRot_X, -90f, 90f);

        // 플레이어의 Y축 회전 (좌우 회전)
        playerTransform.localRotation = Quaternion.Euler(0, fPlayerRot_Y, 0);

        // 카메라의 X축 회전 (상하 회전)
        cameraTransform.localRotation = Quaternion.Euler(fPlayerRot_X, 0, 0);
    }
}
