using UnityEngine;

public class CyberMonsterAnimation : MonoBehaviour
{
    public Animator animator; // Animator ������Ʈ ����
    private bool isDead = false; // ������ ���� ���¸� �����ϴ� ����

 

    // ���Ͱ� �������� ���� �� ȣ��Ǵ� �޼ҵ� (���÷� Death Ʈ�����ϱ� ���� �߰�)
    public void TakeDamage(float damage)
    {
        if (isDead) return; // �̹� �׾����� ó�� �ߴ�

        // ü���� 0 ���ϰ� �� �� Die() ȣ��
        Die();
    }

    // ���Ͱ� ���� �� ȣ��Ǵ� �޼ҵ�
    void Die()
    {
        isDead = true; // ���� ���� ����
        //animator.SetBool("isRunning", false); // "Run" ���� ����
        animator.SetTrigger("Die"); // "Death" �ִϸ��̼� Ʈ����
        Debug.Log("Cyber Monsters 2�� �׾����ϴ�.");

        // �ʿ信 ���� ���� �ð� �� ������Ʈ�� �ı��ϰų� �ٸ� ó���� �� �� �ֽ��ϴ�.
        Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
    }
}
