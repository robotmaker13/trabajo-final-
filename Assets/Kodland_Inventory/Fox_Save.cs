using TMPro;
using UnityEngine;

public class Fox_Save : MonoBehaviour
{
    [SerializeField] TMP_Text saveWarning;
    //Guardar la posición del personaje del jugador
    public void SavePosition(Vector3 playerPos)
    {
        // Guardar la posición del personaje del jugador en todos los ejes en diferentes espacios de PlayerPrefs)
        PlayerPrefs.SetFloat("posX", playerPos.x);
        PlayerPrefs.SetFloat("posY", playerPos.y);
        PlayerPrefs.SetFloat("posZ", playerPos.z);
        // Guardando los datos
        PlayerPrefs.Save();

        saveWarning.text = "The save was succesful";
        Invoke(nameof(DeleteText), 2f);
    }

    public void DeleteText()
    {
        saveWarning.text = "";
    }


    private void OnTriggerEnter(Collider other)
    {
        // Si el trigger del portal se cruzó con el objeto con la etiqueta Player, entonces
        if (other.CompareTag("Player"))
        {
            // Obtener la posición del objeto y pasarlo al método SavePosition
            Vector3 pos = other.transform.position;
            SavePosition(pos);
        }
    }

}



// Los métodos esenciales de PlayerPrefs de Unity:

// 1. PlayerPrefs.SetInt("key", value): Este método se utiliza para almacenar un número entero bajo una clave específica en PlayerPrefs.

// 2. PlayerPrefs.GetInt("key"): Este método se utiliza para obtener un número entero bajo una clave específica de PlayerPrefs.

// 3. PlayerPrefs.SetString("key", value): Este método se utiliza para almacenar un string bajo una clave específica en PlayerPrefs.

// 4. PlayerPrefs.GetString("key"): Este método se utiliza para obtener un string bajo una clave específica de PlayerPrefs.

// 5. PlayerPrefs.DeleteAll(): Este método se utiliza para eliminar todos los valores previamente guardados de PlayerPrefs

// 6. PlayerPrefs.HasKey("key"): Este método se utiliza para verificar si el valor bajo la clave especificada se guardó previamente en PlayerPrefs
