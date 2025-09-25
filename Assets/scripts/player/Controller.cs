using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 添加这个命名空间

public class Controller : MonoBehaviour
{
    // 原有变量保持不变
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

    // 按钮控制变量
    private bool leftButtonDown = false;
    private bool rightButtonDown = false;
    private bool jumpButtonDown = false;

    [SerializeField] Button left;
    [SerializeField] Button right;
    [SerializeField] Button space;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        oscale = transform.localScale;

        // 设置按钮事件
        SetupButtonEvents();
    }

    void SetupButtonEvents()
    {
        // 左按钮
        var leftTrigger = left.gameObject.AddComponent<EventTrigger>();
        AddPointerEvent(leftTrigger, EventTriggerType.PointerDown, () => leftButtonDown = true);
        AddPointerEvent(leftTrigger, EventTriggerType.PointerUp, () => leftButtonDown = false);

        // 右按钮
        var rightTrigger = right.gameObject.AddComponent<EventTrigger>();
        AddPointerEvent(rightTrigger, EventTriggerType.PointerDown, () => rightButtonDown = true);
        AddPointerEvent(rightTrigger, EventTriggerType.PointerUp, () => rightButtonDown = false);

        // 跳跃按钮
        var spaceTrigger = space.gameObject.AddComponent<EventTrigger>();
        AddPointerEvent(spaceTrigger, EventTriggerType.PointerDown, () => jumpButtonDown = true);
        AddPointerEvent(spaceTrigger, EventTriggerType.PointerUp, () => jumpButtonDown = false);
    }

    void AddPointerEvent(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction action)
    {
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((e) => action());
        trigger.triggers.Add(entry);
    }

    void Update()
    {
        // 保持原有逻辑不变
        raycast();
        getInput();
        move();
        onGround();
        jump();
        animation();
    }

    // 修改输入检测，加入按钮状态
    private void getInput()
    {
        // 键盘输入优先
        input_x = Input.GetAxisRaw("Horizontal");

        // 如果键盘没有输入，再检测按钮
        if (input_x == 0)
        {
            if (leftButtonDown) input_x = -1;
            else if (rightButtonDown) input_x = 1;
        }
    }

    // 修改跳跃检测，加入按钮状态
    private void jump()
    {

        if(isGround && (Input.GetKeyDown(KeyCode.Space) || jumpButtonDown))
        {
            rb.AddForce(Vector2.up * 50 * jumpforce);
            jumpButtonDown = false; // 防止连续跳跃
        }
        if (!isGround)
        {
            anim.SetInteger("state", 2);
        }
    }

    // 保持原有方法不变
    private void animation()
    {
        anim.SetBool("Moving", input_x != 0);
        if (input_x != 0 & isGround)
        {
            anim.SetInteger("state", 1);
        }
    }
    private void onGround()
    {
        if (isGround & input_x == 0)
        {
            anim.SetInteger("state", 0);
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

    // 保持原有按钮引用
    void OnLeftClicked() { }
    void OnRightClicked() { }
    void OnSpaceClicked() { }
}