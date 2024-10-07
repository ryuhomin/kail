using System.Collections;
using UnityEngine;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 추가

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // 현재 체력
    public float maxHealth = 100f; // 최대 체력
    public Slider healthSlider; // 플레이어 HP 슬라이더
    public Button reviveButton; // 부활 버튼

    private bool isDead = false; // 플레이어가 죽었는지 여부 확인

    // 화면 효과 UI 이미지
    public Image damageEffectImage; // Damage Effect Image
    public float damageEffectDuration = 0.2f; // 효과 지속 시간

    void Start()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health; // 초기값 설정
        }
        else
        {
            Debug.LogError("HealthSlider가 할당되지 않았습니다!");
        }

        // 효과 이미지 초기화
        if (damageEffectImage != null)
        {
            damageEffectImage.color = new Color(1, 0, 0, 0); // 투명하게 설정
        }

        // 부활 버튼 비활성화
        if (reviveButton != null)
        {
            reviveButton.gameObject.SetActive(false); // 부활 버튼 초기에는 비활성화
            reviveButton.onClick.AddListener(Revive); // 부활 버튼 클릭 시 Revive 메서드 호출
        }
    }

    // 플레이어가 데미지를 받을 때 호출되는 메소드
    public void TakeDamage(float amount)
    {
        if (!isDead)
        {
            health -= amount;
            Debug.Log("플레이어 체력 감소: " + health); // 체력 감소 확인용 로그

            if (healthSlider != null)
            {
                healthSlider.value = health; // 슬라이더의 현재값을 체력으로 설정
            }

            // 화면 효과 호출
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

    // 플레이어의 죽음 처리
    void Die()
    {
        isDead = true;
        Debug.Log("플레이어 사망 처리"); // 플레이어 사망 로그
        Time.timeScale = 0f; // 게임 시간 정지

        // 마우스 포인터 활성화
        Cursor.lockState = CursorLockMode.None; // 마우스 커서를 화면에 고정하지 않음
        Cursor.visible = true; // 마우스 커서를 보이게 함

        // 부활 버튼 활성화
        if (reviveButton != null)
        {
            reviveButton.gameObject.SetActive(true); // 버튼 활성화
        }
    }

    // 화면 빨갛게 효과를 주는 코루틴
    IEnumerator ShowDamageEffect()
    {
        damageEffectImage.color = new Color(1, 0, 0, 0.5f); // 반투명 빨간색
        yield return new WaitForSeconds(damageEffectDuration); // 효과 지속 시간 대기
        damageEffectImage.color = new Color(1, 0, 0, 0); // 다시 투명하게
    }

    // 부활 메소드
    public void Revive()
    {
        health = maxHealth; // 체력 회복
        healthSlider.value = health; // 슬라이더 업데이트

        // 마우스 포인터 비활성화
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 고정
        Cursor.visible = false; // 마우스 커서를 보이지 않게 함

        // 게임 재개
        Time.timeScale = 1f; // 게임 시간 재개

        // 부활 버튼 비활성화
        reviveButton.gameObject.SetActive(false);
    }
}
