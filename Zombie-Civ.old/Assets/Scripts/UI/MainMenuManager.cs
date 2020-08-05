using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour {
	public Image Meme;
    public Sprite[] Memes;
    public float MemeTime;
	public GameObject Main;
    public Slider VolumeRocker;
    public TMP_InputField VolumeInputField;
	// Use this for initialization
	void Start () {
        RandomMeme();
	}
	public void LoadScene(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex);
    }
    public void ChangeVolume(Slider volumeSlider)
    {
        VolumeInputField.SetTextWithoutNotify(volumeSlider.value.ToString());
        AudioListener.volume = volumeSlider.value /volumeSlider.maxValue;
    }
    public void ChangeVolume(TMP_InputField volumeField)
    {
        int volume = int.Parse(volumeField.text);
        if (volume > VolumeRocker.maxValue)
        {
            volume = (int)VolumeRocker.maxValue;
            volumeField.SetTextWithoutNotify(volume.ToString());
        }
        VolumeRocker.SetValueWithoutNotify(volume);
        AudioListener.volume = volume / VolumeRocker.maxValue;
    }
    public void RandomMeme()
    {
        Meme.sprite = Memes[Random.Range(0, Memes.Length)];
    }
    
}
