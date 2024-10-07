using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterFollow : MonoBehaviour
{
    // ������ �̵� �ӵ�
    public float moveSpeed = 4f;

    // ���� �Ÿ�
    public float attackDistance = 3f;

    // ������ �÷��̾�
    private Transform playerTransform;

    // �ִϸ�����
    private Animator animator;

    // �÷��̾ �浹���� �� ���� ������
    public float damage = 20f;

    // ���� ��� ����
    private bool isDead = false;

    // HP �� ���� ����
    private MonsterHealth monsterHealth; // ������ ü���� �����ϴ� ������Ʈ

    void Start()
    {
        // �±װ� "Player"�� ������Ʈ ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // �ִϸ����� ������Ʈ ��������
        animator = GetComponent<Animator>();

        // ���� ü�� ������Ʈ ��������
        monsterHealth = GetComponent<MonsterHealth>();
        if (monsterHealth == null)
        {
            Debug.LogError("MonsterHealth component is missing on this GameObject.");
        }
    }

    void Update()
    {
        if (playerTransform != null && !isDead)
        {
            // �ִϸ��̼� ������Ʈ
            UpdateAnimation();

            // ���Ͱ� �÷��̾ ���� �̵�
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            // ���Ϳ� �÷��̾��� ��ġ ���� ���
            Vector3 direction = playerTransform.position - transform.position;

            // ���� ���͸� ����ȭ�Ͽ� ���� �ӵ��� �̵�
            direction.Normalize();

            // ȸ���� ������ ���
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // �ε巴�� ȸ���ϵ��� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            // ���� �̵�
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            // �׻� �ȱ� �ִϸ��̼� ���
            animator.SetBool("isWalking", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� �浹 �� ó��
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            // �÷��̾�� ������ �ֱ�
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // ������ ü���� ���ҽ�Ű�� �׾��� �� ó��
    public void TakeDamage(float amount)
    {
        // MonsterHealth�� TakeDamage ȣ��
        if (monsterHealth != null)
        {
            monsterHealth.TakeDamage(amount); // ������ ü�� ����
        }
    }
}
