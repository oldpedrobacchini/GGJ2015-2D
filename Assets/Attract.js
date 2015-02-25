#pragma strict

//public var speedMagnetic : float = 5.0;
public var speedMagnetic : Vector2;
public var Suaviza = 0.1;
public var ObjetoColidido : GameObject;
public var atrair : boolean;

public var DistanceToPlayer :Vector2;



function Start () {
atrair = false;

}

function Update () {

	var DistanceToPlayer = Vector2.Distance(ObjetoColidido.gameObject.transform.position, gameObject.transform.position);


	if(atrair==true){
		//print('esta atraindo!!');
		Atracao();
	}
	else if (atrair==false){
		//print('Off!!');
	}
	
	if (DistanceToPlayer>50){
		print('Maior que 50');
		atrair=true;
	}
	else{
		print('Menor');
		atrair=false;
	}
	


//objColidiu = player1
//gameObject = particle
}

function OnTriggerEnter2D(objColidiu: Collider2D) {
	if (objColidiu.gameObject.tag == "Player1"){
	ObjetoColidido = objColidiu.gameObject;
	//Destroy(gameObject.collider2D);
	gameObject.collider2D.isTrigger=false;
	print('colidiu com o PLayer');
	if(PlayerConfig.qdtParticleAtraidas<=PlayerConfig.qdtMaxParticleAtrair){
		atrair =true;
	}
		else {
			print('Player1 CHEIO!!');
	}
	}
	
}

function Atracao(){

	gameObject.transform.position.x = Mathf.SmoothDamp(gameObject.transform.position.x, ObjetoColidido.transform.position.x, speedMagnetic.x, Suaviza);
	gameObject.transform.position.y = Mathf.SmoothDamp(gameObject.transform.position.y, ObjetoColidido.transform.position.y, speedMagnetic.y, Suaviza);

	
}
