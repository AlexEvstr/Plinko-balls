using UnityEngine;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private Button _ballLauncherButton;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _tapToLaunchObject;

    private void Start()
    {
        _ballLauncherButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _ball.GetComponent<Animator>().enabled = false;
        _ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _ballLauncherButton.onClick.RemoveListener(OnButtonClick);
        _tapToLaunchObject.SetActive(false);
    }
}