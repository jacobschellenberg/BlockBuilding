//{"Owners":["JealousKnight"],"HitPoints":1.0,"BlockType":1,"X":0.0,"Y":0.0,"Z":0.0}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JSONSerializer {

	public void Serialize(List<BlockTransport> blockTransports){
		
		Newtonsoft.Json.JsonConvert.SerializeObject(blockTransports, Newtonsoft.Json.Formatting.Indented);
		
		Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
		//@"/Users/jacobschellenberg/Documents/Programming Projects/Unity Projects/AngelsVsDemons/AngelsVsDemons/Assets/worlddata.txt"
		using(StreamWriter sw = new StreamWriter(Application.dataPath + "/worlddata.txt")){
			using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw)){
				serializer.Serialize(writer, blockTransports);
			}
		}
	}

	public List<BlockTransport> Deserialize(){
		using(StreamReader sw = new StreamReader(Application.dataPath + "/worlddata.txt")){
			List<BlockTransport> deserializedBlock = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BlockTransport>>(sw.ReadToEnd());
			return deserializedBlock;
		}
	}
}
