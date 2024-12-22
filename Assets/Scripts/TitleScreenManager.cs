using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject creditsDisplay;
    public GameObject settingsDisplay;
    public GameObject instructionsDisplay;
    [SerializeField] GameObject dropletEffect;
    [SerializeField] private AudioClip dropletSoundClip;


    private void Awake()
    {
        creditsDisplay.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(dropletEffect, new Vector3(clickPos.x, clickPos.y, 0f), Quaternion.identity);
            SoundFXManager.Instance.PlaySoundFXClip(dropletSoundClip, transform, 1f);

        }
    }
    public void DisplayCredits()
    {
        creditsDisplay.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsDisplay.SetActive(false);
    }

    public void DisplaySettings()
    {
        settingsDisplay.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsDisplay.SetActive(false);
    }

    public void DisplayInstructions()
    {
        instructionsDisplay.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionsDisplay.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
