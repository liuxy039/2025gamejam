using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endingflag : MonoBehaviour
{
    [SerializeField] Game_control_system game_Control_System;
    [SerializeField] LevelCompleteManager levelCompleteManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞的物体是否有"Player"标签
        if (other.CompareTag("Player"))
        {
            levelCompleteManager.ShowLevelComplete(this);
        }
    }
}
