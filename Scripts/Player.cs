using UnityEngine;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 추가

public class Player : MonoBehaviour
{
    // 골드 수량을 표시할 UI Text
    public Text goldAmountText;

    // 강화석 수량을 표시할 UI Text
    public Text stoneAmountText;

    // 현재 골드 수량
    private int goldAmount = 0;

    // 현재 강화석 수량
    private int stoneAmount = 0;

    void Start()
    {
        UpdateGoldUI(); // 초기 UI 업데이트
        UpdateStoneUI(); // 초기 강화석 UI 업데이트
    }

    // 충돌 처리
    void OnTriggerEnter(Collider other)
    {
        // "Gold" 태그가 붙은 아이템과 충돌 시
        if (other.CompareTag("Gold"))
        {
            // 골드 아이템 먹기
            CollectGold(other.gameObject);
        }
        // "Stone" 태그가 붙은 아이템과 충돌 시
        else if (other.CompareTag("Stone"))
        {
            // 강화석 아이템 먹기
            CollectStone(other.gameObject);
        }
    }

    // 골드 아이템을 먹는 메소드
    void CollectGold(GameObject goldItem)
    {
        // 골드 수량을 100 증가
        goldAmount += 100;

        // UI 업데이트
        UpdateGoldUI();

        // 골드 아이템 파괴
        Destroy(goldItem);
    }

    // 강화석 아이템을 먹는 메소드
    void CollectStone(GameObject stoneItem)
    {
        // 강화석 수량을 1 증가
        stoneAmount += 1;

        // UI 업데이트
        UpdateStoneUI();

        // 강화석 아이템 파괴
        Destroy(stoneItem);
    }

    // UI에 골드 수량을 표시하는 메소드
    void UpdateGoldUI()
    {
        goldAmountText.text = "골드 : " + goldAmount; // UI에 골드 수량 표시
    }

    // UI에 강화석 수량을 표시하는 메소드
    void UpdateStoneUI()
    {
        stoneAmountText.text = "강화석: " + stoneAmount; // UI에 강화석 수량 표시
    }
}
