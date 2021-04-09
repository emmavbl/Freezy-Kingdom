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
    // Ends
    public End tooMuchWealth;
    public End tooLowWealth;
    public End tooMuchCommunity;
    public End tooLowCommunity;
    public End tooMuchEcosystem;
    public End tooLowEcosystem;


    //Other 
    public GameObject nightScreen;
    //[SerializeField] GameObject statText;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            InitGame();
        }
        else
        {
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
    }
    

    public void ResetGame()
    {
        // reset stats
        stats = new Stats(20, 20, 20);

        // reset end
        activatedEnd = null;
        end = false;

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

        Turn();
	}

    public void Turn()
	{
        // question lieu si lieu accesible !!!




        turn++;
        // check if some played.cards are replayable 
        CheckReusableCard();
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

        // reset stats for next turn
        turnStats = new List<Stats>();

		if (!end)
		{
            // check if end activated by stats
            KeyValuePair<Place, int> statEnd = stats.CheckEnd();

            Debug.Log(statEnd);
            if (statEnd.Value != 0)
			{
                Debug.Log("!end");
				switch (statEnd.Key)
				{
					case Place.Fishing:
						if (statEnd.Value == -1)
						{
                            activatedEnd = tooLowEcosystem;
						} else
						{
                            activatedEnd = tooMuchEcosystem;

                        }
                        break;
					case Place.School:
                        if (statEnd.Value == -1)
                        {
                            activatedEnd = tooLowCommunity;
                        }
                        else
                        {
                            activatedEnd = tooMuchCommunity;
                        }
                        break;
					case Place.Bank:
                        if (statEnd.Value == -1)
                        {
                            activatedEnd = tooLowWealth;
                        }
                        else
                        {
                            activatedEnd = tooMuchWealth;
                        }
                        break;
					default:
						break;
				}
				end = true;
			}
		}

	}

    public void GameOver()
	{
        GetScene(3);
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


    List<Card> GenerateDayDeck(int number)
	{
        List<Card> cards = new List<Card>();
		while (cards.Count < number)
		{
            if (playable.cards.Count > 0)
			{
                Card c = playable.RandomCard();
                if(! cards.Contains(c)) // if deck does not yet contains the picked card 
			    {
                    // add the card
                    cards.Add(c);

                    // remove from playable and add to played
                    playable.Remove(c);
                    c.initLifeTime();
                    played.Add(c);
			    } 
			} else
			{
                break;
            }
		}
        return cards;
	}

    void CheckReusableCard()
	{
        List<Card> toRemove =  new List<Card>();
		foreach (Card card in played.cards)
		{
            card.lifeTime--;
            if (card.lifeTime <= 0)
			{
                toRemove.Add(card);
				if (card.canBePicked)
				{
                    playable.Add(card);
				}
				else
				{
                    notPlayable.Add(card);
				}
			}
		}
        foreach(Card c in toRemove)
		{
            played.Remove(c);
		}
	}

	public void FindAndPutInPlayable(Card[] cardToUnlock)
	{
		for (int i = 0; i < cardToUnlock.Length; i++)
		{
            var c = cardToUnlock[i];
            Debug.Log(c.cardName + " unlocke");
            playable.Add(c);
            if (notPlayable.cards.Contains(c))
			{
                notPlayable.Remove(c);
			} else if (played.cards.Contains(c))
			{
                played.Remove(c);
            }
        }
	}

    public void AddPlace(Place place)
	{
        placesState[place] = true;
    }

    // add deck to game stacks playable and not(yet)playable
    public void AddDeck(Deck deck)
	{
        Debug.Log(deck.deckName);
        playable.Add(deck.Playable());
        notPlayable.Add(deck.NotPlayable());
    }

    #region Fonction Usuelles (AddStats, GetSCene, ...)
    public void AddStats(Stats s)
	{
        turnStats.Add(s);
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
