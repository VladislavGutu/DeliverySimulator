using System;
using System.Collections;
using System.Collections.Generic;
using SBPScripts;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public GameObject[] _tutorialSteps;

    public int _currentStep = 0;

    public void NextStep()
    {
        _currentStep++;
        if (_currentStep >= _tutorialSteps.Length)
        {
            _currentStep = 0;
        }

        foreach (GameObject go in _tutorialSteps)
        {
            go.SetActive(false);
        }
        _tutorialSteps[_currentStep].SetActive(true);
    }

    public void PreviousStep()
    {
        _currentStep--;
        if (_currentStep < 0)
        {
            _currentStep = _tutorialSteps.Length - 1;
        }

        foreach (GameObject go in _tutorialSteps)
        {
            go.SetActive(false);
        }
        _tutorialSteps[_currentStep].SetActive(true);
    }

    public void CloseTutorial()
    {
        UIManager.instance.IsPause = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("FirstEnter",1);
    }
}
