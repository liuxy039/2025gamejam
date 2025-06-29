using UnityEngine;
using System.Collections;

public class MovingGround : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public Transform movingGround;
    public Transform p1, p2;

    [Header("Sprite Settings")]
    public Sprite inactiveSprite;  // 未激活时的精灵图
    public Sprite activeSprite;    // 激活后的精灵图

    private bool isActive = false;
    private bool changed = false;
    private Coroutine movementCoroutine;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // 设置初始精灵图
        if (inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    private void OnMouseDown()
    {
        if (movingGround != null)
        {
            ToggleMovement();
        }
    }

    public void ToggleMovement()
    {
        if (!changed)
        {
            isActive = !isActive;
            changed = true;

            // 切换精灵图
            if (isActive && activeSprite != null)
            {
                spriteRenderer.sprite = activeSprite;
            }
            else if (!isActive && inactiveSprite != null)
            {
                spriteRenderer.sprite = inactiveSprite;
            }

            if (isActive)
            {
                // 启动移动协程
                if (movementCoroutine == null)
                {
                    movementCoroutine = StartCoroutine(MovePlatform(speed));
                }
            }
            else
            {
                // 停止移动协程
                if (movementCoroutine != null)
                {
                    StopCoroutine(movementCoroutine);
                    movementCoroutine = null;
                }
            }
        }
    }

    IEnumerator MovePlatform(float speed)
    {
        Vector3 dir = p1.position - p2.position;
        dir.Normalize();
        float t = 0;
        int direction = 1;

        while (true)
        {
            t += direction * Time.deltaTime * speed;

            // 控制平台在两点间往返移动
            if (t >= 1f)
            {
                direction = -1;
                t = 1f;
            }
            else if (t <= 0f)
            {
                direction = 1;
                t = 0f;
            }

            movingGround.position = Vector3.Lerp(p2.position, p1.position, t);
            yield return null;
        }
    }

    // 玩家站上平台时设为子物体
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    // 玩家离开平台时解除父子关系
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}