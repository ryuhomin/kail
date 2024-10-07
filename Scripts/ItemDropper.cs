using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    // ����� ������ ������
    public GameObject goldDropPrefab;
    public GameObject reinforcementStoneDropPrefab;

    // ��� Ȯ�� (100%�� ����)
    public float reinforcementStoneDropChance = 1.0f;

    // ���Ͱ� �׾��� �� ȣ��Ǵ� �޼ҵ�
    public void DropItems(Vector3 position)
    {
        Debug.Log("DropItems ȣ���"); // ��� �޼ҵ� ȣ�� Ȯ�ο� �α�

        if (goldDropPrefab != null)
        {
            Instantiate(goldDropPrefab, position, Quaternion.identity);
            Debug.Log("��� �����");
        }

        if (reinforcementStoneDropPrefab != null)
        {
            Instantiate(reinforcementStoneDropPrefab, position, Quaternion.identity);
            Debug.Log("��ȭ�� �����");
        }
    }
}
