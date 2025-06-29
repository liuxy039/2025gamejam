using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public Transform movingplatform;
    public Transform p1, p2, p3, p4;

    [Header("Sprite Settings")]
    public Sprite inactiveSprite;  // δ����ʱ�ľ���ͼ
    public Sprite activeSprite;    // �����ľ���ͼ

    private bool isActive = false;
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

        // ���ó�ʼ����ͼ(δ����״̬)
        if (inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    public void ToggleMovement()
    {
        isActive = !isActive;

        // �л�����ͼ
        if (isActive && activeSprite != null)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else if (!isActive && inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }

        // �����ƶ�Э��
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

            // ����Ƿ񵽴��߶��յ�
            if (t >= 1f)
            {
                t = 0f;
                currentSegment = (currentSegment + 1) % points.Length;
            }

            yield return null;
        }
    }

    // ���վ��ƽ̨ʱ��Ϊ������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(movingplatform);
        }
    }

    // ����뿪ƽ̨ʱ������ӹ�ϵ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}