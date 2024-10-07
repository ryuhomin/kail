using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterFollow : MonoBehaviour
{
    // 몬스터의 이동 속도
    public float moveSpeed = 4f;

    // 공격 거리
    public float attackDistance = 3f;

    // 추적할 플레이어
    private Transform playerTransform;

    // 애니메이터
    private Animator animator;

    // 플레이어가 충돌했을 때 입힐 데미지
    public float damage = 20f;

    // 몬스터 사망 여부
    private bool isDead = false;

    // HP 바 관련 변수
    private MonsterHealth monsterHealth; // 몬스터의 체력을 관리하는 컴포넌트

    void Start()
    {
        // 태그가 "Player"인 오브젝트 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // 애니메이터 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 몬스터 체력 컴포넌트 가져오기
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
            // 애니메이션 업데이트
            UpdateAnimation();

            // 몬스터가 플레이어를 향해 이동
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            // 몬스터와 플레이어의 위치 차이 계산
            Vector3 direction = playerTransform.position - transform.position;

            // 방향 벡터를 정규화하여 일정 속도로 이동
            direction.Normalize();

            // 회전할 방향을 계산
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 부드럽게 회전하도록 설정
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            // 몬스터 이동
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            // 항상 걷기 애니메이션 재생
            animator.SetBool("isWalking", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌 시 처리
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            // 플레이어에게 데미지 주기
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // 몬스터의 체력을 감소시키고 죽었을 때 처리
    public void TakeDamage(float amount)
    {
        // MonsterHealth의 TakeDamage 호출
        if (monsterHealth != null)
        {
            monsterHealth.TakeDamage(amount); // 몬스터의 체력 감소
        }
    }
}
