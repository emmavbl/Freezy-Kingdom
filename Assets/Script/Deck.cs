using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
[System.Diagnostics.DebuggerDisplay("{ToString()}")]
public class Deck : ScriptableObject
{
    public string deckName = "default";
    public List<Card> cards = new List<Card>();

	// public float probability;

	public void Add(Card card)
	{
        cards.Add(card);
	}

    public void Add(Deck deck)
    {
        cards.AddRange(deck.cards);
    }

    public Card Remove(Card card)
	{
        cards.Remove(card);
        return card;
	}

    public Deck Playable()
	{
        Deck list = CreateInstance<Deck>();
        list.deckName = "playable";
        foreach (Card c in cards)
		{
			if (c.canBePicked)
			{
                list.Add(c);
			}
		}
        return list;
	}

    public Deck NotPlayable()
    {
        Deck list = CreateInstance<Deck>();
        list.deckName = "notplayable";
        foreach (Card c in cards)
        {
            if (!c.canBePicked)
            {
                list.Add(c);
            }
        }
        return list;
    }


    #region Debug
    public override string ToString()
    {
        return deckName  + ": " + string.Join(",", cards);
    }
    #endregion

}
