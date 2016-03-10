using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public void StartGame(GameObject pauseButton) {
        GameManager.gameManager.StartGame();
        pauseButton.GetComponent<Button>().interactable = true;
    } 

    public void PauseOff() {
        Time.timeScale = 1;
    }

    public void PauseOn() {
        Time.timeScale = 0;
    }

    public void Open(GameObject obj) {
        obj.SetActive(true);
    }

    public void Close(GameObject obj) {
        obj.SetActive(false);
    }

    public void Hide(GameObject obj) {
        obj.GetComponent<CanvasGroup>().alpha = 0;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        obj.GetComponent<CanvasGroup>().interactable = false;
    }

    public void Display(GameObject obj) {
        obj.GetComponent<CanvasGroup>().alpha = 1;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        obj.GetComponent<CanvasGroup>().interactable = true;
    }

    public void Quit() {
        Application.Quit();
    }

    public void Restart() {
        Application.LoadLevel(0);
    }
}
