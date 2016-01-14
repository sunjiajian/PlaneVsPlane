using UnityEngine;
using System.Collections;

public class TrackBullet : MonoBehaviour {

    public float speed = 4f;

    private Enemy target;

	// Use this for initialization
	void Start () {
        print("born");
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
        float dt = Time.deltaTime;

        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * dt);
        }

        if (transform.position.y > 6f || transform.position.y < -6f || transform.position.x > 5f || transform.position.x < -5f) {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            if (!other.GetComponent<Enemy>().isDead) {
                other.gameObject.SendMessage("OnHit");
                Destroy(gameObject);
            }
        }
    }

    public void printss() {
        print("printss");
    }

    public void SetTarget(GameObject t) {
        target = t.GetComponent<Enemy>();
        print("SetTarget");
    }
}
