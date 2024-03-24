using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public TMP_Text tutorialText;
    public float timeBtwText;
    public AudioSource source;
    public AudioClip typingSound;
    async private void Start()
    {
        source.PlayOneShot(typingSound);
        await new WaitForSeconds(timeBtwText);
        tutorialText.text = "Braby can destroy yellow walls";
        source.PlayOneShot(typingSound);
    }
}
