using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string levelToLoad; // level to load when space is pressed 
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) { // if I press the space key 
            SceneManager.LoadScene(levelToLoad); // load the selected level 
        }
    }
}
