using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AnimatedTextController : MonoBehaviour {
 
	public TextMeshProUGUI m_MyText;
	string[] sentenceArray = {"Beans, beans let me give you a hand - or two.", "I've to prepare Dr. Robotnik a beautiful bean feast.", "Come to Arms my beauties."};
	public string sentence1  = "Beans, beans let me give you a hand - or two.";
	public string sentence2 = "I've to prepare Dr. Robotnik a beautiful bean feast.";
	public string sentence3 = "Come to Arms my beauties.";
	public GameObject preGameObject;
    Dictionary<int, string> dictionary = new Dictionary<int, string> { 
   { 0, "string" }, 
   { 1, "string2" }, 
   { 2, "string3" } };
	
	void Start(){
		StartCoroutine (addChar (0.08f));
	}
	
	IEnumerator addChar(float yieldTime){
		foreach(string sentence in sentenceArray){
        if(sentence.Length < 30){
            GameObject.Find("textbubble_large").GetComponent<Renderer>().enabled = false;
            GameObject.Find("textbubble").GetComponent<Renderer>().enabled = true;
        }
        else{
            GameObject.Find("textbubble_large").GetComponent<Renderer>().enabled = true;
            GameObject.Find("textbubble").GetComponent<Renderer>().enabled = false;
        }
		char[] textArray = sentence.ToCharArray();
		foreach (char singleLetter in textArray) {
            Debug.Log("Letter..."+singleLetter);
            
			if(m_MyText){
				m_MyText.text = m_MyText.text + singleLetter;
				yield return new WaitForSeconds(yieldTime);
			}
		}
		yield return new WaitForSeconds(2f);
		if(m_MyText)
			m_MyText.text = "";
		}
		if(m_MyText){
			Destroy(m_MyText);
			preGameObject.GetComponent<PreGameController>().moveCamera();
		}
	}
 }