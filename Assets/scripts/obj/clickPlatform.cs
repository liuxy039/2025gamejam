using UnityEngine;

public class ClickPlatform : MonoBehaviour
{
    private MovingPlatform platformMove; // ����PlatformMove�ű�

    private void Start()
    {
        // ��ȡͬһ�����ϵ�PlatformMove���
        platformMove = GetComponent<MovingPlatform>();
        if (platformMove == null)
        {
            Debug.LogError("PlatformMove component not found on the same GameObject!");
        }
    }

    private void OnMouseDown()
    {
        // �л��ƶ�״̬
        if (platformMove != null)
        {
            platformMove.ToggleMovement();
        }
    }
}