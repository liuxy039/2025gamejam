using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2;
    public Transform movingplatform;
    public Transform p1, p2, p3, p4;
    private bool isMoving = false;
    private Coroutine movementCoroutine;

    public void ToggleMovement()
    {
        isMoving = !isMoving;

        if (isMoving)
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

    // 玩家站上平台时设为子物体

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
}