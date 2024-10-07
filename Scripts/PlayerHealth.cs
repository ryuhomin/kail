using System.Collections;
using UnityEngine;
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� �߰�

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // ���� ü��
    public float maxHealth = 100f; // �ִ� ü��
    public Slider healthSlider; // �÷��̾� HP �����̴�
    public Button reviveButton; // ��Ȱ ��ư

    private bool isDead = false; // �÷��̾ �׾����� ���� Ȯ��

    // ȭ�� ȿ�� UI �̹���
    public Image damageEffectImage; // Damage Effect Image
    public float damageEffectDuration = 0.2f; // ȿ�� ���� �ð�

    void Start()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health; // �ʱⰪ ����
        }
        else
        {
            Debug.LogError("HealthSlider�� �Ҵ���� �ʾҽ��ϴ�!");
        }

        // ȿ�� �̹��� �ʱ�ȭ
        if (damageEffectImage != null)
        {
            damageEffectImage.color = new Color(1, 0, 0, 0); // �����ϰ� ����
        }

        // ��Ȱ ��ư ��Ȱ��ȭ
        if (reviveButton != null)
        {
            reviveButton.gameObject.SetActive(false); // ��Ȱ ��ư �ʱ⿡�� ��Ȱ��ȭ
            reviveButton.onClick.AddListener(Revive); // ��Ȱ ��ư Ŭ�� �� Revive �޼��� ȣ��
        }
    }

    // �÷��̾ �������� ���� �� ȣ��Ǵ� �޼ҵ�
    public void TakeDamage(float amount)
    {
        if (!isDead)
        {
            health -= amount;
            Debug.Log("�÷��̾� ü�� ����: " + health); // ü�� ���� Ȯ�ο� �α�

            if (healthSlider != null)
            {
                healthSlider.value = health; // �����̴��� ���簪�� ü������ ����
            }

            // ȭ�� ȿ�� ȣ��
            if (damageEffectImage != null)
            {
                StartCoroutine(ShowDamageEffect());
            }

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    // �÷��̾��� ���� ó��
    void Die()
    {
        isDead = true;
        Debug.Log("�÷��̾� ��� ó��"); // �÷��̾� ��� �α�
        Time.timeScale = 0f; // ���� �ð� ����

        // ���콺 ������ Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ���� ȭ�鿡 �������� ����
        Cursor.visible = true; // ���콺 Ŀ���� ���̰� ��

        // ��Ȱ ��ư Ȱ��ȭ
        if (reviveButton != null)
        {
            reviveButton.gameObject.SetActive(true); // ��ư Ȱ��ȭ
        }
    }

    // ȭ�� ������ ȿ���� �ִ� �ڷ�ƾ
    IEnumerator ShowDamageEffect()
    {
        damageEffectImage.color = new Color(1, 0, 0, 0.5f); // ������ ������
        yield return new WaitForSeconds(damageEffectDuration); // ȿ�� ���� �ð� ���
        damageEffectImage.color = new Color(1, 0, 0, 0); // �ٽ� �����ϰ�
    }

    // ��Ȱ �޼ҵ�
    public void Revive()
    {
        health = maxHealth; // ü�� ȸ��
        healthSlider.value = health; // �����̴� ������Ʈ

        // ���콺 ������ ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ���� ����
        Cursor.visible = false; // ���콺 Ŀ���� ������ �ʰ� ��

        // ���� �簳
        Time.timeScale = 1f; // ���� �ð� �簳

        // ��Ȱ ��ư ��Ȱ��ȭ
        reviveButton.gameObject.SetActive(false);
    }
}
