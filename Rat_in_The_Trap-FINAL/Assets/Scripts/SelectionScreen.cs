using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    public ChooseOptionController label;
    public PlayDialogue gameController;
    private RectTransform rectTransform;
    private Animator animator;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public TextMeshProUGUI buttonChoice1;
    public TextMeshProUGUI buttonChoice2;
    public TextMeshProUGUI buttonChoice3;
    private ChooseScene main;

    // Timer Variables
    public TextMeshProUGUI timerText;
    private float timer = 0.0f;
    private bool activeTimer = false;
    public float shakeAmount = 0.075f; // This is the amount of text shake you want to apply
    public float shakeSpeed = 50f; // This is the speed of the text shake
    private Vector3 timerOriginalPosition; // This will store the original position of the Text element

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        timerOriginalPosition = timerText.transform.position;
    }

    void Update() 
    {
        // If timer is active
        timerText.gameObject.SetActive(activeTimer);
        timerText.text = (15.0f - Mathf.Round(timer * 100.0f) / 100.0f).ToString();

        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount; // Calculate the shake amount using a sine wave
        timerText.transform.position = timerOriginalPosition + new Vector3(shake, 0f, 0f); // Apply the shake to the Text element's position

        // If the button is active 
        if (timer >= 15.0f && btn1.onClick.GetPersistentEventCount() > 0 && btn2.onClick.GetPersistentEventCount() > 0 && btn3.onClick.GetPersistentEventCount() > 0) 
        {
            PerformChoice(2);
        }
    }

    void FixedUpdate() 
    {
        if (activeTimer) 
        {
            timer += Time.deltaTime;
        }
    }

    public void SetupChoose(ChooseScene scene)
    {   
        animator.SetTrigger("Show");
        main = scene;
        buttonChoice1.text = scene.labels[0].text;
        buttonChoice2.text = scene.labels[1].text;
        buttonChoice3.text = scene.labels[2].text;
        timer = 0.0f;
        activeTimer = true;
    }

    public void PerformChoice(int num)
    {        
        gameController.PlayScene(main.labels[num].nextScene);
        animator.SetTrigger("Hide");
        timer = 0.0f;
        activeTimer = false;
    }
}
