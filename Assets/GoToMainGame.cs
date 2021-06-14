using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainGame : MonoBehaviour
{
    public TMPro.TMP_Text Text;
    public string text;
    private int current_ind=0;
    void Start()
    {
        StartCoroutine(WriteText());
        text= text.Replace("\\n", "\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WriteText()
    {
        while (text!=Text.text)
        {
            Text.text += text[current_ind];
            current_ind++;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("1_Game");
    }
}
