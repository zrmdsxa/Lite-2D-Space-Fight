using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { INTRO,MENU,PLAY,WON,LOST }
public class StateManager : MonoBehaviour {


	public GameObject[] m_gameStates;

	[HideInInspector]
	public GameStates m_activeState;	//enum variable
	private int m_numStates;


	// Use this for initialization
	void Start () {
		m_numStates = m_gameStates.Length;
		
		for(int i = 0; i < m_numStates;i++){
			m_gameStates[i].SetActive(false);
		}
		m_activeState = GameStates.INTRO;
		m_gameStates[(int)m_activeState].SetActive(true);
		//GameManager.Instance.StartGame();
	}
	
	// Update is called once per frame
	/*/
	void Update () {
		
	}
	*/
	public void ChangeState(GameStates newState){
		m_gameStates[(int)m_activeState].SetActive(false);
		m_activeState = newState;
		m_gameStates[(int)m_activeState].SetActive(true);
	}
	public void PlayGame(){
		//GameManager.Instance.m_stateGameMenu.PlayGame();
		m_gameStates[(int)m_activeState].SetActive(false);
		m_activeState = GameStates.PLAY;
		m_gameStates[(int)m_activeState].SetActive(true);
	}

	public void QuitGame(){
		//GameManager.Instance.m_stateGameMenu.QuitGame();
	}
}
