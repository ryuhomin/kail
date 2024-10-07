using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public float health = 100f; // 몬스터의 체력
    public Slider healthSlider; // 몬스터의 HP 슬라이더
    public GameObject goldPrefab; // 골드 프리팹
    public GameObject stonePrefab; // 강화석 프리팹
    public int goldAmount = 1; // 몬스터가 죽었을 때 드롭할 골드 수
    public int stoneAmount = 1; // 몬스터가 죽었을 때 드롭할 강화석 수
    public Animator animator; // 몬스터 애니메이터

    private bool isDead = false; // 몬스터가 이미 죽었는지 여부 확인

    void Start()
    {
        // HP 슬라이더 초기 설정
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
        else
        {
            Debug.LogError("HealthSlider가 할당되지 않았습니다!");
        }
    }

    // 몬스터가 데미지를 받을 때 호출되는 메소드
    public void TakeDamage(float damage)
    {
        if (isDead) return; // 이미 죽었다면 데미지 처리하지 않음

        health -= damage; // 체력 감소
        Debug.Log("몬스터가 데미지를 받았습니다. 남은 체력 : " + health);

        // HP 슬라이더 업데이트
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        // 체력이 0 이하가 되면 죽음 처리
        if (health <= 0)
        {
            Die();
        }
    }

    // 몬스터가 죽었을 때 호출되는 메소드
    void Die()
    {
        if (isDead) return; // 이미 죽은 상태라면 처리하지 않음

        isDead = true; // 죽음 상태 설정
        animator.SetBool("isDead", true); // isDead를 true로 설정하여 애니메이터에 전달
        Debug.Log("몬스터 처치! 죽음 애니메이션 시작");

        // 콜라이더 비활성화
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // 콜라이더 비활성화
        }

        // Rigidbody 비활성화
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Rigidbody를 키네마틱으로 설정하여 중력을 비활성화
        }

        // 골드 드롭
        DropGold(); // 골드 드롭 호출
        // 강화석 드롭
        DropStone(); // 강화석 드롭 호출

        // 죽음 애니메이션 실행
        animator.Play("Death");
        Debug.Log("죽음 애니메이션 재생됨");

        // 애니메이션이 완료된 후 오브젝트를 파괴하기 위해 일정 시간 대기
        Destroy(gameObject, 2f); // 2초 후 오브젝트 삭제
    }

    // 골드 아이템을 드롭하는 메소드
    public void DropGold()
    {
        if (goldPrefab != null)
        {
            Vector3 dropPosition = transform.position;
            for (int i = 0; i < goldAmount; i++)
            {
                Instantiate(goldPrefab, dropPosition, Quaternion.identity);
            }

            Debug.Log(goldAmount + "개의 골드 아이템이 드롭되었습니다.");
        }
        else
        {
            Debug.LogError("GoldPrefab이 할당되지 않았습니다!");
        }
    }

    // 강화석 아이템을 드롭하는 메소드
    public void DropStone()
    {
        if (stonePrefab != null)
        {
            Vector3 dropPosition = transform.position + new Vector3(1f, 0f, 0f); // X축으로 5칸 옆에 드롭
            for (int i = 0; i < stoneAmount; i++)
            {
                Instantiate(stonePrefab, dropPosition, Quaternion.identity);
            }

            Debug.Log(stoneAmount + "개의 강화석 아이템이 드롭되었습니다.");
        }
        else
        {
            Debug.LogError("StonePrefab이 할당되지 않았습니다!");
        }
    }
}
