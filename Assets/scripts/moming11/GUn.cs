using UnityEngine;

public class GUn : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballObject; // �Ѿ����ڵ������
    public Transform launchPoint; // ������ʼ��
    public float launchForce = 10f; // ��������
    public float cooldownTime = 2f; // ��ȴʱ��

    [Header("Sprite Settings")]
    public Sprite inactiveSprite;  // δ����ʱ�ľ���ͼ
    public Sprite activeSprite;    // �����ľ���ͼ

    private float cooldownTimer = 0f;
    private bool canLaunch = true;
    private Rigidbody2D ballRigidbody;
    private Vector2 originalBallPosition;
    private bool isActive = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // ��ȡSpriteRenderer���
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // ���ó�ʼ����ͼ(δ����״̬)
        if (inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }

        // ��ʼ��������
        if (ballObject != null)
        {
            ballRigidbody = ballObject.GetComponent<Rigidbody2D>();
            originalBallPosition = ballObject.transform.position;
            ballObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        // ����󼤻�
        isActive = true;
        Debug.Log("���屻����ˣ�");

        // �л�����ͼ
        if (activeSprite != null)
        {
            spriteRenderer.sprite = activeSprite;
        }
    }

    private void Update()
    {
        if (isActive)
        {
            if (!canLaunch)
            {
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= cooldownTime)
                {
                    canLaunch = true;
                    cooldownTimer = 0f;
                }
            }

            if (canLaunch)
            {
                ResetAndLaunchBall();
                canLaunch = false;
            }
        }
    }

    void ResetAndLaunchBall()
    {
        if (ballObject == null) return;

        // �������λ�ú��ٶ�
        ballObject.transform.position = launchPoint != null ? launchPoint.position : transform.position;

        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector2.zero;
            ballRigidbody.angularVelocity = 0f;
            ballRigidbody.AddForce(Vector2.left * launchForce, ForceMode2D.Impulse);
        }

        ballObject.SetActive(true);
    }

    public void ResetBall()
    {
        if (ballObject != null)
        {
            ballObject.SetActive(false);
            ballObject.transform.position = originalBallPosition;

            // ����Ϊδ����״̬�;���ͼ
            isActive = false;
            if (inactiveSprite != null)
            {
                spriteRenderer.sprite = inactiveSprite;
            }
        }
    }
}