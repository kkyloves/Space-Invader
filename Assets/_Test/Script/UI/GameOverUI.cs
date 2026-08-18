using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Test.Script.UI
{
    public class GameOverUI : MonoBehaviour
    {
        private const string WIN_LABEl = "YOU WON!";
        private const string LOSE_LABEl = "YOU LOSE!";
        [SerializeField] private GameObject root;
        [SerializeField] private Button tryAgainButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private TextMeshProUGUI youLabel;

        private void Awake()
        {
            root.SetActive(false);
        }

        private void OnEnable()
        {
            tryAgainButton.onClick.AddListener(HandleTryAgain);
            quitButton.onClick.AddListener(HandleQuit);
        }

        private void OnDisable()
        {
            tryAgainButton.onClick.RemoveListener(HandleTryAgain);
            quitButton.onClick.RemoveListener(HandleQuit);
        }

        private static void HandleTryAgain()
        {
            SceneManager.LoadScene(0);
        }

        private static void HandleQuit()
        {
            Application.Quit();
        }

        public void Open(int score, bool win)
        {
            root.SetActive(true);
            finalScoreText.text = $"FINAL SCORE: {score}";
            youLabel.text = win ? WIN_LABEl : LOSE_LABEl;
        }
    }
}