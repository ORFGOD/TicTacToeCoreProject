using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatementsManager : MonoBehaviour {

    public static StatementsManager Instance; //instance of StatementsManager class

    public GameObject statementGameObject; //Gameobject who represents default statement
    public UnityEngine.UI.Text statementMessage; //Statement text - displayed when statementGameObject is active
    public UnityEngine.UI.Text statementButtonText; //Text on button - displayed when statementGameObject is active
    public UnityEngine.UI.Button statementButton; //Button - displayed when statementGameObject is active

    public void Awake()
    {
        //Checking if is only one instance of class
        if (StatementsManager.Instance == null)
            StatementsManager.Instance = this;
        else
            Destroy(this);
    }

    //Showing default statement with close button
    public void ShowStatement(string message)
    {
        this.statementMessage.text = message;
        this.statementGameObject.SetActive(true);
    }
    //Showing statement with prepared parametrs
    public void ShowStatement(string message, string buttonText, UnityEngine.Events.UnityAction methodCalledWhenButtonBeenClicked)
    {
        this.statementMessage.text = message;
        this.statementButtonText.text = buttonText;
        this.statementButton.onClick.AddListener(methodCalledWhenButtonBeenClicked);
        this.statementGameObject.SetActive(true);
    }
}
