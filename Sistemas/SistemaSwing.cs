using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//Sistema swing trading calcula la rentabilidad de un sistema que invierte durante varios dias
//La compra se genera en la apertura del dia siguiente a la señal de compra.
//La venta se genera en la apertura del dia siguiente a la señal de venta.


namespace Sistemas
{
    class SistemaSwing
    {
        public string Nombre = "SistemaSwingMacdAdx";
        public string Descripcion = "Cruce de MACD sobre MACDSeñal con adx >20, venta cruce de MACDSeñal sobre MACD";

        public int ComprasTotales;
        public float RentabilidadTotal;
        public float RentabilidadPorCompra;
        public List<String> Resultados = new List<string>();

        public void CalcularRentabilidad(List<Empresa> empresas)
        {
            bool condicionCompra;
            bool condicionVenta;
            bool enCompra = false;
            float precioCompra = 0;
            float rentabilidad;
            int diasCompra = 0;
            string fechaCompra = "";

            foreach (var empresa in empresas)
            {
                for (int i = 100; i <= 2513; i++)
                {
                    if (!enCompra)
                    {
                        //Condicion para comprar
                        condicionCompra = empresa.Macd[i] > empresa.MacdSeñal[i] && empresa.Macd[i - 1] < empresa.MacdSeñal[i - 1] && empresa.Adx[i] > 20;
                        if (condicionCompra)
                        {
                            diasCompra = 0;
                            fechaCompra = empresa.Fecha[i + 1];
                            precioCompra = empresa.Apertura[i + 1];
                            enCompra = true;
                        }

                    }
                    else
                    {
                        diasCompra++;
                        //Condicion para vender
                        condicionVenta = empresa.Macd[i] < empresa.MacdSeñal[i] && empresa.Macd[i - 1] > empresa.MacdSeñal[i - 1];
                        if (condicionVenta)
                        {
                            ComprasTotales++;
                            rentabilidad = (empresa.Apertura[i + 1] / precioCompra) - 1;
                            RentabilidadTotal = RentabilidadTotal + rentabilidad;
                            RentabilidadPorCompra = RentabilidadTotal / ComprasTotales;

                            Resultados.Add(new string("Simbolo: " + empresa.Nombre + " Dia compra: " + fechaCompra + " Dia venta: " + empresa.Fecha[i + 1] + " Dias de duracion: " + diasCompra + " Rentabilidad: " + rentabilidad * 100 + "% Total de compras: " + ComprasTotales + " Rentabilidad por compra: " + RentabilidadPorCompra * 100 + "%"));
                            enCompra = false;
                        }
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
