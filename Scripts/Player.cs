using UnityEngine;
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� �߰�

public class Player : MonoBehaviour
{
    // ��� ������ ǥ���� UI Text
    public Text goldAmountText;

    // ��ȭ�� ������ ǥ���� UI Text
    public Text stoneAmountText;

    // ���� ��� ����
    private int goldAmount = 0;

    // ���� ��ȭ�� ����
    private int stoneAmount = 0;

    void Start()
    {
        UpdateGoldUI(); // �ʱ� UI ������Ʈ
        UpdateStoneUI(); // �ʱ� ��ȭ�� UI ������Ʈ
    }

    // �浹 ó��
    void OnTriggerEnter(Collider other)
    {
        // "Gold" �±װ� ���� �����۰� �浹 ��
        if (other.CompareTag("Gold"))
        {
            // ��� ������ �Ա�
            CollectGold(other.gameObject);
        }
        // "Stone" �±װ� ���� �����۰� �浹 ��
        else if (other.CompareTag("Stone"))
        {
            // ��ȭ�� ������ �Ա�
            CollectStone(other.gameObject);
        }
    }

    // ��� �������� �Դ� �޼ҵ�
    void CollectGold(GameObject goldItem)
    {
        // ��� ������ 100 ����
        goldAmount += 100;

        // UI ������Ʈ
        UpdateGoldUI();

        // ��� ������ �ı�
        Destroy(goldItem);
    }

    // ��ȭ�� �������� �Դ� �޼ҵ�
    void CollectStone(GameObject stoneItem)
    {
        // ��ȭ�� ������ 1 ����
        stoneAmount += 1;

        // UI ������Ʈ
        UpdateStoneUI();

        // ��ȭ�� ������ �ı�
        Destroy(stoneItem);
    }

    // UI�� ��� ������ ǥ���ϴ� �޼ҵ�
    void UpdateGoldUI()
    {
        goldAmountText.text = "��� : " + goldAmount; // UI�� ��� ���� ǥ��
    }

    // UI�� ��ȭ�� ������ ǥ���ϴ� �޼ҵ�
    void UpdateStoneUI()
    {
        stoneAmountText.text = "��ȭ��: " + stoneAmount; // UI�� ��ȭ�� ���� ǥ��
    }
}
