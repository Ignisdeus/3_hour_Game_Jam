using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupHolder_movement : MonoBehaviour
{
    // use x and y to limit the movement of the cup holder 
    public Vector2 posLimits;// limites for the movement of the cup holder 
    // speed for the cup to move
    public float speed = 5f; // speed of the cup holder 
    public Transform newDrinkTrans; 
    public bool cup = false; // if im holding a cup this will be true; 
    public float drinkLevelConstant = 0; // hight of my current drink 
    private void Update() {

       
        float horz = Input.GetAxis("Horizontal"); // get the left and right movement on the horizontal axis

        
        transform.Translate(Vector2.right * horz * speed * Time.deltaTime);// move the object using speed and horz value
        // enforce the movement limitation
        if (transform.position.x < posLimits.x) {
            transform.position = new Vector3(posLimits.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > posLimits.y) {
            transform.position = new Vector3(posLimits.y, transform.position.y, transform.position.z);
        } 
        if (!cup) {
            myChildGlass.SetActive(false); // if im not holding a cup hide the cup that is a child of this object 
        }
    }

    public GameObject myChildGlass; // cup that is a child of this object refrence  
    private void OnTriggerEnter2D(Collider2D other) { // if I collide with another object 
        if (other.gameObject.tag == "Glass") { // if the other collider is tagged glass
            myChildGlass.SetActive(true); // my child cup is now visable 
            cup = true; // i can now holding a cup 
            Destroy(other.gameObject); // destroy this other object I collided with 
        }
    }


}
