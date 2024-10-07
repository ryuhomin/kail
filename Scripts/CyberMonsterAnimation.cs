using UnityEngine;

public class CyberMonsterAnimation : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트 참조
    private bool isDead = false; // 몬스터의 죽음 상태를 추적하는 변수

 

    // 몬스터가 데미지를 받을 때 호출되는 메소드 (예시로 Death 트리거하기 위해 추가)
    public void TakeDamage(float damage)
    {
        if (isDead) return; // 이미 죽었으면 처리 중단

        // 체력이 0 이하가 될 때 Die() 호출
        Die();
    }

    // 몬스터가 죽을 때 호출되는 메소드
    void Die()
    {
        isDead = true; // 죽음 상태 설정
        //animator.SetBool("isRunning", false); // "Run" 상태 종료
        animator.SetTrigger("Die"); // "Death" 애니메이션 트리거
        Debug.Log("Cyber Monsters 2가 죽었습니다.");

        // 필요에 따라 일정 시간 후 오브젝트를 파괴하거나 다른 처리를 할 수 있습니다.
        Destroy(gameObject, 2f); // 2초 후 오브젝트 삭제
    }
}
