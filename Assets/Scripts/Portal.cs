using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Portal
{
    [SerializeField] public long id;
    [SerializeField] public List<Picture> arts;
    [CanBeNull] [SerializeField] public string curator;
    [SerializeField] public float width;
    [SerializeField] public float height;


    public Portal(float width = 0, float height = 0, string curator = "Ruth", long id = 0)
    {
        this.width = width;
        this.height = height;
        arts = new List<Picture>();
        this.curator = curator;
        this.id = id;
    }
    
}
