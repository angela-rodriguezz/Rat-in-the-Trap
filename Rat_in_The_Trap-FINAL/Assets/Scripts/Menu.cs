using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;
using System.Threading;


public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject needles;
    private RectTransform transformer;
    private RectTransform curr;
    private Vector2 user;
    private Button turner;
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        transformer = needles.GetComponent<RectTransform>();
        curr = GetComponent<RectTransform>();
        turner = GetComponent<Button>();
        turner.onClick.AddListener(menuTagger);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressed M");
            MenuShow();
        }
    }

    public void MenuShow()
    {
        panel.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaceArrow(curr);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }


    public void PlaceArrow(RectTransform t)
    {
        user = t.position;
        //user.y -= t.rect.height / 62f;
        transformer.position = user;
    }

    public void menuTagger()
    {
        if (turner.tag == "Resume")
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
        } else if (turner.tag == "Restart")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        } else
        {
            Application.Quit();
        }
    }

    
        
}
