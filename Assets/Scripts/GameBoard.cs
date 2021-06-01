using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

    public static GameBoard Instance; //instance of GameManager class
    //Fields
    public Field LeftTop, MiddleTop, RightTop, LeftMiddle, Center, RightMiddle, LeftDown, MiddleDown, RightDown; 
    
    public void Awake()
    {
        //Checking if is only one instance of class
        if (GameBoard.Instance == null)
            GameBoard.Instance = this;
        else
            Destroy(this);
    }

}
