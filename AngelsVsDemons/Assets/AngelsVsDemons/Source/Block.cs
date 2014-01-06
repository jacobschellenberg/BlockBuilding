using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {
	
	public float hitPoints = 1;
	public BlockType blockType = BlockType.Dirt;
	public Vector3 position;
	public List<string> owners = new List<string>();

	void Start(){
		renderer.material = blockType == BlockType.Dirt ? (Material)Resources.Load("Dirt") : blockType == BlockType.Water ? (Material)Resources.Load("Water") : null;
	}
	
	public void BlockHit(){
		hitPoints--;

		if(hitPoints <= 0){
			WorldBuilder.blocks.Remove(this);
			Debug.Log("BLOCKS: " + WorldBuilder.blocks.Count);
			Destroy(gameObject);
		}
	}
}
