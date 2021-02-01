using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct DialogueInfo
{
    public Sprite speakerImage;
    public string speakerName;
    [TextArea(3, 3)]
    public string dialogue;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueInfo> dialogues;
}
