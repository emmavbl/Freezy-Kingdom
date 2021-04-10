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
    //public static Deck playable;
    //public static Deck notPlayable; // not yet playable cards
    //public static Deck played;

    // game decks
    Deck[] currentGameDecks;
    [HideInInspector]
    public static Deck[] gameplayDeck = new Deck[3]; // 0: playable, 1: notplayable, 2: played
    public Deck schoolDeck;
    [HideInInspector]
    public static Deck[] schoolGameplayDeck = new Deck[3]; // 0: playable, 1: notplayable, 2: played
    public Deck fishingDeck;
    [HideInInspector]
    public static Deck[] fishingGameplayDeck = new Deck[3]; // 0: playable, 1: notplayable, 2: played
    public Deck bankDeck;
    [HideInInspector]
    public static Deck[] bankGameplayDeck = new Deck[3]; // 0: playable, 1: notplayable, 2: played

    // current gameplay deck
    public static Deck[] currentGameplayDeck = new Deck[3]; // 0: playable, 1: notplayable, 2: played

    // card morning ask for going in place ?
    public Card schoolCard;
    public Card bankCard;
    public Card fishingCard;

    // Game parameters
    public static List<Card> turnDeck;
    List<Stats> turnStats = new List<Stats>(); // stats in the turn
    public int turn = 0;
    public Place currentPlace = Place.Castle;
    public End activatedEnd;
    public bool end = false;
    static Dictionary<Place, bool> placesState = new Dictionary<Place, bool>(); //true if in add in game, else false

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

        gameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };
        fishingGameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };
        schoolGameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };
        bankGameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };


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
        gameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };
        fishingGameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };
        schoolGameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };
        bankGameplayDeck = new Deck[3] { SetEmptyDeck("playable"), SetEmptyDeck("notplayable"), SetEmptyDeck("played") };

        foreach (var currentGameDeck in currentGameDecks)
        {
            AddDeck(gameplayDeck, currentGameDeck);
        }
        AddDeck(fishingGameplayDeck, fishingDeck);
        AddDeck(schoolGameplayDeck, schoolDeck);
        AddDeck(bankGameplayDeck, bankDeck);


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
        turn++;

        // question lieu si lieu accesible !!!
        List<Place> accessiblePlace = new List<Place>();
		foreach (var item in placesState)
		{
			if (item.Value)
			{
                accessiblePlace.Add(item.Key);
			}
		}

        currentGameplayDeck = gameplayDeck;
        // check if some played.cards are replayable 
        CheckReusableCard();
        ConsoleDecks();
        // genere le deck de 3 question
        turnDeck = GenerateDayDeck(3);
        Place place = Place.Castle;

        if (UnityEngine.Random.Range(0f, 1f) >= 0.5 && accessiblePlace.Count > 0)
		{
            place = accessiblePlace.ElementAt<Place>(UnityEngine.Random.Range(0, accessiblePlace.Count));
		}
        Debug.Log("Choose place : " + place);
		switch (place)
		{
			case Place.Fishing:
                FindObjectOfType<DisplayQuestion>().UpdateToCard(fishingCard);
                break;
			case Place.School:
                FindObjectOfType<DisplayQuestion>().UpdateToCard(schoolCard);
                break;
			case Place.Bank:
                FindObjectOfType<DisplayQuestion>().UpdateToCard(bankCard);
                break;
			default:
                DisplayNextCard();
                break;
		}

    }

    public void Night()
	{
        // display on screen the stats made during the turn
        GameObject temp_screen = Instantiate(nightScreen,
            FindObjectOfType<Canvas>().transform.position,
            Quaternion.identity,
            FindObjectOfType<Canvas>().transform);

        temp_screen.GetComponentInChildren<Button>().transform.localScale = new Vector3(0, 0, 0);
        Button button = temp_screen.GetComponentInChildren<Button>();
        LeanTween.scale(button.gameObject, new Vector3(1, 1, 1), .5f).setEaseOutBack().setDelay(6.5f);
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

            if (statEnd.Value != 0)
			{
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
            Night();
            return;
		}
        FindObjectOfType<DisplayQuestion>().UpdateToCard(turnDeck.ElementAt<Card>(0));
        turnDeck.RemoveAt(0);

    }


    public List<Card> GenerateDayDeck(int number)
	{
        List<Card> cards = new List<Card>();
		while (cards.Count < number)
		{
            if (currentGameplayDeck[0].cards.Count > 0)
			{
                Card c = currentGameplayDeck[0].RandomCard();
                if(! cards.Contains(c)) // if deck does not yet contains the picked card 
			    {
                    // add the card
                    cards.Add(c);

                    // remove from playable and add to played
                    currentGameplayDeck[0].Remove(c);
                    currentGameplayDeck[2].Add(c);
			    } 
			} else
			{
                break;
            }
		}
        return cards;
	}

    public void CheckReusableCard()
	{
        ConsoleDecks();
        List<Card> toRemove =  new List<Card>();
		foreach (Card card in currentGameplayDeck[2].cards)
		{
            card.lifeTime--;
            if (card.lifeTime <= 0)
			{
                toRemove.Add(card);
				if (card.canBePicked)
				{
                    currentGameplayDeck[0].Add(card);
				}
				else
				{
                    currentGameplayDeck[1].Add(card);
				}
			}
		}
        foreach(Card c in toRemove)
		{
            currentGameplayDeck[2].Remove(c);
		}
	}

	public void FindAndPutInPlayable(Card[] cardToUnlock)
	{
		for (int i = 0; i < cardToUnlock.Length; i++)
		{
            var c = cardToUnlock[i];
            Debug.Log(c.cardName + " unlocke");
            currentGameplayDeck[0].Add(c);
            if (currentGameplayDeck[1].cards.Contains(c))
			{
                currentGameplayDeck[1].Remove(c);
			} else if (currentGameplayDeck[2].cards.Contains(c))
			{
                currentGameplayDeck[2].Remove(c);
            }
        }
	}

    public void AddPlace(Place place)
	{
        placesState[place] = true;
    }

    // add deck to game stacks playable and not(yet)playable
    public void AddDeck(Deck[] gameplayDeck, Deck deck)
	{
        Debug.Log(deck.deckName);
        gameplayDeck[0].Add(deck.Playable());
        gameplayDeck[1].Add(deck.NotPlayable());
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


    public void ConsoleDecks()
	{
        Debug.Log(currentGameplayDeck[0]);
        Debug.Log(currentGameplayDeck[1]);
        Debug.Log(currentGameplayDeck[2]);
    }

    #endregion
}
