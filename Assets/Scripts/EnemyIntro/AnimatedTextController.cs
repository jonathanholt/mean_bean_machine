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
   
   public Dictionary<char, string> fontDictionary = new Dictionary<char, string> { 
   {',',"<sprite=1>"},
{'.',"<sprite=2>"},
{'0',"<sprite=4>"},
{'1',"<sprite=5>"},
{'2',"<sprite=6>"},
{'3',"<sprite=7>"},
{'4',"<sprite=8>"},
{'5',"<sprite=9>"},
{'7',"<sprite=10>"},
{'8',"<sprite=11>"},
{'9',"<sprite=12>"},
{'A',"<sprite=13>"},
{'B',"<sprite=14>"},
{'C',"<sprite=15>"},
{'L',"<sprite=16>"},
{'K',"<sprite=17>"},
{'J',"<sprite=18>"},
{'I',"<sprite=19>"},
{'S',"<sprite=20>"},
{'T',"<sprite=21>"},
{'U',"<sprite=22>"},
{'V',"<sprite=23>"},
{'b',"<sprite=24>"},
{'c',"<sprite=25>"},
{'d',"<sprite=26>"},
{'e',"<sprite=27>"},
{'n',"<sprite=28>"},
{'m',"<sprite=29>"},
{'l',"<sprite=30>"},
{'k',"<sprite=31>"},
{'t',"<sprite=32>"},
{'u',"<sprite=33>"},
{'v',"<sprite=34>"},
{'w',"<sprite=35>"},
{'!',"<sprite=36>"},
{'?',"<sprite=37>"},
{';',"<sprite=38>"},
{':',"<sprite=39>"},
{'G',"<sprite=40>"},
{'F',"<sprite=41>"},
{'E',"<sprite=42>"},
{'D',"<sprite=43>"},
{'M',"<sprite=44>"},
{'O',"<sprite=45>"},
{'P',"<sprite=46>"},
{'Q',"<sprite=47>"},
{'Z',"<sprite=48>"},
{'Y',"<sprite=49>"},
{'X',"<sprite=50>"},
{'W',"<sprite=51>"},
{'f',"<sprite=52>"},
{'g',"<sprite=53>"},
{'h',"<sprite=54>"},
{'i',"<sprite=55>"},
{'r',"<sprite=56>"},
{'q',"<sprite=57>"},
{'p',"<sprite=58>"},
{'o',"<sprite=59>"},
{'x',"<sprite=60>"},
{'z',"<sprite=61>"},
{'N',"<sprite=62>"},
{'s',"<sprite=63>"},
{'a',"<sprite=64>"},
{'H',"<sprite=65>"},
{'-',"<sprite=66>"},
{'R',"<sprite=67>"},
{'j',"<sprite=68>"}
   };
	
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
            
			if(m_MyText){
				if(fontDictionary.ContainsKey(singleLetter)){
					m_MyText.text = m_MyText.text + fontDictionary[singleLetter];
				}
				else{
					m_MyText.text = m_MyText.text + singleLetter;
				}
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