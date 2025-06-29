using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float saleChangeNum = 3.0f;
    [Header("Movement Settings")]
    public float baseSpeed = 5f;          // �����ƶ��ٶ�
    public float rotationSpeed = 360f;    // �����ת�ٶȣ���/�룩
    private Vector2 currentDirection;
    private float dirChangeTimer = 0f;
    private float dirChangeIntervalMin = 0.1f;
    private float dirChangeIntervalMax = 0.25f;
    [SerializeField] private Controller playerController;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;     // ���ײ����ҵĻ�����

    [Header("Sprite Settings")]
    public Sprite inactiveSprite;         // δ����ʱ�ľ���ͼ
    public Sprite activeSprite;           // �����ľ���ͼ

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isActive = false;        // �Ƿ�������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���ó�ʼ����ͼ
        if (inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    void Update()
    {
        // 1. �������
        if (!isActive && Input.GetMouseButtonDown(0))
        {
            Vector2 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapPoint(m) == GetComponent<Collider2D>())
            {
                ActivateMushroom();
            }
        }

        // 2. ��󣺳������������ + ��ת��
        if (isActive)
        {
            // ������
            dirChangeTimer -= Time.deltaTime;
            if (dirChangeTimer <= 0f)
                ChooseNewDirection();

            // �ƶ�
            float speed = baseSpeed * Random.Range(3f, 8f);
            rb.MovePosition(rb.position + currentDirection * speed * Time.deltaTime);

            // ��ת
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // ����Ģ��
    void ActivateMushroom()
    {
        isActive = true;

        // �л�����ͼ
        if (activeSprite != null)
        {
            spriteRenderer.sprite = activeSprite;
        }

        ChooseNewDirection();
    }

    // ��������붨ʱ��
    void ChooseNewDirection()
    {
        currentDirection = Random.insideUnitCircle.normalized;
        dirChangeTimer = Random.Range(dirChangeIntervalMin, dirChangeIntervalMax);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        var other = coll.gameObject;

        // ���� ײ����� ���� 
        if (other.CompareTag("Player"))
        {
            var playerRb = other.GetComponent<Rigidbody2D>();
            if (!isActive)
            {
                // �ǰ����ҳԵ�Ģ�� �� ��ұ��Ģ����ʧ
                playerController.jumpforce = 8;
                Destroy(gameObject);
            }
            else
            {
                // �����ұ�ײ�� �� ��ӻ�������Ģ��������
                if (playerRb != null)
                {
                    Vector2 knockDir = (other.transform.position - transform.position).normalized;
                    playerRb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
                }
            }

            return;
        }

        // ���� ײ�����淽�� ���� 
        if (other.CompareTag("GroundBlock") && isActive)
        {
            // ��󣺵�����Ģ����ʧ
            other.transform.localScale *= saleChangeNum;
            Destroy(gameObject);
        }
    }
}