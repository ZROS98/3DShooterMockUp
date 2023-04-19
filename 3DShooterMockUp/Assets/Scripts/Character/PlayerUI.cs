using System;
using ShooterMockUp.Player;
using ShooterMockUp.Utilities;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [field: Header(ProjectConstants.HEADER_REFERENCES)]
    [field: SerializeField]
    private TMP_Text PlayerStateText { get; set; }

    protected virtual void Start ()
    {
        UpdatePlayerStateText(PlayerState.IDLE);
    }

    public void UpdatePlayerStateText (PlayerState playerState)
    {
        PlayerStateText.text = playerState.ToString();
    }
}