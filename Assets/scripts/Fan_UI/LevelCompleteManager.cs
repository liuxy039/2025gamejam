using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject levelCompletePanel; // 通关面板
    public Button nextLevelButton;     // 下一关按钮
    public Button menuButton;          // 主菜单按钮
    public Button testButton;         // 测试按钮（仅编辑器可见）
    [SerializeField]Game_control_system gameControlSystem;

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


    public void ShowLevelComplete(bool hasNextLevel, string statsInfo = "")
    {
        levelCompletePanel.SetActive(true);
        
  
        
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


    public void HideLevelComplete()
    {
        levelCompletePanel.SetActive(false);
    }

    // ========== 按钮事件 ==========
    
    private void OnNextLevelClicked()
    {
        // 外部需要通过事件或其他方式处理实际逻辑
        gameControlSystem.nextLevel();
    }

    private void OnMenuClicked()
    {
        gameControlSystem.backToMenu();
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