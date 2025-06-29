using UnityEngine;

public class GUn : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballObject; // 已经存在的球对象
    public Transform launchPoint; // 发射起始点
    public float launchForce = 10f; // 发射力度
    public float cooldownTime = 2f; // 冷却时间

    [Header("Sprite Settings")]
    public Sprite inactiveSprite;  // 未激活时的精灵图
    public Sprite activeSprite;    // 激活后的精灵图

    private float cooldownTimer = 0f;
    private bool canLaunch = true;
    private Rigidbody2D ballRigidbody;
    private Vector2 originalBallPosition;
    private bool isActive = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // 设置初始精灵图(未激活状态)
        if (inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }

        // 初始化球设置
        if (ballObject != null)
        {
            ballRigidbody = ballObject.GetComponent<Rigidbody2D>();
            originalBallPosition = ballObject.transform.position;
            ballObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        // 点击后激活
        isActive = true;
        Debug.Log("物体被点击了！");

        // 切换精灵图
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

        // 重置球的位置和速度
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

            // 重置为未激活状态和精灵图
            isActive = false;
            if (inactiveSprite != null)
            {
                spriteRenderer.sprite = inactiveSprite;
            }
        }
    }
}