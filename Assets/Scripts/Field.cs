using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerClickHandler
{
    public StateOfField State
    {
        get { return state; }
        set { ChangeFieldState(value); }
    }
    public enum StateOfField
    {
        EMPTY = 0,
        CIRCLE = 1,
        CROSS = 2
    }
    public UnityEngine.UI.Image FieldRepresentation; //Current representation of field in Game

    private Sprite crossSprite, circleSprite, emptySprite; //Different representations of field
    private StateOfField state; //Current state of field

    public void Awake()
    {
        //Load sprites from Resources folder
        crossSprite = Resources.Load<Sprite>("crossSprite");
        circleSprite = Resources.Load<Sprite>("circleSprite");
        emptySprite = Resources.Load<Sprite>("emptySprite");
    }

    private void ChangeFieldState(StateOfField newState)
    {
        this.state = newState;
        switch(newState)
        {
            case Field.StateOfField.EMPTY:
                FieldRepresentation.sprite = this.emptySprite;
                break;

            case Field.StateOfField.CIRCLE:
                FieldRepresentation.sprite = this.circleSprite;
                break;

            case Field.StateOfField.CROSS:
                FieldRepresentation.sprite = this.crossSprite;
                break;
        }
    }
    //This method get called when user click on UI Image
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.FieldWasClicked(this);
    }
}
