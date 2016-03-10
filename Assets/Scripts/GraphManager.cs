using UnityEngine;
using UnityEngine.UI;
using System;

public class GraphManager : MonoBehaviour {

    [SerializeField]
    GameObject timeText;
    [SerializeField]
    GameObject scoreText;
    [SerializeField]
    GameObject levelText;
    [SerializeField]
    GameObject taskImage;
    [SerializeField]
    GameObject winDefPanel;

    public void SetTime(float time) {
        if(time < 0) { time = 0; }
        timeText.GetComponent<Text>().text = String.Format("Осталось: " +  "{0:00.00}" + " сек" , time ) ;
    }

    public void SetLevel(int level) {
        levelText.GetComponent<Text>().text = String.Format("Уровень: " + level);
    }

    public void SetTaskImage(Sprite image) {
        taskImage.GetComponent<Image>().sprite = image;
    }

    public void ShowImage() {
        taskImage.GetComponent<CanvasGroup>().alpha = 1;
        taskImage.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void ShowWinDefPanel(string text, int score) {
        winDefPanel.transform.FindChild("ScoreText").GetComponent<Text>().text = String.Format("Счет: " + score);
        winDefPanel.transform.FindChild("TitelText").GetComponent<Text>().text = text;
        winDefPanel.GetComponent<CanvasGroup>().alpha = 1;
        winDefPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        winDefPanel.GetComponent<CanvasGroup>().interactable = true;

    }

    public void SetScore(int score) {
        scoreText.GetComponent<Text>().text = String.Format("Счет: " + score);
        
    }

}
