using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    public ChooseOptionController label; 
    public PlayDialogue gameController; // used to play the next scene that corresponds to the choice the player made
    private RectTransform rectTransform; // used to alter the positions of the timer
    public Animator animator; // animator to show or hide the button options
    public Button btn1; // button one choice
    public Button btn2; // button two choice
    public Button btn3; // button three choice
    public TextMeshProUGUI buttonChoice1; // text for button 1
    public TextMeshProUGUI buttonChoice2; // text for button 2
    public TextMeshProUGUI buttonChoice3; // text for button 3
    private ChooseScene main; // current choice scene

    // Timer Variables
    public TextMeshProUGUI timerText;
    private float timer = 0.0f;
    private bool activeTimer = false;
    public float shakeAmount = 0.075f; // This is the amount of text shake you want to apply
    public float shakeSpeed = 50f; // This is the speed of the text shake
    private Vector3 timerOriginalPosition; // This will store the original position of the Text element

    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        timerOriginalPosition = timerText.transform.position;
    }

    void Update() 
    {
        if (activeTimer)
        {
            // If timer is active
            timerText.gameObject.SetActive(activeTimer);
            timerText.text = (15.0f - Mathf.Round(timer * 100.0f) / 100.0f).ToString();

            float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount; // Calculate the shake amount using a sine wave
            timerText.transform.position = timerOriginalPosition + new Vector3(shake, 0f, 0f); // Apply the shake to the Text element's position
        }

        // If the button is active 
        if (timer >= 15.0f && btn1.onClick.GetPersistentEventCount() > 0 && btn2.onClick.GetPersistentEventCount() > 0 && btn3.onClick.GetPersistentEventCount() > 0) 
        {
            PerformChoice(2);
        }

    }

    // adds to the value of the timer if active
    void FixedUpdate() 
    {
        if (activeTimer) 
        {
            timer += Time.deltaTime;
        }
    }

    // Shows the scene choice options
    // sets the main to the current scene
    // changes the text of the buttons to each of the options
    // starts the timer
    public void SetupChoose(ChooseScene scene)
    {
        activeTimer = true;
        StartCoroutine(EnterLoad());
        animator.SetTrigger("Show");
        main = scene;
        buttonChoice1.text = scene.labels[0].text;
        buttonChoice2.text = scene.labels[1].text;
        buttonChoice3.text = scene.labels[2].text;
        timer = 0.0f;
    }
    // Plays the scene dialogue that occurs after making the choice
    // hides the previous choices
    // hides the timer
    public void PerformChoice(int num)
    {
        activeTimer = false;
        timerText.gameObject.SetActive(activeTimer);
        gameController.PlayScene(main.labels[num].nextScene);
        StartCoroutine(EnterLoad());
        animator.SetTrigger("Hide");
        timer = 0.0f;
    }

    private IEnumerator EnterLoad()
    {
        yield return new WaitForSeconds(60f);
    }
}
