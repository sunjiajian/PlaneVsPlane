using UnityEngine;
using System.Collections;

public class TrackGun : MonoBehaviour
{
    private float rate = 0.1f;
    private float tick = 0;
    private float maxDistance = 2f;

    public GameObject bullet;

    // Use this for initialization
    void Start()
    {
        tick = rate;
    }

    // Update is called once per frame
    void Update()
    {
        tick -= Time.deltaTime;
        if (tick <= 0)
        {
            float minDis = maxDistance;
            GameObject minDisEnemy = null;
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemys)
            {
                if (enemy.GetComponent<Enemy>() != null && enemy.transform.position.y > transform.position.y + 0.5f)
                {
                    float dis = Vector3.Distance(transform.position, enemy.transform.position);
                    if (dis < minDis)
                    {
                        minDis = dis;
                        minDisEnemy = enemy;
                    }
                }
            }

            if (minDis < maxDistance)
            {
                GameObject obf = (GameObject)GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
                obf.GetComponent<TrackBullet>().SetTarget(minDisEnemy);

                tick = rate;
            }
        }
    }
}
