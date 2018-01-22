using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController{

	private Text textScore;

	public HUDController(Text score){
		textScore = score;
		textScore.text = string.Empty; 
	}

	public void setScore(string score){
		textScore.text = score;
	}
	
	public string getScore(){
		return textScore.text;
	}
}
