using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrateController : MonoBehaviour {

    public Text textBox;
    public GameObject closedSprite;
    public GameObject openedSprite;
    public GameObject foundObject;
    public string foundText;
    private bool found = false;
    private AudioSource audioSource;
    private AudioSource textBoxAudioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        textBoxAudioSource = textBox.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            closedSprite.GetComponent<SpriteRenderer>().enabled = false;
            openedSprite.GetComponent<SpriteRenderer>().enabled = true;
            if (!found)
                other.gameObject.GetComponent<PlayerController>().AddInventory("rosetta");
            found = true;
            StartCoroutine(ShowObject());
            StartCoroutine(AnimateText(foundText));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !found)
        {
            audioSource.Play();
            closedSprite.GetComponent<SpriteRenderer>().enabled = true;
            openedSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator ShowObject()
    {
        int i = 0;
        while (i < 10)
        {
            i++;
            var currentColor = foundObject.GetComponent<SpriteRenderer>().color;
            var newColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 0.1f);
            foundObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return new WaitForSeconds(0.1F);
        }
        yield return new WaitForSeconds(3.0F);
        i = 0;
        while (i < 10)
        {
            i++;
            var currentColor = foundObject.GetComponent<SpriteRenderer>().color;
            var newColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - 0.1f);
            foundObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return new WaitForSeconds(0.1F);
        }
    }

    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1.0f);
        textBox.text = "";
        while (i < strComplete.Length)
        {
            textBox.text += strComplete[i++];
            textBoxAudioSource.Play();
            yield return new WaitForSeconds(0.1F);
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
    }
}
