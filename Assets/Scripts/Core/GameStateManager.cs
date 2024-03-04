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

    public int RoomsTraversed { get; private set; } = 0;


    public CanvasGroup GameHUDCanvas;
    public PlayerHUD playerHUD;
    public AbilityManager Player;
    public PlayerController playerController;
    private Tween HUDFadeTween;

    public RewardGenerator DefaultRewardGenerator;
    private System.Random random = new();

    private void OnEnable() {
        rewardScreen.OnChoicesFinished += OnRewardCompleted;
    }

    private void OnRewardCompleted() {
        HideSwapUI();
    }


    [ContextMenu("Show Reward Menu")]
    public void ShowSwapUI() {
        playerController.enabled = false;
        rewardScreen.LoadPlayerData(Player);
        SwapMenuCanvas.gameObject.SetActive(true);
        RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 0, 1, FadeDuration);

        // TODO Set levels on rewards
        rewardScreen.remainingTries = 3;
        rewardScreen.RewardPanel.SetRewards(DefaultRewardGenerator.GetRandomActions(random), DefaultRewardGenerator.GetRandomModifiers(random));


        HUDFadeTween = Tween.Alpha(GameHUDCanvas, 1, 0, FadeDuration);
    }

    [ContextMenu("Hide Reward Menu")]
    public void HideSwapUI() {
        playerController.enabled = true;
        RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 1, 0, FadeDuration).OnComplete(() => SwapMenuCanvas.gameObject.SetActive(false));
        HUDFadeTween = Tween.Alpha(GameHUDCanvas, 0, 1, FadeDuration);
    }
}
