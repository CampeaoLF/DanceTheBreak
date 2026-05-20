using UnityEngine;
using UnityEngine.UI;
using Fusion;
using System.Linq;

public class ButtonScript : NetworkBehaviour
{
    [Networked] public NetworkObject player { get; set; }
    [SerializeField] public GameManager gameManager;
    [SerializeField] public Animator playerAnimator;

    [Header("Botões de movimento base")]
    [SerializeField] public Button[] buttonsMoveBase;

    //[Header("Botão de movimento especial")]
    [Networked] public NetworkObject buttonMoveSpecial { get; set; }

    [Header("Sprite")]
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public Sprite[] cenario;
    [SerializeField] public NetworkObject backGround1;
    [SerializeField] public NetworkObject backGround2;
    [SerializeField] public NetworkObject backGround3;

    [Header("Animação")]
    private GameObject animManager;

    [Header("Váriaves de movimento")]
    public string bottomSimple1;
    public string bottomSimple2;
    public string bottomNormal;
    public string bottomSpecial;

    void Start()
    {

        animManager = GameObject.Find("AnimManager");

        bottomSimple1 = animManager.gameObject.GetComponent<ChooseAnim>().bottonSimple.ElementAt(0);
        bottomSimple2 = animManager.gameObject.GetComponent<ChooseAnim>().bottonSimple.ElementAt(1);
        bottomNormal = animManager.gameObject.GetComponent<ChooseAnim>().bottonNormal.ElementAt(0);
        bottomSpecial = animManager.gameObject.GetComponent<ChooseAnim>().bottonSpecial.ElementAt(0);

        IsNetworkReady();

        
    }

    void ChangeScenario()
    {
        if (gameManager.score >= 5)
        {
            backGround1.gameObject.SetActive(true);
            backGround2.gameObject.SetActive(false);
            backGround3.gameObject.SetActive(false);
        }
        if (gameManager.score >= 10)
        {
            backGround1.gameObject.SetActive(false);
            backGround2.gameObject.SetActive(true);
            backGround3.gameObject.SetActive(false);
        }
        if (gameManager.score >= 50)
        {
            backGround1.gameObject.SetActive(false);
            backGround2.gameObject.SetActive(false);
            backGround3.gameObject.SetActive(true);
        }
    }


    public override void FixedUpdateNetwork()
    {
        if (player == null && Runner != null && Runner.IsRunning)
        {
            player = Runner.GetPlayerObject(Runner.LocalPlayer);
        }
        ChangeScenario();



    }

    private bool IsNetworkReady()
    {
       
        if (Object == null || !Object.IsValid || player == null)
        {
            Debug.LogWarning("Fusion ainda não spawnou este objeto ou o Player não foi sincronizado.");
            return false;
        }
        return true;
    }

    public void ClickMoveBaseFirst(Button botao)
    {


        if (Input.touchCount == 1 && buttonsMoveBase[0])
        {

            var spriteAtual = player.GetComponent<SpriteRenderer>();
            spriteAtual.sprite = sprites[0];
            gameManager.Rpc_GainPoints(1);
            playerAnimator.SetBool(bottomSimple1, true);
            playerAnimator.SetBool(bottomSimple2, false);
            playerAnimator.SetBool(bottomNormal, false);
            playerAnimator.SetBool(bottomSpecial, false);
            Debug.Log(bottomSimple1);
        }

    }

    public void ClickMoveBaseSecond()
    {


        var spriteAtual = player.GetComponent<SpriteRenderer>();
        if (spriteAtual != null)
        {
            spriteAtual.sprite = sprites[1];
        }
        if (buttonMoveSpecial)
        {
            spriteAtual.sprite = sprites[1];
        }
        gameManager.Rpc_GainPoints(5);


    }

    public void ClickMoveBaseThird()
    {
        if (buttonsMoveBase[2])
        {

            var spriteAtual = player.GetComponent<SpriteRenderer>();
            if (spriteAtual != null)
            {
                spriteAtual.sprite = sprites[2];
            }
            if (buttonMoveSpecial)
            {
                spriteAtual.sprite = sprites[2];
            }
            gameManager.Rpc_GainPoints(5);

        }

    }

    public void ClickMoveBaseSpecial(Button botao)
    {

        var spriteAtual = player.GetComponent<SpriteRenderer>();
        if (spriteAtual != null)
        {
            spriteAtual.sprite = sprites[3];
        }
        if (buttonMoveSpecial)
        {
            spriteAtual.sprite = sprites[3];
        }
        gameManager.Rpc_GainPoints(10);
    }


}
