using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Speech
{
    public string name;
    public List<string> messages;
    public UnityEvent onFinished;
    public bool canRepeat;
}

public class Dialogue : MonoBehaviour
{
    private bool isTalking;
    private int messageCounter;
    private Speech speech;
    private TextBubble textBubble;
    private List<int> history = new List<int>();

    public List<Speech> speeches;
    public GameObject textBubblePrefab;
    public Transform textBubbleLocation;
    public float textSpeed;
    public float bubbleLife;

    public void Talk (int id)
    {
        if (history.Contains(id))
        {
            if (!speeches[id].canRepeat) 
                return;
        }
        else
        {
            history.Add(id);
        }

        if (isTalking) StopTalking();

        isTalking = true;
        messageCounter = 0;
        speech = speeches[id];

        NextBubble();
    }

    public void NextBubble ()
    {
        if (textBubble)
        {
            Destroy(textBubble.gameObject);
            textBubble = null;
        }

        if (speech == null) { StopTalking(); return; }
        if (messageCounter >= speech.messages.Count) { StopTalking(); return; }

        GameObject newBubble = Instantiate(textBubblePrefab, textBubbleLocation) as GameObject;
        newBubble.transform.localPosition = Vector3.zero;
        textBubble = newBubble.GetComponent<TextBubble>();
        textBubble.Play(speech.messages[messageCounter], textSpeed, bubbleLife);
        textBubble.onComplete.AddListener(NextBubble);
        messageCounter++;
    }

    public void StopTalking ()
    {
        if (textBubble)
        {
            Destroy(textBubble.gameObject);
            textBubble = null;
        }

        isTalking = false;
        messageCounter = 0;

        speech.onFinished.Invoke();

        if (!isTalking) speech = null;
    }
}
