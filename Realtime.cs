using System;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Unity.VisualScripting;

public class Realtime : MonoBehaviour
{
    // conexion con Firebase
    private FirebaseApp _app;
    // Singleton de la Base de Datos
    private FirebaseDatabase _db;
    // referencia a la 'coleccion' Clientes
    private DatabaseReference _refClientes;
    // referencia a un cliente en concreto
    private DatabaseReference _refAA002;
    // referencia a una moneda en concreto
    private DatabaseReference _refMoneda;
    
    // obejto 
    private GroundController ground;
    // la bola del jugador local
    private PlayerController player;
    
    /*
     * Base de datos usada en formato JSON
     *      {
              "Jugadores": {
                    "AA01": {
                      "nombre": "Vegeta",
                      "puntos": 0
                    },
                    "AA02": {
                      "nombre": "Son Goku",
                      "puntos": 1
                    }
               }
            }
     */
    
    // Start is called before the first frame update
    void Start()
    {
        
        // realizamos la conexion a Firebase
        _app = Conexion();
        
        // obtenemos el Singleton de la base de datos
        _db = FirebaseDatabase.DefaultInstance;
        
        // Obtenemos la referencia a TODA la base de datos
        // DatabaseReference reference = db.RootReference;
        
        // Definimos la referencia a Clientes
        _refClientes = _db.GetReference("jugadores");
        
        // Asignamos la referencia de la moneda
        _refMoneda = _db.GetReference("prefabs");
        
        
        // Para Uso De La Base De Datos
        ground = GameObject.Find("Ground").GetComponent<GroundController>();
        
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        // Recogemos todos los valores de Moneda
        _refMoneda.GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    // mostramos los datos
                    RecorreResultado(snapshot);
                    //Debug.Log(snapshot.value);
                }
            });
        // Recogemos todos los valores de Jugadores  para sustituirlos en local
        /*
		_refClientes.GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    // mostramos los datos
                    RecorreResultadoClientes(snapshot);
                    //Debug.Log(snapshot.value);
                }
            });
        
        */
        
        
        // List<double> listaCoordenadas = RecorreResultado();
        // hago spawn de monedas en las coordenadas
        /*
        for (int i = 0; i < UPPER; i=i+3)
        {
            spawn(i)
        }
        */
        
        
        
        
        // Añadimos el evento cambia un valor
        // _refMoneda.ValueChanged += HandleValueChanged;

		/*
        // Añadimos un nodo
        AltaDevice();
		*/
    }
    
    // realizamos la conexion a Firebase
    // devolvemos una instancia de esta aplicacion
    FirebaseApp Conexion()
    {
        FirebaseApp firebaseApp = null;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                firebaseApp = FirebaseApp.DefaultInstance;
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
                firebaseApp = null;
            }
        });
            
        return firebaseApp;
    }
    
    // evento cambia valor en AA02
    // escalo objeto en la escena
    void HandleValueChanged(object sender, ValueChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Mostramos lo resultados
        MuestroJugador(args.Snapshot);
        // escalo objeto
        float escala = float.Parse(args.Snapshot.Child("puntos").Value.ToString());
        
        
        
    }

    // recorro un snapshot de un nivel
    void RecorreResultado(DataSnapshot snapshot)
    {
        // Array que guarda x,y,z de los diferentes resultados
        // List<double> listaCoordenadas = null;
        
        foreach(var resultado in snapshot.Children) // Monedas
        {
            float x=0, y=0, z=0;
            Debug.LogFormat("Key = {0}", resultado.Key);  // "Key = prefabXX"
            foreach(var levels in resultado.Children)
            {
                if (levels.Key == "x")
                {
                    x = float.Parse(levels.Value.ToString());
                    // listaCoordenadas.Add(x);
                }
                if (levels.Key == "y")
                {
                    y = float.Parse(levels.Value.ToString());
                    // listaCoordenadas.Add(y);
                }
                if (levels.Key == "z")
                {
                    z = float.Parse(levels.Value.ToString());
                    // listaCoordenadas.Add(z);
                }
                
                Debug.LogFormat("(key){0}:(value){1}", levels.Key, levels.Value);
            }
            // usar el SpawnPickup de GroundController
            ground.SpawnPickup(x, y, z);
            Debug.Log("spawneado");
        }

        // return listaCoordenadas;
    }

    // recorro un snapshot de un nivel
    void RecorreResultadoClientes(DataSnapshot snapshot)
    {
        // Array que guarda x,y,z de los diferentes resultados
        // List<double> listaCoordenadas = null;
        
        foreach(var resultado in snapshot.Children) // Monedas
        {
            float x=0, y=0, z=0;
            Debug.LogFormat("Key = {0}", resultado.Key);  // "Key = prefabXX"
            foreach(var levels in resultado.Children)
            {
//.Child("j1")
                if (levels.Child("j1").Key == "x")
                {
                    x = float.Parse(levels.Child("j1").Value.ToString());
                    // listaCoordenadas.Add(x);
                }
                if (levels.Child("j1").Key == "y")
                {
                    y = float.Parse(levels.Child("j1").Value.ToString());
                    // listaCoordenadas.Add(y);
                }
                if (levels.Child("j1").Key == "z")
                {
                    z = float.Parse(levels.Child("j1").Value.ToString());
                    // listaCoordenadas.Add(z);
                }
                
                Debug.LogFormat("(key){0}:(value){1}", levels.Key, levels.Value);
            }
            // Cambio los valores del player
			/*
			player.transform.position.x = x;
			player.transform.position.y = y;
			player.transform.position.z = z;
			*/
			
			player.transform.position = new Vector3(x,y,z);
            Debug.Log("Cambiando valores");
        }

        // return listaCoordenadas;
    }
    
    // muestro un jugador
    void MuestroJugador(DataSnapshot jugador)
    {
        foreach (var resultado in jugador.Children) // jugador
        {
            Debug.LogFormat("{0}:{1}", resultado.Key, resultado.Value);
        }
    }


    // doy de alta un nodo con un identificador unico
    void AltaDevice()
    {
        _refClientes.Child(SystemInfo.deviceUniqueIdentifier).Child("nombre").SetValueAsync("Mi dispositivo");
    }
    
    // Update is called once per frame
    void Update()
    {
        // Actualizo la base de datos en cada frame, CUIDADO!!!!! 
        _refClientes.Child("j1").Child("puntos").SetValueAsync(player.count);
		// cambiar el valor de j1, su campo y con su valor de posision x del transform
        _refClientes.Child("j1").Child("x").SetValueAsync(player.transform.position.x);
		// cambiar el valor de j1, su campo y con su valor de posision y del transform
	    _refClientes.Child("j1").Child("y").SetValueAsync(player.transform.position.y);
		// cambiar el valor de j1, su campo y con su valor de posision z del transform
	    _refClientes.Child("j1").Child("z").SetValueAsync(player.transform.position.z);
		
		// setJugadorValores();


		
	async void setJugadorValores()
	{

		DataSnapshot snapshot = await _refClientes.Child("j1").GetValueAsync();

        // Verificar si se encontró un valor en la base de datos
        if (snapshot.Exists)
        {
            // Obtener el valor de la posición x como un float
            float posicionX = float.Parse(snapshot.Child("x").Value.ToString());
            // Obtener el valor de la posición y como un float
            float posicionY = float.Parse(snapshot.Child("y").Value.ToString());
            // Obtener el valor de la posición z como un float
            float posicionZ = float.Parse(snapshot.Child("z").Value.ToString());

			player.transform.position = new Vector3(posicionX,posicionY,posicionZ);            


        }


	}
    }

/*
	void FixedUpdate()
	{
		_refMoneda.GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    // mostramos los datos
                    RecorreResultado(snapshot);
                    //Debug.Log(snapshot.value);
                }
            });
	

	}
    
*/
}