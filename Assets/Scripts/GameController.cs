using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController_ : MonoBehaviour
{
    private List<GameObject> gameObjects = new List<GameObject>();
    public bool Isbump;
    private bool Lock;
    public int N = 0;
    public Text score_Text;
    // Start is called before the first frame update
    void Start()
    {
        Isbump=false;
        Lock=false;
        foreach (Transform child in transform)
        {
            gameObjects.Add(child.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        score_Text.text= N.ToString();
        if (Isbump&& Lock==false)
        {
            Lock=true;
            Invoke("reset", 5);
        }      
    }
    private void reset()
    {
       
        foreach (Transform child in transform)
        {    
            child.gameObject.GetComponent<score>().reset();
        }
        Lock = false;
        Isbump=false;
    }
}
