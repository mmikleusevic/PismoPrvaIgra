using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject optionsPanel;

    public void Play()
    {
        panel.SetActive(false);
        GameManager.Instance.PlayGame();
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
