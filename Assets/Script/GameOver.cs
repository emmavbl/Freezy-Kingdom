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
        Instantiate(FindObjectOfType<GameManager>().activatedEnd.character,
            GetComponentsInChildren<RectTransform>()[3].transform.position,
            Quaternion.identity,
            GetComponentsInChildren<RectTransform>()[3].transform);

        FindObjectOfType<GameManager>().ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonBack()
	{
        FindObjectOfType<GameManager>().GetScene(0);
        FindObjectOfType<AudioManager>().Stop("GameOver");
        FindObjectOfType<AudioManager>().Play("Menu_no");

    }

}
