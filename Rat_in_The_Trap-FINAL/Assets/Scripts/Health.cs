using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public static int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 3) 
        {
            health1.SetActive(true);
            health2.SetActive(true);
            health3.SetActive(true);
        }

        else if (health == 2)
        {
            health1.SetActive(true);
            health2.SetActive(true);
            health3.SetActive(false);
        }

        else if (health == 1) 
        {
            health1.SetActive(true);
            health2.SetActive(false);
            health3.SetActive(false);
        }

        else 
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
        }
    }
}
