using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject GarageMenu;

    private bool isMenuVisible = true;

    void Start()
    {
        if 
    (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuVisible = !isMenuVisible;
        MainMenu.SetActive(isMenuVisible);
        SettingsMenu.SetActive(false);
        GarageMenu.SetActive(false);
        }
    }
    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        GarageMenu.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        GarageMenu.SetActive(false);
    }

    public void ShowGarageMenu()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        GarageMenu.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }
}