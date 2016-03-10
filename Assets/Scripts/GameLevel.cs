using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class GameLevel {
    [SerializeField]
    string shapeName;
    public string ShapeName {get {return shapeName;}}
    [SerializeField]
    int seconds;
    public int Seconds { get { return seconds; } }
    [SerializeField]
    Sprite image;
    public Sprite _Image { get { return image; } }
    [SerializeField]
    List<Vector3> vertices;
    public List<Vector3> Vertices { get { return vertices; } }

}
