using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class head : MonoBehaviour
{
    private bool isalive = false;//检测点击的变量
    public float offset = 20.0f; // 右移的距离
    public float speed = 0.2f; //左移的速度
    public enum MoveState { Left, Right, Idle }
    private MoveState currentState = MoveState.Left;
    private Rigidbody2D rb;
    private void OnMouseDown()
    {
        // 点击后执行的行为
        Debug.Log("物体被点击了！");
        isalive = true;
    }
    
    public Vector2 direction = Vector2.left;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        //transform.Translate(direction * speed * Time.deltaTime);
        if (isalive)
        {
            Debug.Log(Time.time);
            currentState = MoveState.Right; // 切换为右移状态
            //rb.MovePosition(rb.position + Vector2.right * offset);

            isalive =false;
        }
    }
    /*public class DeadlyObject : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // 调用主角的死亡方法
                collision.gameObject.GetComponent<PlayerController>().Die();
            }
        }
    }碰撞主角相关内容*/
    void FixedUpdate()
    {
        switch (currentState)
        {
            case MoveState.Left:
                rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);
                break;
            case MoveState.Right:
                rb.MovePosition(rb.position + Vector2.right * offset);
                currentState = MoveState.Left; // 右移后停止
                break;
        }
    }
}
