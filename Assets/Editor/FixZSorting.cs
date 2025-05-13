using UnityEngine;
using UnityEditor;

public class FixZSorting : MonoBehaviour
{
	[MenuItem("Tools/Fijar Z como sortingOrder")]
	static void FixAllZPositions()
	{
		int objectsModified = 0;

		// Recorre todos los objetos en la escena
		foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
		{
			SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
			if (sr != null)
			{
				float z = obj.transform.position.z;

				// Convierte Z en sortingOrder
				sr.sortingOrder = Mathf.RoundToInt(-z * 100); // Más profundo = menor orden

				// Pone Z a 0
				Vector3 newPos = obj.transform.position;
				newPos.z = 0;
				obj.transform.position = newPos;

				objectsModified++;
			}
		}

		Debug.Log($"✅ Hecho: {objectsModified} objetos modificados. SortingOrder actualizado y Z puesto a 0.");
	}
}
