using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // ���콺 ����
    protected float fMouse_Sens = 5.0f;

    // ���� ����
    protected float fjumpForce = 5.0f;

    // �÷��̾� �ӵ�
    protected float fPlayer_Speed = 5.0f;
    protected float fPlayer_RunSpeed = 12.0f;

    // �̵�
    protected float fPlayerMov_X = 0;
    protected float fPlayerMov_Y = 0;

    // ���콺 ȸ��
    protected float fPlayerRot_X = 0;
    protected float fPlayerRot_Y = 0;

    //�÷��̾� ��ǥ
    protected Vector3 vecPlayerPos = Vector3.zero;
    protected Vector3 vecPlayerRotPos_X = Vector3.zero;
    protected Vector3 vecPlayerRotPos_Y = Vector3.zero;

    // �÷��̾��� Transform (ī�޶� �پ��ִ� ���)
    public Transform playerTransform;
    public Transform cameraTransform;

    void Update()
    {
        // ���콺 �Է� ó��
        HandleMouseMovement();
    }

    void HandleMouseMovement()
    {
        // ���콺 �Է��� �޾� ȸ�� �� ���
        fPlayerRot_X -= Input.GetAxis("Mouse Y") * fMouse_Sens;  // ���� ȸ��
        fPlayerRot_Y += Input.GetAxis("Mouse X") * fMouse_Sens;  // �¿� ȸ��

        // ���� ȸ�� ������ ���� (ī�޶� �ʹ� ���Ʒ��� ȸ������ �ʵ���)
        fPlayerRot_X = Mathf.Clamp(fPlayerRot_X, -90f, 90f);

        // �÷��̾��� Y�� ȸ�� (�¿� ȸ��)
        playerTransform.localRotation = Quaternion.Euler(0, fPlayerRot_Y, 0);

        // ī�޶��� X�� ȸ�� (���� ȸ��)
        cameraTransform.localRotation = Quaternion.Euler(fPlayerRot_X, 0, 0);
    }
}
