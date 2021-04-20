using System;
using System.Collections.Generic;
using System.Text;

namespace Sistemas
{
    class Program
    {
        static void Main(string[] args)
        {
            //Importar la lista de nombres
            List<String> nombres = Empresa.ListarEmpresas();

            //Crear una lista de empresas
            List<Empresa> empresas = new List<Empresa>();
            foreach(var nombre in nombres)
            {
                empresas.Add(new Empresa(nombre));
            }

            //Cargar la informacion de cada empresa
            foreach(var empresa in empresas)
            {
                empresa.CargarDatos();
                empresa.CalcularIndicadores();
            }

            //Probar sistema day trading
            SistemaDay sistemaDay = new SistemaDay();
            sistemaDay.CalcularRentabilidad(empresas);
            sistemaDay.ImprimirResultados();

            //Probar sistema swing trading
            SistemaSwing sistemaSwing = new SistemaSwing();
            sistemaSwing.CalcularRentabilidad(empresas);
            sistemaSwing.ImprimirResultados();

            Console.WriteLine("La prueba de sistemas ha terminado");
        }
    }
}
