using UnityEngine;
using System.Collections.Generic;

public class GameProperties : MonoBehaviour {

    [SerializeField]
    List<GameLevel> levels;
    public int CurLevel { get; set; }
    public float CurTimer { get; set; }
    public Sprite CurImage { get; set; }
    public List<Vector3> CurVertices { get; set; }

    public void StartNextLevel() {
        CurTimer = levels[CurLevel].Seconds;
        CurImage = levels[CurLevel]._Image;
        CurVertices = levels[CurLevel].Vertices;
        CurLevel++;
    }

    public void TimerGo(float time) {
        CurTimer -= time;
    }

    public int GetCountLevels() {
        return levels.Count;
    }

}
