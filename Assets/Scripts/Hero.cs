using UnityEngine;
using System.Collections;


public class Hero : MonoBehaviour
{
    public bool is_animation = true;

    public int frameCountPerseconds = 10;
    public float timer = 0;
    public Sprite[] sprites;

    private SpriteRenderer spriteRender;

    //private Vector3 lastMousePosition = Vector3.zero;
    //private bool isMouseDown = false;

    public float extraBulletTime = 0;

    public Gun gunTop;
    public Gun gunRight;
    public Gun gunLeft;

    private int upDownDir = 0;      // -1:down      0:none      1:up
    private int leftRightDir = 0;   // -1:left      0:none      1:right

    private float speedX = 3f;
    private float speedY = 3f;

/*
    public float myAngle = 0f;
    public Vector3 mouPos;
    public Vector3 dirc;
    public float angle;*/

    // Use this for initialization
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        SwitchExtraBullet(false);

        print(Vector3.Angle(Vector3.up, Vector3.left));
        print(Vector3.Angle(Vector3.left, Vector3.up));
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (is_animation)
        {
            timer += dt;
            int frameIndex = (int)(timer / (1f / frameCountPerseconds));
            spriteRender.sprite = sprites[frameIndex % 2];
        }

        //keyboard control
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            upDownDir += 1;
            upDownDir = upDownDir > 1 ? 1 : upDownDir;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            upDownDir -= 1;
            upDownDir = upDownDir < -1 ? -1 : upDownDir;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            leftRightDir += 1;
            leftRightDir = leftRightDir > 1 ? 1 : leftRightDir;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            leftRightDir -= 1;
            leftRightDir = leftRightDir < -1 ? -1 : leftRightDir;
        }

        Vector3 pos = transform.position;
        pos.x += leftRightDir * speedX * dt;
        pos.y += upDownDir * speedY * dt;
        transform.position = pos;
        checkPosition();
        //transform.Rotate(Vector3.forward * Time.deltaTime * 90);

/*
        //mouse control
        if (Input.GetMouseButtonDown(0))
        {
            mouPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dirc = new Vector3(mouPos.x, mouPos.y, 0) - transform.position;
            angle = Vector3.Angle(Vector3.up, dirc) * fuhao(dirc.x) * -1;
            transform.Rotate(Vector3.forward * (angle - myAngle));
            myAngle = angle;
        }*/

        if (extraBulletTime > 0)
        {
            extraBulletTime -= dt;
            if (extraBulletTime < 0)
            {
                SwitchExtraBullet(false);
            }
        }
    }

    private float fuhao(float x)
    {
        return x < 0 ? -1 : 1;
    }

    private void SwitchExtraBullet(bool open)
    {
        gunTop.SwitchFire(false);
        gunRight.SwitchFire(false);
        gunLeft.SwitchFire(false);
        //gunTop.SwitchFire(!open);
        //gunRight.SwitchFire(open);
        //gunLeft.SwitchFire(open);
    }

    void checkPosition()
    {
        Vector3 pos = transform.position;
        if (pos.x < -3)
        {
            pos.x = -3;
        }
        else if (pos.x > 3)
        {
            pos.x = 3;
        }
        if (pos.y < -5)
        {
            pos.y = -5;
        }
        else if (pos.y > 5)
        {
            pos.y = 5;
        }

        transform.position = pos;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Award")
        {
            Award aw = other.GetComponent<Award>();
            if (aw.type == 0)
            {
                extraBulletTime = 10;
                SwitchExtraBullet(true);
            }

            other.gameObject.SendMessage("OnCatched");
        }
    }
}
