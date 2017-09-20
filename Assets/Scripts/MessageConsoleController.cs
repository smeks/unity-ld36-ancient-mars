using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageConsoleController : MonoBehaviour {

    public GameObject encryptedMessageBackground;
    public GameObject encryptedMessage;
    public GameObject decryptedMessage;
    public string foundMessageText;
    public string rosettaText;
    public AudioClip consoleStartup;
    public AudioClip consoleClick;
    public Text textBox;
    public GameObject door;
    

    private AudioSource textBoxAudioSource;
    private AudioSource audioSource;
    private AudioSource doorAudioSource;
    private bool found = false;

    // Use this for initialization
    void Start () {
        textBoxAudioSource = textBox.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        doorAudioSource = door.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            var hasRosetta = collider.gameObject.GetComponent<PlayerController>().GetInventory("rosetta");

            StartCoroutine(TurnConsoleOn(hasRosetta));

            if (!found)
            {
                found = true;
                StartCoroutine(AnimateText(foundMessageText));
            }else if (hasRosetta)
            {
                doorAudioSource.Play();
                StartCoroutine(OpenDoor());
                StartCoroutine(AnimateText(rosettaText));
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            TurnConsoleOff();
        }
    }

    bool IsConsoleOn()
    {
        return encryptedMessageBackground.GetComponent<SpriteRenderer>().enabled;
    }

    IEnumerator OpenDoor()
    {
        for (int i = 0; i < 26; i++)
        {
            door.transform.Translate(0, 0.5f, 0);
            yield return new WaitForSeconds(0.1F);
        }
    }

    IEnumerator TurnConsoleOn(bool hasRosetta)
    {
        if (hasRosetta)
        {
            encryptedMessageBackground.GetComponent<SpriteRenderer>().enabled = true;
            encryptedMessage.GetComponent<MeshRenderer>().enabled = false;
            decryptedMessage.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            encryptedMessageBackground.GetComponent<SpriteRenderer>().enabled = true;
            encryptedMessage.GetComponent<MeshRenderer>().enabled = true;
            decryptedMessage.GetComponent<MeshRenderer>().enabled = false;
        }

        audioSource.clip = consoleStartup;
        audioSource.Play();
        yield return new WaitForSeconds(3.0F);
        audioSource.clip = consoleClick;
        audioSource.loop = true;
        audioSource.Play();
    }

    void TurnConsoleOff()
    {
        encryptedMessageBackground.GetComponent<SpriteRenderer>().enabled = false;
        encryptedMessage.GetComponent<MeshRenderer>().enabled = false;
        decryptedMessage.GetComponent<MeshRenderer>().enabled = false;
        audioSource.Stop();
    }


    IEnumerator AnimateText(string strComplete)
    {
        yield return new WaitForSeconds(3.0F);
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

    }
}
