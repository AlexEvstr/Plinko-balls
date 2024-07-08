using UnityEngine;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private Button _ballLauncherButton;
    [SerializeField] private Rigidbody2D _ballRigidbody;

    private void Start()
    {
        _ballLauncherButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        _ballLauncherButton.onClick.RemoveListener(OnButtonClick);
    }
}