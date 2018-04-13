using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    #region Attributes

    #endregion

    #region Properties
    public static List<Team> Teams  // The teams taken so far
    { get; private set; }    
	public int ID                   // The team's ID
    { get; private set; }                     
    public Color Color              // The team's color
    { get; private set; }                
    #endregion

    #region Constructors
    // Make a new team from an ID that is not taken
    public Team ()
    {
        if (Teams == null)
        {
            Teams = new List<Team>();
        }
        for (int i = 0; i <= Teams.Count; ++i)
        {
            if (i == Teams.Count)
            {
                ID = i;
            }
            else
            {
                bool notPresent = true;
                foreach (Team t in Teams)
                {
                    if (t.ID == i)
                    {
                        notPresent = false;
                        break;
                    }
                }
                if (notPresent)
                {
                    ID = i;
                    break;
                }
            }
        }
        Color = Utils.RandomColor(false);
        Teams.Add(this);
    }

    // Destructor
    ~Team ()
    {
        Teams.Remove(this);
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
