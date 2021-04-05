using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Animate sprite
        Invoke("Continue", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Continue()
	{
        FindObjectOfType<GameManager>().GetScene(2);
    }
}
