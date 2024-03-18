using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    private RectTransform rect;
    public RectTransform rect_A;
    // Start is called before the first frame update
    void Start()
    {
        rect=GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        rect_A.sizeDelta = new Vector2(rect.rect.height / 5, rect.rect.height / 10);
        rect_A.position = new Vector3(rect_A.rect.width / 2, rect_A.rect.height / 2, 0);


    }
    public void quit()
    {
       
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
