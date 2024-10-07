using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public float health = 100f; // ������ ü��
    public Slider healthSlider; // ������ HP �����̴�
    public GameObject goldPrefab; // ��� ������
    public GameObject stonePrefab; // ��ȭ�� ������
    public int goldAmount = 1; // ���Ͱ� �׾��� �� ����� ��� ��
    public int stoneAmount = 1; // ���Ͱ� �׾��� �� ����� ��ȭ�� ��
    public Animator animator; // ���� �ִϸ�����

    private bool isDead = false; // ���Ͱ� �̹� �׾����� ���� Ȯ��

    void Start()
    {
        // HP �����̴� �ʱ� ����
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
        else
        {
            Debug.LogError("HealthSlider�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }

    // ���Ͱ� �������� ���� �� ȣ��Ǵ� �޼ҵ�
    public void TakeDamage(float damage)
    {
        if (isDead) return; // �̹� �׾��ٸ� ������ ó������ ����

        health -= damage; // ü�� ����
        Debug.Log("���Ͱ� �������� �޾ҽ��ϴ�. ���� ü�� : " + health);

        // HP �����̴� ������Ʈ
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        // ü���� 0 ���ϰ� �Ǹ� ���� ó��
        if (health <= 0)
        {
            Die();
        }
    }

    // ���Ͱ� �׾��� �� ȣ��Ǵ� �޼ҵ�
    void Die()
    {
        if (isDead) return; // �̹� ���� ���¶�� ó������ ����

        isDead = true; // ���� ���� ����
        animator.SetBool("isDead", true); // isDead�� true�� �����Ͽ� �ִϸ����Ϳ� ����
        Debug.Log("���� óġ! ���� �ִϸ��̼� ����");

        // �ݶ��̴� ��Ȱ��ȭ
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ
        }

        // Rigidbody ��Ȱ��ȭ
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Rigidbody�� Ű�׸�ƽ���� �����Ͽ� �߷��� ��Ȱ��ȭ
        }

        // ��� ���
        DropGold(); // ��� ��� ȣ��
        // ��ȭ�� ���
        DropStone(); // ��ȭ�� ��� ȣ��

        // ���� �ִϸ��̼� ����
        animator.Play("Death");
        Debug.Log("���� �ִϸ��̼� �����");

        // �ִϸ��̼��� �Ϸ�� �� ������Ʈ�� �ı��ϱ� ���� ���� �ð� ���
        Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
    }

    // ��� �������� ����ϴ� �޼ҵ�
    public void DropGold()
    {
        if (goldPrefab != null)
        {
            Vector3 dropPosition = transform.position;
            for (int i = 0; i < goldAmount; i++)
            {
                Instantiate(goldPrefab, dropPosition, Quaternion.identity);
            }

            Debug.Log(goldAmount + "���� ��� �������� ��ӵǾ����ϴ�.");
        }
        else
        {
            Debug.LogError("GoldPrefab�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }

    // ��ȭ�� �������� ����ϴ� �޼ҵ�
    public void DropStone()
    {
        if (stonePrefab != null)
        {
            Vector3 dropPosition = transform.position + new Vector3(1f, 0f, 0f); // X������ 5ĭ ���� ���
            for (int i = 0; i < stoneAmount; i++)
            {
                Instantiate(stonePrefab, dropPosition, Quaternion.identity);
            }

            Debug.Log(stoneAmount + "���� ��ȭ�� �������� ��ӵǾ����ϴ�.");
        }
        else
        {
            Debug.LogError("StonePrefab�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }
}
