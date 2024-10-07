using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // ���� ������
    public float spawnInterval = 5f; // ��ȯ ���� (��)

    void Start()
    {
        // ��ȯ �ڷ�ƾ ����
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true) // ���� ����
        {
            // ���� ��ȯ
            SpawnMonster();

            // ��ȯ ���ݸ�ŭ ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefab != null)
        {
            // ���� ��ġ���� Y���� 7 ���缭 ���� ����
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 7f, transform.position.z);
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("���� ��ȯ��: " + monsterPrefab.name);
        }
        else
        {
            Debug.LogError("MonsterPrefab�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }
}
