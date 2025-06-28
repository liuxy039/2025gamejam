using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    public float speed = 7f;
    public float jumpforce = 15f;
    [SerializeField] LayerMask raycastLayer = 1 << 6;
    bool isGround = false;
    float input_x;
    Rigidbody2D rb;
    CapsuleCollider2D cld;
    Animator anim;
    Vector3 oscale;
    [SerializeField] private Slider hp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().sprite = sprite;
        oscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        raycast();
        getInput();
        move();
        onGround();
        jump();
        animation();
    }
    void animation()
    {
        anim.SetBool("Moving", input_x != 0);
        if (input_x != 0 & isGround)
        {
            anim.Play("Walk");
        }
    }

    private void onGround()
    {
        if (isGround)
        {
            anim.Play("Still");
        }
    }
    private void jump()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 50 * jumpforce);
        }
        if (!isGround)
        {
            anim.Play("Jump");
        }
    }

    private void move()
    {
        rb.velocity = new Vector2(input_x * speed, rb.velocity.y);
        if (input_x < 0)
        {
            transform.localScale = new Vector3(-oscale.x, oscale.y, oscale.z);
        }
        else if (input_x > 0)
        {
            transform.localScale = new Vector3(oscale.x, oscale.y, oscale.z);
        }
    }
    private void raycast()
    {
        Vector2 origin = cld.bounds.center + new Vector3(0, -cld.bounds.extents.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, 1 << 6);
        Debug.DrawLine(origin, origin + Vector2.down * 0.1f, Color.red, Time.deltaTime);
        if (hit.collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    private void getInput()
    {
        input_x = Input.GetAxisRaw("Horizontal");
    }

}
