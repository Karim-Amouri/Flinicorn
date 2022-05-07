using UnityEngine;
using TMPro;

public class LastPopup : MonoBehaviour
{
    [SerializeField] private QuestionsManager QCMs;
    [SerializeField] private TextMeshProUGUI Geo, Lang, Math;

    public void Assigne()
    {
        Geo.text = QCMs.score[0].ToString();
        Lang.text = QCMs.score[1].ToString();
        Math.text = QCMs.score[2].ToString();
    }
}
