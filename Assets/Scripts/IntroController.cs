using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {

    public Sprite[] introImages;
    public GameObject introSprite;
    private string intro1 = "The year is 2020.  Donald Trump is the world president.  Planet earth is a place we could never of imagined growing up.  The middle class has completly dissapeared and the planet is now ran by mega corporations.  99% of the human race is enslaved to the corporate machines.";
    private string intro2 = "The megacorp WASA has discovered a new subspace communication technology.  The human race might have hope with this newfound technology.";
    private string intro3 = "3 months after the subspace technology is discovered the first subspace communication satelite was launched into space.  A weak subspace signal is discovered under the surface of Mars.";
    private string intro4 = "Donald Trump, the president of the World gives a speech.  \"We choose to go to mars. We choose to go to mars and do the other things, not because they are easy, but because they are hard.\"";
    private string intro5 = "The Mars mission to find the Mars beacon is assembled.  You are amongst the crew to travel to Mars and uncover the signal.";

    private Text textBox;

    private AudioSource audioSource;

    // Use this for initialization
    IEnumerator Start () {
        textBox = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        
        var spriteRenderer = introSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = introImages[0];
        textBox.text = "";
        StartCoroutine(AnimateText(intro1));

        yield return new WaitForSeconds(10.0F);
        spriteRenderer.sprite = introImages[1];
        textBox.text = "";
        StartCoroutine(AnimateText(intro2));

        yield return new WaitForSeconds(10.0F);
        textBox.text = "";
        StartCoroutine(AnimateText(intro3));

        yield return new WaitForSeconds(10.0F);
        spriteRenderer.sprite = introImages[2];
        textBox.text = "";
        StartCoroutine(AnimateText(intro4));

        yield return new WaitForSeconds(10.0F);
        spriteRenderer.sprite = introImages[3];
        textBox.text = "";
        StartCoroutine(AnimateText(intro5));

        yield return new WaitForSeconds(10.0F);
        Application.LoadLevel("crash");
    
    }
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        while (i < strComplete.Length)
        {
            textBox.text += strComplete[i++];
            audioSource.Play();
            yield return new WaitForSeconds(0.01F);
        }
    }
}
