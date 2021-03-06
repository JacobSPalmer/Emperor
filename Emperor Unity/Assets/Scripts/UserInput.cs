using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInput : MonoBehaviour
{
    public Text scorePanelObj;
    public int foundationPoints;
    private Emperor emperor;
    public GameObject selectedCard;
    public int movesTaken;


    // Start is called before the first frame update
    void Start()
    {
        foundationPoints = 0;
        emperor = FindObjectOfType<Emperor>();
        selectedCard = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //TODO Utilize switch to remove if statement redundancy
            if(hit)
            {
                if(hit.collider.CompareTag("Deck"))
                {
                    Deck();
                }
                else if(hit.collider.CompareTag("Card"))
                {
                    Card(hit.collider.gameObject);
                }
                else if(hit.collider.CompareTag("Foundation"))
                {
                    Foundation(hit.collider.gameObject);
                }
                else if(hit.collider.CompareTag("Tableau"))
                {
                    Tableau(hit.collider.gameObject);
                }
                else if(hit.collider.CompareTag("Waste"))
                {
                    Waste();
                }
            }
        }
    }

    void Deck()
    {
        print("Hit Deck");
        emperor.DrawDeck();
        selectedCard = this.gameObject;
    }

    void Tableau(GameObject newSelected)
    {
        print("Hit Tableau");
        if(selectedCard.CompareTag("Card") && emperor.bottomPos[newSelected.GetComponent<Selectable>().posRow].transform.childCount == 0)
        {
            Move(newSelected);
        }
    }

    void Foundation(GameObject newSelected)
    {
        print("Hit Foundation");
        if(selectedCard.CompareTag("Card") && selectedCard.GetComponent<Selectable>().rank == 1)
        {
            Move(newSelected);
        }
    }

    //TODO Find a better solution for selecting
    void Card(GameObject newSelected)
    {
        print("Hit Card");
        print("Card Buried: " + Buried(newSelected));
        if(!newSelected.GetComponent<Selectable>().faceUp && !Buried(newSelected))
        {
            newSelected.GetComponent<Selectable>().faceUp = true;
            selectedCard = this.gameObject;
        }
        else if (newSelected.GetComponent<Selectable>().faceUp && !Buried(newSelected))
        {
            if (selectedCard == this.gameObject)
            {
                selectedCard = newSelected;
            }
            else if (selectedCard != newSelected)
            {
                if (isValidMove(newSelected))
                {
                    Move(newSelected);
                }
                else
                {
                    selectedCard = newSelected;
                }
            }
        }
    }

    void Waste()
    {
        print("Hit Waste");
    }

    //checks the currently selected card to a second selected card to determine if the movement is a valid move given the Emperor rule parameters
    bool isValidMove(GameObject selected)
    {
        Selectable origin = selectedCard.GetComponent<Selectable>();
        Selectable destination = selected.GetComponent<Selectable>();

        //TODO add switch statements here in place of if statements

        //if the destination card (newly selected card) is on the foundation pile, check to see if the card is the same suit as the origin card
        //or if the origin card (the current 'blue' selected card) is an ace (rank value of 1)
        if(destination.onFoundation)
        {
            if(origin.suit == destination.suit || (origin.rank == 1))
            {
                if(origin.rank == destination.rank + 1)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        //if the destination is in waste, always return false as you can never move a card to the waste pile (pile next to the deck)
        else if(destination.inWaste)
        {
            return false;
        }
        //primary game rule for moving in tableau: if the destination card is of a different color and has a rank one higher than the origin card, then return true
        else
        {
            if(origin.rank == destination.rank + 1 && origin.color != destination.color)
            {
                    print("Can Stack");
                    return true;
            }
            else{
                print("Not stackable");
                return false;
            }
        }
        return false;
    }
    void Move(GameObject newSelected)
    {
        movesTaken += 1;
        Selectable origin = selectedCard.GetComponent<Selectable>();
        Selectable destination = newSelected.GetComponent<Selectable>();

        float yOffset = 0.3f;

        //if the card is on the foundation or the card is a king, do not offset the card from the position object
        if(destination.onFoundation || (!destination.onFoundation && (emperor.bottomPos[destination.posRow].transform.childCount == 0)))
        {
            yOffset = 0;
        }

        //move the origin card to the destination cards location and offset by .3f; add the origin card to the parent object of the destination card
        selectedCard.transform.position = new Vector3(newSelected.transform.position.x, newSelected.transform.position.y - yOffset, newSelected.transform.position.z - 0.02f);
        //if the destination is a card, move to the cards parent
        if(destination.CompareTag("Card"))
        {
            selectedCard.transform.parent = newSelected.transform.parent;
        }
        //if the destination is a position, make it a child of that position gameobject
        else{
            selectedCard.transform.parent = newSelected.transform;
        }
        //if origin is in the waste pile, remove from the waste pile
        if(origin.inWaste)
        {
            emperor.waste.Remove(selectedCard.name);
        }
        //if the origin is on the foundation, reset the object to have a rank - 1 (for bookkeeping)
        else if(origin.onFoundation)
        {
            emperor.topPos[origin.posRow].GetComponent<Selectable>().rank = origin.rank - 1;
            determineFoundationPoint();
        }
        else
        {
            // emperor.tableaus[destination.posRow].Add(selectedCard.name);
            emperor.tableaus[origin.posRow].Remove(selectedCard.name);
        }

        origin.inWaste = false;
        origin.posRow = destination.posRow;

        if(destination.onFoundation)
        {
            emperor.topPos[origin.posRow].GetComponent<Selectable>().suit = origin.suit;
            emperor.topPos[origin.posRow].GetComponent<Selectable>().rank = origin.rank;
            origin.onFoundation = true;
            determineFoundationPoint();
            //If foundation is complete, activate win game overlay
            if(isCompleteFoundation())
            {
                SceneManager.LoadScene("EndGameScene", LoadSceneMode.Single);
            }
        }
        else
        {
            origin.onFoundation = false;
        }

        selectedCard = this.gameObject;
    }

    bool Buried(GameObject obj)
    {
        Selectable card = obj.GetComponent<Selectable>();
        if(card.inWaste || card.onFoundation){
            return false;
        }
        // else if(card.name == emperor.bottomPos[card.posRow].){
        //     return false;
        // }
        else if (!card.inWaste && !card.onFoundation)
        {
            int lastChildIndex = emperor.bottomPos[card.posRow].transform.childCount - 1;
            if(obj.name == emperor.bottomPos[card.posRow].transform.GetChild(lastChildIndex).gameObject.name)
            {
                return false;
            }
        }
        
        return true;

    }

    bool isCompleteFoundation(){
        bool isComplete = true;
        foreach(GameObject foundation in emperor.topPos){
           if(foundation.transform.childCount == 13)
           {
               continue;
           }
           else
           {
              isComplete = false;
              break;
           }
        }
        return isComplete;
    }

    private void determineFoundationPoint()
    {
        int score = 0;
        foreach(GameObject foundation in emperor.topPos)
        {
            int n = foundation.transform.childCount;
            // print(n);
            for(int i = 1; i <= n; i++)
            {
                score += i * 10;
            }
        }
        foundationPoints = score;
    }

    private void updateScore()
    {
        scorePanelObj.text = foundationPoints.ToString();
    }

    void OnDisable()
    {
        print(movesTaken + " " + foundationPoints);
        PlayerPrefs.SetInt("moves", movesTaken);
        PlayerPrefs.SetInt("score", foundationPoints);
    }
}
