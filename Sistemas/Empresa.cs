using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sistemas
{
    class Empresa
    {
        public static List<string> ListarEmpresas()
        {
            List<String> lista = new List<String>();
            lista.Add("AAPL");
            lista.Add("AMGN");
            lista.Add("AXP");
            lista.Add("BA");
            lista.Add("CAT");
            lista.Add("CRM");
            lista.Add("CSCO");
            lista.Add("CVX");
            lista.Add("DIS");
            lista.Add("GS");
            lista.Add("HD");
            lista.Add("HON");
            lista.Add("IBM");
            lista.Add("INTC");
            lista.Add("JNJ");
            lista.Add("JPM");
            lista.Add("KO");
            lista.Add("MCD");
            lista.Add("MMM");
            lista.Add("MRK");
            lista.Add("MSFT");
            lista.Add("NKE");
            lista.Add("PFE");
            lista.Add("PG");
            lista.Add("TRV");
            lista.Add("UNH");
            lista.Add("V");
            lista.Add("VZ");
            lista.Add("WBA");
            lista.Add("WMT");
            return lista;
        }

        public string Nombre;
        public string[] Fecha = new string[2515];
        public float[] Apertura = new float[2515];
        public float[] Minimo = new float[2515];
        public float[] Maximo = new float[2515];
        public float[] Cierre = new float[2515];
        public float[] CierreAjustado = new float[2515];
        public float[] Volumen = new float[2515];
        public float[] CambioDesdeApertura = new float[2515];
        public float[] CambioDesdeAyer = new float[2515];

        //Indicadores
        public float[] Ema5 = new float[2515];
        public float[] Ema10 = new float[2515];
        public float[] Ema20 = new float[2515];
        public float[] Ema50 = new float[2515];
        public float[] Macd = new float[2515];
        public float[] MacdSeñal = new float[2515];
        public float[] EstocasticoK = new float[2515];
        public float[] EstocasticoD = new float[2515];
        public float[] Rsi = new float[2515];
        public float[] Atr = new float[2515];
        public float[] Pdi = new float[2515];
        public float[] Ndi = new float[2515];
        public float[] Adx = new float[2515];

        public Empresa(string nombre)
        {
            this.Nombre = nombre;
        }

        public void CargarDatos()
        {
            StreamReader streamReader = new StreamReader(@"C:\Users\C\Desktop\Programas\Sistemas\Datos\" + Nombre + ".csv");
            string fila = streamReader.ReadLine(); //Fila con nombres de las columnas
            for(int i=0; i<=2514;i++)
            {
                fila = streamReader.ReadLine(); //Cada fila de datos
                string[] celdas = fila.Split(',');
                //Datos de la tabla
                Fecha[i] = celdas[0];
                Apertura[i] = Convert.ToSingle(celdas[1]);
                Maximo[i] = Convert.ToSingle(celdas[2]);
                Minimo[i] = Convert.ToSingle(celdas[3]);
                Cierre[i] = Convert.ToSingle(celdas[4]);
                CierreAjustado[i] = Convert.ToSingle(celdas[5]);
                Volumen[i] = Convert.ToSingle(celdas[6]);

                //Datos calculados
                CambioDesdeApertura[i] = (Cierre[i] / Apertura[i])-1;
                if (i != 0)
                {
                    CambioDesdeAyer[i] = (Cierre[i] / Cierre[i-1]) - 1;
                }
                else
                {
                    CambioDesdeAyer[i] = 0;
                }
            }
            streamReader.Close();
        }

        public void CalcularIndicadores()
        {
            CalcularEma5();
            CalcularEma10();
            CalcularEma20();
            CalcularEma50();
            CalcularMacd();
            CalcularEstocastico();
            CalcularRsi();
            CalcularAdx();
        }

        public void CalcularEma5()
        {
            float multiplo = 0.3333f;
            for (int i = 0; i <= 2514; i++)
            {
                if (i != 0)
                {
                    Ema5[i] = ((Cierre[i] - Ema5[i - 1]) * multiplo) + Ema5[i - 1];
                }
                else
                {
                    Ema5[i] = Cierre[i];
                }
            }
        }

        public void CalcularEma10()
        {
            float multiplo = 0.1818f;
            for (int i = 0; i <= 2514; i++)
            {
                if (i != 0)
                {
                    Ema10[i] = ((Cierre[i] - Ema10[i - 1]) * multiplo) + Ema10[i - 1];

                }
                else
                {
                    Ema10[i] = Cierre[i];
                }
            }
        }

        public void CalcularEma20()
        {
            float multiplo = 0.0952f;
            for (int i = 0; i <= 2514; i++)
            {
                if (i != 0)
                {
                    Ema20[i] = ((Cierre[i] - Ema20[i - 1]) * multiplo) + Ema20[i - 1];
                }
                else
                {
                    Ema20[i] = Cierre[i];
                }
            }
        }

        public void CalcularEma50()
        {
            float multiplo = 0.0392f;
            for (int i = 0; i <= 2514; i++)
            {
                if (i != 0)
                {
                    Ema50[i] = ((Cierre[i] - Ema50[i - 1]) * multiplo) + Ema50[i - 1];
                }
                else
                {
                    Ema50[i] = Cierre[i];
                }
            }
        }

        public void CalcularMacd()
        {
            float multiploEma9 = 0.2f;
            float multiploEma12 = 0.1538f;
            float multiploEma26 = 0.0740f;
            float[] Ema12 = new float[2515];
            float[] Ema26 = new float[2515];

            for (int i = 0; i <= 2514; i++)
            {
                if (i != 0)
                {
                    Ema12[i] = ((Cierre[i] - Ema12[i - 1]) * multiploEma12) + Ema12[i - 1];
                    Ema26[i] = ((Cierre[i] - Ema26[i - 1]) * multiploEma26) + Ema26[i - 1];
                    Macd[i] = Ema12[i] - Ema26[i];
                    MacdSeñal[i] = ((Macd[i] - MacdSeñal[i - 1]) * multiploEma9) + MacdSeñal[i - 1];
                }
                else
                {
                    Ema12[i] = Cierre[i];
                    Ema26[i] = Cierre[i];
                    Macd[i] = 0;
                    MacdSeñal[i] = 0;
                }
            }
        }

        public void CalcularEstocastico()
        {
            float formula;
            float minimo14;
            float maximo14;

            for (int i = 0; i <= 2514; i++)
            {
                minimo14 = Minimo[i];
                maximo14 = Maximo[i];
                if (i >= 13)
                {
                    for(int j = i-13; j <= i;j++)
                    {
                        if (Minimo[j]< minimo14)
                        {
                            minimo14 = Minimo[j];
                        }
                        if (Maximo[j] > maximo14)
                        {
                            maximo14 = Maximo[j];
                        }
                    }

                    formula = 100 * (Cierre[i] - minimo14) / (maximo14 - minimo14);

                    EstocasticoK[i] = (2 * EstocasticoK[i - 1] + formula) / 3;
                    EstocasticoD[i] = (2 * EstocasticoD[i - 1] + EstocasticoK[i]) / 3;

                }
                else
                {
                    EstocasticoD[i] = 50;
                    EstocasticoK[i] = 50;
                }
            }
        }

        public void CalcularRsi()
        {
            float u=0, u14=0, d=0, d14=0, rs=0;

            for (int i = 0; i <= 2514; i++)
            {
                if (i != 0)
                {
                    if (CambioDesdeAyer[i] > 0)
                    {
                        u = CambioDesdeAyer[i];
                        d = 0;
                    }
                    else
                    {
                        u = 0;
                        d = -CambioDesdeAyer[i];
                    }
                    u14 = (u + 13 * u14) / 14;
                    d14 = (d + 13 * d14) / 14;
                    rs = u14 / d14;
                    Rsi[i] = 100 - (100 / (1 + rs));

                }
                else
                {
                    u = 0;
                    d = 0;
                    d14 = 0;
                    u14 = 0;
                    rs = 0;
                    Rsi[i] = 0;
                }
            }
        }

        public void CalcularAdx()
        {
            float tr = 0, ndm = 0, pdm = 0, pdm14 = 0, ndm14 = 0, dx = 0;

            for (int i = 0; i <= 2514; i++)
            {
                tr = 0;
                ndm = 0;
                pdm = 0;
                if (i != 0)
                {
                    tr = Maximo[i] - Minimo[i];
                    if (Math.Abs(Maximo[i]-Cierre[i-1]) > tr)
                    {
                        tr = Math.Abs(Maximo[i] - Cierre[i - 1]);
                    }
                    if (Math.Abs(Minimo[i] - Cierre[i - 1]) > tr)
                    {
                        tr = Math.Abs(Minimo[i] - Cierre[i - 1]);
                    }

                    if ((Maximo[i] - Maximo[i - 1]) > (Minimo[i - 1] - Minimo[i]))
                    {
                        if ((Maximo[i] - Maximo[i - 1])>0)
                        {
                            pdm = Maximo[i] - Maximo[i - 1];
                        }
                    }
                    else
                    {
                        if ((Minimo[i - 1] - Minimo[i]) > 0)
                        {
                            ndm = Minimo[i - 1] - Minimo[i];
                        }
                    }

                    Atr[i] = (tr + 13 * Atr[i-1]) / 14;

                    pdm14 = (pdm + 13 * pdm14) / 14;
                    ndm14 = (ndm + 13 * ndm14) / 14;

                    Pdi[i] = 100 * (pdm14 / Atr[i]);
                    Ndi[i] = 100 * (ndm14 / Atr[i]);

                    dx = 100 * Math.Abs((Pdi[i] - Ndi[i]) / (Pdi[i] + Ndi[i]));
                    Adx[i] = (dx + 13 * Adx[i-1]) / 14;
                }
                else
                {
                    tr = 0;
                    ndm = 0;
                    pdm = 0;
                    Atr[i] = 0;
                    pdm14 = 0;
                    ndm14 = 0;
                    Pdi[i] = 0;
                    Ndi[i] = 0;
                    dx = 0;
                    Adx[i] = 0;
                }
            }
        }
    }
}
