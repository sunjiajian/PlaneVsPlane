using UnityEngine;
using System.Collections;


public class Hero : MonoBehaviour {

    public bool is_animation = true;

    public int frameCountPerseconds = 10;
    public float timer = 0;
    public Sprite[] sprites;

    private SpriteRenderer spriteRender;

    private Vector3 lastMousePosition = Vector3.zero;
    private bool isMouseDown = false;

    public float extraBulletTime = 0;

    public Gun gunTop;
    public Gun gunRight;
    public Gun gunLeft;

    private int upDownDir = 0;      // -1:down      0:none      1:up
    private int leftRightDir = 0;   // -1:left      0:none      1:right

    private float speedX = 1;
    private float speedY = 2.5f;


	// Use this for initialization
	void Start () {
		spriteRender = GetComponent<SpriteRenderer>();
        SwitchExtraBullet(false);
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        if (is_animation) {
            timer += dt;
            int frameIndex = (int)(timer / (1f / frameCountPerseconds));
            spriteRender.sprite = sprites[frameIndex % 2];
        }

        //keyboard control
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
            upDownDir += 1;
            upDownDir = upDownDir > 1 ? 1 : upDownDir;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) {
            upDownDir -= 1;
            upDownDir = upDownDir < -1 ? -1 : upDownDir;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) {
            leftRightDir += 1;
            leftRightDir = leftRightDir > 1 ? 1 : leftRightDir;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.A)) {
            leftRightDir -= 1;
            leftRightDir = leftRightDir < -1 ? -1 : leftRightDir;
        }

        Vector3 pos = transform.position;
        pos.x += leftRightDir * speedX * dt;
        pos.y += upDownDir * speedY * dt;
        transform.position = pos;
        checkPosition();

/*      //mouse control
        if (Input.GetMouseButtonDown(0)) {
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            lastMousePosition = Vector3.zero;
            isMouseDown = false;
        }
        if (isMouseDown && GameManager._instance.state == GameState.Gaming) {
            if (lastMousePosition != Vector3.zero) {
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                transform.position = transform.position + offset;
                checkPosition();
            }
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }*/

        if (extraBulletTime > 0) {
            extraBulletTime -= dt;
            if (extraBulletTime < 0) {
                SwitchExtraBullet(false);
            }
        }
	}

    private void SwitchExtraBullet(bool open) {
        gunTop.SwitchFire(!open);
        gunRight.SwitchFire(open);
        gunLeft.SwitchFire(open);
    }

    void checkPosition() {
        Vector3 pos = transform.position;
        if (pos.x < -3) {
            pos.x = -3;
        }
        else if (pos.x > 3) {
            pos.x = 3;
        }
        if (pos.y < -5) {
            pos.y = -5;
        }
        else if (pos.y > 5) {
            pos.y = 5;
        }

        transform.position = pos;
    }
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Award") {
            Award aw = other.GetComponent<Award>();
            if (aw.type == 0) {
                extraBulletTime = 10;
                SwitchExtraBullet(true);
            }

            other.gameObject.SendMessage("OnCatched");
        }
    }
}
