using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorshake : MonoBehaviour
{
    [SerializeField] float saleChangeNum = 3.0f;
    [Header("Movement Settings")]
    public float baseSpeed = 5f;          // 基础移动速度
    public float rotationSpeed = 360f;    // 活化后自转速度（度/秒）
    private Vector2 currentDirection;
    private float dirChangeTimer = 0f;
    private float dirChangeIntervalMin = 0.1f;
    private float dirChangeIntervalMax = 0.25f;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;     // 活化后撞到玩家的击退力

    private Rigidbody2D rb;
    private bool isActive = false;        // 是否点击激活

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. 点击激活
        if (!isActive && Input.GetMouseButtonDown(0))
        {
            Vector2 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapPoint(m) == GetComponent<Collider2D>())
            {
                isActive = true;
                ChooseNewDirection();
            }
        }

        // 2. 活化后：持续「疯狂乱跑 + 自转」
        if (isActive)
        {
            // 换方向
            dirChangeTimer -= Time.deltaTime;
            if (dirChangeTimer <= 0f)
                ChooseNewDirection();

            // 移动
            float speed = baseSpeed * Random.Range(3f, 8f);
            rb.MovePosition(rb.position + currentDirection * speed * Time.deltaTime);

            // 自转
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // 随机方向与定时器
    void ChooseNewDirection()
    {
        currentDirection = Random.insideUnitCircle.normalized;
        dirChangeTimer = Random.Range(dirChangeIntervalMin, dirChangeIntervalMax);
    }

}
