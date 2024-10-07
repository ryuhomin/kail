using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject sword;
    public float swingCooldown = 1f; // �� �ֵθ��� ��ٿ� �ð� (��)
    private float lastSwingTime = 0f; // ������ �� �ֵθ� �ð�

    // ���� �ı� ����
    public float destroyRange = 50f; // �÷��̾� �ֺ� 50����

    // ��Ŭ�� ��Ÿ��
    public float rightClickCooldown = 10f; // 2�� (��)
    private float lastRightClickTime = 0f; // ������ ��Ŭ�� �ð�
    private bool isFirstRightClick = true; // ù ��° ��Ŭ�� üũ

    void Start()
    {
        // �⺻������ Sword�� Ȱ��ȭ
        sword.SetActive(true);
    }

    void Update()
    {
        // ���� �ð�
        float currentTime = Time.time;

        // ���Ⱑ Ȱ��ȭ�� ���¿��� ���콺 ���� ��ư�� ������, ��ٿ��� ������ ����
        if (Input.GetMouseButtonDown(0) && currentTime - lastSwingTime >= swingCooldown)
        {
            StartCoroutine(SwingSword());
            lastSwingTime = currentTime; // ������ �ֵθ� �ð� ������Ʈ
        }

        // ��Ŭ�� �� ���Ϳ� ������ �ֱ�
        if (Input.GetMouseButtonDown(1)) // 1�� ���콺 ������ ��ư
        {
            if (isFirstRightClick) // ù ��° ��Ŭ���̸� ��Ÿ�� ���� ����
            {
                DamageMonstersInRange(100f); // ���Ϳ� 100�� ������ �ֱ�
                isFirstRightClick = false; // ù ��° ��Ŭ�� �Ϸ�
            }
            else // ������ ��Ŭ���� ���� ��Ÿ�� ����
            {
                // ��Ÿ�� üũ
                if (currentTime - lastRightClickTime >= rightClickCooldown)
                {
                    DamageMonstersInRange(100f); // ���Ϳ� 100�� ������ �ֱ�
                    lastRightClickTime = currentTime; // ������ ��Ŭ�� �ð� ������Ʈ
                }
                else
                {
                    // ���� ��Ÿ�� ���
                    float remainingCooldown = rightClickCooldown - (currentTime - lastRightClickTime);
                    Debug.Log("��Ŭ�� ��Ÿ�� ����: " + remainingCooldown + "��");
                }
            }
        }
    }

    IEnumerator SwingSword()
    {
        // �� �ֵθ��� �ִϸ��̼� �Ǵ� ȿ�� �߰� ����
        // �ʿ信 ���� ���� �ð��� ȿ���� �߰��� �� ����

        // ��� ��� (�ֵθ��� �ִϸ��̼� ���� �ð�)
        yield return new WaitForSeconds(0.1f);

        // �ִϸ��̼� ������, ���� Ȱ��ȭ ���´� ����
        // sword.SetActive(true); // ���⼭�� �ʿ����� ����
    }

    void DamageMonstersInRange(float damage)
    {
        // �÷��̾� �ֺ��� ���͸� ã��
        Collider[] colliders = Physics.OverlapSphere(transform.position, destroyRange);

        foreach (Collider collider in colliders)
        {
            // "Monster" �±װ� ���� ������Ʈ���� Ȯ��
            if (collider.CompareTag("Monster"))
            {
                // ������ MonsterHealth ������Ʈ�� �����ͼ� ������ ����
                MonsterHealth monsterHealth = collider.GetComponent<MonsterHealth>();
                if (monsterHealth != null)
                {
                    monsterHealth.TakeDamage(damage); // ������ �ֱ�
                    Debug.Log(collider.gameObject.name + "���� " + damage + "�� �������� �־����ϴ�."); // ������ �α�
                }
            }
        }
    }
}
