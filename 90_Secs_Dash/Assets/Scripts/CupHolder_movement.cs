using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupHolder_movement : MonoBehaviour
{
    // use x and y to limit the movement of the cup holder 
    public Vector2 posLimits;
    // speed for the cup to move
    public float speed = 5f;
    public Transform newDrinkTrans; 
    public bool cup = false; 
    private void Update() {

        // get the left and right movement on the horizontal axis
        float horz = Input.GetAxis("Horizontal");
   
        // move the object using speed and horz value
        transform.Translate(Vector2.right * horz * speed * Time.deltaTime);
        // enforce the movement limitation
        if (transform.position.x < posLimits.x) {
            transform.position = new Vector3(posLimits.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > posLimits.y) {
            transform.position = new Vector3(posLimits.y, transform.position.y, transform.position.z);
        } if (!cup) {
            myChildGlass.SetActive(false);
        }
    }

    public GameObject myChildGlass; 
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Glass") {

            myChildGlass.SetActive(true);
            cup = true;
            Destroy(other.gameObject); 
        }
    }


}
