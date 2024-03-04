using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.UI.Rewards;
using PrimeTween;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public float FadeDuration = 1;
    public CanvasGroup SwapMenuCanvas;
    public RewardScreen rewardScreen;
    private Tween RewardFadeTween;


    public CanvasGroup GameHUDCanvas;
    public PlayerHUD playerHUD;
    public AbilityManager Player;
    public PlayerController playerController;
    private Tween HUDFadeTween;


    [ContextMenu("Show Reward Menu")]
    public void ShowSwapUI() {
        playerController.enabled = false;
        rewardScreen.LoadPlayerData(Player);
        SwapMenuCanvas.gameObject.SetActive(true);
        RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 0, 1, FadeDuration);

        HUDFadeTween = Tween.Alpha(GameHUDCanvas, 1, 0, FadeDuration);
    }

    [ContextMenu("Hide Reward Menu")]
    public void HideSwapUI() {
        playerController.enabled = true;
        RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 1, 0, FadeDuration).OnComplete(() => SwapMenuCanvas.gameObject.SetActive(false));
        HUDFadeTween = Tween.Alpha(GameHUDCanvas, 0, 1, FadeDuration);
    }
}
