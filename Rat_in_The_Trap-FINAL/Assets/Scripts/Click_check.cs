using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Click_check : MonoBehaviour
{
    public bool left = true;
    public bool right = true;
    public int total = 0;
    public float time_left = 10;
    public GameObject mouse; 
    private Vector2 original_pos;

    void Start() 
    {
        original_pos = mouse.transform.position;
        mouse.transform.position = new Vector2(4, mouse.transform.position.y); 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mouse.transform.position.x);
        if (mouse.transform.position.x >= 3.75) {
            mouse.transform.position = new Vector2(3.75f, mouse.transform.position.y);
        }

        if (time_left > 0)
        {
            time_left -= Time.deltaTime;
            mouse.transform.position = new Vector2(mouse.transform.position.x - 0.005f, mouse.transform.position.y); 
            
            if (mouse.transform.position.x < -3)
            {
                Debug.Log("Failed");
                SceneManager.LoadScene(5);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && left)
            {
                total++;
                left = false;
                right = true;
                mouse.transform.position = new Vector2((total/time_left)/5, mouse.transform.position.y); 
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && right)
            {
                total++;
                left = true;
                right = false;
                mouse.transform.position = new Vector2((total/time_left)/5, mouse.transform.position.y); 
            }
            else if (Input.anyKeyDown)
            {
                Debug.Log("Failed");
                SceneManager.LoadScene(5);
                time_left = 0;
            }
        }
        else
        {
            if (total >= 30)
            {
                Debug.Log("Win");
                Health.health = 1;
                SceneManager.LoadScene(7);
            }
            else
            {
                Debug.Log("Failed");
                SceneManager.LoadScene(5);
            }
        }
    }
}
