using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuestionsManager : MonoBehaviour
{
    private ListQCMs list;
    private bool[] Visited = new bool[9];

    public int CurrentIndex;

    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI Answer1, Answer2, Answer3, Answer4;
    [SerializeField] private GameObject button1, button2, button3, button4;

    [SerializeField] private GameObject LosePopup;
    [SerializeField] private GameObject lastPopup;
    [SerializeField] private GameObject finalPopup;

    private int QuestionsNumber = 9;
    public int[] themeNumber;

    private Color color;
    private bool test = false;

    private int intScore = 0;
    [SerializeField] private TextMeshProUGUI textScore;

    //questions number update:
    private int[] available = new int[3];
    public int[] score = new int[3];

    void Awake()
    {
        string path,path0,path1,path2;
        string jsonString;

        for (int i=0; i < 3; i++) score[i] = 0;
        themeNumber = new int[3];
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID == 1)
        {
            themeNumber[0] = 3;
            themeNumber[1] = 3;
            themeNumber[2] = 3;
        }
        else
        {
            path = Application.dataPath + "/Data/Theme Number.json";
            jsonString = File.ReadAllText(path);
            ThemeNumberUpdate themeNumberUpdate = JsonUtility.FromJson<ThemeNumberUpdate>(jsonString);
            themeNumber[0] = themeNumberUpdate.geographie;
            themeNumber[1] = themeNumberUpdate.grammar;
            themeNumber[2] = themeNumberUpdate.math;
        }
        switch (sceneID)
        {
            case 1:
                path0 = "/Data/Geographie.json";
                path1 = "/Data/Grammaire.json";
                path2 = "/Data/Math.json";
                break;
            case 2:
                path0 = "/Data/Geographie1.json";
                path1 = "/Data/Grammaire1.json";
                path2 = "/Data/Math1.json";
                break;
            case 3:
                path0 = "/Data/Geographie2.json";
                path1 = "/Data/Grammaire2.json";
                path2 = "/Data/Math2.json";
                break;
            case 4:
                path0 = "/Data/Geographie3.json";
                path1 = "/Data/Grammaire3.json";
                path2 = "/Data/Math3.json";
                break;
            default:
                path0 = "/Data/Geographie.json";
                path1 = "/Data/Grammaire.json";
                path2 = "/Data/Math.json";
                break;
        }
        //for (int i = 0; i < 3; i++) { Debug.Log(themeNumber[i]); }
        list = new ListQCMs();

        path = Application.dataPath + path0;
        jsonString = File.ReadAllText(path);
        Debug.Log(jsonString);


        ListQCMs listGeo = JsonUtility.FromJson<ListQCMs>(jsonString);
        //listGeo.Shuffle();

        for (int i = 0; i < themeNumber[0]; i++)
        {
            Debug.Log(i);
            Debug.Log(listGeo.QCMs.Count);
            listGeo[i].type = 0;
            Debug.Log(listGeo[i]);
            list.Add(listGeo[i]);
        }

        path = Application.dataPath + path1;
        jsonString = File.ReadAllText(path);

        ListQCMs listGrammar = JsonUtility.FromJson<ListQCMs>(jsonString);
        listGrammar.Shuffle();

        for (int i = 0; i < themeNumber[1]; i++)
        {
            listGrammar[i].type = 1;
            list.Add(listGrammar[i]);
        }
        path = Application.dataPath + path2;
        jsonString = File.ReadAllText(path);

        ListQCMs listMath = JsonUtility.FromJson<ListQCMs>(jsonString);
        listMath.Shuffle();

        for (int i = 0; i < themeNumber[2]; i++)
        {
            listMath[i].type = 2;
            list.Add(listMath[i]);
        }
        list.Shuffle();
        
        color = button1.GetComponent<Image>().color;
    }

    void Update()
    {
        textScore.text = intScore.ToString();

        if (Visited[8] && !test)
        {
            if (intScore > QuestionsNumber / 2) NextLevel();
            else
            {
                Debug.Log("You lose"); LosePopup.SetActive(true);
            }
        }
    }
    public void Assigne()
    {
        question.text = list[CurrentIndex].Question;
        Answer1.text = list[CurrentIndex].reponse1;
        Answer2.text = list[CurrentIndex].reponse2;
        Answer3.text = list[CurrentIndex].reponse3;
        Answer4.text = list[CurrentIndex].reponse4;

        if (Visited[CurrentIndex])
        {
            AnswerChoice(1, button1);
            AnswerChoice(2, button2);
            AnswerChoice(3, button3);
            AnswerChoice(4, button4);
        }
    }
    public void Unassign()
    {
        question.text = "";
        Answer1.text = "";
        Answer2.text = "";
        Answer3.text = "";
        Answer4.text = "";

        button1.GetComponent<Image>().color = color;
        button2.GetComponent<Image>().color = color;
        button3.GetComponent<Image>().color = color;
        button4.GetComponent<Image>().color = color;
    }
    public void AnswerChoice(int n)
    {
        GameObject selButton = GameObject.Find(EventSystem.current.currentSelectedGameObject.name);
        if (!Visited[CurrentIndex])
        {
            if (n == list[CurrentIndex].ReponseCorrecte)
            {
                intScore++;
                score[list[CurrentIndex].type]++;

                Debug.Log("Correcte");
            }
            else
            {
                Debug.Log("non correcte");
            }
            Visited[CurrentIndex] = true;
        }
        AnswerChoice(1, button1);
        AnswerChoice(2, button2);
        AnswerChoice(3, button3);
        AnswerChoice(4, button4);

    }
    public void AnswerChoice(int n, GameObject button)
    {
        if (n == list[CurrentIndex].ReponseCorrecte) { button.GetComponent<Image>().color = Color.green; }
        else { button.GetComponent<Image>().color = Color.red; }
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");
        for (int i = 0; i < 3; i++) { Debug.Log("Score: " + score[i]); }
        /*
            score[0] = 3;
            score[1] = 2;
            score[2] = 1;
        */
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("(themeNumber[i] == QuestionsNumber / 3) = " + (themeNumber[i] == QuestionsNumber / 3));
            //Debug.Log("themeNumber[i] = " + themeNumber[i]);
            //Debug.Log("QuestionsNumber / 3 = " + QuestionsNumber );
            //Debug.Log("themeNumber = " + themeNumber);
            if (themeNumber[i] == QuestionsNumber / 3f)
            {
                if (score[i] == QuestionsNumber / 3f) { available[i] = 2; }
                else if (score[i] > QuestionsNumber / 6f) available[i] = 1;
                else if (score[i] == QuestionsNumber / 6f) available[i] = 0;
                else if (score[i] <= QuestionsNumber / 12f) available[i] = -2;
                else available[i] = -1;
            }
            else if (themeNumber[i] == QuestionsNumber / 9f)
            {
                if (score[i] == 0) available[i] = -2;
                else if (score[i] < QuestionsNumber / 9f) available[i] = -1;
                else available[i] = 0;
            }
            else if (themeNumber[i] > QuestionsNumber / 3f)
            {
                if (score[i] > QuestionsNumber / 3f) available[i] = 2;
                else if (score[i] > QuestionsNumber / 6f) available[i] = 1;
                else if (score[i] == QuestionsNumber / 6f) available[i] = 0;
                else if (score[i] <= QuestionsNumber / 12f) available[i] = -2;
                else available[i] = -1;
            }
            else if (themeNumber[i] < QuestionsNumber / 3f)
            {
                if (score[i] > themeNumber[i] / 2f) available[i] = 1;
                else if (score[i] == themeNumber[i] / 6f) available[i] = 0;
                else if (score[i] <= QuestionsNumber / 12f) available[i] = -2;
                else available[i] = -1;
            }
        }
        for (int i = 0; i < 3;i++)
        {
            //Debug.Log("available[" + i + "] < 0 = " + (available[i] < 0));
            if (available[i] < 0)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((available[j] > 0) && (available[i] < 0))
                    {
                        available[j]--;
                        available[i]++;
                        themeNumber[i]++;
                        themeNumber[j]--;
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    if ((available[j] > 0) && (available[i] < 0)) { i--; break; }
                }
            }
        }
        ThemeNumberUpdate aux = new ThemeNumberUpdate();
        aux.geographie = themeNumber[0];
        aux.grammar = themeNumber[1];
        aux.math = themeNumber[2];

        File.WriteAllText(Application.dataPath + "/Data/Theme Number.json", JsonUtility.ToJson(aux));
        for (int i = 0; i < 3; i++) { Debug.Log("Score: " + score[i]); }
        for (int i = 0; i < 3; i++) { Debug.Log("avai: " + available[i]); }
        for (int i = 0; i < 3; i++) { Debug.Log("Final: " + themeNumber[i]); }
        test = true;

        lastPopup.GetComponent<LastPopup>().Assigne();
        lastPopup.SetActive(true);
    }
    public void NextLevelExecute()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    [System.Serializable]
    private class QCM
    {
        public string Question;
        public string reponse1;
        public string reponse2;
        public string reponse3;
        public string reponse4;
        public int ReponseCorrecte;
        public int type;
    }
    [System.Serializable]
    private class ListQCMs
    {
        public List<QCM> QCMs;
        public int type;  //  0 for Geo , 1 for Grammar , 2 for Math; 
        public IEnumerator GetEnumerator()
            { return QCMs.GetEnumerator(); }

        public ListQCMs() { QCMs = new List<QCM>(); }
        public void Shuffle()
        { 
            var rnd = new System.Random(); 
            QCMs = QCMs.OrderBy(item => rnd.Next()). ToList(); 
        }
        public QCM this[int index]
        { 
            get { return QCMs[index]; }
            set { QCMs[index] = value; }
        }
        public void Add(QCM value) { QCMs.Add(value); }
        //public int Count() {  return QCMs.Count(); }
    }
    [System.Serializable]
    private class ThemeNumberUpdate
    {
        public int geographie;
        public int grammar;
        public int math;
    }
}
