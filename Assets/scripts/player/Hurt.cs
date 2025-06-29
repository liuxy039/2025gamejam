using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    [Header("������Ч")]
    public ParticleSystem deathEffect;  // ����������Ч
    public float effectDuration = 2f;
    private float t = 0f;
    [SerializeField] GameObject fire;
    [SerializeField] GameObject Player;
    [SerializeField] Game_control_system game_control;
    private bool dead = false;
    private void Start()
    {
        fire.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("hurt"))
        {
            fire.gameObject.SetActive(true);
            Player.gameObject.SetActive(false);
            dead = true;
            game_control.gameover();
        }
    }
    private void Update()
    {
        if (t >= effectDuration || dead==false)
        {
            fire.gameObject.SetActive (false);
        }
        if(dead==true)
        {
            t += Time.deltaTime;
        }
    }


}
