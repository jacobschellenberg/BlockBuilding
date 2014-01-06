using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerController : MonoBehaviour {

	public string playerName = "JealousKnight";
	public float checkDistance = 3;
	public Animator animator;

	void Update () {
		if(Input.GetButtonDown("Fire1")){
			StartCoroutine("Hit");
		}

		if(Input.GetButtonDown ("Fire2")){
			StartCoroutine("Set");
		}

		if(Input.GetKeyDown (KeyCode.R)){
			gameObject.transform.position = new Vector3(0,10,0);
		}
	}

	void LateUpdate(){
		float vertical = Input.GetAxis ("Vertical");
		
		animator.SetFloat("Speed", vertical); 
	}

	IEnumerator Set(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, checkDistance)){
			Debug.DrawLine(ray.origin, hit.point);
//			Debug.Log ("Normal: "  + hit.normal);

			if(hit.transform.GetComponent<Block>().blockType == BlockType.Dirt){
				if(hit.normal.x > 0){
	//				Debug.Log("RIGHT");
					if(!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x + 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), 0.2f)){
						GameObject block = (GameObject)Instantiate((GameObject)Resources.Load("Block"), new Vector3(hit.collider.bounds.center.x + 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), Quaternion.identity);
						block.GetComponent<Block>().owners.Add(playerName);
						WorldBuilder.blocks.Add(block.GetComponent<Block>());
					}
				}
				else if(hit.normal.x < 0){
	//				Debug.Log("LEFT");
					if(!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x - 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), 0.2f)){
						GameObject block = (GameObject)Instantiate((GameObject)Resources.Load("Block"), new Vector3(hit.collider.bounds.center.x - 1, hit.collider.bounds.center.y, hit.collider.bounds.center.z), Quaternion.identity);
						block.GetComponent<Block>().owners.Add(playerName);
						WorldBuilder.blocks.Add(block.GetComponent<Block>());
					}
				}
				else if(hit.normal.y > 0){
	//				Debug.Log ("TOP");
					if(!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y + 1, hit.collider.bounds.center.z), 0.2f)){
						GameObject block = (GameObject)Instantiate((GameObject)Resources.Load("Block"), new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y + 1, hit.collider.bounds.center.z), Quaternion.identity);
						block.GetComponent<Block>().owners.Add(playerName);
						WorldBuilder.blocks.Add(block.GetComponent<Block>());
					}
				}
				else if(hit.normal.y < 0){
	//				Debug.Log ("BOTTOM");
					if(!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y - 1, hit.collider.bounds.center.z), 0.2f)){
						GameObject block = (GameObject)Instantiate((GameObject)Resources.Load("Block"), new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y - 1, hit.collider.bounds.center.z), Quaternion.identity);
						block.GetComponent<Block>().owners.Add(playerName);
						WorldBuilder.blocks.Add(block.GetComponent<Block>());
					}
				}
				else if(hit.normal.z > 0){
	//				Debug.Log ("FRONT");
					if(!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z + 1), 0.2f)){
						GameObject block = (GameObject)Instantiate((GameObject)Resources.Load("Block"), new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z + 1), Quaternion.identity);
						block.GetComponent<Block>().owners.Add(playerName);
						WorldBuilder.blocks.Add(block.GetComponent<Block>());
					}
				}
				else if(hit.normal.z < 0){
	//				Debug.Log ("BACK");
					if(!Physics.CheckSphere(new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z - 1), 0.2f)){
						GameObject block = (GameObject)Instantiate((GameObject)Resources.Load("Block"), new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y, hit.collider.bounds.center.z - 1), Quaternion.identity);
						block.GetComponent<Block>().owners.Add(playerName);
						WorldBuilder.blocks.Add(block.GetComponent<Block>());
					}
				}
			}
		}
		
		yield return null;
	}

	IEnumerator Hit(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, checkDistance)){
		Debug.DrawLine(ray.origin, hit.point);
//		Debug.Log (hit.transform.name);

			if(hit.transform.GetComponent<Block>() != null){
				if(hit.transform.GetComponent<Block>().owners != null){
					if(hit.transform.GetComponent<Block>().owners.Contains(playerName)){
						if(hit.transform.GetComponent<Block>().blockType == BlockType.Dirt){
							hit.transform.GetComponent<Block>().BlockHit();
						}
					}
				}
			}
		}

		yield return null;
	}
}
