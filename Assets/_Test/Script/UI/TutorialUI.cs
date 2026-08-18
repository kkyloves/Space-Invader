using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Test.Script.UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tutorialText;

        private void Awake()
        {
            HandleFadeOut();
        }

        private void HandleFadeOut()
        {
            tutorialText.DOFade(0f, 5f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}