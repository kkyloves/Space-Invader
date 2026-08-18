using _Test.Script.General;
using _Test.Script.UI;
using UnityEngine;
using VContainer;

namespace _Test.Script.Data
{
    public class GameData : MonoBehaviour
    {
        [SerializeField] private int maxPlayerLives = 5;
        private int playerScore;
        private int playerLives;
        public int PlayerLives => playerLives;
        private UIManager uiManager;
        private AudioManager audioManager;

        [Inject]
        public void Construct(UIManager uiManager, AudioManager audioManager)
        {
            this.uiManager = uiManager;
            this.audioManager = audioManager;
        }

        private void Awake()
        {
            playerLives = maxPlayerLives;
        }

        private void Start()
        {
            uiManager.LivesUI.UpdateText(playerLives.ToString());
            uiManager.ScoreUI.UpdateText(playerScore.ToString());
        }

        public void HandleDamage(int damage = 1)
        {
            playerLives -= damage;
            uiManager.LivesUI.UpdateText(playerLives.ToString());

            if (playerLives > 0) return;

            //spawn game over
            uiManager.GameOverUI.Open(playerScore, false);
        }

        public void HandleScore(int score, bool stillHasEnemy)
        {
            playerScore += score;
            uiManager.ScoreUI.UpdateText(playerScore.ToString());

            if (!stillHasEnemy)
            {
                //spawn game over
                uiManager.GameOverUI.Open(playerScore, true);
            }
        }
    }
}