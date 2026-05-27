using Fusion;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{

    public int players = 0;
    public Button game;
    public ChooseAnim chooseAnim;

    void Update()
    {
        PlayersWaiting();
        
    }

    void PlayersWaiting()
    {
        if (players >= 2 && chooseAnim ) 
        {
            game.gameObject.SetActive(true);
        }
    }
    public void MainGame()
    {

        NetworkRunner runner = FindObjectOfType<NetworkRunner>();

        if (runner != null && runner.IsRunning)
        {

            runner.LoadScene("MainGame", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        else
        {

            SceneManager.LoadScene("MainGame");
        }
    }
}
