using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
  public Path route;
  private Waypoint[] myPathThroughLife;
  public int coinWorth = 0;
  public float health = 100;
  public float speed = .25f;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop = false;
  private float healthPerUnit;
    public Game reference;
    public Transform healthBar;
    public UnityEvent DeathEvent;

    void Start()
  {
    healthPerUnit = 100f / health;
    
    myPathThroughLife = route.path;
    transform.position = myPathThroughLife[index].transform.position;
    Recalculate();
  }

  void Update()
  {

    if (!stop)
    {
      if ((transform.position - myPathThroughLife[index + 1].transform.position).magnitude < .1f)
      {
        index = index + 1;
        Recalculate();
      }


      Vector3 moveThisFrame = nextWaypoint * Time.deltaTime * speed;
      transform.Translate(moveThisFrame);
    }

  }

  void Recalculate()
  {
        if (index < myPathThroughLife.Length - 1)
        {
            nextWaypoint = (myPathThroughLife[index + 1].transform.position - myPathThroughLife[index].transform.position).normalized;

        }
        else if (index == myPathThroughLife.Length - 1)
        {
            GameObject.Find("Tester").GetComponent<Game>().lifeLoss();
            Destroy(this.gameObject);
        }
        else
        {
            stop = true;
            
        }
  }

    public void die()
    {
        Destroy(this.gameObject);
    }
    public void Damage()
    {
        Damage(20);
    }
    public void Damage(float hitAmount)
  {
        health -= hitAmount;
    if (health <= 0)
    {

      
      Debug.Log($"{transform.name} is Dead");
            GameObject.Find("Tester").GetComponent<Game>().updateScore(coinWorth);
            
            DeathEvent.Invoke();
            
            DeathEvent.RemoveAllListeners();
            GameObject.Find("Tester").GetComponent<Game>().death.Play();
            Destroy(this.gameObject);
            
        }

    float percentage = healthPerUnit * health;
    Vector3 newHealthAmount = new Vector3(percentage/100f , healthBar.localScale.y, healthBar.localScale.z);
    healthBar.localScale = newHealthAmount;
  }

}
