using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [Header("UI组件")]
    public Button button;
    public TMP_Text levelNameText;
    public Image thumbnailImage;
    
    public void Setup(MainMenuManager.LevelInfo levelInfo, System.Action onClickAction)
    {
        // 设置文本
        if (levelNameText != null)
        {
            levelNameText.text = levelInfo.levelName;
        }
        
        // 设置缩略图
        if (thumbnailImage != null)
        {
            if (levelInfo.thumbnail != null)
            {
                thumbnailImage.sprite = levelInfo.thumbnail;
                thumbnailImage.gameObject.SetActive(true);
            }
            else
            {
                thumbnailImage.gameObject.SetActive(false);
            }
        }
        
        // 设置点击事件
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClickAction?.Invoke());
    }
}