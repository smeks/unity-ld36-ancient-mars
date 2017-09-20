using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusMessageController : MonoBehaviour {

    public Text textBox;
    public string startText;

    private AudioSource textBoxAudioSource;

    // Use this for initialization
    void Start () {
        textBoxAudioSource = textBox.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            textBox.text = "";
            StartCoroutine(AnimateText(startText));
        }
    }

    IEnumerator AnimateText(string strComplete)
    {
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1.0f);
        textBox.text = "";
        int i = 0;
        while (i < strComplete.Length)
        {
            textBox.text += strComplete[i++];
            textBoxAudioSource.Play();
            yield return new WaitForSeconds(0.05F);
        }
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        while (textBox.color.a > 0)
        {
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a - 0.1f);
            yield return new WaitForSeconds(0.1F);
        }
        textBox.text = "";
    }
}
