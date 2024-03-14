# Unity Firebase 

Para la siguiente guia se usara como base un proyecto de **Roll a Ball**.

## Instalación

Se requiere tener una cuenta de **Firebase**, y tener un base del mismo, en ese caso se usara el **RealTime**.

Se ira a la configuracion del **Realtime** y descargar el ``sdk`` del mismo, asi como el ``goolge-service``.

En el Unity hub se añadira el modulo ``IOS Build Support`` a la vesion de unity que estemos usando.

## Configuración externa al unity

Se descomprime el ``sdk`` en la carpeta creada de ``sdk`` dentro del la carpeta de ``Assest`` en el proyecto de Unity.

Se añade el archivo ``google-service.json`` en la carpeta de ``Assets``.

## Configuración en Unity

Se ira a donde se extrajo el ``sdk`` y se seleccionara el archivo que vayamos a usar.

## Uso dentro de Unity

Se creara un script que se encargara de la conexion con **Firebase**, en mi caso lo llame **Realtime**.

Se creara un objeto vacio en el escenario y se le añadira el script creado.

#### Con esto ya se podria usar el Realtime de Firebase de forma propia sin tener que añadir alguna configuración a mayores, solo se debera de modificar la conexion de forma propia.

## Usos Interesantes

Implemente que al momento de comenzar el juego este cargue la posicion de los coleccionables que se encuentren en el RealTime. 

```csharp
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
```

De esta forma buscara todos los objetos monedas que haya en la base de datos y los cargara cuando comienze el juego.

Otra implementación fue la de que se guardase la posicion del jugador dentro de la base de datos, asi como la cantidad de monedas que este tiene

```csharp 
    _refClientes.Child("j1").Child("puntos").SetValueAsync(player.count);
    // cambiar el valor de j1, su campo y con su valor de posision x del transform
    _refClientes.Child("j1").Child("x").SetValueAsync(player.transform.position.x);
    // cambiar el valor de j1, su campo y con su valor de posision y del transform
    _refClientes.Child("j1").Child("y").SetValueAsync(player.transform.position.y);
    // cambiar el valor de j1, su campo y con su valor de posision z del transform
    _refClientes.Child("j1").Child("z").SetValueAsync(player.transform.position.z);
		
```