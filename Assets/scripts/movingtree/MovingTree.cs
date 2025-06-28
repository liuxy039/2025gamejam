using UnityEngine;

public class MovingTree : MonoBehaviour
{
    private bool isalive = false;
    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private PhysicsMaterial2D originalMaterial;
    private PhysicsMaterial2D bouncyMaterial;

    [Header("�������")]
    public float bounceForce = 10f;
    public float moveSpeed = 5f;
    public float inactiveElasticity = 0f; // δ����ʱ�ĵ���
    public float activeElasticity = 0.8f; // �����ĵ���

    [Header("����ͼ����")]
    public Sprite inactiveSprite;
    public Sprite activeSprite;

    private void Start()
    {
        // ��ȡ����ӱ�Ҫ�����
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // ���ó�ʼ����ͼ
        spriteRenderer.sprite = inactiveSprite;

        // ���������������
        originalMaterial = new PhysicsMaterial2D()
        {
            bounciness = inactiveElasticity,
            friction = 0.4f
        };

        bouncyMaterial = new PhysicsMaterial2D()
        {
            bounciness = activeElasticity,
            friction = 0.1f
        };

        // ��ʼ����Ϊ�޵�������
        rb.sharedMaterial = originalMaterial;
        rb.gravityScale = 1f;

        // ������Ҷ���
        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("δ�ҵ���Ҷ�����ȷ����һ������'Player'��ǩ�Ķ���");
        }
    }

    private void OnMouseDown()
    {
        // ����󼤻�
        isalive = true;
        Debug.Log("���屻���");

        // �л�����ͼ
        spriteRenderer.sprite = activeSprite;

        // �л����е������������
        rb.sharedMaterial = bouncyMaterial;

        // ������ϵĵ���
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (isalive)
        {
            // ȷ�����������Ч
            if (player == null)
            {
                FindPlayer();
                return;
            }

            // �����ƶ�����
            float direction = (player.position.x > transform.position.x) ? -1 : 1;
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ֻ�ڼ���״̬�²��е���Ч��
        if (isalive && collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            rb.AddForce(-normal * bounceForce * 0.5f, ForceMode2D.Impulse);
        }
    }

}