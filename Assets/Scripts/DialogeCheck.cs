using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogeCheck : MonoBehaviour
{

    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI NpcText;
    public string[] lines;
    public float textSpeed;
    private int PIndex;
    private int Character;
    public int Dialoge;
    private String[] PlayerDialoge = {"Hello Phonic how have you been"};

    // Start is called before the first frame update
    void Start()
    {
        PlayerText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {

        };
    }

    void StartDialogue()
    {
        PIndex = 0;
        StartCoroutine(TypeLine());
    }
    void SwitchDialogueBox()
    {
        
    }
    
   IEnumerator TypeLine()
    {
        foreach (char c in PlayerDialoge[PIndex].ToCharArray())
        {
            PlayerText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
