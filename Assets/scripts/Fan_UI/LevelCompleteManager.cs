using UnityEngine;
using UnityEngine.SceneManagement;
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
    
    [Header("Settings")]
    public float showDelay = 1f;       // 显示面板前的延迟
    public string levelCompleteMessage = "关卡完成！";
    
    private string nextSceneName;      // 下一关场景名

    private void Start()
    {
        // 确保开始时隐藏面板
        levelCompletePanel.SetActive(false);
        
        // 添加按钮事件监听
        nextLevelButton.onClick.AddListener(LoadNextLevel);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    // ======== 公开接口 ========
    
    // 触发通关（外部调用）
    public void TriggerLevelComplete(string nextScene, string stats = "")
    {
        nextSceneName = nextScene;
        
        // 设置统计信息（可选）
        if (statsText != null)
        {
            statsText.text = stats;
        }
        
        // 延迟显示面板
        Invoke("ShowLevelCompletePanel", showDelay);
    }

    // ======== UI控制 ========
    
    private void ShowLevelCompletePanel()
    {
        // 暂停游戏时间
        Time.timeScale = 0f;
        
        // 显示面板
        levelCompletePanel.SetActive(true);
        
        // 设置文本
        if (levelCompleteText != null)
        {
            levelCompleteText.text = levelCompleteMessage;
        }
        
        // 如果没有下一关，禁用下一关按钮
        if (string.IsNullOrEmpty(nextSceneName))
        {
            nextLevelButton.interactable = false;
            
            // 可选：修改按钮文本
            TMP_Text buttonText = nextLevelButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = "已是最后一关";
            }
        }
        else
        {
            nextLevelButton.interactable = true;
        }
        
        // 可选：播放音效
        // AudioManager.Instance.PlaySound("LevelComplete");
    }

    // ======== 按钮功能 ========
    
    private void LoadNextLevel()
    {
        // 恢复时间
        Time.timeScale = 1f;
        
        // 加载下一关
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void ReturnToMenu()
    {
        // 恢复时间
        Time.timeScale = 1f;
        
        // 加载主菜单（假设索引为0）
        SceneManager.LoadScene(0);
    }

    // ======== 编辑器工具 ========
    
    // 测试通关界面（在编辑器中点击按钮调用）
    [ContextMenu("测试通关界面")]
    public void TestLevelComplete()
    {
        TriggerLevelComplete("TestScene", "测试统计信息");
    }
}