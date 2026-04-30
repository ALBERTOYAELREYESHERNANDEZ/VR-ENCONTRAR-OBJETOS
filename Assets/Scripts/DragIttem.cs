using UnityEngine;
using Oculus.Interaction; //LIBRERIA DE FACEBOOK PARA PODER MANIPULAR OBJETOS EN UNITY

public class DragItem : MonoBehaviour
{
    //STRING VALORES + HOLA +, + MUNDO + 2026 ) "HOLA, MUNDO 2026"
    //INT   12334456789
    //BOOLEAN SI O NO 1 O 0000 TRUE OR FALSE 
    public static int puntajeGlobal = 0; //INICIAMOS LA PUNTUACION EN 0 PARA DESPUES CREAR LAS DEMAS CCALF
    public string nombreDelObjeto = "ObjetoSearch";

    private Rigidbody rb; //VARIABLE PARA GUARDAR EL COMPONENTE DE FISICAS

    private void Awake() //SE EJECUTA EN EL JUEGO ANTES QUE EL METODO START
    {
        ConfiguracionComponentes();
    }

    private void ConfiguracionComponentes()
    {
        //HACEMOS QUE EL OBJETO TENGA FISICAS COMO LA GRAVEDAD
        rb = GetComponent<Rigidbody>(); //BUSCAMOS SI YA TIENE RIGIDBODY

        if (rb == null) //SI NO TIENE, SE LO AGREGAMOS
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = true; //ACTIVAMOS LA GRAVEDAD

        //GRABBABLE PERMITE QUE META-QUEST O EL SDK DE FACEBOOK RECONOZCA QUE ES UN OBJETO SELECCIONABLE
        if (!GetComponent<Grabbable>()) //LOS LENTES SEPAN QUE SE PUEDE TOMAR
        {
            gameObject.AddComponent<Grabbable>();
        }

        if (!GetComponent<GrabInteractable>()) //PARA QUE EL COMPONENTE SE PUEDA MOVER EJES XYZ
        {
            //CREA EL COMPONENTE Y LO GUARDA CON AYUDA DE LA VARIABLE
            var interactable = gameObject.AddComponent<GrabInteractable>();

            //PASA Y CONECTA EL RIGIDBODY AL SISTEMA DE MANOS (IMPORTANTE PARA EL SDK)
            interactable.InjectRigidbody(rb);
        }

        //PARA COMPROBAR SI TIENE COLLIDER Y FISICAS EL OBJETO QUE ESTEMOS AGARRANDO
        if (GetComponent<Collider>() == null)
        {
            //SI SE CUMPLE QUE NO TIENE, QUE LE AÑADA UN COLLIDER AL OBJETO
            gameObject.AddComponent<BoxCollider>();
        }
    }

    public void RecoleccionObjetos() //FUNCION PARA DAR PUNTOS POR ENCONTRAR OBJETOS
    {
        puntajeGlobal++; //ESTO SUMA UN PUNTO
        Debug.Log("Objeto Encontrado! Felicidades Puntaje: " + puntajeGlobal); //IMPRIME EN CONSOLA

        //AQUÍ PODRÍAS AVISARLE AL MENÚ HORIZONTAL QUE ESTE OBJETO YA SE ENCONTRÓ

        Destroy(gameObject); //DESTRUYE EL OBJETO PARA QUE NO SE REPITA INFINITAMENTE
    }

    public void AlSerRecogido()
    {
        //PRUEBA DE QUE FUNCIONA BIEN EL CODIGO E IMPRIME EL NOMBRE EN CONSOLA
        Debug.Log("Has recogido el objeto: " + nombreDelObjeto);
    }
}