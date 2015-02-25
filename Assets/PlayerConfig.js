#pragma strict
public static var qdtParticleAtraidas = 0;
public static var qdtMaxParticleAtrair = 4;


function Start () {

}

function Update () {

}

function OnTriggerEnter2D(objColidiu: Collider2D) {
	if (objColidiu.gameObject.tag == "green"){
	qdtParticleAtraidas =qdtParticleAtraidas+1;
	
	}
	else if (objColidiu.gameObject.tag == "red"){
	qdtParticleAtraidas =qdtParticleAtraidas-1;
	
	}
	
}