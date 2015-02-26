#pragma strict
public var ObjetoColididoBH : GameObject;
public var speedMagnetic : Vector2;
public var speedMagneticCam : Vector2;
public var speedMagneticCamSize : float;
static var SuavizaCam = 20;

public var Suaviza = 0.6;

public var objSinalFinal : GameObject;
public var objCamera : Camera;
public static var GanhouP1 : boolean;
public static var GanhouP2 : boolean;
public var animaScreenWinP1:GameObject;
public var animaScreenWinP2:GameObject;
public var CameraTarget:Transform;


function Start () {
GanhouP1 = false;
GanhouP2 = false;

}

function Update () {
	if(GanhouP1==true){
			
			AtracaoBlackHoleP1();
			
			
		}
	if(GanhouP2==true){
			
			AtracaoBlackHoleP2();

		}

	}


function OnTriggerEnter2D(objColidiu: Collider2D) {
	ObjetoColididoBH = objColidiu.gameObject;
	
	if(AttractPlayerConfig.qdtParticleAtraidasP1 ==4 && ObjetoColididoBH.tag =='Player1'){
		P1WIN();


	}
	
	if(AttractPlayerConfig.qdtParticleAtraidasP2 ==4 && ObjetoColididoBH.tag =='Player2'){
		P2WIN();


	}

	
}


function P1WIN(){
	GanhouP1 = true;
	animaScreenWinP1.transform.active =true;
	
	var sinalBH1 = Instantiate (objSinalFinal, gameObject.transform.position, gameObject.transform.rotation);
	(GameObject.Find("Player1").GetComponent( "MovimentPlayer1" ) as MonoBehaviour).enabled = false;
	(GameObject.Find("Player2").GetComponent( "MovimentPlayer2" ) as MonoBehaviour).enabled = false;
	GameObject.Find("Player1").collider2D.isTrigger=true;
	GameObject.Find("Player2").collider2D.isTrigger=true;
	
	yield WaitForSeconds (1);
	Destroy(sinalBH1);
	
	print('Ganhou P1');
}



function P2WIN(){
	GanhouP2 = true;
	animaScreenWinP2.transform.active =true;
	
	var sinalBH2 = Instantiate (objSinalFinal, gameObject.transform.position, gameObject.transform.rotation);
	(GameObject.Find("Player1").GetComponent( "MovimentPlayer1" ) as MonoBehaviour).enabled = false;
	(GameObject.Find("Player2").GetComponent( "MovimentPlayer2" ) as MonoBehaviour).enabled = false;
	GameObject.Find("Player1").collider2D.isTrigger=true;
	GameObject.Find("Player2").collider2D.isTrigger=true;
	
	yield WaitForSeconds (1);
	Destroy(sinalBH2);
	
	print('Ganhou P2');
}





function AtracaoBlackHoleP1(){
	
	
	(AttractRED.GreenNodesScreenP1[0].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	(AttractRED.GreenNodesScreenP1[1].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	(AttractRED.GreenNodesScreenP1[2].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	(AttractRED.GreenNodesScreenP1[3].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	
	
	
	AttractRED.GreenNodesScreenP1[0].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[0].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP1[0].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[0].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);

	AttractRED.GreenNodesScreenP1[1].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[1].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP1[1].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[1].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);

	AttractRED.GreenNodesScreenP1[2].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[2].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP1[2].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[2].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);

	AttractRED.GreenNodesScreenP1[3].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[3].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP1[3].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP1[3].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);
	
	
	ObjetoColididoBH.transform.position.x = Mathf.SmoothDamp(ObjetoColididoBH.transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	ObjetoColididoBH.transform.position.y = Mathf.SmoothDamp(ObjetoColididoBH.transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);
	
	objCamera.transform.position.x = Mathf.SmoothDamp(objCamera.transform.position.x, gameObject.transform.position.x, speedMagneticCam.x, SuavizaCam*Time.deltaTime);
	objCamera.transform.position.y = Mathf.SmoothDamp(objCamera.transform.position.y, gameObject.transform.position.y, speedMagneticCam.y, SuavizaCam*Time.deltaTime);
	objCamera.camera.orthographicSize = Mathf.SmoothDamp(objCamera.camera.orthographicSize,11,speedMagneticCamSize, SuavizaCam*Time.deltaTime);

	
}

function AtracaoBlackHoleP2(){
	
	
	(AttractRED.GreenNodesScreenP2[0].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	(AttractRED.GreenNodesScreenP2[1].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	(AttractRED.GreenNodesScreenP2[2].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	(AttractRED.GreenNodesScreenP2[3].GetComponent( "AttractGREEN" ) as MonoBehaviour).enabled = false;
	
	
	
	AttractRED.GreenNodesScreenP2[0].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[0].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP2[0].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[0].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);

	AttractRED.GreenNodesScreenP2[1].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[1].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP2[1].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[1].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);

	AttractRED.GreenNodesScreenP2[2].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[2].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP2[2].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[2].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);

	AttractRED.GreenNodesScreenP2[3].transform.position.x = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[3].transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	AttractRED.GreenNodesScreenP2[3].transform.position.y = Mathf.SmoothDamp(AttractRED.GreenNodesScreenP2[3].transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);
	
	
	ObjetoColididoBH.transform.position.x = Mathf.SmoothDamp(ObjetoColididoBH.transform.position.x, gameObject.transform.position.x, speedMagnetic.x, Suaviza);
	ObjetoColididoBH.transform.position.y = Mathf.SmoothDamp(ObjetoColididoBH.transform.position.y, gameObject.transform.position.y, speedMagnetic.y, Suaviza);
	
	objCamera.transform.position.x = Mathf.SmoothDamp(objCamera.transform.position.x, gameObject.transform.position.x, speedMagneticCam.x, SuavizaCam*Time.deltaTime);
	objCamera.transform.position.y = Mathf.SmoothDamp(objCamera.transform.position.y, gameObject.transform.position.y, speedMagneticCam.y, SuavizaCam*Time.deltaTime);
	objCamera.camera.orthographicSize = Mathf.SmoothDamp(objCamera.camera.orthographicSize,11,speedMagneticCamSize, SuavizaCam*Time.deltaTime);
	
		
	
	
}
