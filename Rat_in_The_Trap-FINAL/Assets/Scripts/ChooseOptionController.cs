using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseOptionController : MonoBehaviour
{
    public Color defaultColor;
    public Color hoverColor;
    private Scenes scene;
    private TextMeshProUGUI textMesh;
    public SelectionScreen controller;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.color = defaultColor;
    }

    void Start()
    { 
        btn1.onClick.AddListener(DecisionOne);
        btn2.onClick.AddListener(DecisionTwo);
        btn3.onClick.AddListener(DecisionThree);  
    }

    public void DecisionOne()
    {
        controller.PerformChoice(0);  
    }

    public void DecisionTwo()
    {
        controller.PerformChoice(1);
    }

    public void DecisionThree()
    {
        controller.PerformChoice(2);
    }
}
