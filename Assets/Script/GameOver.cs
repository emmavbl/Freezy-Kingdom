using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Animate sprite
        GetComponentInChildren<Text>().text = FindObjectOfType<GameManager>().activatedEnd.description;

        FindObjectOfType<GameManager>().ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
