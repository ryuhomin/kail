using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 몬스터 프리팹
    public float spawnInterval = 5f; // 소환 간격 (초)

    void Start()
    {
        // 소환 코루틴 시작
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true) // 무한 루프
        {
            // 몬스터 소환
            SpawnMonster();

            // 소환 간격만큼 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefab != null)
        {
            // 현재 위치에서 Y값을 7 낮춰서 몬스터 생성
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 7f, transform.position.z);
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("몬스터 소환됨: " + monsterPrefab.name);
        }
        else
        {
            Debug.LogError("MonsterPrefab이 할당되지 않았습니다!");
        }
    }
}
