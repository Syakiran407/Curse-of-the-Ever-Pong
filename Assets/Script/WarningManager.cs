using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningManager : MonoBehaviour
{
    public static WarningManager instance = null;

    public TMP_Text warningText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetText();
    }

    public void ChangeText(string _string)
    {
        warningText.text = _string;
        StartCoroutine(DelayedKill());
    }

    IEnumerator DelayedKill()
    {
        yield return new WaitForSeconds(3f);
        ResetText();
    }

    public void ResetText()
    {
        warningText.text = "";
    }

}
