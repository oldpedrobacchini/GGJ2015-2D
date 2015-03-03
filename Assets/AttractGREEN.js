#pragma strict

public var speedMagnetic : Vector2;
public var Suaviza = 0.1;
public var ObjetoColidido : GameObject;
public var atrair : boolean;
public var jaColidiu : boolean;


public var objSinal : GameObject;
//private var myScript : Script;

public var DistanceToPlayer :Vector2;



function Start () 
{
	atrair = false;
	jaColidiu = false;
}

function Update () 
{
	var DistanceToPlayer = 0;
	
	if(jaColidiu)
		DistanceToPlayer = Vector2.Distance(ObjetoColidido.gameObject.transform.position, gameObject.transform.position);

	if(atrair==true){
		Atracao();
	}
	else if (atrair==false){
		//print('Off!!');
	}
	
	if (DistanceToPlayer>3)
	{
		atrair=true;
		(gameObject.GetComponent( "UpDown" ) as MonoBehaviour).enabled = false;	
	}
	else{
		atrair=false;
	}
//objColidiu = player1
//gameObject = particle
}

function OnCollisionEnter2D(objColidiu: Collision2D) 
{
//Player1
	if (jaColidiu==false)
	{
		if (objColidiu.gameObject.tag == "Player1" && AttractPlayerConfig.qdtParticleAtraidasP1 < AttractPlayerConfig.qdtMaxParticleAtrair)
		{
			AttractPlayerConfig.qdtParticleAtraidasP1 = AttractPlayerConfig.qdtParticleAtraidasP1+1;
			Suaviza=Random.Range(0.1,0.4);
//			print (AttractPlayerConfig.qdtParticleAtraidasP1);

				jaColidiu = true;
				gameObject.tag = 'greenColididoP1';
				ObjetoColidido = objColidiu.gameObject;
				//gameObject.collider2D.isTrigger=false;
				var sinal1 = Instantiate (objSinal, gameObject.transform.position, gameObject.transform.rotation);
				yield WaitForSeconds (1);
				Destroy(sinal1);


		}
		//Player2
		if (objColidiu.gameObject.tag == "Player2" && AttractPlayerConfig.qdtParticleAtraidasP2 < AttractPlayerConfig.qdtMaxParticleAtrair)
		{
			AttractPlayerConfig.qdtParticleAtraidasP2 =AttractPlayerConfig.qdtParticleAtraidasP2+1;
			Suaviza=Random.Range(0.1,0.4);
//			print (AttractPlayerConfig.qdtParticleAtraidasP2);
			

				jaColidiu = true;
				gameObject.tag = 'greenColididoP2';
				ObjetoColidido = objColidiu.gameObject;
				//gameObject.collider2D.isTrigger=false;
				var sinal2 = Instantiate (objSinal, gameObject.transform.position, gameObject.transform.rotation);
				yield WaitForSeconds (1);
				Destroy(sinal2);

		}
	}
}



function Atracao()
{
	gameObject.transform.position.x = Mathf.SmoothDamp(gameObject.transform.position.x, ObjetoColidido.transform.position.x, speedMagnetic.x, Suaviza);
	gameObject.transform.position.y = Mathf.SmoothDamp(gameObject.transform.position.y, ObjetoColidido.transform.position.y, speedMagnetic.y, Suaviza);	
}
