using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameMaster : MonoBehaviour
{
    public ParticleSystem stars; 
    public static int drinkPos = 99;
    public float timer = 90f;

    AudioSource audioSFX;
    // clip one add score
    public AudioClip clips; 

    public Text request;
    private void Start() {
        NewRequest();
        foreach (string s in currentRequest) {
            Debug.Log(s); 
        }

        audioSFX = gameObject.AddComponent<AudioSource>(); 
    }

    public Text timerDisplay; 
    private void Update() {
        request.text = "I would like some " + currentRequest[0] + " , " + currentRequest[1] + " , " + currentRequest[2] + " Please and thank you";

        timer -= Time.deltaTime;
        float timerToFloor = Mathf.RoundToInt(timer);
        timerDisplay.text = timerToFloor.ToString(); 

        if (timer <= 0) {
            TimeUP();
        } 
    }
    string[] currentRequest = { "", "","" };


    string[] possibleRequests = {"Milk", "Coffee", "Chocolate", "Tea" }; 
    void NewRequest() {

        float value = Random.Range(0f,1f);
        if (value < 1) {
            currentRequest[0] = possibleRequests[Random.Range(0, possibleRequests.Length)];
        }
        if (value < 0.8f) {
            currentRequest[1] = possibleRequests[Random.Range(0, possibleRequests.Length)];
        }
        if (value < 0.4f) {
            currentRequest[2] = possibleRequests[Random.Range(0, possibleRequests.Length)];
        }

    }

    string[] list = new string[3];
    int pos = 0;

    public void AddedItem(string name) {
        if (pos < 3) {
            list[pos] = name;
            pos++;
        }

    }

    int score = 0;
    public Text scoreT, FinalScore; 
    void CheckScore() {
        audioSFX.PlayOneShot(clips, 1f);
        for (int i = 0; i < list.Length; i++) {

            if (list[i] == currentRequest[i]) {
                score += 100; 
            }
            
        }
        scoreT.text = score.ToString() ;
         
        NewRequest();
        ResetAll(); 
    }
    public GameObject cupHolder; 
    private void ResetAll() {
        stars.Clear();
        stars.Play(); 
        for (int i = 0; i < list.Length; i++) {

            list[i] = " ";
            

        }
        pos = 0;
        drinkPos = 99;

        GameObject[] fillings = GameObject.FindGameObjectsWithTag("Filling");
        foreach (GameObject g in fillings) {
            Destroy(g); 
        }
        cupHolder.GetComponent<CupHolder_movement>().cup = false; 
    }

    public GameObject FinalCanvis, oldCanvis; 
    void TimeUP() {
        oldCanvis.SetActive(false);
        FinalCanvis.SetActive(true);
        FinalScore.text = score.ToString();


    } 

    
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Cup holder") {
            CheckScore();

        }

     }

    public string levelToLoad; 
    public void GameReset() {

        SceneManager.LoadScene(levelToLoad); 
    }


}
