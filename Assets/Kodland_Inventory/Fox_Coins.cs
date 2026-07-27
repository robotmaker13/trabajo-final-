using UnityEngine;

public class Fox_Coins : MonoBehaviour
{
    Fox_Logic foxLogic;

    // El nombre del objeto
    public string objectName;
    // ¿Se ha recogido el objeto?
    public bool isTaken;

    private void Start()
    {
        foxLogic = FindObjectOfType<Fox_Logic>();

        // Si tenemos un espacio de guardado con ese nombre
        if (PlayerPrefs.HasKey(objectName))
        {
            // Comparando el valor de este espacio con 1, almacenando el resultado de la comprobación en la variable isTaken
            // Si existe dicho espacio, inevitablemente compararemos 1 con 1, lo que siempre devolverá True
            isTaken = PlayerPrefs.GetInt(objectName) == 1;

            // Establecer el estado del objeto en Habilitado/Deshabilitado según el valor de la variable isTaken
            gameObject.SetActive(!isTaken);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si el objeto que tocó la moneda tiene la etiqueta "Jugador", entonces...
        if (other.CompareTag("Player"))
        {
            //Estableciendo la variable
            isTaken = true;

            // Creando la ranura de guardado con el nombre del objeto, almacenando "1" en ella
            PlayerPrefs.SetInt(objectName, 1);

            // Desactivar la moneda
            gameObject.SetActive(false);

            // Obteniendo la cantidad de monedas de la ranura de guardado y almacenándola en una variable temporal
            // Si dicha ranura no existe, establecemos el valor en 0
            var value = PlayerPrefs.GetInt("Coins", 0);

            // Almacenar la cantidad actualizada de monedas recolectadas en la ranura "Monedas"
            // Para eso, necesitamos tomar la variable actual y agregarle uno
            PlayerPrefs.SetInt("Coins", value + 1);

            // Llamar al método de actualización de la interfaz de usuario (no te preocupes por el error; es solo que aún no hemos escrito el método)
            foxLogic.GetCoin();
        }
    }
}
