using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button PlayButton;

    [SerializeField]
    private Button ExitButton;


    private void Start()
    {
        PlayButton.onClick.AddListener(OnClickPlay);
        ExitButton.onClick.AddListener(OnClickExit);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
