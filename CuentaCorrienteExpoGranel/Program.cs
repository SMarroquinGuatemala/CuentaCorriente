using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CuentaCorrienteExpoGranel
{
    class Program
    {
     
     static DateTime  FechaInicial;
      static DateTime FechaInicialMaxa;
      static DateTime FechaFinal;

      static void Main(string[] args)
       {
         try
         {

            //FechaFinal = DateTime.Now; //Convert.ToDateTime( DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
            FechaFinal = Convert.ToDateTime("01/06/2021");
            FechaInicial = Convert.ToDateTime(FechaFinal.AddDays(-1).ToString("dd/MM/yyyy"));
            FechaInicialMaxa = Convert.ToDateTime(FechaFinal.AddDays(-1).ToString("dd/MM/yyyy"));


            //FechaInicialMaxa=Convert.ToDateTime("01/11/2020");
            //FechaInicial = Convert.ToDateTime("06/11/2020");
            
            //FechaFinal = Convert.ToDateTime("06/11/2020");

            MAXA_HTTP_GET();


            Task t = new Task(HTTP_GETTR);
            t.Start();
            ////Task t2 = new Task(HTTP_GETSD);
            ////t2.Start();
            Console.ReadLine();

         }
         catch (Exception ex)
         {
            Console.WriteLine("Error en el metodo Main: " +ex.Message);
            Console.ReadLine();
            }

        }

         static void MAXA_HTTP_GET()
         {
         try { 
            
            string jsonMAXA;
            MAXA.movimientosClient movimientosClient = new MAXA.movimientosClient();

            //jsonMAXA = movimientosClient.movimientos("ws_sandiego", "WSS@nd1r3c3P$", DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
            jsonMAXA = movimientosClient.movimientos("ws_sandiego", "WSS@nd1r3c3P$", FechaInicialMaxa.ToString("dd/MM/yyyy"), FechaInicial.ToString("dd/MM/yyyy"));
            List<MaxaResult> maxaResults = new List<MaxaResult>();
            maxaResults = JsonConvert.DeserializeObject<List<MaxaResult>>(jsonMAXA);
            if (maxaResults.Count > 0)
            {
                  SaveResultMAXA(maxaResults, FechaInicialMaxa);
            }

         }
         catch (Exception ex) 
         {
            Console.WriteLine("Error en el metodo MAXA_HTTP_GET: " + ex.Message);
            Console.ReadLine();
         }
         }
        static async void HTTP_GETTR()
        {
            try
            {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string CodigoIngenio, Ingenio;

                Ingenio = "TRINIDAD";
            //DateTime.Now.ToString("yyyy-MM-dd")
            string TARGETURL = string.Concat("https://api.expogranel.com/api/granel/cuentacorriente?fechainicio=", FechaInicial.ToString("yyyy-MM-dd") , "&fechafin=",FechaFinal.ToString("yyyy-MM-dd"), "&ingenio=19&producto=1&bodega=1");
            
            //string TARGETURL = string.Concat("https://api.expogranel.com/api/granel/cuentacorriente?fechainicio=2019-12-20&fechafin=2019-12-21&ingenio=19&producto=1&bodega=1");
            HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                var byteArray = Encoding.ASCII.GetBytes("serviciosInternet@sandiego.com.gt:SI1589SD");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                HttpResponseMessage response = await client.GetAsync(TARGETURL);
                HttpContent content = response.Content;
                // ... Check Status Code                                
                if ((int)response.StatusCode == 200)
                {
                    Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();
                    Dictionary<string, string> nodes = new Dictionary<string, string>();
                    // put your JSON object here
                    JObject rootObject = JObject.Parse(result);
                    ParseJson(rootObject, nodes);
                    // nodes dictionary contains xpath-like node locations
                    Console.WriteLine("JSON:");

                    InsertMovimientoDiario(nodes, Ingenio, Convert.ToDateTime(FechaInicial.ToString("dd/MM/yyyy")));

                    /*//TMP
                     DateTime Fecha;
                     Fecha = Convert.ToDateTime("01/11/2019");
                     while (Fecha <= Convert.ToDateTime("20/12/2019"))
                     {
                         InsertMovimientoDiario(nodes, Ingenio, Convert.ToDateTime(Fecha));
                         Fecha = Fecha.AddDays(1);
                     }*/
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
            HTTP_GETSD();
            }
        }

        static async void HTTP_GETSD()
        {
            try
            {
                string CodigoIngenio, Ingenio;

                Ingenio = "SAN DIEGO";
                //DateTime.Now.ToString("yyyy-MM-dd")
                string TARGETURL = string.Concat("https://api.expogranel.com/api/granel/cuentacorriente?fechainicio=", FechaInicial.ToString("yyyy-MM-dd"), "&fechafin=", FechaFinal.ToString("yyyy-MM-dd"), "&ingenio=9&producto=1&bodega=1");
                //string TARGETURL = string.Concat("https://api.expogranel.com/api/granel/cuentacorriente?fechainicio=2019-12-20&fechafin=2019-12-21&ingenio=9&producto=1&bodega=1");
                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                var byteArray = Encoding.ASCII.GetBytes("serviciosInternet@sandiego.com.gt:SI1589SD");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                HttpResponseMessage response = await client.GetAsync(TARGETURL);
                HttpContent content = response.Content;
                // ... Check Status Code                                
                if ((int)response.StatusCode == 200)
                {
                    Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();
                    Dictionary<string, string> nodes = new Dictionary<string, string>();
                    // put your JSON object here
                    JObject rootObject = JObject.Parse(result);
                    ParseJson(rootObject, nodes);
                    // nodes dictionary contains xpath-like node locations
                    Console.WriteLine("JSON:");

                    InsertMovimientoDiario(nodes, Ingenio, Convert.ToDateTime(FechaInicial.ToString("dd/MM/yyyy")));
                    /*//TMP
                    DateTime Fecha;
                    Fecha = Convert.ToDateTime("01/11/2019");
                    while (Fecha <= Convert.ToDateTime("19/12/2019"))
                    {
                        InsertMovimientoDiario(nodes, Ingenio, Convert.ToDateTime(Fecha));
                        Fecha = Fecha.AddDays(1);
                    }*/
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
             //  Environment.Exit(-1);
            }
        }

        public static void InsertMovimientoDiario(Dictionary<string, string> nodes, string Ingenio, DateTime Fecha)
         {
            try
            {
                string getProperty = "";
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database namedDB = factory.Create("Produccion");
                decimal IngresosMaquila, IngresosAjustes, IngresosNormales, EgresosMaquila, EgresosAjustes, EgresosNormales;
                string Nominacion, Comentarios;


                IngresosMaquila = 0;
                IngresosAjustes = 0;
                IngresosNormales = 0;
                EgresosMaquila = 0;
                EgresosAjustes = 0;
                EgresosNormales = 0;
                Comentarios = "";
                Nominacion = "";

                getProperty = "data." + Fecha.ToString("yyyyMMdd") + "|" + Ingenio + ".";

                foreach (string key in nodes.Keys)
                {
                    if (key.IndexOf(getProperty) == 0)
                    {
                        if (key.IndexOf(".ingresos-normales") > 0)
                        {
                            IngresosNormales = Convert.ToDecimal(nodes[key]);
                        }
                        if (key.IndexOf(".ingresos-maquila") > 0)
                        {
                            IngresosMaquila = Convert.ToDecimal(nodes[key]);
                        }
                        if (key.IndexOf(".ingresos-ajustes") > 0)
                        {
                            IngresosAjustes = Convert.ToDecimal(nodes[key]);
                        }
                        if (key.IndexOf(".egresos-normales") > 0)
                        {
                            EgresosNormales = Convert.ToDecimal(nodes[key]);
                        }
                        if (key.IndexOf(".egresos-maquila") > 0)
                        {
                            EgresosMaquila = Convert.ToDecimal(nodes[key]);
                        }
                        if (key.IndexOf(".egresos-ajustes") > 0)
                        {
                            EgresosAjustes = Convert.ToDecimal(nodes[key]);
                        }

                        if (key.IndexOf(".comentario") > 0)
                        {
                            Comentarios = Comentarios + " " + nodes[key];
                        }

                        if (key.IndexOf(".nominacion") > 0)
                        {
                            Nominacion = nodes[key];
                        }
                    }

                    Console.WriteLine(key + " = " + nodes[key]);
                }

                namedDB.ExecuteNonQuery(System.Data.CommandType.Text, "delete from TblCuentaCorrienteExpoGranel where ingenio= '" + Ingenio + "' and fecha = '" + Fecha.ToString("dd/MM/yyyy") + "'");

                System.Data.Common.DbCommand cmd = namedDB.GetSqlStringCommand("INSERT INTO TblCuentaCorrienteExpoGranel(Ingenio,Fecha,IngresosMaquila,IngresosAjustes,IngresosNormales,EgresosMaquila,EgresosAjustes,EgresosNormales,Nominacion,Comentarios) VALUES (@Ingenio,@Fecha,@IngresosMaquila,@IngresosAjustes,@IngresosNormales,@EgresosMaquila,@EgresosAjustes,@EgresosNormales,@Nominacion,@Comentarios)");
                namedDB.AddInParameter(cmd, "Ingenio", System.Data.DbType.String, Ingenio);
                namedDB.AddInParameter(cmd, "Fecha", System.Data.DbType.Date, Fecha);
                namedDB.AddInParameter(cmd, "IngresosMaquila", System.Data.DbType.Decimal, IngresosMaquila);
                namedDB.AddInParameter(cmd, "IngresosAjustes", System.Data.DbType.Decimal, IngresosAjustes);
                namedDB.AddInParameter(cmd, "IngresosNormales", System.Data.DbType.Decimal, IngresosNormales);
                namedDB.AddInParameter(cmd, "EgresosMaquila", System.Data.DbType.Decimal, EgresosMaquila);
                namedDB.AddInParameter(cmd, "EgresosAjustes", System.Data.DbType.Decimal, EgresosAjustes);
                namedDB.AddInParameter(cmd, "EgresosNormales", System.Data.DbType.Decimal, EgresosNormales);
                namedDB.AddInParameter(cmd, "Nominacion", System.Data.DbType.String, Nominacion);
                namedDB.AddInParameter(cmd, "Comentarios", System.Data.DbType.String, Comentarios);
                namedDB.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public static void SaveResultMAXA(List<MaxaResult> maxaResults, DateTime Fecha)
        {

            try
            {
                // Configure the DatabaseFactory to read its configuration from the .config file
                DatabaseProviderFactory factory = new DatabaseProviderFactory();

                // Create a Database object from the factory using the connection string name.
                Database namedDB = factory.Create("Produccion");
                  string sql =  "delete from TblCuentaCorrienteMAXA where Fecha >=@PFecha";
                
                  System.Data.Common.DbCommand sqlCommand  = namedDB.GetSqlStringCommand(sql);
                  namedDB.AddInParameter(sqlCommand, "PFecha", System.Data.DbType.DateTime, Fecha);
                  namedDB.ExecuteNonQuery(sqlCommand);

            foreach (var item in maxaResults)
                {
                    namedDB.ExecuteNonQuery("UPICuentaCorrienteMAXA", item.tipo_movimiento, item.fecha, item.serie, item.numero, item.convenio, item.sacos, item.calidad, item.empresa, item.identificador);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public static void SaveResult(decimal IngresosNormales, string Nominacion, decimal IngresosAjustes, string Comentarios, decimal EgresosAjustes)
        {
            try
            {
            
                // Configure the DatabaseFactory to read its configuration from the .config file
                DatabaseProviderFactory factory = new DatabaseProviderFactory();

                // Create a Database object from the factory using the connection string name.
                Database namedDB = factory.Create("Produccion");
                namedDB.ExecuteNonQuery(System.Data.CommandType.Text, "delete from TblCuentaCorrienteExpoGranel");

                System.Data.Common.DbCommand cmd = namedDB.GetSqlStringCommand("INSERT INTO TblCuentaCorrienteExpoGranel(Fecha,IngresosNormales,Nominacion,IngresosAjustes,Comentarios,EgresosAjustes) VALUES (@PFechaDeConsulta,@PIngresosNormales,@PNominacion,@PIngresosAjustes,@PComentarios,@PEgresosAjustes)");
                namedDB.AddInParameter(cmd, "PFechaDeConsulta", System.Data.DbType.Date, DateTime.Now);
                namedDB.AddInParameter(cmd, "PIngresosNormales", System.Data.DbType.Decimal, IngresosNormales);
                namedDB.AddInParameter(cmd, "PNominacion", System.Data.DbType.String, Nominacion);
                namedDB.AddInParameter(cmd, "PIngresosAjustes", System.Data.DbType.Decimal, IngresosAjustes);
                namedDB.AddInParameter(cmd, "PComentarios", System.Data.DbType.String, Comentarios);
                namedDB.AddInParameter(cmd, "PEgresosAjustes", System.Data.DbType.Decimal, EgresosAjustes);
                namedDB.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
        /// <summary>
        /// Parse a JSON object and return it as a dictionary of strings with keys showing the heirarchy.
        /// </summary>
        /// <param name = "token"></param>
        /// <param name = "nodes"></param>
        /// <param name = "parentLocation"></param>
        /// <returns></returns>
        static bool ParseJson(JToken token, Dictionary<string, string> nodes, string parentLocation = "")
        {
            if (token.HasValues)
            {
                foreach (JToken child in token.Children())
                {
                    if (token.Type == JTokenType.Property)
                    {
                        if (parentLocation == "")
                        {
                            parentLocation = ((JProperty)token).Name;
                        }
                        else
                        {
                            parentLocation += "." + ((JProperty)token).Name;
                        }
                    }

                    ParseJson(child, nodes, parentLocation);
                }

                // we are done parsing and this is a parent node
                return true;
            }
            else
            {
                // leaf of the tree
                if (nodes.ContainsKey(parentLocation))
                {
                    // this was an array
                    nodes[parentLocation] += "|" + token.ToString();
                }
                else
                {
                    // this was a single property
                    nodes.Add(parentLocation, token.ToString());
                }

                return false;
            }
        }

    }
    public class Data
    {
        public decimal ingresosnormales { get; set; }
        public string nominacion { get; set; }
        public decimal ingresosajustes { get; set; }
        public string comentarios { get; set; }
        public decimal egresosajustes { get; set; }
    }

    public class RootObject
    {
        public int codigoerror { get; set; }
        public string error { get; set; }
        public Data data { get; set; }
    }


    public class MaxaResult
    {
        public int codigo_ingenio { get; set; }
        public string tipo_movimiento { get; set; }
        public DateTime fecha { get; set; }
        public string serie { get; set; }
        public Int64 numero { get; set; }
        public string convenio { get; set; }
        public decimal sacos { get; set; }
        public string calidad { get; set; }
        public string empresa { get; set; }
        public int identificador { get; set; }
    }


}
