using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    int score = 0;
    public Text points;
    // Start is called before the first frame update
    void Start()
    {
        
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

                hit.collider.gameObject.GetComponent<Enemy>().hit();
                

            }


        }

    }

    public void updateScore(int amt)
    {
        score += amt;
        points.text = score.ToString();
    }
}
