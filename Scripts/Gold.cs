using UnityEngine;

public class Gold : MonoBehaviour
{
    public float moveDistance = 0.5f; // 이동 거리
    public float moveSpeed = 1f; // 이동 속도
    public float moveDuration = 2f; // 이동 주기

    private float timer = 0f; // 타이머 변수
    private bool movingUp = true; // 위로 움직이는 상태인지 여부

    void Start()
    {
        // 아이템이 일정 시간이 지나면 자동으로 파괴되도록 설정
        Destroy(gameObject, 30f);
    }

    void Update()
    {
        // 일정 주기마다 Gold 아이템을 위아래로 움직이도록 설정
        timer += Time.deltaTime;
        if (timer >= moveDuration)
        {
            timer = 0f;
            movingUp = !movingUp; // 이동 방향을 반전시킵니다.
        }

        // 위아래로 이동하는 방향과 거리를 계산하여 이동합니다.
        float moveAmount = moveDistance * Time.deltaTime * moveSpeed;
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveAmount);
        }
        else
        {
            transform.Translate(Vector3.down * moveAmount);
        }
    }
}
