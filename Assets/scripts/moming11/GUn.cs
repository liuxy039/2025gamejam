using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUn : MonoBehaviour
{
    public GameObject ballObject; // 已经存在的球对象
    public Transform launchPoint; // 发射起始点
    public float launchForce = 10f; // 发射力度
    public float cooldownTime = 2f; // 冷却时间

    private float cooldownTimer = 0f;
    private bool canLaunch = true;
    private Rigidbody2D ballRigidbody;
    private Vector2 originalBallPosition; // 存储球的原始位置
    private bool isalive = false;
    private void OnMouseDown()
    {
        // 点击后执行的行为
        Debug.Log("物体被点击了！");
        isalive = true;
    }
    private void Start()
    {
        if (ballObject != null)
        {
            ballRigidbody = ballObject.GetComponent<Rigidbody2D>();
            originalBallPosition = ballObject.transform.position;

            // 初始时禁用球
            ballObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (isalive)
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

            // 如果可以发射，重置并发射球
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

        // 激活球
        ballObject.SetActive(true);
    }

    // 可选：当球离开屏幕或碰撞时调用此方法重置球
    public void ResetBall()
    {
        if (ballObject != null)
        {
            ballObject.SetActive(false);
            ballObject.transform.position = originalBallPosition;
        }
    }
}