using UnityEngine;
using TMPro;

public class InitialPopup : MonoBehaviour
{
    [SerializeField] private QuestionsManager QCMs;
    [SerializeField] private TextMeshProUGUI Geo, Lang, Math;

    void Start()
    {
        Geo.text = QCMs.themeNumber[0].ToString();
        Lang.text = QCMs.themeNumber[1].ToString();
        Math.text = QCMs.themeNumber[2].ToString();
    }
}