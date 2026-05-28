using Fusion;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LobbyManager : NetworkBehaviour
{
    public ButtonScript player;
    public int playersMax ;
    public Button game;
    

    void Update()
    {
        PlayersWaiting();

    }

    void PlayersWaiting()
    {
        
        //if (Runner >= playersMax)
        //{
        //    game.gameObject.SetActive(true);
        //}
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
