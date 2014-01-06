using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public UILabel locationLabel;
	public UILabel nextSaveTimeLabel;

	PlayerController player;

	void Start(){
		if(player == null){
			StartCoroutine("FindPlayer");
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Screen.lockCursor = !Screen.lockCursor;
		}
	}

	void FixedUpdate(){
		if(player != null){
			StartCoroutine("UpdateLocation");
		}

		StartCoroutine("UpdateNextSaveLabel");
	}

	IEnumerator UpdateLocation(){
		if(player != null){
			locationLabel.text = string.Format("Coordinates: ({0:00},{1:00},{2:00})", player.transform.position.x, player.transform.position.y, player.transform.position.z);
		}

		yield return null;
	}

	IEnumerator UpdateNextSaveLabel(){
		nextSaveTimeLabel.text = string.Format("Next Save: {0:0}", WorldBuilder.TimeUntilSave);

		yield return null;
	}

	IEnumerator FindPlayer(){
		while(player == null){
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
			yield return null;
		}
	}

	IEnumerator FocusMouse(bool focusStatus){
		if(focusStatus){
			Debug.Log("FOCUSED");
			yield return new WaitForSeconds(0.1f);
			Screen.lockCursor = true;
		}
		else{
			Debug.Log("LOST FOCUS");
			Screen.lockCursor = false;
		}

		yield return null;
	}

	void OnApplicationFocus(bool focusStatus){
		StartCoroutine("FocusMouse", focusStatus);
	}
}
