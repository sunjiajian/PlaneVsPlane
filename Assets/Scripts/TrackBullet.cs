using UnityEngine;
using System.Collections;

public class TrackBullet : MonoBehaviour
{

    private float speed = 6f;

    private Enemy target;

    private Vector3 diretion;

    private float minChangeDireDis = 0.5f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (target != null && !target.isDead && Vector3.Distance(target.transform.position, transform.position) > minChangeDireDis)
        {
            diretion = target.transform.position - transform.position;
            diretion.Normalize();
        }
        transform.position += diretion * speed * dt;

        if (transform.position.y > 5.2f || transform.position.y < -5.2f || transform.position.x > 3.2f || transform.position.x < -3.2f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (!other.GetComponent<Enemy>().isDead)
            {
                other.gameObject.SendMessage("OnHit");
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(GameObject t)
    {
        target = t.GetComponent<Enemy>();

        diretion = target.transform.position - transform.position;
        diretion.Normalize();
    }
}
