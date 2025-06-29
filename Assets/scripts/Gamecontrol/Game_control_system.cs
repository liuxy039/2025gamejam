using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_control_system : MonoBehaviour
{

    [SerializeField]GameOverManager gameOverManager;
    public void gameover()
    {
        gameOverManager.ShowGameOver();
    }
    public void retry() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新加载场景
    }
    public void pause()
    {
        Time.timeScale = 0.0f;
    }
    public void resume()
    {
        Time.timeScale = 1.0f;    
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }//切换到+1的场景去
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) 
        {
            retry();
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
