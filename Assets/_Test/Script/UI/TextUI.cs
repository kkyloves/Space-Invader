using TMPro;
using UnityEngine;

namespace _Test.Script.UI
{
    public class TextUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI uiText;

        public void UpdateText(string value)
        {
            uiText.text = value;
        }
    }
}
