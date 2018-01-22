using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore {

	public static readonly string TAG_SCORE = "score";
	private int score;

	public GameScore(){
		score = 0;
	}

	public void setScore(){
		score++;
	}

	public void setScore(int point){
		score += point;
	}

	public int getScore(){
		return score;
	}


}
