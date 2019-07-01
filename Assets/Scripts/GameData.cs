using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField] public List<Portal> portals;

    public GameData()
    {
        portals = new List<Portal>();
    }
    
}