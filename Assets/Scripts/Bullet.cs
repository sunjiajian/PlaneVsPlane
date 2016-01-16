using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed = 4f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > 6f)
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
}
