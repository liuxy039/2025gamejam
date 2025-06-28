using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject levelCompletePanel; // 通关面板
    public TMP_Text levelCompleteText;  // 通关文本
    public TMP_Text statsText;         // 统计信息文本（可选）
    public Button nextLevelButton;     // 下一关按钮
    public Button menuButton;          // 主菜单按钮
    public Button testButton;         // 测试按钮（仅编辑器可见）

    [Header("Settings")]
    public string levelCompleteMessage = "关卡完成！";

    private void Start()
    {
        // 初始化UI状态
        levelCompletePanel.SetActive(false);
        
        // 绑定按钮事件
        nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        menuButton.onClick.AddListener(OnMenuClicked);
        
        // 仅在编辑器中显示测试按钮
        #if UNITY_EDITOR
            testButton.gameObject.SetActive(true);
            testButton.onClick.AddListener(TestLevelComplete);
        #else
            testButton.gameObject.SetActive(false);
        #endif
    }

    // ========== 公开接口 ==========
    
    /// <summary>
    /// 显示通关界面
    /// </summary>
    /// <param name="hasNextLevel">是否有下一关</param>
    /// <param name="statsInfo">统计信息（可选）</param>
    public void ShowLevelComplete(bool hasNextLevel, string statsInfo = "")
    {
        levelCompletePanel.SetActive(true);
        
        // 设置文本
        if (levelCompleteText != null)
        {
            levelCompleteText.text = levelCompleteMessage;
        }
        
        if (statsText != null && !string.IsNullOrEmpty(statsInfo))
        {
            statsText.text = statsInfo;
        }
        
        // 设置下一关按钮状态
        nextLevelButton.interactable = hasNextLevel;
        
        // 如果没有下一关，修改按钮文本
        if (!hasNextLevel)
        {
            TMP_Text buttonText = nextLevelButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = "已是最后一关";
            }
        }
    }

    /// <summary>
    /// 隐藏通关界面
    /// </summary>
    public void HideLevelComplete()
    {
        levelCompletePanel.SetActive(false);
    }

    // ========== 按钮事件 ==========
    
    private void OnNextLevelClicked()
    {
        // 外部需要通过事件或其他方式处理实际逻辑
    }

    private void OnMenuClicked()
    {
        // 外部需要通过事件或其他方式处理实际逻辑
    }

    // ========== 测试功能 ==========
    
    #if UNITY_EDITOR
    [ContextMenu("测试通关界面")]
    private void TestLevelComplete()
    {
        ShowLevelComplete(true, "测试数据:\n分数: 1000\n时间: 02:30");
    }
    #endif
}