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
        // �л��ƶ�״̬
        if (movingGround != null)
        {
            ToggleMovement();
        }
    }
    public void ToggleMovement()
    {
        if (!changed)
        {
            isMoving = !isMoving; // �л��ƶ�״̬
            changed = true;
            if (isMoving)
            {
                // ��������ƶ�������Э��
                if (movementCoroutine == null)
                {
                    movementCoroutine = StartCoroutine(movePlat(speed));
                }
            }
            else
            {
                // ���ֹͣ�ƶ���ֹͣЭ��
                if (movementCoroutine != null)
                {
                    StopCoroutine(movementCoroutine);
                    movementCoroutine = null;
                }
            }
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