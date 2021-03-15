using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameMaster : MonoBehaviour
{
    public ParticleSystem stars; // stars partical system 
    public static int drinkPos = 99; // layering order for the sprite hiding componenet 
    public float timer = 90f; // timer of lenght of the game 

    AudioSource audioSFX;// audio source 
    // clip one add score
    public AudioClip clips; 

    public Text request; // request text 
    private void Start() {
        NewRequest(); // get a new order request 
        audioSFX = gameObject.AddComponent<AudioSource>(); // create and add new audio Source to the refrence and game object  
    }

    public Text timerDisplay; // display for the timer 
    private void Update() {
        // display request 
        request.text = "I would like some " + currentRequest[0] + " , " + currentRequest[1] + " , " + currentRequest[2] + " Please and thank you";

        timer -= Time.deltaTime; // timer ticks down 
        float timerToFloor = Mathf.RoundToInt(timer); // display timer rounded to int
        timerDisplay.text = timerToFloor.ToString(); // set timerDisplay text to timer rounded down value 

        if (timer <= 0) { // if timer == 0
            TimeUP();// trigger time up 
        } 
    }
    string[] currentRequest = { "", "","" }; // store requests 


    string[] possibleRequests = {"Milk", "Coffee", "Chocolate", "Tea" }; // possible drinks 
    void NewRequest() { // create new request 

        float value = Random.Range(0f,1f);
        if (value < 1) { // sets requests 
            currentRequest[0] = possibleRequests[Random.Range(0, possibleRequests.Length)];
        }
        if (value < 0.8f) {
            currentRequest[1] = possibleRequests[Random.Range(0, possibleRequests.Length)];
        }
        if (value < 0.4f) {
            currentRequest[2] = possibleRequests[Random.Range(0, possibleRequests.Length)];
        }

    }

    string[] list = new string[3]; // create new list of strings 
    int pos = 0;

    public void AddedItem(string name) { // add new item to the list 
        if (pos < 3) {
            list[pos] = name;
            pos++;
        }

    }

    int score = 0; // score value 
    public Text scoreT, FinalScore; // score text values  
    void CheckScore(float x) { // * x to score 
        audioSFX.PlayOneShot(clips, 1f); // play add score audio 
        for (int i = 0; i < list.Length; i++) { // for every correct item add score 

            if (list[i] == currentRequest[i]) { // check is is correct 
                score += Mathf.CeilToInt(100 * x); // if it is correct add to score 
            }
            
        }
        scoreT.text = score.ToString() ; // update the displayed score 
         
        NewRequest(); // get a new requests 
        ResetAll(); // reset all values 
    }
    public GameObject cupHolder; // refrence to cup holder 
    private void ResetAll() { // reset all display values 
        stars.Clear(); // clear stars particals 
        stars.Play(); // play the stars particals 
        for (int i = 0; i < list.Length; i++) { // reset list to empty 

            list[i] = " ";
            

        }
        pos = 0; // reset pos
        drinkPos = 99; // reset drink pos

        GameObject[] fillings = GameObject.FindGameObjectsWithTag("Filling");
        foreach (GameObject g in fillings) {
            Destroy(g); 
        }
        cupHolder.GetComponent<CupHolder_movement>().cup = false; 
    }

    public GameObject FinalCanvis, oldCanvis; 
    void TimeUP() {
        oldCanvis.SetActive(false); // set canvis to false 
        FinalCanvis.SetActive(true);// set game over canvis to active 
        FinalScore.text = score.ToString(); // update and display final score 


    } 

    
    private void OnTriggerEnter2D(Collider2D other) { // if i collide with another game object 

        if (other.gameObject.tag == "Cup holder") { // if that object is called cup holder 
            
            CheckScore(other.GetComponent<CupHolder_movement>().drinkLevelConstant); // check the components for score 
            other.GetComponent<CupHolder_movement>().drinkLevelConstant = 0; // set hight of contents to 0 

        }

     }

    public string levelToLoad; // store level to load 
    public void GameReset() { // reset the game 

        SceneManager.LoadScene(levelToLoad); // load selected level. 
    }


}
