using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public TMP_Text text;
    public float timeBtwText;
    public AudioSource source;
    public AudioClip typingSound;
    public List<string> sentenceList;
    public GameObject tipText;
    public string loadScene;
    public bool isSciping = true;
    async private void Start()
    { 
        for (int i = 0; i < sentenceList.Count; i++)
        { 
            source.PlayOneShot(typingSound);
            text.text = sentenceList[i];
            await new WaitForSeconds(timeBtwText);
        }
    }
    private void Update()
    {
        if(isSciping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(loadScene);
            }

        }
    }

}
