using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(SpriteRenderer))]
public class Maanspawnaus : MonoBehaviour {

    public int offsetX = 2;
    public bool hasARightBuddy = false; 
    public bool hasALeftBuddy = false;
    public bool reverseScale = false;
  

    private float spriteWidth = 0f;
    private Camera cam;
    private Transform mTransform;









    private void Awake()
    {
        cam = Camera.main;
        mTransform = transform;
       
}

	void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;

	} //start
	
	public void Update () {
        // vieläkö spawnataan lisäää(tarkistus)
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            float camHorizontaalinenLisaus = cam.orthographicSize * Screen.width / Screen.height;

            // lasketaan x akselin kohta kameran näkemästä alueesta
            float reunaOikea = (mTransform.position.x + spriteWidth / 2) - camHorizontaalinenLisaus;

            float reunaVasen = (mTransform.position.x - spriteWidth / 2) + camHorizontaalinenLisaus;

            if (cam.transform.position.x >= reunaOikea - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= reunaVasen + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }


     
        }

    }//update
    void MakeNewBuddy(int oikeaVasen)//tarkistetaan kummalle puolella lisätään seinää
    {
        Vector3 newPosition = new Vector3(mTransform.position.x + mTransform.localScale.x * spriteWidth * oikeaVasen, mTransform.position.y, mTransform.position.z);
        Transform uusi =  (Transform)Instantiate (mTransform, newPosition, mTransform.rotation);
        if (reverseScale == true)
        {
            uusi.localScale = new Vector3 (uusi.localScale.x * -1, uusi.localScale.y, uusi.localScale.z);
        }
        uusi.parent = mTransform.parent;
        if (oikeaVasen > 0)
        {
            uusi.GetComponent<Maanspawnaus>().hasALeftBuddy = true;

        }else
        {
            uusi.GetComponent<Maanspawnaus>().hasARightBuddy = true;
        }
    }
}//class
