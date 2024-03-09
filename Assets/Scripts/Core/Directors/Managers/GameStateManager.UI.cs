using Core.UI.Rewards;
using PrimeTween;
using UnityEngine;

namespace Core.Directors.Managers {
    public partial class GameStateManager : MonoBehaviour {
        [Header("Reward UI")]
        public CanvasGroup SwapMenuCanvas;
        public RewardScreen rewardScreen;
        public CanvasGroup GameHUDCanvas;
        public PlayerHUD playerHUD;
        public RewardGenerator DefaultRewardGenerator;
        public float FadeDuration = 1;
        private Tween RewardFadeTween;
        private Tween HUDFadeTween;

        [ContextMenu("Show Reward Menu")]
        public void ShowSwapUI() {
            playerController.enabled = false;
            rewardScreen.LoadPlayerData(Player);
            SwapMenuCanvas.gameObject.SetActive(true);
            RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 0, 1, FadeDuration);

            // TODO Set levels on rewards
            rewardScreen.remainingTries = 3;
            rewardScreen.RewardPanel.SetRewards(DefaultRewardGenerator.GetRandomActions(), DefaultRewardGenerator.GetRandomModifiers());


            HUDFadeTween = Tween.Alpha(GameHUDCanvas, 1, 0, FadeDuration);
        }

        [ContextMenu("Hide Reward Menu")]
        public void HideSwapUI() {
            playerController.enabled = true;
            RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 1, 0, FadeDuration).OnComplete(() => SwapMenuCanvas.gameObject.SetActive(false));
            HUDFadeTween = Tween.Alpha(GameHUDCanvas, 0, 1, FadeDuration);
        }

    }
}