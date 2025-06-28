using UnityEngine;

public class ClickPlatform : MonoBehaviour
{
    private MovingPlatform platformMove; // 引用PlatformMove脚本

    private void Start()
    {
        // 获取同一物体上的PlatformMove组件
        platformMove = GetComponent<MovingPlatform>();
        if (platformMove == null)
        {
            Debug.LogError("PlatformMove component not found on the same GameObject!");
        }
    }

    private void OnMouseDown()
    {
        // 切换移动状态
        if (platformMove != null)
        {
            platformMove.ToggleMovement();
        }
    }
}