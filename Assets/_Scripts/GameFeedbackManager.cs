using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFeedbackManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickAudioClip;
    [SerializeField] private AudioClip winAudioClip;
    [SerializeField] private AudioClip loseAudioClip;
    [SerializeField] private AudioClip circleCollisionAudioClip;
    [SerializeField] private AudioClip cupCollisionAudioClip;
    [SerializeField] private AudioClip swipeAudioClip;
    [SerializeField] private AudioClip chooseCupAudioClip;
    [SerializeField] private AudioClip bottomCollisionAudioClip;
    private AudioSource audioPlayer;
    public static bool isVibrationEnabled;

    private void Awake()
    {
        Vibration.Init();
        int vibrationSetting = PlayerPrefs.GetInt("vibrationSetting", 1);
        isVibrationEnabled = vibrationSetting == 1;
        ////////
        isVibrationEnabled = false;
        ////////
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayClickFeedback()
    {
        PlaySound(clickAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.VibrateIOS(ImpactFeedbackStyle.Soft);
        }
    }

    public void PlayWinFeedback()
    {
        audioPlayer.Stop();
        PlaySound(winAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.VibrateIOS(NotificationFeedbackStyle.Success);
        }
    }

    public void PlayLoseFeedback()
    {
        audioPlayer.Stop();
        PlaySound(loseAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.Vibrate();
        }
    }

    public void PlayShotFeedback()
    {
        PlaySound(circleCollisionAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
        }
    }

    public void PlayCupCollisionSound()
    {
        PlaySound(cupCollisionAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.VibrateIOS(ImpactFeedbackStyle.Rigid);
        }
    }

    public void PlaySwipeSound()
    {
        PlaySound(swipeAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
        }
    }

    public void PlaychooseCupSound()
    {
        PlaySound(chooseCupAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.VibrateIOS(ImpactFeedbackStyle.Rigid);
        }
    }

    public void PlaybottomCollisionSound()
    {
        StartCoroutine(WaitToBorder());
    }

    private IEnumerator WaitToBorder()
    {
        PlaySound(bottomCollisionAudioClip);
        if (isVibrationEnabled)
        {
            Vibration.VibrateIOS(ImpactFeedbackStyle.Rigid);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioPlayer != null && clip != null)
        {
            audioPlayer.PlayOneShot(clip);
        }
    }
}
