using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float damage = 50f;
    private Collider swordCollider;
    private HashSet<Collider> hitMonsters = new HashSet<Collider>(); // �浹�� ���͸� ����

    void Start()
    {
        swordCollider = GetComponent<Collider>();
        if (swordCollider != null)
        {
            swordCollider.enabled = true; // �ݶ��̴� �׻� Ȱ��ȭ
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && !hitMonsters.Contains(other))
        {
            hitMonsters.Add(other);

            // ������ Health ��ũ��Ʈ�� ã�Ƽ� ������ �ֱ�
            MonsterHealth monsterHealth = other.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.TakeDamage(damage);
                Debug.Log("50�� �������� ���Ϳ��� �����ϴ�.");
            }
        }
    }

    // �浹�� ������ �� ȣ���
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            hitMonsters.Remove(other);
        }
    }
}
