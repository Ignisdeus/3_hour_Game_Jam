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

    public string levelToLoad; 
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
