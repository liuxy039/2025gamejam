using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUn : MonoBehaviour
{
    public GameObject ballObject; // �Ѿ����ڵ������
    public Transform launchPoint; // ������ʼ��
    public float launchForce = 10f; // ��������
    public float cooldownTime = 2f; // ��ȴʱ��

    private float cooldownTimer = 0f;
    private bool canLaunch = true;
    private Rigidbody2D ballRigidbody;
    private Vector2 originalBallPosition; // �洢���ԭʼλ��
    private bool isalive = false;
    private void OnMouseDown()
    {
        // �����ִ�е���Ϊ
        Debug.Log("���屻����ˣ�");
        isalive = true;
    }
    private void Start()
    {
        if (ballObject != null)
        {
            ballRigidbody = ballObject.GetComponent<Rigidbody2D>();
            originalBallPosition = ballObject.transform.position;

            // ��ʼʱ������
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

            // ������Է��䣬���ò�������
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

        // ������
        ballObject.SetActive(true);
    }

    // ��ѡ�������뿪��Ļ����ײʱ���ô˷���������
    public void ResetBall()
    {
        if (ballObject != null)
        {
            ballObject.SetActive(false);
            ballObject.transform.position = originalBallPosition;
        }
    }
}