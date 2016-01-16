using UnityEngine;
using System.Collections;

enum TrackRocketState {
    NOTARGET,
    HASTARGET,
}

public class TrackRocket : MonoBehaviour
{
    public float edgeX;
    public float edgeY;

    private float speed = 1f;
    private float maxSpeed = 10f;
    private float acceleration = 5f;

    private Enemy target = null;
    private TrackRocketState trackState = TrackRocketState.NOTARGET;

    private Vector3 myDirection = Vector3.up;
    private float minChangeDireDis = 0.5f;
    private float maxTargetDis = 2f;
    private float maxTrackAngle = 45f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (speed < maxSpeed)
        {
            speed += acceleration * dt;
            speed = speed > maxSpeed ? maxSpeed : speed;
        }

        if ((hasTarget() || findTarget()) && Vector3.Distance(target.transform.position, transform.position) > minChangeDireDis)
        {
            myDirection = target.transform.position - transform.position;
            myDirection.Normalize();
        }
        transform.position += myDirection * speed * dt;

        if (transform.position.y > edgeY || transform.position.y < -edgeY || transform.position.x > edgeX || transform.position.x < -edgeX)
        {
            Destroy(gameObject);
        }
    }

    private bool hasTarget()
    {
        if (trackState == TrackRocketState.HASTARGET && target != null && !target.isDead)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            if (Vector3.Angle(myDirection, targetDirection) < maxTrackAngle)
            {
                return true;
            }
        }
        return false;
    }

    private bool findTarget()
    {
        float minDis = maxTargetDis;
        GameObject minDisEnemy = null;
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemys)
        {
            if (enemy.GetComponent<Enemy>() != null && !enemy.GetComponent<Enemy>().isDead && enemy.transform.position.y > transform.position.y + 0.5f)
            {
                Vector3 targetDirection = enemy.transform.position - transform.position;
                float dis = Vector3.Distance(transform.position, enemy.transform.position);
                if (dis < minDis && Vector3.Angle(myDirection, targetDirection) < maxTrackAngle)
                {
                    minDis = dis;
                    minDisEnemy = enemy;
                }
            }
        }

        if (minDisEnemy != null)
        {
            target = minDisEnemy.GetComponent<Enemy>();
            trackState = TrackRocketState.HASTARGET;
            return true;
        }

        target = null;
        trackState = TrackRocketState.NOTARGET;
        return false;
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

    //public void SetTarget(GameObject t)
    //{
    //    target = t.GetComponent<Enemy>();

    //    diretion = target.transform.position - transform.position;
    //    diretion.Normalize();
    //}
}
