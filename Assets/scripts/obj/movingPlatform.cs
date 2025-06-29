using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public Transform movingplatform;
    public Transform p1, p2, p3, p4;

    [Header("Sprite Settings")]
    public Sprite inactiveSprite;  // 未激活时的精灵图
    public Sprite activeSprite;    // 激活后的精灵图

    private bool isActive = false;
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

        // 设置初始精灵图(未激活状态)
        if (inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    public void ToggleMovement()
    {
        isActive = !isActive;

        // 切换精灵图
        if (isActive && activeSprite != null)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else if (!isActive && inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }

        // 控制移动协程
        if (isActive)
        {
            if (movementCoroutine == null)
            {
                movementCoroutine = StartCoroutine(MoveBetweenPoints());
            }
        }
        else
        {
            if (movementCoroutine != null)
            {
                StopCoroutine(movementCoroutine);
                movementCoroutine = null;
            }
        }
    }

    IEnumerator MoveBetweenPoints()
    {
        Transform[] points = new Transform[] { p1, p2, p3, p4 };
        int currentSegment = 0;
        float t = 0f;

        while (true)
        {
            Transform startPoint = points[currentSegment];
            Transform endPoint = points[(currentSegment + 1) % points.Length];

            t += Time.deltaTime * speed;
            movingplatform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);

            // 检查是否到达线段终点
            if (t >= 1f)
            {
                t = 0f;
                currentSegment = (currentSegment + 1) % points.Length;
            }

            yield return null;
        }
    }

    // 玩家站上平台时设为子物体
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(movingplatform);
        }
    }

    // 玩家离开平台时解除父子关系
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}