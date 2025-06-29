using UnityEngine;
using System.Collections;

public class MovingGround : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public Transform movingGround;
    public Transform p1, p2;

    [Header("Sprite Settings")]
    public Sprite inactiveSprite;  // δ����ʱ�ľ���ͼ
    public Sprite activeSprite;    // �����ľ���ͼ

    private bool isActive = false;
    private bool changed = false;
    private Coroutine movementCoroutine;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // ��ȡSpriteRenderer���
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // ���ó�ʼ����ͼ
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

            // �л�����ͼ
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
                // �����ƶ�Э��
                if (movementCoroutine == null)
                {
                    movementCoroutine = StartCoroutine(MovePlatform(speed));
                }
            }
            else
            {
                // ֹͣ�ƶ�Э��
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

            // ����ƽ̨������������ƶ�
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

    // ���վ��ƽ̨ʱ��Ϊ������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    // ����뿪ƽ̨ʱ������ӹ�ϵ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}