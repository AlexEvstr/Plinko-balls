using UnityEngine;

public class AudioVibrationManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickAudioClip;
    [SerializeField] private AudioClip purchaseAudioClip;
    private AudioSource audioPlayer;
    public static bool isVibrationEnabled;

    [SerializeField] private GameObject soundEnabledIcon;
    [SerializeField] private GameObject soundDisabledIcon;
    [SerializeField] private GameObject vibrationEnabledIcon;
    [SerializeField] private GameObject vibrationDisabledIcon;

    private void Awake()
    {
        Vibration.Init();
        audioPlayer = GetComponent<AudioSource>();

        LoadSettings();
    }

    private void LoadSettings()
    {
        int vibrationSetting = PlayerPrefs.GetInt("vibrationSetting", 1);
        isVibrationEnabled = vibrationSetting == 1;
        //////////////
        isVibrationEnabled = false;
        //////////////
        if (isVibrationEnabled)
        {
            vibrationDisabledIcon.SetActive(false);
            vibrationEnabledIcon.SetActive(true);
        }
        else
        {
            vibrationEnabledIcon.SetActive(false);
            vibrationDisabledIcon.SetActive(true);
        }

        int soundSetting = PlayerPrefs.GetInt("soundSetting", 1);
        if (soundSetting == 1)
            EnableSound();
        else
            DisableSound();
    }

    public void PlayClickFeedback()
    {
        PlaySound(clickAudioClip);
        if (isVibrationEnabled)
            Vibration.VibrateIOS(ImpactFeedbackStyle.Soft);
    }

    public void PlayPurchaseFeedback()
    {
        PlaySound(purchaseAudioClip);
        if (isVibrationEnabled)
            Vibration.VibrateIOS(NotificationFeedbackStyle.Success);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioPlayer != null && clip != null)
        {
            audioPlayer.PlayOneShot(clip);
        }
    }

    public void ToggleSound()
    {
        if (AudioListener.volume == 1)
            DisableSound();
        else
            EnableSound();
    }

    public void ToggleVibration()
    {
        if (isVibrationEnabled)
            DisableVibration();
        else
            EnableVibration();
    }

    public void DisableSound()
    {
        soundEnabledIcon.SetActive(false);
        soundDisabledIcon.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("soundSetting", 0);
        PlayerPrefs.Save();
    }

    public void EnableSound()
    {
        soundDisabledIcon.SetActive(false);
        soundEnabledIcon.SetActive(true);
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("soundSetting", 1);
        PlayerPrefs.Save();
    }

    public void DisableVibration()
    {
        vibrationEnabledIcon.SetActive(false);
        vibrationDisabledIcon.SetActive(true);
        isVibrationEnabled = false;
        PlayerPrefs.SetInt("vibrationSetting", 0);
        PlayerPrefs.Save();
    }

    public void EnableVibration()
    {
        vibrationDisabledIcon.SetActive(false);
        vibrationEnabledIcon.SetActive(true);
        isVibrationEnabled = true;
        PlayerPrefs.SetInt("vibrationSetting", 1);
        PlayerPrefs.Save();
    }
}
