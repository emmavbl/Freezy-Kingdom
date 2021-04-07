using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Place
{
    Castle, Fishing, School, Bank
}
public class GameManager : MonoBehaviour
{
    // GameManager is a Singleton, there is only one instance of it
    public static GameManager inst = null;

    // Player
    // Character currentCharacter;

    // current game stats (scale from 0 to 40)
    static Stats stats = new Stats(20, 20, 20);

    // All decks of questions-card 
    [SerializeField] Deck[] startingDecks; // do not modify ! 

    // my stacks of played and playable card
    public static Deck playable;
    public static Deck notPlayable; // not yet playable cards
    public static Deck played;

    // game decks
    Deck[] currentGameDecks;
    public Deck schoolDeck;
    public Deck fishingDeck;
    public Deck bankDeck;

    // Game parameters
    static List<Card> turnDeck;
    List<Stats> turnStats = new List<Stats>(); // stats in the turn
    public int turn = 0;
    public Place currentPlace = Place.Castle;
    public End activatedEnd;
    public bool end = false;
    Dictionary<Place, bool> placesState = new Dictionary<Place, bool>(); //true if in add in game, else false
    // Evenement en cours ?

    //Other 
    public GameObject nightScreen;
    //[SerializeField] GameObject statText;

    private void Awake()
    {
        if (inst == null)
        {
            Debug.Log("define inst");
            inst = this;
            InitGame();
        }
        else
        {
            Debug.Log("delete inst");
            Destroy(gameObject);
			return;
		}
        DontDestroyOnLoad(gameObject);
    }

	private void InitGame()
	{
        Debug.Log("init gameManager obj");

        playable = SetEmptyDeck("playable");
        notPlayable = SetEmptyDeck("notplayable");
        played = SetEmptyDeck("played");

        placesState.Add(Place.Bank, false);
        placesState.Add(Place.Fishing, false);
        placesState.Add(Place.School, false);

        currentGameDecks = startingDecks;
        ResetGame();
        ConsoleDecks();
    }
    

    private void ResetGame()
    {
        // reset stats
        stats = new Stats(20, 20, 20);

        // reset the playable decks
        playable = SetEmptyDeck("playable");
        notPlayable = SetEmptyDeck("notplayable");
        played = SetEmptyDeck("played");
        foreach (var currentGameDeck in currentGameDecks)
        {
            AddDeck(currentGameDeck);
        }

        // reset Place 
        currentPlace = Place.Castle;
   
        Debug.Log("Reset done");
    }

	public void Game()
	{
		// initialisation
		// choose character

		while (!end)
		{
			Turn();
			turn++;
            end = true;
		}

		//ResetGame();
	}

    public void Turn()
	{
        // question lieu si lieu accesible

        ConsoleDecks();
        // genere le deck de 3 question
        turnDeck = GenerateDayDeck(3);

		foreach (Card item in turnDeck)
		{
            Debug.Log(item.cardName);
		}

        DisplayNextCard();
    }

    public void Night()
	{
        // display on screen the stats made during the turn
        GameObject temp_screen = Instantiate(nightScreen,
            FindObjectOfType<Canvas>().transform.position,
            Quaternion.identity,
            FindObjectOfType<Canvas>().transform);

        temp_screen.GetComponent<DisplayStats>().SetStats(turnStats);

		// Add turnStat to stats
		foreach (Stats s in turnStats)
		{
            stats.Add(s);
		}

        Debug.Log(stats.Print());

        // increment turn and decrement played.cards.lifetime 
        // check if some played.cards are replayable 


        // reset stats for next turn
        turnStats = new List<Stats>();

        // check if end
	}


    public void DisplayNextCard()
	{
		if (turnDeck.Count <= 0)
		{
            Debug.Log("end the turn");
            Night();
            return;
		}
        FindObjectOfType<DisplayQuestion>().UpdateToCard(turnDeck.ElementAt<Card>(0));
        turnDeck.RemoveAt(0);

    }

    public void EndNigth()
	{
        Destroy(GetComponent<DisplayStats>().gameObject);
        Turn();
	}

    List<Card> GenerateDayDeck(int number)
	{
        List<Card> cards = new List<Card>();
        ConsoleDecks();
		while (cards.Count < number)
		{
            Card c = playable.RandomCard();
            if(! cards.Contains(c)) // if deck does not yet contains the picked card 
			{
                // add the card
                cards.Add(c);

                // remove from playable and add to played
                playable.Remove(c);
                played.Add(c);
			} 
		}
        return cards;
	}


    #region Fonction Usuelles (AddDeck, GetSCene, ...)
    public void AddStats(Stats s)
	{
        turnStats.Add(s);
	}

    // add deck to game stacks playable and not(yet)playable
    private void AddDeck(Deck deck)
	{
        playable.Add(deck.Playable());
        notPlayable.Add(deck.NotPlayable());
    }

    //generate a Card List of nb randomly picked playable cards

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


    void ConsoleDecks()
	{
        Debug.Log(playable);
        Debug.Log(played);
        Debug.Log(notPlayable);
        Debug.Log("Turn :" + turn);
    }

    #endregion
}
