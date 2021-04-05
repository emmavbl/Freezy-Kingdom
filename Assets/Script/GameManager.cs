using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Place
{
    Castle, Fishing, School, Bank
}
public class GameManager : MonoBehaviour
{
    // GameManager is a Singleton, there is only one instance of it
    public static GameManager inst;

    // Player
    // Character currentCharacter;

    // current game stats (scale from 0 to 40)
    Stats stats = new Stats(20, 20, 20);

    // All decks of questions-card 
    [SerializeField] Deck[] startingDecks; // do not modify ! 

    // my stacks of played and playable card
    public Deck playable;
    public Deck notPlayable; // not yet playable cards
    public Deck played;

    // game decks
    Deck[] currentGameDecks;
    public Deck schoolDeck;
    public Deck fishingDeck;
    public Deck bankDeck;

    // Game parameters
    Vector3[] turnStats; // stats in the 
    public int turn = 0;
    public Place currentPlace = Place.Castle;
    public End activatedEnd;
    Dictionary<Place, bool> placesState = new Dictionary<Place, bool>(); //true if in add in game, else false
    // Evenement en cours ?

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

	private void Start()
	{
        Debug.Log("start");

        playable = SetEmptyDeck("playable");
        notPlayable = SetEmptyDeck("notplayable");
        played = SetEmptyDeck("played");

        placesState.Add(Place.Bank, false);
        placesState.Add(Place.Fishing, false);
        placesState.Add(Place.School, false);

        currentGameDecks = startingDecks;
        ResetGame();
        Turn();
    }
    

	public void Game()
	{
        // initialisation
            // choose character
        while(activatedEnd == null)
		{
            Turn();
            turn++;
		}

        ResetGame();
	}

    private void ResetGame()
    {
        // reset stats
        stats = new Stats(20, 20, 20);

        // reset the playable decks
        foreach (var currentGameDeck in currentGameDecks)
        {
            AddDeck(currentGameDeck);
        }

        // reset Place 
        currentPlace = Place.Castle;
    }


    public void Turn()
	{
        Debug.Log(playable);
        Debug.Log(played);
        Debug.Log(notPlayable);
        // question lieu si lieu accesible
        // genere le deck de 3 question
        // (en fonction des évenements en cours, niveau de jauge, ...)
        // 3 fois :
        //      question 
        //      decrement all card lifetime
        // Nigth()
    }

    public void Night()
	{
        // Add turnStat to stats
        // display on screen the stats made during the turn
        // check if end
	}

    #region Fonction Usuelles (AddDeck, GetSCene)
    // add deck to game stacks playable and not(yet)playable
    private void AddDeck(Deck deck)
	{
        playable.Add(deck.Playable());
        notPlayable.Add(deck.NotPlayable());
    }

    // GetScene(int) allows to screen the next scene in game,
    // The ids are define in build settings as :
    // StartMenu: 0, StartCinematic: 1, SampleScene: 2 ...
    public void GetScene(int id) 
    {
        SceneManager.LoadScene(id);
    }

    public Deck SetEmptyDeck(string name)
	{
        Deck deck = ScriptableObject.CreateInstance<Deck>();
        deck.deckName = name;
        return deck;
    }

    #endregion
}
