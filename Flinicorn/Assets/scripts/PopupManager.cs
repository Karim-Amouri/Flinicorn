using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject InitialPopup;


    private void Start()
    {
        InitialPopup.SetActive(true);
    }
}
