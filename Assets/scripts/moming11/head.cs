using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class head : MonoBehaviour
{
    private bool isalive = false;//������ı���
    public float offset = 20.0f; // ���Ƶľ���
    public float speed = 0.2f; //���Ƶ��ٶ�
    public enum MoveState { Left, Right, Idle }
    private MoveState currentState = MoveState.Left;
    private Rigidbody2D rb;
    private void OnMouseDown()
    {
        // �����ִ�е���Ϊ
        Debug.Log("���屻����ˣ�");
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
            currentState = MoveState.Right; // �л�Ϊ����״̬
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
                // �������ǵ���������
                collision.gameObject.GetComponent<PlayerController>().Die();
            }
        }
    }��ײ�����������*/
    void FixedUpdate()
    {
        switch (currentState)
        {
            case MoveState.Left:
                rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);
                break;
            case MoveState.Right:
                rb.MovePosition(rb.position + Vector2.right * offset);
                currentState = MoveState.Left; // ���ƺ�ֹͣ
                break;
        }
    }
}
