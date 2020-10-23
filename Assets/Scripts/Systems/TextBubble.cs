using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextBubble : MonoBehaviour
{
    public TextMeshPro textMesh;
    public UnityEvent onComplete = new UnityEvent();

    private int character;
    private string message;
    private float life;
    private Transform cam;

    public void Start()
    {
        cam = Camera.main.transform;
    }

    public void Update()
    {
        transform.LookAt(cam);
    }

    public void Play (string message, float speed, float life)
    {
        textMesh.text = "";
        textMesh.ForceMeshUpdate();
        this.message = message;
        this.life = life;
        StartCoroutine(Speak(1f / speed));
    }

    public void Complete ()
    {
        onComplete.Invoke();
    }

    public IEnumerator Speak (float wait)
    {
        yield return new WaitForSeconds(wait);

        if (string.IsNullOrEmpty(message))
        {
            StartCoroutine(Live(life));
        }

        else
        {
            character++;

            textMesh.text = message.Substring(0, Mathf.Min(character, message.Length));
            textMesh.ForceMeshUpdate();

            if (character >= message.Length)
            {
                StartCoroutine(Live(life));
            }

            else
            {
                StartCoroutine(Speak(wait));
            }
        }
    }

    public IEnumerator Live (float wait)
    {
        yield return new WaitForSeconds(wait);
        Complete();
    }
}
