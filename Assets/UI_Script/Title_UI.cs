using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_UI : MonoBehaviour
{
    GameObject pf;
    Text stText;

    private bool Story1;
    private int first;

    private TextAsset storyText1;
    private TextAsset storyText2;

    void Start()
    {
        pf = GameObject.Find("Panel_Fade");
        pf.SetActive(false);
        stText = GameObject.Find("Story_Text").GetComponent<Text>();
        Story1 = false;
        first = 0;
        storyText1 = Resources.Load("Story") as TextAsset;
        storyText2 = Resources.Load("Story2") as TextAsset;
    }

    void Update()
    {
        Story2();

        SceneChange();
    }

    public void Start_Button()
    {
        pf.SetActive(true);
        Image Fade = GameObject.Find("Panel_Fade").GetComponent<Image>();
        StartCoroutine("Fade", Fade);


    }

    private void Story2()
    {
        if (Story1)
        {
            if (Input.GetMouseButton(0) && first == 1)
            {
                first++;
                StartCoroutine("Story_Text", storyText2);
            }
        }
    }

    IEnumerator Fade(Image Fade)
    {
        float Count = 0;

        while(Count < 1.0f)
        {
            Count += 0.01f;
            yield return new WaitForSeconds(0.01f);
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, Count);
        }
        StartCoroutine("Story_Text", storyText1);
    }

    IEnumerator Story_Text(TextAsset storyText)
    {
        string[] stReaders = storyText.text.Split("\n");
        stText.text = "";

        for (int i = 0; i < stReaders.Length; i++)
        {
            for (int j = 0; j < stReaders[i].Length; j++)
            {
                stText.text += stReaders[i].Substring(j, 1);
                yield return new WaitForSeconds(0.05f);
            }
            stText.text += "\n";
        }
        first++;
        Story1 = true;

        yield return new WaitForSeconds(0.5f);
        StopCoroutine("Story_Text");
    }

    public void SceneChange()
    {
        if(Input.GetMouseButton(0) && first == 3)
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
