using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class QuestionsManager : MonoBehaviour
{
    private ListQCMs list;
    private bool[] Visited = new bool[9];

    public int CurrentIndex;

    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI Answer1, Answer2, Answer3, Answer4;
    [SerializeField] private GameObject button1, button2, button3, button4;

    private int QuestionsNumber;
    private int[] themeNumber = new int[3];
    //themeNumber[0] = 3;
    //themeNumber[1] = 3;
    //themeNumber[2] = 3;

    private Color color;

    
    private int intScore = 0;
    [SerializeField] private TextMeshProUGUI textScore;

    //questions number update:
    private int[] available = new int[3] ;
    private int[] taken = new int[3] ;
    private int[] score = new int[3] ; 
    
    void Awake()
    {
        themeNumber[0] = 3;
        themeNumber[1] = 3;
        themeNumber[2] = 3;
        list = new ListQCMs();
        
        string path = Application.dataPath + "/Data/Geographie.json";
        string jsonString = File.ReadAllText(path);


        ListQCMs listGeo = JsonUtility.FromJson<ListQCMs>(jsonString);
        listGeo.Shuffle();

        for (int i = 0; i < themeNumber[0]; i++)
        {
            list.Add(listGeo[i]);
        }

        path = Application.dataPath + "/Data/grammaire.json";
        jsonString = File.ReadAllText(path);

        ListQCMs listGrammar = JsonUtility.FromJson<ListQCMs>(jsonString);
        listGrammar.Shuffle();

        for (int i = 0; i < themeNumber[1]; i++)
        {
            list.Add(listGrammar[i]);
        }
        path = Application.dataPath + "/Data/math.json";
        jsonString = File.ReadAllText(path);

        ListQCMs listMath = JsonUtility.FromJson<ListQCMs>(jsonString);
        listMath.Shuffle();

        for (int i = 0; i < themeNumber[2]; i++)
        {
            list.Add(listMath[i]);
        }
        list.Shuffle();



        color = button1.GetComponent<Image>().color;
    }

    void Update()
    {
        textScore.text = intScore.ToString();
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
        Debug.Log(CurrentIndex);
        if (!Visited[CurrentIndex])
        {
            if (n == list[CurrentIndex].ReponseCorrecte)
            {
                intScore ++;
                score [list.type]++;
                
                Debug.Log("Correcte");
            }
            else
            {
                Debug.Log("non correcte");
            }
            Visited[CurrentIndex] = true;
        }
        AnswerChoice(1,button1);
        AnswerChoice(2,button2);
        AnswerChoice(3,button3);
        AnswerChoice(4,button4);
            
    }
    public void AnswerChoice(int n, GameObject button)
    {
        if (n == list[CurrentIndex].ReponseCorrecte) { button.GetComponent<Image>().color = Color.green; }
        else { button.GetComponent<Image>().color = Color.red; }
    }
    public void NextLevel()
    {
        for (int i = 0; i > 3; i++)
        {
            if (themeNumber[i] == QuestionsNumber / 3)
            {
                if (score[i] == QuestionsNumber / 3) available[i] = 2;
                else if (score[i] > QuestionsNumber / 6) available[i] = 1;
                else if (score[i] == QuestionsNumber / 6) available[i] = 0;
                else if (score[i] <= QuestionsNumber / 12) available[i] = -2;
                else available[i] = -1;
            }
            else if (themeNumber[i] == QuestionsNumber / 9)
            {
                if (score[i] == 0) available[i] = -2;
                else if (score[i] < QuestionsNumber / 9) available[i] = -1;
                else available[i] = 0;
            }
            else if (themeNumber[i] > QuestionsNumber / 3)
            {
                if (score[i] > QuestionsNumber / 3) available[i] = 2;
                else if (score[i] > QuestionsNumber / 6) available[i] = 1;
                else if (score[i] == QuestionsNumber / 6) available[i] = 0;
                else if (score[i] <= QuestionsNumber / 12) available[i] = -2;
                else available[i] = -1;
            }
            else if (themeNumber[i] < QuestionsNumber / 3)
            {
                if (score[i] > themeNumber[i] / 2) available[i] = 1;
                else if (score[i] == themeNumber[i] / 6) available[i] = 0;
                else if (score[i] <= QuestionsNumber / 12) available[i] = -2;
                else available[i] = -1;
            }
        }
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

}
