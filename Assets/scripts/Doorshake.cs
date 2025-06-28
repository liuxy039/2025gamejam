using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorshake : MonoBehaviour
{
    [SerializeField] float saleChangeNum = 3.0f;
    [Header("Movement Settings")]
    public float baseSpeed = 5f;          // �����ƶ��ٶ�
    public float rotationSpeed = 360f;    // �����ת�ٶȣ���/�룩
    private Vector2 currentDirection;
    private float dirChangeTimer = 0f;
    private float dirChangeIntervalMin = 0.1f;
    private float dirChangeIntervalMax = 0.25f;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;     // ���ײ����ҵĻ�����

    private Rigidbody2D rb;
    private bool isActive = false;        // �Ƿ�������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. �������
        if (!isActive && Input.GetMouseButtonDown(0))
        {
            Vector2 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapPoint(m) == GetComponent<Collider2D>())
            {
                isActive = true;
                ChooseNewDirection();
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

    // ��������붨ʱ��
    void ChooseNewDirection()
    {
        currentDirection = Random.insideUnitCircle.normalized;
        dirChangeTimer = Random.Range(dirChangeIntervalMin, dirChangeIntervalMax);
    }

}
