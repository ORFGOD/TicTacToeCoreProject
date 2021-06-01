using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; //instance of GameManager class
    public GameBoard gameBoard;
    public enum turn
    {
        firstPlayerTurn,
        secondPlayerTurn
    }
    private enum gameResoult
    {
        nobodyWon,
        somebodyWon,
        deadHeat
    }
    public turn currentTurn;

    //Players marks
    private Field.StateOfField firstPlayerMarker;
    private Field.StateOfField secondPlayerMarker;

    //currentTurnUIRepresentation
    public UnityEngine.UI.Image crossCurrentTurnRepresentation;
    public UnityEngine.UI.Image circleCurrentTurnRepresentation;

    public void Awake()
    {
        //Checking if is only one instance of class
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            //setting up default parametrs
            this.firstPlayerMarker = Field.StateOfField.CROSS;
            this.secondPlayerMarker = Field.StateOfField.CIRCLE;
            this.currentTurn = turn.firstPlayerTurn;
            this.crossCurrentTurnRepresentation.enabled = true;
            this.circleCurrentTurnRepresentation.enabled = false;
        }   
        else
        {
            Destroy(this);
        }
    }

    public void RestartGame()
    {
        //Setting up all field as empty
        this.gameBoard.LeftTop.State = Field.StateOfField.EMPTY;
        this.gameBoard.MiddleTop.State = Field.StateOfField.EMPTY;
        this.gameBoard.RightTop.State = Field.StateOfField.EMPTY;
        this.gameBoard.LeftMiddle.State = Field.StateOfField.EMPTY;
        this.gameBoard.Center.State = Field.StateOfField.EMPTY;
        this.gameBoard.RightMiddle.State = Field.StateOfField.EMPTY;
        this.gameBoard.LeftDown.State = Field.StateOfField.EMPTY;
        this.gameBoard.MiddleDown.State = Field.StateOfField.EMPTY;
        this.gameBoard.RightDown.State = Field.StateOfField.EMPTY;
    }

    //This method checking if someone won game
    private gameResoult someoneWon()
    {
        //Horizontal
        if (gameBoard.LeftTop.State == gameBoard.MiddleTop.State &&
           gameBoard.LeftTop.State == gameBoard.RightTop.State &&
           gameBoard.MiddleTop.State == gameBoard.RightTop.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
              return gameResoult.somebodyWon;

        if (gameBoard.LeftMiddle.State == gameBoard.Center.State &&
            gameBoard.LeftMiddle.State == gameBoard.RightMiddle.State &&
            gameBoard.Center.State == gameBoard.RightMiddle.State &&
           gameBoard.LeftMiddle.State != Field.StateOfField.EMPTY)
              return gameResoult.somebodyWon;

        if (gameBoard.LeftDown.State == gameBoard.MiddleDown.State &&
           gameBoard.LeftDown.State == gameBoard.RightDown.State &&
           gameBoard.MiddleDown.State == gameBoard.RightDown.State &&
           gameBoard.LeftDown.State != Field.StateOfField.EMPTY)
              return gameResoult.somebodyWon;
        //Vertical
        if (gameBoard.LeftTop.State == gameBoard.LeftMiddle.State &&
           gameBoard.LeftTop.State == gameBoard.LeftDown.State &&
           gameBoard.LeftMiddle.State == gameBoard.LeftDown.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.MiddleTop.State == gameBoard.Center.State &&
           gameBoard.MiddleTop.State == gameBoard.MiddleDown.State &&
           gameBoard.Center.State == gameBoard.MiddleDown.State &&
           gameBoard.MiddleTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.RightTop.State == gameBoard.RightMiddle.State &&
           gameBoard.RightTop.State == gameBoard.RightDown.State &&
           gameBoard.RightMiddle.State == gameBoard.RightDown.State &&
           gameBoard.RightTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;
        //Cross
        if (gameBoard.LeftTop.State == gameBoard.Center.State &&
           gameBoard.LeftTop.State == gameBoard.RightDown.State &&
           gameBoard.Center.State == gameBoard.RightDown.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.RightTop.State == gameBoard.Center.State &&
           gameBoard.RightTop.State == gameBoard.LeftDown.State &&
           gameBoard.Center.State == gameBoard.LeftDown.State &&
           gameBoard.RightTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        //Checking if is dead-heat
        if (gameBoard.LeftTop.State != Field.StateOfField.EMPTY &&
            gameBoard.MiddleTop.State != Field.StateOfField.EMPTY &&
            gameBoard.RightTop.State != Field.StateOfField.EMPTY &&
            gameBoard.LeftMiddle.State != Field.StateOfField.EMPTY &&
            gameBoard.Center.State != Field.StateOfField.EMPTY &&
            gameBoard.RightMiddle.State != Field.StateOfField.EMPTY &&
            gameBoard.LeftDown.State != Field.StateOfField.EMPTY &&
            gameBoard.MiddleDown.State != Field.StateOfField.EMPTY &&
            gameBoard.RightDown.State != Field.StateOfField.EMPTY)
            return gameResoult.deadHeat;

        return gameResoult.nobodyWon;
    }
    //Called when field was clicked
    public void FieldWasClicked(Field field)
    {
        //Checking if state isn't empty
        if (field.State != Field.StateOfField.EMPTY) return;
        //Setting up field state based on current player turn
        field.State = this.currentTurn == turn.firstPlayerTurn ? firstPlayerMarker : secondPlayerMarker;
        //Checking if someone won
        gameResoult currentGameReoult = someoneWon();
        //End when somebody won or is dead-heat
        if (currentGameReoult != gameResoult.nobodyWon) endGame(currentGameReoult);

        if (currentTurn == turn.firstPlayerTurn)
        {
            this.currentTurn = turn.secondPlayerTurn;
            this.crossCurrentTurnRepresentation.enabled = false;
            this.circleCurrentTurnRepresentation.enabled = true;
        }
        else
        {
            this.currentTurn = turn.firstPlayerTurn;
            this.crossCurrentTurnRepresentation.enabled = true;
            this.circleCurrentTurnRepresentation.enabled = false;
        }
    }

    private void endGame(gameResoult currentGameReoult)
    {
        Debug.Log("endGame");
        if(currentGameReoult == gameResoult.somebodyWon)
        {
            Debug.Log("somebodyWon");
            string winnerName = currentTurn == turn.firstPlayerTurn ? "First Player" : "Second Player";
            StatementsManager.Instance.ShowStatement(winnerName + " won", "Restart Game", this.RestartGame);
        }
        else if(currentGameReoult == gameResoult.deadHeat)
        {
            Debug.Log("dead-heat");
            StatementsManager.Instance.ShowStatement("Dead-heat! Nobody won!", "Restart Game", this.RestartGame);
        }
        
    }
}
