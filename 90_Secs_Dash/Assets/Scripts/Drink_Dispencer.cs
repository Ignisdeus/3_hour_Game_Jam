using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink_Dispencer : MonoBehaviour
{
    public ParticleSystem steam; //steam partical system
    public string name; // name of this drink 
    public Color drinkColor; // colour of this drink 
    GameObject cupHolder; // cup holder game object 
    public AudioSource steamSFX;// audio clip for the steam
    float drinkLevel = 0; // level to display sprite in layers
    
    
    public GameObject gameMaster; // the game master for this level 
    public bool pourDrink = true; // if i can pour the drink 
    private void Start(){
        cupHolder = GameObject.FindGameObjectWithTag("Cup holder"); // find and store the cupholder refrence 
        stream.GetComponent<SpriteRenderer>().color = drinkColor; // set the colour of the drink on the sprite 
        steam.Pause();// stop the steam partical effect  
    }
    private void Update() {

        
        // if pour buttion is pressed run the pour drink medhod
        if (pourDrink) {

            PourDrink();

        } else {

            // when the buttion is not pressed return the stream back to its starting point
            if (stream.transform.localScale.y > 0) {
                stream.transform.localScale = new Vector3
                    (stream.transform.localScale.x, stream.transform.localScale.y - 5 * Time.deltaTime, stream.transform.localScale.z);
            }
            // to ensure that the stream does not go over the desired area
            if (stream.transform.localScale.y < 0) {
                stream.transform.localScale = new Vector3
                    (stream.transform.localScale.x, 0, stream.transform.localScale.z);
            }
        }
    }
    // speed to pour drink and max stream size 
    public float pourSpeed = 1f, maxStream = 0.56f; 
    void PourDrink() { // pour the drink 

        GameObject child = heldDrink.GetComponent<HoldMyChild>().child;
        if (drinkLevel < 1) {
            float addRate = pourSpeed * Time.deltaTime; 
            // change the hight of the child object by add rate 
            child.transform.localScale = new Vector3(1, child.transform.localScale.y + addRate, 1);
            // store the level high of the drink on the cup holder 
            cupHolder.GetComponent<CupHolder_movement>().drinkLevelConstant = child.transform.localScale.y; 
            if (stream.transform.localScale.y < maxStream) { // if the stream is less than max steam high 
                // make the stream longer by 5 and time as a scaler. 
                stream.transform.localScale = new Vector3(stream.transform.localScale.x, stream.transform.localScale.y + 5 * Time.deltaTime, stream.transform.localScale.z);
            }
        }


    } 
    public GameObject newDrink;
    GameObject heldDrink;
    public GameObject buttion, stream;
    
    Color buttionColorOG = Color.white; 
    private void OnMouseDown() {
        steam.Play();
        steamSFX.Play();

        if (cupFill && heldDrink == null) {
            // find the possision that the sprite layer need to be on
            int orderInLayer = GameMaster.drinkPos;
            // create new drink liquid
            heldDrink = Instantiate(newDrink, cupHolder.GetComponent<CupHolder_movement>().newDrinkTrans.position, Quaternion.identity);
            // sets parent to the cupholder gameObject
            heldDrink.transform.SetParent(cupHolder.transform);
            //sets color of the drink 
            heldDrink.GetComponent<SpriteRenderer>().color = drinkColor;
            //sets the sorting order 
            heldDrink.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            //sets sprite mask effect area 
            heldDrink.GetComponent<HoldMyChild>().child.GetComponent<SpriteMask>().frontSortingOrder = orderInLayer;
            heldDrink.GetComponent<HoldMyChild>().child.GetComponent<SpriteMask>().backSortingOrder = 0;
            float x = cupHolder.GetComponent<CupHolder_movement>().drinkLevelConstant;
            heldDrink.GetComponent<HoldMyChild>().child.transform.localScale = new Vector3(1, x, 1);
            //sends the drink name to the game master for checking the score later
            gameMaster.GetComponent<GameMaster>().AddedItem(name);
            // updates the posision for the next Drink
            GameMaster.drinkPos--;
            buttion.GetComponent<SpriteRenderer>().color = Color.green;

        } 
        if(heldDrink != null){

            gameMaster.GetComponent<GameMaster>().AddedItem(name);
            pourDrink = true;

        }
        
    }
    private void OnMouseUp() {
        steam.Stop();
        steamSFX.Stop(); 
        buttion.GetComponent<SpriteRenderer>().color = Color.white;
        pourDrink = false;
        //heldDrink = null;
    }

    bool cupFill; 
    // if the cupholder enters this trigger I can pour my drink 
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Cup holder") { 
            if (cupHolder.GetComponent<CupHolder_movement>().cup == true) {
                cupFill = true; 
            }


        }
    }
    // if the cup holder leaves this area I can no longer pour my drink. 
    private void OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.tag == "Cup holder") {
            if (cupHolder.GetComponent<CupHolder_movement>().cup == true) {
                
                cupFill = false;
                heldDrink = null;
            }


        }
    }
}
