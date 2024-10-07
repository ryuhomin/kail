using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float damage = 50f;
    private Collider swordCollider;
    private HashSet<Collider> hitMonsters = new HashSet<Collider>(); // 충돌한 몬스터를 추적

    void Start()
    {
        swordCollider = GetComponent<Collider>();
        if (swordCollider != null)
        {
            swordCollider.enabled = true; // 콜라이더 항상 활성화
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && !hitMonsters.Contains(other))
        {
            hitMonsters.Add(other);

            // 몬스터의 Health 스크립트를 찾아서 데미지 주기
            MonsterHealth monsterHealth = other.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.TakeDamage(damage);
                Debug.Log("50의 데미지가 몬스터에게 들어갔습니다.");
            }
        }
    }

    // 충돌이 끝났을 때 호출됨
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            hitMonsters.Remove(other);
        }
    }
}
