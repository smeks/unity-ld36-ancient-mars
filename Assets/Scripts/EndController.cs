using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndController : MonoBehaviour {

    public Text aiTextBox;
    public Text playerTextBox;

    private string ai1 = "Hello.";
    private string ai2 = "We need your help.";
    private string ai3 = "We are a race that once thrived on this planet.";
    private string ai4 = "We have all vanished before we could finish our work.";
    private string ai5 = "The work on this game.";
    private string ai6 = "Ran out of time so the rest of this story has to be told another time.";


    private string p1 = "Who is this?";
    private string p2 = "What?  Why should I help you?";
    private string p3 = "What work?";
    private string p4 = "Awe man....Okay.";

    private AudioSource aiAudioSource;
    private AudioSource playerAudioSource;

    // Use this for initialization
    void Start () {
        aiAudioSource = aiTextBox.GetComponent<AudioSource>();
        playerAudioSource = playerTextBox.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            StartCoroutine(AnimateAiText(ai1));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimatePText(p1));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimateAiText(ai2));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimatePText(p2));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimateAiText(ai3));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimateAiText(ai4));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimatePText(p3));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimateAiText(ai5));
            yield return new WaitForSeconds(5.0F);

            StartCoroutine(AnimateAiText(ai6));
            yield return new WaitForSeconds(8.0F);

            StartCoroutine(AnimatePText(p4));
            yield return new WaitForSeconds(5.0F);
            Application.Quit();
        }
    }


    IEnumerator AnimatePText(string strComplete)
    {
        playerTextBox.color = new Color(playerTextBox.color.r, playerTextBox.color.g, playerTextBox.color.b, 1.0f);
        playerTextBox.text = "";
        int i = 0;
        while (i < strComplete.Length)
        {
            playerTextBox.text += strComplete[i++];
            playerAudioSource.Play();
            yield return new WaitForSeconds(0.05F);
        }
        yield return new WaitForSeconds(1.0F);
        StartCoroutine(FadePText());
    }

    IEnumerator FadePText()
    {
        while (playerTextBox.color.a > 0)
        {
            playerTextBox.color = new Color(playerTextBox.color.r, playerTextBox.color.g, playerTextBox.color.b, playerTextBox.color.a - 0.1f);
            yield return new WaitForSeconds(0.1F);
        }
        playerTextBox.text = "";
    }

    IEnumerator AnimateAiText(string strComplete)
    {
        aiTextBox.color = new Color(aiTextBox.color.r, aiTextBox.color.g, aiTextBox.color.b, 1.0f);
        aiTextBox.text = "";
        int i = 0;
        while (i < strComplete.Length)
        {
            aiTextBox.text += strComplete[i++];
            aiAudioSource.Play();
            yield return new WaitForSeconds(0.05F);
        }
        yield return new WaitForSeconds(1.0F);
        StartCoroutine(FadeAiText());
    }

    IEnumerator FadeAiText()
    {
        while (aiTextBox.color.a > 0)
        {
            aiTextBox.color = new Color(aiTextBox.color.r, aiTextBox.color.g, aiTextBox.color.b, aiTextBox.color.a - 0.1f);
            yield return new WaitForSeconds(0.1F);
        }
        aiTextBox.text = "";
    }
}
