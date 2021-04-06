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
    List<Card> turnDeck;
    List<Stats> turnStats = new List<Stats>(); // stats in the turn
    public int turn = 0;
    public Place currentPlace = Place.Castle;
    public End activatedEnd;
    public bool end = false;
    Dictionary<Place, bool> placesState = new Dictionary<Place, bool>(); //true if in add in game, else false
    // Evenement en cours ?

    //Other 
    [SerializeField] GameObject statsDisplay;
    [SerializeField] GameObject statText;

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
        turnDeck = GenerateDayDeck(2);
		foreach (Card item in turnDeck)
		{
            Debug.Log(item.cardName);
		}
        DisplayNextCard();
        // Debug.Log(cardsToPlay);

        //FindObjectOfType<DisplayQuestion>().card = cardsToPlay.ElementAt<Card>(0);

        //ConsoleDecks();
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

        // I was trying to create a new scene but
        // finally i'll just try to put a interface on
        // top of game scene displaying le results

        //GameObject display = GameObject.Find("StatsDisplay");
        //Debug.Log(display);


		//for (int i = 0; i < turnStats.Count; i++)
		//{
  //          Stats stat = turnStats.ElementAt<Stats>(i);

  //          // Add turnStat to stats
  //          stats.Add(stat);
  //          GameObject UItextGO = new GameObject("Text");
  //          GameObject text = Instantiate(statText,
  //              display.GetComponent<DisplayStats>().statsPosition.ElementAt(i).transform.position,
  //              Quaternion.identity,
  //              display.GetComponent<DisplayStats>().statsPosition.ElementAt(i).transform);
  //          text.GetComponent<Text>().text = stat.wealth + " " + stat.community + " " + stat.ecosystem;
		//}         // display on screen the stats made during the turn
       
            

        // reset stats for next turn
        turnStats = new List<Stats>();

        // check if end
	}


    public void DisplayNextCard()
	{
        Debug.Log(turnDeck.Count);
		if (turnDeck.Count <= 0)
		{
            Debug.Log("end the turn");
            Night();
            return;
		}
        FindObjectOfType<DisplayQuestion>().UpdateToCard(turnDeck.ElementAt<Card>(0));
        turnDeck.RemoveAt(0);
    }

    List<Card> GenerateDayDeck(int number)
	{
        List<Card> cards = new List<Card>();
		while (cards.Count < number)
		{
            //ConsoleDecks();
            Card c = playable.RandomCard();
            if(! cards.Contains(c)) // if cards does not contains the picked card 
			{
                cards.Add(c);
                //remove from playable and add to played
			} 
		}
        return cards;
	}

    public void AddStats(Stats s)
	{
        turnStats.Add(s);
	}

    #region Fonction Usuelles (AddDeck, GetSCene)
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
    }

    #endregion
}
