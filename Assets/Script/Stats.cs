using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    // current game stats (scale from 0 to 40)
    public float wealth;
    public float community;
    public float ecosystem;

    public Stats( int _wealth, int _ecosystem, int _community)
	{
        wealth = _wealth;
        ecosystem = _ecosystem;
        community = _community;
    }

    public void Add(Stats stats)
	{
        wealth += stats.wealth;
        ecosystem += stats.ecosystem;
        community += stats.community;
    }

    public string Print()
	{
        return "(" + wealth + "," + community + "," + ecosystem + ")";
	}
}
