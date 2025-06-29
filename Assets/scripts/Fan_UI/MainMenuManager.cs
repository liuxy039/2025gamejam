using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    [Header("主菜单UI")]
    public GameObject mainMenuPanel;
    public Button startButton;
    public Button quitButton;

    [Header("关卡选择面板")]
    public GameObject levelSelectPanel;
    public Transform levelButtonContainer; // 关卡按钮的父对象
    public GameObject levelButtonPrefab;   // 关卡按钮预制体
    public Button backButton;
    public CanvasGroup levelSelectCanvasGroup; // 用于淡入淡出效果
    
    [Header("关卡设置")]
    public List<LevelInfo> levels = new List<LevelInfo>();
    
    [System.Serializable]
    public class LevelInfo
    {
        public string levelName;       // 关卡显示名称
        public string sceneName;       // 场景名称
        public Sprite thumbnail;       // 缩略图（可选）
    }

    private void Start()
    {
        // 初始化界面状态
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        
        // 主菜单按钮事件
        startButton.onClick.AddListener(ShowLevelSelect);
        quitButton.onClick.AddListener(QuitGame);
        
        // 返回按钮事件
        backButton.onClick.AddListener(HideLevelSelect);
        
        // 生成关卡按钮
        GenerateLevelButtons();
    }

    private void Update()
    {
        // ESC键关闭关卡选择面板
        if (levelSelectPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            HideLevelSelect();
        }
    }

    // 显示关卡选择面板
    public void ShowLevelSelect()
    {
        StartCoroutine(ShowLevelSelectAnimation());
    }

    private IEnumerator ShowLevelSelectAnimation()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
        
        // 初始化动画状态
        levelSelectPanel.transform.localScale = Vector3.zero;
        if (levelSelectCanvasGroup != null)
        {
            levelSelectCanvasGroup.alpha = 0f;
        }
        
        // 缩放动画
        float duration = 0.3f;
        float timer = 0f;
        
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            
            // 缩放效果
            levelSelectPanel.transform.localScale = Vector3.Lerp(
                Vector3.zero, 
                Vector3.one, 
                EaseOutBack(progress)
            );
            
            // 淡入效果
            if (levelSelectCanvasGroup != null)
            {
                levelSelectCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);
            }
            
            yield return null;
        }
        
        // 确保最终状态
        levelSelectPanel.transform.localScale = Vector3.one;
        if (levelSelectCanvasGroup != null)
        {
            levelSelectCanvasGroup.alpha = 1f;
        }
    }

    // 隐藏关卡选择面板
    public void HideLevelSelect()
    {
        StartCoroutine(HideLevelSelectAnimation());
    }

    private IEnumerator HideLevelSelectAnimation()
    {
        // 初始状态
        Vector3 startScale = levelSelectPanel.transform.localScale;
        float startAlpha = levelSelectCanvasGroup != null ? levelSelectCanvasGroup.alpha : 1f;
        
        // 动画参数
        float duration = 0.2f;
        float timer = 0f;
        
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            
            // 缩放效果
            levelSelectPanel.transform.localScale = Vector3.Lerp(
                startScale, 
                Vector3.zero, 
                EaseInBack(progress)
            );
            
            // 淡出效果
            if (levelSelectCanvasGroup != null)
            {
                levelSelectCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, progress);
            }
            
            yield return null;
        }
        
        // 确保最终状态
        levelSelectPanel.transform.localScale = Vector3.zero;
        if (levelSelectCanvasGroup != null)
        {
            levelSelectCanvasGroup.alpha = 0f;
        }
        
        // 关闭面板并显示主菜单
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // 缓动函数 - 回弹效果
    private float EaseOutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(x - 1f, 3f) + c1 * Mathf.Pow(x - 1f, 2f);
    }

    // 缓动函数 - 快速进入效果
    private float EaseInBack(float x)
    {
        float c1 = 1.70158f;
        return c1 * x * x * x - c1 * x * x;
    }

    // 生成关卡按钮
    private void GenerateLevelButtons()
    {
        // 清除现有按钮（如果有）
        foreach (Transform child in levelButtonContainer)
        {
            Destroy(child.gameObject);
        }
        
        // 创建新按钮
        for (int i = 0; i < levels.Count; i++)
        {
            int index = i; // 创建局部变量避免闭包问题
            
            // 实例化按钮
            GameObject buttonObj = Instantiate(levelButtonPrefab, levelButtonContainer);
            LevelButton levelButton = buttonObj.GetComponent<LevelButton>();
            
            // 设置按钮信息
            if (levelButton != null)
            {
                levelButton.Setup(levels[index], () => LoadLevel(levels[index].sceneName));
            }
            
            // 可选：添加按钮动画效果
            StartCoroutine(AnimateButtonAppearance(buttonObj, i * 0.1f));
        }
    }

    // 按钮出现动画
    private IEnumerator AnimateButtonAppearance(GameObject button, float delay)
    {
        button.transform.localScale = Vector3.zero;
        
        // 延迟
        yield return new WaitForSecondsRealtime(delay);
        
        float duration = 0.3f;
        float timer = 0f;
        
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            
            button.transform.localScale = Vector3.Lerp(
                Vector3.zero, 
                Vector3.one, 
                EaseOutBack(progress)
            );
            
            yield return null;
        }
        
        button.transform.localScale = Vector3.one;
    }

    // 加载关卡场景
    public void LoadLevel(string sceneName)
    {
        // 简单场景加载
        SceneManager.LoadScene(sceneName);
        
        // 可选：添加加载动画
        // StartCoroutine(LoadLevelWithAnimation(sceneName));
    }

    // 退出游戏
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // === 编辑器工具方法 ===
    
    // 添加新关卡
    [ContextMenu("添加新关卡")]
    public void AddNewLevel()
    {
        levels.Add(new LevelInfo{
            levelName = "新关卡",
            sceneName = "Level_" + (levels.Count + 1)
        });
        GenerateLevelButtons();
    }
    
    // 移除最后一个关卡
    [ContextMenu("移除最后关卡")]
    public void RemoveLastLevel()
    {
        if (levels.Count > 0)
        {
            levels.RemoveAt(levels.Count - 1);
            GenerateLevelButtons();
        }
    }
}