using UnityEngine;

public class MovingTree : MonoBehaviour
{
    private bool isalive = false;
    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private PhysicsMaterial2D originalMaterial;
    private PhysicsMaterial2D bouncyMaterial;

    [Header("物理参数")]
    public float bounceForce = 10f;
    public float moveSpeed = 5f;
    public float inactiveElasticity = 0f; // 未激活时的弹性
    public float activeElasticity = 0.8f; // 激活后的弹性

    [Header("精灵图设置")]
    public Sprite inactiveSprite;
    public Sprite activeSprite;

    private void Start()
    {
        // 获取或添加必要的组件
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

        // 设置初始精灵图
        spriteRenderer.sprite = inactiveSprite;

        // 创建两种物理材质
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

        // 初始设置为无弹力材质
        rb.sharedMaterial = originalMaterial;
        rb.gravityScale = 1f;

        // 查找玩家对象
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
            Debug.LogWarning("未找到玩家对象！请确保有一个带有'Player'标签的对象");
        }
    }

    private void OnMouseDown()
    {
        // 点击后激活
        isalive = true;
        Debug.Log("物体被激活！");

        // 切换精灵图
        spriteRenderer.sprite = activeSprite;

        // 切换到有弹力的物理材质
        rb.sharedMaterial = bouncyMaterial;

        // 添加向上的弹力
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (isalive)
        {
            // 确保玩家引用有效
            if (player == null)
            {
                FindPlayer();
                return;
            }

            // 计算移动方向
            float direction = (player.position.x > transform.position.x) ? -1 : 1;
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 只在激活状态下才有弹力效果
        if (isalive && collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            rb.AddForce(-normal * bounceForce * 0.5f, ForceMode2D.Impulse);
        }
    }

}