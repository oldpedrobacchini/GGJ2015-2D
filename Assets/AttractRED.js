#pragma strict


public var ObjetoColidido : GameObject;
public var jaColidiu : boolean;
public var objSinal : GameObject;
public static var GreenNodesScreenP1 : GameObject[];
public static var GreenNodesScreenP2 : GameObject[];


function Start () {
jaColidiu = false;

}

function Update () {

	GreenNodesScreenP1 = GameObject.FindGameObjectsWithTag("greenColididoP1");
	GreenNodesScreenP2 = GameObject.FindGameObjectsWithTag("greenColididoP2");



//objColidiu = player1
//gameObject = particle
}


//Player1
function OnTriggerEnter2D(objColidiu: Collider2D) {

	if (jaColidiu==false){

		if (objColidiu.gameObject.tag == "Player1"){
			jaColidiu = true;
			Destroy(gameObject);
			
			var sinal1 = Instantiate (objSinal, gameObject.transform.position, gameObject.transform.rotation);
			AttractPlayerConfig.qdtParticleAtraidasP1 =AttractPlayerConfig.qdtParticleAtraidasP1-1;
			Destroy(GreenNodesScreenP1[0]);
			yield WaitForSeconds (1);
			Destroy(sinal1);
	

	}
	}
	//Player2
	if (jaColidiu==false){

		if (objColidiu.gameObject.tag == "Player2"){
			jaColidiu = true;
			Destroy(gameObject);

			var sinal2 = Instantiate (objSinal, gameObject.transform.position, gameObject.transform.rotation);
			AttractPlayerConfig.qdtParticleAtraidasP2 =AttractPlayerConfig.qdtParticleAtraidasP2-1;
			Destroy(GreenNodesScreenP2[0]);
			yield WaitForSeconds (1);
			Destroy(sinal2);
			
			


		}
	}
}




