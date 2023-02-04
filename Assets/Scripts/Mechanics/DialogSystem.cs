using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private Text dialogText;
    [SerializeField] private Text nameText;
    [SerializeField] private Image characterImage;
    [SerializeField, Range(0, 1)] private float visibleTextPercent;
    [SerializeField] private float timePerLetter = 0.05f;

    private float totalTimeToType;
    private float currentTime;

    private string lineToShow;

    private DialogContainer currentDialog;
    private int currentTextLine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }

        TypeOutText();
    }

    private void TypeOutText()
    {
        if (visibleTextPercent >= 1) return;

        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp01(visibleTextPercent);
        UpdateText();
    }

    private void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
        dialogText.text = lineToShow[..letterCount];
    }

    public void Initialize(DialogContainer dialogContainer)
    {
        this.nameText.text = dialogContainer.Actor.Name;
        this.characterImage.sprite = dialogContainer.Actor.Portrait; 
        this.currentDialog = dialogContainer;
        this.gameObject.SetActive(true);
        currentTextLine = 0;
        CycleLine();
    }

    private void CycleLine()
    {
        lineToShow = currentDialog.Lines[currentTextLine];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0;
        visibleTextPercent = 0;
        dialogText.text = string.Empty;
        currentTextLine += 1;
    }

    private void PushText()
    {
        if (visibleTextPercent < 1)
        {
            visibleTextPercent = 1;
            UpdateText();
            return;
        }

        if (currentTextLine >= currentDialog.Lines.Count)
        {
            Conclude();
        }
        else
        {
            CycleLine();
        }
    }

    private void Conclude()
    {
        this.gameObject.SetActive(false);
    }
}
