using System.Collections;
using UnityEngine;

public class Movingground : MonoBehaviour
{
    public float speed = 2;
    public Transform movingGround;
    public Transform p1, p2;
    private bool changed = false;
    private bool isMoving = false;
    private Coroutine movementCoroutine;

    private void OnMouseDown()
    {
        // 切换移动状态
        if (movingGround != null)
        {
            ToggleMovement();
        }
    }
    public void ToggleMovement()
    {
        if (!changed)
        {
            isMoving = !isMoving; // 切换移动状态
            changed = true;
            if (isMoving)
            {
                // 如果正在移动，启动协程
                if (movementCoroutine == null)
                {
                    movementCoroutine = StartCoroutine(movePlat(speed));
                }
            }
            else
            {
                // 如果停止移动，停止协程
                if (movementCoroutine != null)
                {
                    StopCoroutine(movementCoroutine);
                    movementCoroutine = null;
                }
            }
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
    IEnumerator movePlat(float speed)
    {
        Vector3 dir = p1.position - p2.position;
        dir.Normalize();
        float t = 0;
        int di = 1;

        while (true)
        {
            t += di * Time.deltaTime * speed;
            if (t >= 1)
            {
                di = -1;
            }
            if (t <= 0)
            {
                di = 1;
            }
            movingGround.transform.position = Vector3.Lerp(p2.position, p1.position, t);
            yield return null;
        }
    }
}