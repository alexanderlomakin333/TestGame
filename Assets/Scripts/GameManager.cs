using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public ParticleSystem particle;
    public Camera mainCamera;
    [SerializeField]
    public Shape shape;
    Shape questShape;
    GameProperties properties;
    GraphManager graph;
    int score;

    void Awake() {
        gameManager = this;
        properties = this.GetComponent<GameProperties>();
        graph = this.GetComponent<GraphManager>();
        properties.StartNextLevel();
        questShape = new Shape(properties.CurVertices);
        Time.timeScale = 0;
    }

    void Update() {
        CheckDefeat(properties.CurTimer);
        graph.SetTime(properties.CurTimer);
        graph.SetLevel(properties.CurLevel);
        graph.SetScore(score);
        properties.TimerGo(Time.deltaTime);
        MouseScan();

    }

    public void StartGame() {
        graph.SetTaskImage(properties.CurImage);
        graph.ShowImage();
        Time.timeScale = 1;
    }

    float GetAngleFrom2Roint(Vector3 point1, Vector3 point2) {
        return (Mathf.Atan2(point1.y - point2.y, point1.x - point2.x) / Mathf.PI * 180) + 180;
    }

    void Win() {
        score++;
        if(properties.CurLevel == (properties.GetCountLevels())) {
            graph.ShowWinDefPanel("Победа!!!", score);
            Time.timeScale = 0;
            return;
        }
        NextLevel();
    }

    void NextLevel() {
        properties.StartNextLevel();
        graph.SetTaskImage(properties.CurImage);
        graph.ShowImage();
        questShape = new Shape(properties.CurVertices);
    }

    void Defeat() {
        graph.ShowWinDefPanel("Поражение :`(", score);
        Time.timeScale = 0;
    }

    void CheckDefeat(float time) {
        if (time < 0)
            Defeat();
    }

    void MouseScan() {
        Vector3 curPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(mainCamera.transform.position.z));
        Vector3 mouse = mainCamera.ScreenToWorldPoint(curPoint);
        if (Input.GetMouseButton(0)) {
            particle.transform.position = mouse;
            particle.emissionRate = 200;
            if (shape == null) {
                shape = new Shape(curPoint);
            }
            else {
                shape.newPoint(curPoint);
            }
        }
        else if (Input.GetMouseButtonUp(0)) {
            shape.CloseShape(curPoint);
            if (shape.EqualsShape(questShape)) {
                Win();
            }
            shape = null;
            particle.emissionRate = 0;
        }
    }

   

}