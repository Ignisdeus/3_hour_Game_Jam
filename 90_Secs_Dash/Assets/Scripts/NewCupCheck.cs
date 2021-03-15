using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCupCheck : MonoBehaviour
{
    // where to drop new cups from
    public Transform cupDropPoint; 

    // new cup game object 
    public GameObject newCup; 
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Cup holder") {
            GameObject holder = other.gameObject; // store the other game object refrence 
            if (holder.GetComponent<CupHolder_movement>().cup == false) { // if the other object does not have a cup active 
                Instantiate(newCup, cupDropPoint.transform.position, Quaternion.identity); // drop a new cup onto the other game object. 

            }
        }
    }
}
