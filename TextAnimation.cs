using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextAnimation : MonoBehaviour
{
    Text txt;
	string story;

	public AudioClip typewritter;

	[Space(10)]
	[Header("Important Values (DO NOT CHANGE UNLESS NEEDED)")]

	[Range(0.035f, 0.01f)]
    public float speed;

	public int currentline = 0;
    public List<string> linesOfText = new List<string>();

	[Space(10)]
	[Header("Extras")]

	[SerializeField]
	public static bool canInput = false;
	public int scene;
	public GameObject CEA;
	public int endingLine;

	void Start()
	{
		CallPlayText();
	}

	void CallPlayText() 
	{
		txt = GetComponent<Text>();
		story = linesOfText[currentline];
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine ("PlayText");
	}

	IEnumerator PlayText()
	{
		foreach (char c in story) 
		{
			txt.text += c;
			gameObject.GetComponent<AudioSource>().PlayOneShot(typewritter, 0.7f);
			yield return new WaitForSeconds (speed);
		}
	}

    void Update() 
    {
        if(OVRInput.GetDown(OVRInput.Button.Two))
        {
			if(currentline == endingLine)
				CallChangeScene();
			else
				ChangeLine();
        }

		if(txt.text == linesOfText[currentline])
			canInput = true;
		else
			canInput = false;

    }

	void ChangeLine()
	{
		if(canInput == true)
			{
            	currentline += 1;
            	CallPlayText();
				Debug.Log(currentline);
			}
	}

	void CallChangeScene()
	{
		StartCoroutine(ChangeScene());
	}

	IEnumerator ChangeScene()
	{
		CEA.GetComponent<OVRScreenFade>().FadeOut();
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(scene);
	}
}
