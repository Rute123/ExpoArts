using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Portal
{
    [SerializeField] public List<Picture> arts;
    [CanBeNull] [SerializeField] public string curator;
    [SerializeField] public float width;
    [SerializeField] public float height;
}
