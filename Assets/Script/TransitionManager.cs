using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance = null;
    private Animator thisAnimator;



    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

    }

    void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }


    public void RunCommand(int _index)
    {
        switch (_index)
        {
            case 1: //Go to main game
                StartCoroutine(GoToScene(1, 1));
                break;

            case 2: //Go to title screen
                StartCoroutine(GoToScene(0, 1));
                break;

            case 3: //Death
                thisAnimator.SetTrigger("Death");
                break;

            default:
                Debug.Log("Error Command");
                break;
        }
    }

    public void ChangeScene(int _index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_index);
    }

    IEnumerator GoToScene(int _index, float _changeSpeed)
    {
        yield return new WaitForSeconds(_changeSpeed);
        ChangeScene(_index);
    }




}
