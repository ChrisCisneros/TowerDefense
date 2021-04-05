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

    // Start is called before the first frame update
    void Start()
    {
        subtract.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {

                hit.collider.gameObject.GetComponent<Enemy>().Damage();
                

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
}
