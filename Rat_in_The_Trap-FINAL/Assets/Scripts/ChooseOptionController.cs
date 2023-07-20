using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseOptionController : MonoBehaviour
{
    [SerializeField]
    private SelectionScreen controller;
    [SerializeField]
    private Button btn1;
    [SerializeField]
    private Button btn2;
    [SerializeField]
    private Button btn3;
    

    

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
