using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink_Dispencer : MonoBehaviour
{
    public ParticleSystem steam; 
    public string name;
    public Color drinkColor;
    GameObject cupHolder;
    public AudioSource steamSFX; 
    
    public GameObject gameMaster;
    public bool pourDrink = true;
    private void Start() {
        cupHolder = GameObject.FindGameObjectWithTag("Cup holder");
        stream.GetComponent<SpriteRenderer>().color = drinkColor;
        
        steam.Pause(); 
    }
    private void Update() {
        if (pourDrink) {
            PourDrink();

        } else {

            if (stream.transform.localScale.y > 0) {
               
                stream.transform.localScale = new Vector3(stream.transform.localScale.x, stream.transform.localScale.y - 5 * Time.deltaTime, stream.transform.localScale.z);
            }
        }
    }

    public float pourSpeed = 1f, maxStream = 0.56f; 
    void PourDrink() {

        GameObject child = heldDrink.GetComponent<HoldMyChild>().child;
        if (child.transform.localScale.y < 1) {
            
            child.transform.localScale = new Vector3(1, child.transform.localScale.y + pourSpeed * Time.deltaTime, 1);
            if (stream.transform.localScale.y < maxStream) {
                stream.transform.localScale = new Vector3(stream.transform.localScale.x, stream.transform.localScale.y + 5 * Time.deltaTime, stream.transform.localScale.z);
            }
        } else {
             
        }


    } 
    public GameObject newDrink;
    GameObject heldDrink;
    public GameObject buttion, stream;
    
    Color buttionColorOG = Color.white; 
    private void OnMouseDown() {
        steam.Play();
        steamSFX.Play();
        if (cupFill) {
            int orderInLayer = GameMaster.drinkPos;
            heldDrink = Instantiate(newDrink, cupHolder.GetComponent<CupHolder_movement>().newDrinkTrans.position, Quaternion.identity);
            heldDrink.transform.SetParent(cupHolder.transform);
            heldDrink.GetComponent<SpriteRenderer>().color = drinkColor;
            heldDrink.GetComponent<SpriteRenderer>().sortingOrder = GameMaster.drinkPos;
            heldDrink.GetComponent<HoldMyChild>().child.GetComponent<SpriteMask>().frontSortingOrder = orderInLayer;
            heldDrink.GetComponent<HoldMyChild>().child.GetComponent<SpriteMask>().backSortingOrder = 0;
            gameMaster.GetComponent<GameMaster>().AddedItem(name);
            GameMaster.drinkPos--;
            buttion.GetComponent<SpriteRenderer>().color = Color.green; 
            pourDrink = true; 
        }
    }
    private void OnMouseUp() {
        steam.Stop();
        steamSFX.Stop(); 
        pourDrink = false;
        cupFill = false;
        buttion.GetComponent<SpriteRenderer>().color = Color.white;
        heldDrink = null;
    }

    bool cupFill; 
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Cup holder") {
            if (cupHolder.GetComponent<CupHolder_movement>().cup == true) {
                cupFill = true; 
            }


        }
    }
    private void OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.tag == "Cup holder") {
            if (cupHolder.GetComponent<CupHolder_movement>().cup == true) {
                cupFill = false;
            }


        }
    }
}
