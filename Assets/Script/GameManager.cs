using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager is a Singleton, there is only one instance of it
    public static GameManager inst;

    // current game stats
    float wealth;
    float ecosystem;
    float community;

    // All decks of questions-card 
    public Deck[] playable;
    public Deck schoolDeck;
    public Deck fishingDeck;
    public Deck bankDeck;

    // Game parameters
    public int turn = 0;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // GetScene(int) allows to screen the next scene in game,
    // The ids are define in build settings as :
    // StartMenu: 0, StartCinematic: 1, SampleScene: 2 ...
    public void GetScene(int id) 
    {
        SceneManager.LoadScene(id);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
