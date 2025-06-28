using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spite : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isalive = false;
    private float jumpTimer = 0f;
    private int jumpPhase = 0; // 0:δ��Ծ, 1:���Ϸ���Ծ, 2:���Ϸ���Ծ
    public float jumpSpeed = 5f; // ��Ծ�ٶ�
    public float jumpDuration = 9.0f; // ÿ����Ծ����ʱ��

    //��ͼ״̬�л�
    //public Sprite aliveSprite;
    //public Sprite deadSprite;
    private SpriteRenderer spriteRenderer;
    private void OnMouseDown()
    {
        // �����ִ�е���Ϊ
        Debug.Log("���屻����ˣ�");
        isalive = true;
        jumpPhase = 1;
    }
    private void Start()
    {
    //    spriteRenderer = GetComponent<SpriteRenderer>();//״̬�л�
    }
    private void Update()
    {
        UpdateSprite();
        if (isalive)
        {
            jumpTimer += Time.deltaTime;

            // ���Ϸ���Ծ
            if (jumpPhase == 1)
            {
                transform.position += new Vector3(-1, 1, 0) * jumpSpeed * Time.deltaTime;
                if (jumpTimer >= jumpDuration)
                {
                    jumpPhase = 2; // �л�Ϊ���Ϸ���Ծ
                    jumpTimer = 0f;
                }
            }
            // ���Ϸ���Ծ
            else if (jumpPhase == 2)
            {
                transform.position += new Vector3(1, 1, 0) * jumpSpeed * Time.deltaTime;
                if (jumpTimer >= jumpDuration)
                {
                    jumpPhase = 1; // �л������Ϸ���Ծ���γ�ѭ��
                    jumpTimer = 0f;
                }
            }
        }
    }
    private void UpdateSprite()
    {
      //  spriteRenderer.sprite = isalive ? aliveSprite : deadSprite;//״̬�л�
    }
}