using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Configuration : MonoBehaviour
{
    [SerializeField] [Tooltip("Barra de ajuste de volumen de los SFX")]
    private Scrollbar volumeSFXBar;

    [SerializeField] [Tooltip("Barra de ajuste de volumen de la música")]
    private Scrollbar volumeMusicBar;

    [SerializeField] [Tooltip("Caja de texto de los segundos totales")]
    private TMP_InputField secondsText;

    [SerializeField] [Tooltip("Caja de texto de los minutos totales")]
    private TMP_InputField minutsText;

    private void OnEnable()
    {
        volumeSFXBar.value = PlayerPrefs.GetFloat("SFX_VOLUME");
        volumeMusicBar.value = PlayerPrefs.GetFloat("AMBIANCE_VOLUME");

        float totalSeconds = PlayerPrefs.GetFloat("TOTAL_SECONDS");
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);

        minutsText.text = minutes.ToString();
        secondsText.text = seconds.ToString();
    }

    public void OnIncreaseSecondsButton()
    {
        float currentValue = float.Parse(secondsText.text);
        if (currentValue < 59f)
        {
            currentValue++;
            secondsText.text = currentValue.ToString();
        }
    }
    
    public void OnDecreaseSecondsButton()
    {
        float currentValue = float.Parse(secondsText.text);
        if (currentValue > 0f)
        {
            currentValue--;
            secondsText.text = currentValue.ToString();
        }
    }
    
    public void OnIncreaseMinutsButton()
    {
        float currentValue = float.Parse(minutsText.text);
        if (currentValue < 99f)
        {
            currentValue++;
            minutsText.text = currentValue.ToString();
        }
    }
    
    public void OnDecreaseMinutsButton()
    {
        float currentValue = float.Parse(minutsText.text);
        if (currentValue > 0f)
        {
            currentValue--;
            minutsText.text = currentValue.ToString();
        }
    }

    public void OnDoneButton()
    {
        float newSeconds = float.Parse(secondsText.text);
        float newMinutes = float.Parse(minutsText.text);
        float newSFXVolume = volumeSFXBar.value;
        float newAmbianceVolume = volumeMusicBar.value;

        float totalSeconds = newMinutes * 60 + newSeconds;
        PlayerPrefs.SetFloat("TOTAL_SECONDS", totalSeconds);
        GameObject.Find("TimerBomb").GetComponent<Timer>().SetInitialTime();

        PlayerPrefs.SetFloat("SFX_VOLUME", newSFXVolume);
        PlayerPrefs.SetFloat("AMBIANCE_VOLUME", newAmbianceVolume);
        SoundManager.SharedInstance.SetVolumeEffects(newSFXVolume);
        SoundManager.SharedInstance.SetVolumeAmbiance(newAmbianceVolume);

        gameObject.SetActive(false);
    }
    
}
