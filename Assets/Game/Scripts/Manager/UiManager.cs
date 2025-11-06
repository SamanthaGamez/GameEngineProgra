using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    static public UiManager instance;

    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI enemiesRemainingText;

    private float unscaledDeltaTimeAccumulator;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateEnemiesTxt(int enemiesCount)
    {
        enemiesRemainingText.text = "Enemigos restantes " + enemiesCount;
    }

    private void Update()
    {
        CalculateFPS();
    }

    private void CalculateFPS()
    {
        unscaledDeltaTimeAccumulator += (Time.unscaledDeltaTime - unscaledDeltaTimeAccumulator) * 0.1f;
        int fps = Mathf.CeilToInt(1f / unscaledDeltaTimeAccumulator);
        fpsText.text = "FPS: " + fps;
    }
}
