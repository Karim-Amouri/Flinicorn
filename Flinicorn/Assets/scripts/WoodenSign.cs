using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodenSign : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private QuestionsManager QcmManager;
    [SerializeField] private JoyButton open;

    public int index;
    //public bool Visited = false;
    
    private bool InRange = false;

    // Update is called once per frame
    void Update()
    {
        if (InRange && open.Pressed)
        {
            popup.SetActive(true);
            QcmManager.CurrentIndex = index;
            QcmManager.Assigne();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") InRange=true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InRange = false;
            //yield return new WaitForSeconds(2f);
            //popup.SetActive(false);
        }
    }
}
