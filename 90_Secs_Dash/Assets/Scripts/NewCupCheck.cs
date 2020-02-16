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
            Debug.Log("Working");
            GameObject holder = other.gameObject;


            if (holder.GetComponent<CupHolder_movement>().cup == false) {
                Instantiate(newCup, cupDropPoint.transform.position, Quaternion.identity);

            }


        }
        
    }
}
