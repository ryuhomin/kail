using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject sword;
    public float swingCooldown = 1f; // 검 휘두르기 쿨다운 시간 (초)
    private float lastSwingTime = 0f; // 마지막 검 휘두른 시간

    // 몬스터 파괴 범위
    public float destroyRange = 50f; // 플레이어 주변 50미터

    // 우클릭 쿨타임
    public float rightClickCooldown = 10f; // 2분 (초)
    private float lastRightClickTime = 0f; // 마지막 우클릭 시간
    private bool isFirstRightClick = true; // 첫 번째 우클릭 체크

    void Start()
    {
        // 기본적으로 Sword를 활성화
        sword.SetActive(true);
    }

    void Update()
    {
        // 현재 시간
        float currentTime = Time.time;

        // 무기가 활성화된 상태에서 마우스 왼쪽 버튼을 누르고, 쿨다운이 지나면 공격
        if (Input.GetMouseButtonDown(0) && currentTime - lastSwingTime >= swingCooldown)
        {
            StartCoroutine(SwingSword());
            lastSwingTime = currentTime; // 마지막 휘두른 시간 업데이트
        }

        // 우클릭 시 몬스터에 데미지 주기
        if (Input.GetMouseButtonDown(1)) // 1은 마우스 오른쪽 버튼
        {
            if (isFirstRightClick) // 첫 번째 우클릭이면 쿨타임 적용 안함
            {
                DamageMonstersInRange(100f); // 몬스터에 100의 데미지 주기
                isFirstRightClick = false; // 첫 번째 우클릭 완료
            }
            else // 이후의 우클릭에 대해 쿨타임 적용
            {
                // 쿨타임 체크
                if (currentTime - lastRightClickTime >= rightClickCooldown)
                {
                    DamageMonstersInRange(100f); // 몬스터에 100의 데미지 주기
                    lastRightClickTime = currentTime; // 마지막 우클릭 시간 업데이트
                }
                else
                {
                    // 남은 쿨타임 출력
                    float remainingCooldown = rightClickCooldown - (currentTime - lastRightClickTime);
                    Debug.Log("우클릭 쿨타임 남음: " + remainingCooldown + "초");
                }
            }
        }
    }

    IEnumerator SwingSword()
    {
        // 검 휘두르기 애니메이션 또는 효과 추가 가능
        // 필요에 따라 검의 시각적 효과를 추가할 수 있음

        // 잠시 대기 (휘두르기 애니메이션 지속 시간)
        yield return new WaitForSeconds(0.1f);

        // 애니메이션 끝나면, 검의 활성화 상태는 유지
        // sword.SetActive(true); // 여기서는 필요하지 않음
    }

    void DamageMonstersInRange(float damage)
    {
        // 플레이어 주변의 몬스터를 찾기
        Collider[] colliders = Physics.OverlapSphere(transform.position, destroyRange);

        foreach (Collider collider in colliders)
        {
            // "Monster" 태그가 붙은 오브젝트인지 확인
            if (collider.CompareTag("Monster"))
            {
                // 몬스터의 MonsterHealth 컴포넌트를 가져와서 데미지 적용
                MonsterHealth monsterHealth = collider.GetComponent<MonsterHealth>();
                if (monsterHealth != null)
                {
                    monsterHealth.TakeDamage(damage); // 데미지 주기
                    Debug.Log(collider.gameObject.name + "에게 " + damage + "의 데미지를 주었습니다."); // 데미지 로그
                }
            }
        }
    }
}
