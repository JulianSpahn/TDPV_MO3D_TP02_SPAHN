using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArray : MonoBehaviour
{
    //cantidad de objetos que encontraremos en el eje x e y
    [SerializeField]
    private int n_Object_X;
    [SerializeField]
    private int m_Object_Y;
    //contadores de objetos en el eje x e y
    private int actual_Objects_Cuantity_On_X;
    private int actual_Objects_Cuantity_On_Y;
    //vectores para manejar la posicion actual y la posicion siguiente de los prefab
    private Vector3 actual_Object_Position;
    private Vector3 upcoming_Object_Position;
    //Instancia para poner el prefab
    [SerializeField]
    private GameObject the_Prefab;
    //elemento que, utilizando una funcion, obtendra el tamaño de los prefab
    private Vector3 object_Size;

    //inicializamos los componentes
    void Start()
    {
        Initialize();//funcion que inicializa los componentes del script
    }
    //actualizamos el programa
    void Update()
    {
        Run_Program();//funcion que maneja lo que sucede en el script
    }
    //función para calcular el tamaño combinado de los elemento que componen al Prefab
    private Vector3 PrefabSize(GameObject new_object)
    {
        //obtenemos los Renderers de los objetos hijos/clones que se generan 
        Renderer[] prefab_Renderers = new_object.GetComponentsInChildren<Renderer>();
        //vector donde almacenaremos el tamaño obtenido
        Vector3 object_Sizes = Vector3.zero;
        //calculamos el tamaño final del objeto.
        foreach (Renderer prefab_Son_Renderer in prefab_Renderers)
        {
            object_Sizes += prefab_Son_Renderer.bounds.size;
        }
        //retornamos el tamaño obtenido
        return object_Sizes;
    }
    //funcion que se encarga de inicializar las variables
    private void Initialize()
    {
        actual_Object_Position = new Vector3(0, 0, 0);//posicion actual del objeto, esta inicializada en 0 porque en esa posicion ira el primer objeto
        upcoming_Object_Position = actual_Object_Position;//cambiamos la posicion actual del objeto por la siguiente donde se colocara otro objeto
        actual_Objects_Cuantity_On_X = 0;//contador de objetos de las filas
        actual_Objects_Cuantity_On_Y = 1;//contador de objetos de las columnas,
        object_Size = PrefabSize(the_Prefab);//obtenemos el tamaño completo del prefab
    }
    //funcion que se encarga de crear y posicionar los objetos
    private void Run_Program()
    {
        //en caso de que la cantidad de columnas asignadas sea mayor a la cantidad de objetos en el eje X
        if (m_Object_Y > actual_Objects_Cuantity_On_X)
        {
            //instanciamos un nuevo prefab
            Instantiate(the_Prefab, upcoming_Object_Position, Quaternion.identity);
            //actualizamos la posicion para que este  preparada para el siguiente objeto
            upcoming_Object_Position.x += object_Size.x;
            //aumentamos el contador de objetos en el eje X
            actual_Objects_Cuantity_On_X++;

        }
        //en caso de que la cantidad de filas asignadas sea mayor a la cantidad de objetos en el eje Y
        else if (n_Object_X > actual_Objects_Cuantity_On_Y)
        {
            //reiniciamos la posición en X para cuando pongamos el proximo objeto
            upcoming_Object_Position.x = actual_Object_Position.x;
            //modificamos la posición en el eje y
            upcoming_Object_Position.y += object_Size.y / 2;
            //instanciamos el nuevo objeto
            Instantiate(the_Prefab, upcoming_Object_Position, Quaternion.identity);
            //actualizamos el valor inicial del contador de objetos para evitar que los objetos se generen fuera de la base de los primeros objetos
            actual_Objects_Cuantity_On_X = 1;
            //actualizamos la proxima posicion en X de los objetos.
            upcoming_Object_Position.x += object_Size.x;
            //por ultimo, actualizamos el contador de objetos en Y
            actual_Objects_Cuantity_On_Y++;
        }
    }
}
