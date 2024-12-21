using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject creditsDisplay;
    public GameObject settingsDisplay;
    [SerializeField] GameObject dropletEffect;

    private void Awake()
    {
        creditsDisplay.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(dropletEffect, new Vector3(clickPos.x, clickPos.y, 0f) , Quaternion.identity);
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
}
