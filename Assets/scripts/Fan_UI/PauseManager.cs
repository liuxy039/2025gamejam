using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel; // 暂停面板
    public Button pauseButton;    // 暂停按钮
    public Button resumeButton;   // 继续按钮
    public Button restartButton;  // 重新开始按钮
    public Button menuButton;     // 主菜单按钮


    private bool isPaused = false;

    void Start()
    {
        // 初始隐藏暂停面板
        pausePanel.SetActive(false);
        
        // 添加按钮事件监听
        pauseButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartLevel);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    void Update()
    {
        // 检测ESC键按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        
        // 暂停游戏时间
        Time.timeScale = 0f;
        
        // 显示暂停菜单
        pausePanel.SetActive(true);
        
        // 隐藏暂停按钮
        pauseButton.gameObject.SetActive(false);
        
        // 可选：播放暂停音效
        // AudioManager.Instance.PlaySound("Pause");
    }

    public void ResumeGame()
    {
        isPaused = false;
        
        // 恢复游戏时间
        Time.timeScale = 1f;
        
        // 隐藏暂停菜单
        pausePanel.SetActive(false);
        
        // 显示暂停按钮
        pauseButton.gameObject.SetActive(true);
        
        // 可选：播放继续音效
        // AudioManager.Instance.PlaySound("Resume");
    }

    public void RestartLevel()
    {
        // 恢复时间以防暂停状态影响重新加载
        Time.timeScale = 1f;
        
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        // 恢复时间
        Time.timeScale = 1f;
        
        // 加载主菜单场景，假设主菜单场景索引为0
        SceneManager.LoadScene(0);
    }
}