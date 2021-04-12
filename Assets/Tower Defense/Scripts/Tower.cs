using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<Enemy> currentEnemies;
    public Enemy currentTarget;
    public Transform turret;
    private delegate void enemySubscription(Enemy enemy);

    private LineRenderer laser;
    public float hitAmount = 10;
    public float health = 100;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        //laser.positionCount = 2;
        laser.SetPosition(0, turret.transform.position);
    }

    void Update()
    {
        if (currentTarget)
        {

            currentTarget.Damage(hitAmount * Time.deltaTime);
            laser.SetPosition(1, currentTarget.transform.position);
            health -= hitAmount * Time.deltaTime;
        }

        if (health <= 0)
            Destroy(this.gameObject);
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            Enemy newEnemy = collider.GetComponent<Enemy>();
            newEnemy.DeathEvent.AddListener(delegate { BookKeeping(newEnemy); });
            currentEnemies.Add(newEnemy);
            if (currentTarget == null) currentTarget = newEnemy;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            Enemy oldEnemy = collider.GetComponent<Enemy>();
            BookKeeping(oldEnemy);
        }
    }

    void BookKeeping(Enemy enemy)
    {
        currentEnemies.Remove(enemy);
        currentTarget = (currentEnemies.Count > 0) ? currentEnemies[0] : null;

    }
}
