using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    [SerializeField] private Button item;

    void Start()
    {
        item.onClick.AddListener(startAgain);
    }
    public void startAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
