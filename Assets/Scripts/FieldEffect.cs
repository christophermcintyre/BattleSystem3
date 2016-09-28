using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FieldEffect : MonoBehaviour {

	public List<Sprite> elementSprites = new List<Sprite> ();
	public List<Image> elementSlotsImage = new List<Image>();
	public List<ElementDef> elementQueue = new List<ElementDef>();
	public int maxElementQueue = 6;
	public List<ElementDef> elements = new List<ElementDef>();
	public List<int> matches = new List<int> ();

	public List<int> pattern;

	void Start () {

		pattern = new List<int> (){ 1, 2, 3 };
		//pattern is fire, water, wind

		foreach (Image image in elementSlotsImage) {
			image.sprite = elementSprites [0];
		}

		ElementDef none = new ElementDef ();
		none.elementType = ElementType.NONE;
		none.cancelsElement = ElementType.NONE;
		none.elementID = (int)none.elementType;

		ElementDef fire = new ElementDef ();
		fire.elementType = ElementType.FIRE;
		fire.cancelsElement = ElementType.WATER;
		fire.elementID = (int)fire.elementType;

		ElementDef water = new ElementDef ();
		water.elementType = ElementType.WATER;
		water.cancelsElement = ElementType.FIRE;
		water.elementID = (int)water.elementType;

		ElementDef wind = new ElementDef ();
		wind.elementType = ElementType.WIND;
		wind.cancelsElement = ElementType.EARTH;
		wind.elementID = (int)wind.elementType;

		ElementDef earth = new ElementDef ();
		earth.elementType = ElementType.EARTH;
		earth.cancelsElement = ElementType.WIND;
		earth.elementID = (int)earth.elementType;

		elements.Add (none);
		elements.Add (fire);
		elements.Add (water);
		elements.Add (wind);
		elements.Add (earth);

	}


	void Update () {

		for (int j = 0; j < elementQueue.Count; j++) {
			elementSlotsImage[j].sprite = elementSprites[elementQueue [j].elementID];
		}

	}


	public void addElement (int id){

		if (elementQueue.Count < maxElementQueue){
				
			elementQueue.Add (elements [id]);

		} else {

			elementQueue.RemoveAt (0);
			elementQueue.Add (elements [id]);

		}

		checkPattern ();
	}


	public void clear(){

		elementQueue.Clear ();
		foreach (Image image in elementSlotsImage) {
			image.sprite = elementSprites [0];
		}

	}

	public void checkPattern(){

		matches.Clear ();
		int j = 0;

		for (int i = 0; i < elementQueue.Count; i++) {

			if (elementQueue [i].elementID == pattern [j]) {

				matches.Add (i);
				j++;

				if (matches.Count == pattern.Count) {
					Debug.Log ("Complete Match found");
					//matching elements need to be removed or ignored for subsequent scans
					//could break at this point but it might be possible to find another match down the queue
					j = 0;
				}

			} else {

				i = i - matches.Count; //if the pattern starts to match but fails, need to recheck the index that failed it for the start of a new pattern
				matches.Clear ();
				j = 0;

			}
			
		}

	}

}
