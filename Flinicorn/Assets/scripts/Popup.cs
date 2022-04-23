using System.Collections;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject Pop;
    [SerializeField] private QuestionsManager QcmManager;

    private Animator anim;
    private bool state;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        state = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        anim.SetBool("state", state);

    }
    public void BTrigger()
    {
        state = false;
        StartCoroutine(Retard());
        //QcmManager.Unassign();
        //Pop.SetActive(false);
    }
    private IEnumerator Retard()
    {
        yield return new WaitForSeconds(0.45f);
        state = true; 
        QcmManager.Unassign();
        Pop.SetActive(false);
        //Debug.Log("hi");
    }
}
