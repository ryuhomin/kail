using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    // 드랍될 아이템 프리팹
    public GameObject goldDropPrefab;
    public GameObject reinforcementStoneDropPrefab;

    // 드랍 확률 (100%로 설정)
    public float reinforcementStoneDropChance = 1.0f;

    // 몬스터가 죽었을 때 호출되는 메소드
    public void DropItems(Vector3 position)
    {
        Debug.Log("DropItems 호출됨"); // 드랍 메소드 호출 확인용 로그

        if (goldDropPrefab != null)
        {
            Instantiate(goldDropPrefab, position, Quaternion.identity);
            Debug.Log("골드 드랍됨");
        }

        if (reinforcementStoneDropPrefab != null)
        {
            Instantiate(reinforcementStoneDropPrefab, position, Quaternion.identity);
            Debug.Log("강화석 드랍됨");
        }
    }
}
