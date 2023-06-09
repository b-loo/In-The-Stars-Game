using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private bool isIntro;


    private int index;

    //Audio Playing On Every Character
    [Header("Audio")]
    private AudioClip dialogueTypingSoundClip; //unserialized field
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        isIntro = true;
        textComponent.text = string.Empty;
        StartDialogue();

        //audioSource = this.gameObject.AddComponent<AudioSource>();
        dialogueTypingSoundClip = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIntro)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        int counter = 0;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed/3);
            if (counter % 2 == 0)
            {
                audioSource.PlayOneShot(dialogueTypingSoundClip);
            }
            counter++;
           // yield return new WaitForSeconds(textSpeed / 3);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else
        {
            gameObject.SetActive(false);
            isIntro = false;
            DoorZero.SetClickTrue();
        }
    }
}
