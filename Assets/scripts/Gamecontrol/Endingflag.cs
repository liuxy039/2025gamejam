using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endingflag : MonoBehaviour
{
    [SerializeField] Game_control_system game_Control_System;
    [SerializeField] LevelCompleteManager levelCompleteManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����ײ�������Ƿ���"Player"��ǩ
        if (other.CompareTag("Player"))
        {
            levelCompleteManager.ShowLevelComplete(this);
        }
    }
}
