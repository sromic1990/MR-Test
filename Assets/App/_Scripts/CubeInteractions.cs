using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteractions : MonoBehaviour, IFocusable, IInputClickHandler, ISpeechHandler
{
    public Color NormalColor, HighlightColor;
    private Renderer myRenderer;
    private Material myMaterial;
    private Vector3 UpdatedRotation, initialRotation;

    bool canRotate = false;


    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        myMaterial = myRenderer.material;
        initialRotation = gameObject.transform.localRotation.eulerAngles;
    }

    void Update()
    {
        if(canRotate)
        {
            //Rotate
            UpdatedRotation = Vector3.zero;
            UpdatedRotation.x = 1;
            transform.rotation *= Quaternion.Euler(UpdatedRotation);
        }
    }

    public void OnFocusEnter()
    {
        myMaterial.color = HighlightColor;
    }

    public void OnFocusExit()
    {
        myMaterial.color = NormalColor;
    }

    private void OnDestroy()
    {
        Destroy(myMaterial);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Clicked on "+gameObject.name);
        StartStopRotation();
    }

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        Debug.Log("speech is " + eventData.RecognizedText.ToLower());
        switch(eventData.RecognizedText.ToLower())
        {
            case "rotate":
                StartStopRotation();
                break;
        }
    }

    public void StartStopRotation()
    {
        canRotate = !canRotate;
    }
}
