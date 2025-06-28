using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel; // 游戏结束面板
    public TMP_Text gameOverText;   // 游戏结束文本（可选）
    public Button restartButton;    // 重新开始按钮
    public Button menuButton;       // 主菜单按钮
    public Button testButton;       // 测试按钮（可选，仅在编辑器中显示）

    [Header("Settings")]
    public float gameOverDelay = 1f; // 显示游戏结束面板前的延迟

    private void Start()
    {
        // 确保开始时隐藏面板
        gameOverPanel.SetActive(false);
        
        // 添加按钮事件监听
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(ReturnToMenu);
        
        // 在编辑器中显示测试按钮，发布版本中隐藏
        #if UNITY_EDITOR
        if (testButton != null)
        {
            testButton.gameObject.SetActive(true);
            testButton.onClick.AddListener(TestGameOver);
        }
        #else
        if (testButton != null)
        {
            testButton.gameObject.SetActive(false);
        }
        #endif
    }

    // 触发游戏结束（在满足条件时调用）
    public void TriggerGameOver(string reason = "游戏结束")
    {
        // 可选：设置游戏结束原因文本
        if (gameOverText != null)
        {
            gameOverText.text = reason;
        }
        
        // 延迟显示面板，给玩家一点反应时间
        Invoke("ShowGameOverPanel", gameOverDelay);
    }

    private void ShowGameOverPanel()
    {
        // 暂停游戏时间
        Time.timeScale = 0f;
        
        // 显示游戏结束面板
        gameOverPanel.SetActive(true);
        
        // 可选：暂停音频
        // AudioListener.pause = true;
        
        // 可选：播放游戏结束音效
        // AudioManager.Instance.PlaySound("GameOver");
    }

    public void RestartGame()
    {
        // 恢复时间
        Time.timeScale = 1f;
        
        // 可选：恢复音频
        // AudioListener.pause = false;
        
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        // 恢复时间
        Time.timeScale = 1f;
        
        // 可选：恢复音频
        // AudioListener.pause = false;
        
        // 加载主菜单场景（假设索引为0）
        SceneManager.LoadScene(0);
    }

    // ====== 测试功能 ======
    
    // 编辑器测试按钮调用的方法
    public void TestGameOver()
    {
        TriggerGameOver("测试游戏结束");
    }

    // 在Inspector中添加测试按钮（无需UI按钮）
    [ContextMenu("测试游戏结束界面")]
    public void EditorTestGameOver()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("测试功能只能在运行模式下使用！");
            return;
        }
        TestGameOver();
    }
}