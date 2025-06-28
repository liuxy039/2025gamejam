using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spite : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isalive = false;
    private float jumpTimer = 0f;
    private int jumpPhase = 0; // 0:未跳跃, 1:左上方跳跃, 2:右上方跳跃
    public float jumpSpeed = 5f; // 跳跃速度
    public float jumpDuration = 9.0f; // 每次跳跃持续时间

    //贴图状态切换
    //public Sprite aliveSprite;
    //public Sprite deadSprite;
    private SpriteRenderer spriteRenderer;
    private void OnMouseDown()
    {
        // 点击后执行的行为
        Debug.Log("物体被点击了！");
        isalive = true;
        jumpPhase = 1;
    }
    private void Start()
    {
    //    spriteRenderer = GetComponent<SpriteRenderer>();//状态切换
    }
    private void Update()
    {
        UpdateSprite();
        if (isalive)
        {
            jumpTimer += Time.deltaTime;

            // 左上方跳跃
            if (jumpPhase == 1)
            {
                transform.position += new Vector3(-1, 1, 0) * jumpSpeed * Time.deltaTime;
                if (jumpTimer >= jumpDuration)
                {
                    jumpPhase = 2; // 切换为右上方跳跃
                    jumpTimer = 0f;
                }
            }
            // 右上方跳跃
            else if (jumpPhase == 2)
            {
                transform.position += new Vector3(1, 1, 0) * jumpSpeed * Time.deltaTime;
                if (jumpTimer >= jumpDuration)
                {
                    jumpPhase = 1; // 切换回左上方跳跃，形成循环
                    jumpTimer = 0f;
                }
            }
        }
    }
    private void UpdateSprite()
    {
      //  spriteRenderer.sprite = isalive ? aliveSprite : deadSprite;//状态切换
    }
}