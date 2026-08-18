using TMPro;
using UnityEngine;

namespace _Test.Script.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private LivesUI livesUI;
        public LivesUI LivesUI => livesUI;

        [SerializeField] private ScoreUI scoreUI;
        public ScoreUI ScoreUI => scoreUI;

        [SerializeField] private GameOverUI gameOverUI;
        public GameOverUI GameOverUI => gameOverUI;
    }
}
