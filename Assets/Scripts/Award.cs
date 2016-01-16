using UnityEngine;
using System.Collections;

public class Award : MonoBehaviour
{

    public float speed = 2;
    public int type;        // 0 gun    1 bomb

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -5.86f)
        {
            Destroy(gameObject);
        }
    }

    public void OnCatched()
    {
        Destroy(gameObject);
    }
}
