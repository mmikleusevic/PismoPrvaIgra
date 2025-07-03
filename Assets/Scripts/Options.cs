using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeText;

    private void Start()
    {
        AudioManager.Instance.SetVolume(volumeSlider.value);
    }

    public void Back()
    {
        optionsPanel.SetActive(false);
    }

    public void ChangeVolume()
    {
        volumeText.text = $"Volume: {Math.Round(volumeSlider.value * 100, 2)} %";
        AudioManager.Instance.SetVolume(volumeSlider.value);
    }
}
