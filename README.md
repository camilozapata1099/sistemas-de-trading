# sistemas-de-trading

Este programa sirve para proponer y evaluar sistemas de trading automáticos.
El programa carga los datos de 30 empresas del Dow Jones para un periodo desde el 1 de enero de 2010 hasta el 30 de diciembre del 2019.
Después calcula los indicadores técnicos EMA5, EMA10, EMA20, EMA50, MACD, estocástico, RSI y ADX.
Por último evalúa el desempeño del sistema simulando las compras realizadas en función de las condiciones de compra y de venta y genera un archivo .log con los resultados.


//Guía para usar el programa: 

-Cambiar la ruta del streamReader en el método CargarDatos() de la clase Empresa de acuerdo a la ruta en donde ubicas el proyecto.

-Cambiar la ruta del streamWriter en el método ImprimirResultados() de las clases SistemaDay y SistemaSwing de acuerdo a la ruta en donde ubicas el proyecto.

-Para un sistema de day trading (compra-venta en el mismo día) editar el nombre, la descripción y la condición de compra en la clase SistemaDay.

-Para un sistema de swing trading (compra-venta en un intervalo de días) editar el nombre, la descripción, la condición de compra y la condición de venta en la clase SistemaSwing.

Nota: los nombres de los sistemas se usan para crear los archivos .log donde se muestra el registro de los resultados (usar un nombre que no genere conflicto)

-Casi al final del metodo Main() de la clase Program hay 3 líneas para crear, calcular e imprimir resultados del sistema day trading y 3 líneas para el sistema swing trading. Si únicamente se está probando un sistema comentar el otro.

//Para modificar las condiciones de compra o venta de los sistemas:

-En caso de buscar cambios en indicadores: recordar que si usamos el índice i corresponde al día actual en el que va la evaluación del sistema y el índice i-1 es el día anterior.

-Los datos e indicadores que podemos usar para establecer las condiciones de compra y venta:

(La evaluación se realiza en un bucle foreach var empresa in empresas)

empresa.Apertura[]

empresa.Minimo[] 

empresa.Maximo[]

empresa.Cierre[]

empresa.CierreAjustado[]

empresa.Volumen[]

empresa.CambioDesdeApertura[]

empresa.CambioDesdeAyer[]

empresa.Ema5[]

empresa.Ema10[]

empresa.Ema20[]

empresa.Ema50[]

empresa.Macd[]

empresa.MacdSeñal[]

empresa.EstocasticoK[]

empresa.EstocasticoD[]

empresa.Rsi[]

empresa.Atr[]

empresa.Pdi[] //+DI

empresa.Ndi[] //-DI

empresa.Adx[]
