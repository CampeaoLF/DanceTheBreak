using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(UpdateScore))] public int score { get; set; }
    [SerializeField] public GameObject background;
    [SerializeField] public TextMeshProUGUI scoreNumber;
    [SerializeField] public Button special;


    void Start()
    {

    }

    void UpdateScore()
    {
        scoreNumber.text = score.ToString();
    }



    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
            return;


        EnabledMoveSpecial();
        UpdateScore();
    }


    private void EnabledMoveSpecial()
    {
        if (score < 10) return;
        ButtonScript buttonScript = FindAnyObjectByType<ButtonScript>();
        if (!buttonScript && !buttonScript.buttonMoveSpecial) return;
        buttonScript.buttonMoveSpecial.gameObject.SetActive(true);
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_GainPoints(int value)
    {
        score = score + value;
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_Special()
    {
        var buttonSpecial = special.GetComponent<Button>();
        if (score >= 10)
        {
            buttonSpecial.interactable = true;
        }
        else
        {
            buttonSpecial.interactable = false;
        }
    }
}
