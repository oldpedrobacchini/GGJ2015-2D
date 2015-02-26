#pragma strict
public static var qdtParticleAtraidasP1=0;
public static var qdtParticleAtraidasP2=0;
public static var qdtMaxParticleAtrair = 4;
//public var other : BlackHoleConfig;

public var objSinalFinal : GameObject;


public var animaScreenWinP1:GameObject;
public var animaScreenWinP2:GameObject;

public var CameraTarget1:Transform;
public var objCamera1 : Camera;
public var speedMagneticCam1 : Vector2;
public var speedMagneticCamSize1 : float;
static var SuavizaCam1 = 50;




function Start () {

}

function Update () {

if(qdtParticleAtraidasP1 <0){
	P2WIN_PLAYER();
	
	

}
if(qdtParticleAtraidasP2 <0){

	P1WIN_PLAYER();


}

}


function P1WIN_PLAYER(){
	print('Ganhou P1');
	BlackHoleConfig.GanhouP1 = true;
	animaScreenWinP1.transform.active =true;
	
	
	(GameObject.Find("Player1").GetComponent( "MovimentPlayer1" ) as MonoBehaviour).enabled = false;
	(GameObject.Find("Player2").GetComponent( "MovimentPlayer2" ) as MonoBehaviour).enabled = false;
	GameObject.Find("Player1").collider2D.isTrigger=true;
	GameObject.Find("Player2").collider2D.isTrigger=true;

	objCamera1.transform.position.x = Mathf.SmoothDamp(objCamera1.transform.position.x, GameObject.Find("Player1").transform.position.x, speedMagneticCam1.x, SuavizaCam1*Time.deltaTime);
	objCamera1.transform.position.y = Mathf.SmoothDamp(objCamera1.transform.position.y, GameObject.Find("Player1").transform.position.y, speedMagneticCam1.y, SuavizaCam1*Time.deltaTime);
	objCamera1.camera.orthographicSize = Mathf.SmoothDamp(objCamera1.camera.orthographicSize,11,speedMagneticCamSize1, SuavizaCam1*Time.deltaTime);
	
}

function P2WIN_PLAYER(){
	print('Ganhou P1');
	BlackHoleConfig.GanhouP1 = true;
	animaScreenWinP2.transform.active =true;
	
	
	(GameObject.Find("Player1").GetComponent( "MovimentPlayer1" ) as MonoBehaviour).enabled = false;
	(GameObject.Find("Player2").GetComponent( "MovimentPlayer2" ) as MonoBehaviour).enabled = false;
	GameObject.Find("Player1").collider2D.isTrigger=true;
	GameObject.Find("Player2").collider2D.isTrigger=true;

	objCamera1.transform.position.x = Mathf.SmoothDamp(objCamera1.transform.position.x, GameObject.Find("Player2").transform.position.x, speedMagneticCam1.x, SuavizaCam1*Time.deltaTime);
	objCamera1.transform.position.y = Mathf.SmoothDamp(objCamera1.transform.position.y, GameObject.Find("Player2").transform.position.y, speedMagneticCam1.y, SuavizaCam1*Time.deltaTime);
	objCamera1.camera.orthographicSize = Mathf.SmoothDamp(objCamera1.camera.orthographicSize,11,speedMagneticCamSize1, SuavizaCam1*Time.deltaTime);
	
}

