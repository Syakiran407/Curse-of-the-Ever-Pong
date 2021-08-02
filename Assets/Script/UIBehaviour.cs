using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIBehaviour : MonoBehaviour
{

    public bool State = false;
    public bool RaycastState = false;
    public float FadeDuration;
    public KeyCode keycode;

    private bool Activated = false;

    //Audio Stuff
    public AudioClip SoundOnMouseEnter;

    private void Start()
    {
        State = !State;
        SwitchState();

    }

    private void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            if (TryGetComponent(out Button _button))
                _button.onClick.Invoke();
        }
    }



    public void FadeOut()
    {
        if (!Activated)
        {
            StartCoroutine(FadeOut(FadeDuration));
        }
    }

    public void Close()
    {
        State = false;
        foreach (Image obj in transform.GetComponentsInChildren<Image>())
        {
            Color _color = obj.GetComponent<Image>().color;
            _color.a = 0;
            obj.GetComponent<Image>().color = _color;
        }

        foreach (TMP_Text obj in transform.GetComponentsInChildren<TMP_Text>())
        {
            Color _color = obj.color;
            _color.a = 0;
            obj.color = _color;
        }
    }

    public void SwitchState()
    {
        State = !State;
        if (State == true)
        {
            foreach (Image obj in transform.GetComponentsInChildren<Image>())
            {
                Color _color = obj.GetComponent<Image>().color;
                _color.a = 1;
                obj.GetComponent<Image>().color = _color;
            }

            foreach (TMP_Text obj in transform.GetComponentsInChildren<TMP_Text>())
            {
                Color _color = obj.color;
                _color.a = 1;
                obj.color = _color;
            }

            foreach (Selectable obj in transform.GetComponentsInChildren<Selectable>())
            {
                obj.interactable = true;
            }

        }

        else
        {
            foreach (Image obj in transform.GetComponentsInChildren<Image>())
            {
                Color _color = obj.GetComponent<Image>().color;
                _color.a = 0;
                obj.GetComponent<Image>().color = _color;
            }

            foreach (TMP_Text obj in transform.GetComponentsInChildren<TMP_Text>())
            {
                Color _color = obj.color;
                _color.a = 0;
                obj.color = _color;
            }

            foreach (Selectable obj in transform.GetComponentsInChildren<Selectable>())
            {
                obj.interactable = false;
            }

        }

    }

    public void SwitchRayCast()
    {
        RaycastState = !RaycastState;
        if (RaycastState == true)
        {
            foreach (Image obj in transform.GetComponentsInChildren<Image>())
            {
                obj.GetComponent<Image>().raycastTarget = true;
            }
        }

        else
        {
            foreach (Image obj in transform.GetComponentsInChildren<Image>())
            {
                obj.GetComponent<Image>().raycastTarget = false;
            }
        }
    }


    public IEnumerator FadeOut(float FadeDuration)
    {
        float StartingTime = Time.time;
        Activated = true;
        while (Activated)
        {
            foreach (TMP_Text obj in transform.GetComponentsInChildren<TMP_Text>())
            {
                Color _color = obj.color;
                _color.a -= FadeDuration / 100;
                obj.color = _color;
            }

            foreach (Image obj in transform.GetComponentsInChildren<Image>())
            {
                Color _color = obj.GetComponent<Image>().color;
                _color.a -= FadeDuration / 100;
                obj.GetComponent<Image>().color = _color;
            }

            DisableButtonInChildren();
            DisableImageInChildren();

            yield return new WaitForSeconds(FadeDuration / 100);

            if (Time.time >= StartingTime + FadeDuration)
            {
                BreakMethod();
                break;
            }

        }

    }

    public void DisableImageInChildren()
    {
        foreach (Image obj in transform.GetComponentsInChildren<Image>())
        {
            Color _color = obj.GetComponent<Image>().color;
            _color.a -= FadeDuration / 100;
            obj.GetComponent<Image>().color = _color;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fadein(FadeDuration));
    }

    public IEnumerator Fadein(float FadeDuration)
    {
        float StartingTime = Time.time;
        Activated = true;
        while (Activated)
        {
            foreach (Image obj in transform.GetComponentsInChildren<Image>())
            {
                Color _color = obj.GetComponent<Image>().color;
                _color.a += FadeDuration / 100;
                obj.GetComponent<Image>().color = _color;
            }

            foreach (TMP_Text obj in transform.GetComponentsInChildren<TMP_Text>())
            {
                Color _color = obj.color;
                _color.a += FadeDuration / 100;
                obj.color = _color;
            }

            yield return new WaitForSeconds(FadeDuration / 100);

            if (Time.time >= StartingTime + FadeDuration)
                Activated = false;

        }

    }

    public void DisableButtonInChildren()
    {
        foreach (Button obj in transform.GetComponentsInChildren<Button>())
        {
            Color _color = obj.GetComponent<Image>().color;
            _color.a = 0;
            obj.GetComponent<Image>().color = _color;
            obj.interactable = false;
        }
    }




    public void BreakMethod()
    {
        StopAllCoroutines();
        Activated = false;
        gameObject.SetActive(false);
    }

}
