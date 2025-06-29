using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private TMP_Text gameOverMessageText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    
    [Header("Animation Settings")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.3f;

    // 事件定义
    public System.Action OnRestartRequested;
    public System.Action OnMainMenuRequested;

    private void Awake()
    {
        // 确保初始状态
        gameOverCanvasGroup.alpha = 0;
        gameOverCanvasGroup.blocksRaycasts = false;
        gameOverCanvasGroup.interactable = false;

        // 绑定按钮事件
        if (restartButton != null)
            restartButton.onClick.AddListener(() => OnRestartRequested?.Invoke());
        
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(() => OnMainMenuRequested?.Invoke());
    }

    /// <summary>
    /// 显示游戏结束界面
    /// </summary>
    /// <param name="message">要显示的游戏结束信息</param>
    public void ShowGameOver(string message = "Game Over")
    {
        if (gameOverMessageText != null)
            gameOverMessageText.text = message;

        gameOverCanvasGroup.blocksRaycasts = true;
        gameOverCanvasGroup.interactable = true;
        
        // 淡入动画
        StopAllCoroutines();
        StartCoroutine(FadeCanvasGroup(0, 1, fadeInDuration));
    }

    /// <summary>
    /// 隐藏游戏结束界面
    /// </summary>
    public void HideGameOver()
    {
        gameOverCanvasGroup.blocksRaycasts = false;
        gameOverCanvasGroup.interactable = false;
        
        // 淡出动画
        StopAllCoroutines();
        StartCoroutine(FadeCanvasGroup(1, 0, fadeOutDuration));
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            gameOverCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        gameOverCanvasGroup.alpha = endAlpha;
    }

    #if UNITY_EDITOR
    // 编辑器测试功能
    [ContextMenu("Test Show Game Over")]
    private void EditorTestShow()
    {
        if (!Application.isPlaying) return;
        ShowGameOver("Test Message");
    }

    [ContextMenu("Test Hide Game Over")]
    private void EditorTestHide()
    {
        if (!Application.isPlaying) return;
        HideGameOver();
    }
    #endif
}