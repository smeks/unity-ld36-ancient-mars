using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StructureController : MonoBehaviour {

    public Text textBox;
    public Vector2 cameraOffset;
    public float cameraOrtho;
    public string structureFoundText = "Distress Signal Source Found...";
    private AudioSource audioSource;
    private AudioSource textBoxAudioSource;
    private bool found;
    

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        textBoxAudioSource = textBox.GetComponent<AudioSource>();
        found = false;
    }

    // Update is called once per frame
    void Update () {
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter" + other.tag);
        if (other.gameObject.tag == "Player" && !found)
        {
            other.gameObject.GetComponent<PlayerController>().nearStructure = true;
            other.gameObject.GetComponent<PlayerController>().cameraOffsetNearStructure = cameraOffset;
            other.gameObject.GetComponent<PlayerController>().cameraOrthoNearStructure = cameraOrtho;
            textBox.text = "";
            found = true;
            StartCoroutine(AnimateText(structureFoundText));
            audioSource.Play();
            yield return new WaitForSeconds(5.0F);
            StartCoroutine(FadeText());
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit" + other.tag);
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().nearStructure = false;
        }
    }


    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        while (i < strComplete.Length)
        {
            textBox.text += strComplete[i++];
            textBoxAudioSource.Play();
            yield return new WaitForSeconds(0.1F);
        }
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
