using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public int coins = 0;
    public Text points;
    public float timer = 0f;
    public Text subtract;
    public int lives = 3;
    public bool active = true;


    public Image heart1;
    public Image heart2;
    public Image heart3;
    public GameObject game;
    public GameObject start;
    public GameObject uiStart;
    public GameObject uiGame;
    public GameObject uiGameOver;

    public AudioSource death;

    // Start is called before the first frame update
    void Start()
    {

        subtract.enabled = false;
        game.SetActive(false);
        uiStart.SetActive(false);

        active = false;
    }

    // Update is called once per frame

    public void clickStart()
    {
        game.SetActive(true);
        start.SetActive(false);
        uiStart.SetActive(true);
        active = true;
        lives = 3;
        heart1.enabled = true;
        heart2.enabled = true;
        heart3.enabled = true;
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
              Camera.main.transform.position.z));

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<Enemy>().Damage();
                }
            }

        }
        
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            subtract.enabled = true;
        }

        if(timer < 0)
        {
            subtract.enabled = false;
            timer = 0;
        }
    }

    public void updateScore(int amt)
    {
        coins += amt;
        points.text = coins.ToString();
    }

    public void lifeLoss()
    {
        lives--;
        if (lives == 2)
            heart1.enabled = false;
        else if (lives == 1)
            heart2.enabled = false;
        else
        {
            heart3.enabled = false;
            gameOver();
        }

    }

    public void gameOver()
    {
        game.SetActive(false);
        uiGame.SetActive(false);
        uiGameOver.SetActive(true);
        GameObject.Find("SmallBadGuy(Clone)").GetComponent<Enemy>().die();
        GameObject.Find("BigBadGuy(Clone)").GetComponent<Enemy>().die();
    }
}
