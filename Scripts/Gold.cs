using UnityEngine;

public class Gold : MonoBehaviour
{
    public float moveDistance = 0.5f; // �̵� �Ÿ�
    public float moveSpeed = 1f; // �̵� �ӵ�
    public float moveDuration = 2f; // �̵� �ֱ�

    private float timer = 0f; // Ÿ�̸� ����
    private bool movingUp = true; // ���� �����̴� �������� ����

    void Start()
    {
        // �������� ���� �ð��� ������ �ڵ����� �ı��ǵ��� ����
        Destroy(gameObject, 30f);
    }

    void Update()
    {
        // ���� �ֱ⸶�� Gold �������� ���Ʒ��� �����̵��� ����
        timer += Time.deltaTime;
        if (timer >= moveDuration)
        {
            timer = 0f;
            movingUp = !movingUp; // �̵� ������ ������ŵ�ϴ�.
        }

        // ���Ʒ��� �̵��ϴ� ����� �Ÿ��� ����Ͽ� �̵��մϴ�.
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
