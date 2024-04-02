using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clickSound;
    public Transform buttonObj; // to do on, off anim
    public GameObject wallOnButton;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        source.PlayOneShot(clickSound);
        buttonObj.position = new Vector3(buttonObj.position.x, -4.795f, buttonObj.position.z);
        wallOnButton.SetActive(false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        source.PlayOneShot(clickSound);
        buttonObj.position = new Vector3(buttonObj.position.x, -4.661f, buttonObj.position.z);
        wallOnButton.SetActive(true);
    }
}
