using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//Sistema day trading calcula la rentabilidad de un sistema que invierte durante un unico dia.
//La compra se genera en la apertura del dia siguiente a la señal de compra.
//La venta se genera al cierre de ese mismo dia.


namespace Sistemas
{
    class SistemaDay
    {
        //Modificar los atributos de nombre y descripcion 
        public string Nombre = "SistemaDayMacdAdx";
        public string Descripcion = "Cruce de MACD sobre MACDSeñal con adx >20";

        public int ComprasTotales;
        public float RentabilidadTotal;
        public float RentabilidadPorCompra;
        public List<String> Resultados = new List<string>();

        public void CalcularRentabilidad(List<Empresa> empresas)
        {
            bool condicion;
            foreach (var empresa in empresas)
            {
                for (int i = 100; i <= 2513; i++)
                {
                    //Condicion para comprar
                    condicion = empresa.Macd[i] > empresa.MacdSeñal[i] && empresa.Macd[i - 1] < empresa.MacdSeñal[i - 1] && empresa.Adx[i] > 20;
                    if (condicion)
                    {
                        ComprasTotales++;
                        RentabilidadTotal = RentabilidadTotal + empresa.CambioDesdeApertura[i + 1];
                        RentabilidadPorCompra = RentabilidadTotal / ComprasTotales;

                        Resultados.Add(new string("Simbolo: " + empresa.Nombre + " Dia proximo: " + empresa.Fecha[i + 1] + " Rentabilidad: " + empresa.CambioDesdeApertura[i + 1] * 100 + "% Total de compras: " + ComprasTotales + " Rentabilidad por compra: " + RentabilidadPorCompra * 100 + "%"));
                    }
                }
            }
        }

        public void ImprimirResultados()
        {
            //Modificar la direccion dependiendo de la ruta de la carpeta 
            StreamWriter streamWriter = new StreamWriter(@"C:\Users\C\Desktop\Programas\Sistemas\" + Nombre + ".log", true);
            streamWriter.WriteLine(Nombre);
            streamWriter.WriteLine(Descripcion);
            try
            {
                foreach (var resultado in Resultados)
                {
                    streamWriter.WriteLine(resultado);
                }
            }
            catch (Exception)
            {

            }
            streamWriter.Close();
        }
    }
}
