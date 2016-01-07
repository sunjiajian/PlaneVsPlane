using UnityEngine;
using System.Collections;

public enum EnemyType { 
    Enemy0,
    Enemy1,
    Enemy2,
}

public class Enemy : MonoBehaviour {

    public float speed = 5f;
    public float edge;

    public int hp;
    public int score;
    public EnemyType type;

    public bool isDead = false;

    private SpriteRenderer render;

    private float animationRate = 0.1f;

    private float explosionTimer = -1;
    public Sprite[] explosionSprites;

    private float hitTimer = -1;
    public Sprite[] hitSprites;

	// Use this for initialization
	void Start () {
        render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < edge) {
            Destroy(gameObject);
        }

        if (isDead) {
            explosionTimer += Time.deltaTime;
            int frameIndex = (int)(explosionTimer / animationRate);
            if (frameIndex >= explosionSprites.Length) {
                Destroy(gameObject);
                return;
            }
            render.sprite = explosionSprites[frameIndex];
        }
        else if (hitTimer >= 0) {
            hitTimer += Time.deltaTime;
            int frameIndex = (int)(hitTimer / animationRate);
            if (frameIndex >= hitSprites.Length) {
                hitTimer = -1;
                return;
            }
            render.sprite = hitSprites[frameIndex];
        }
	}

    void OnHit() {
        hp -= 1;
        if (hp <= 0) {
            if (!isDead) {
                isDead = true;
                explosionTimer = 0;

                GameManager._instance.score += score;
            }
        }
        else if (hitTimer < 0 && type != EnemyType.Enemy0) {
            hitTimer = 0;
        }
    }
}
