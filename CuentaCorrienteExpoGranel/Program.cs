using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CuentaCorrienteExpoGranel
{
   class Program
   {
    

      static void Main(string[] args)
      {

         Task t = new Task(HTTP_GET);
         t.Start();
         Console.ReadLine();        
      }

      static async void HTTP_GET()
      {
         try
         {
            string partone;
            string parttwo;
            string resultalter;
            string TARGETURL = string.Concat("https://api.expogranel.com/api/granel/cuentacorriente?fechainicio=", DateTime.Now.ToString("yyyy-MM-dd"), "&fechafin=", DateTime.Now.ToString("yyyy-MM-dd"), "&ingenio=19&producto=1&bodega=1");
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
               //Root root = JsonConvert.DeserializeObject<Root>(result);
               //result= "{"codigoerror": 0,"error": "","data": {"ingresosnormales": "29103870","nominacion": "","ingresosajustes": "253948","comentarios": "Saldo inicial","egresosajustes": "22000000"}}"
               partone = result.Split(new string[] { " |TRINIDAD" }, StringSplitOptions.None)[0].Split(new String[] { ":{" }, StringSplitOptions.None)[0];
               parttwo = result.Split(new string[] { " |TRINIDAD" }, StringSplitOptions.None)[1];

               resultalter = string.Concat(partone.Substring(0, partone.Length - 1), parttwo.Replace("-", "").Replace(" ", "").Replace("}}}", "}}"));
               //string[] data = result.Split(new string[] { "|"}, StringSplitOptions.None);
               RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(resultalter);
               SaveResult(rootObject.data.ingresosnormales,
                           rootObject.data.nominacion,
                           rootObject.data.ingresosajustes,
                           rootObject.data.comentarios,
                           rootObject.data.egresosajustes);
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("Response StatusCode: " + ex.Message);
         }
         finally
         {
            Environment.Exit(-1);
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

            System.Data.Common.DbCommand cmd = namedDB.GetSqlStringCommand("INSERT INTO TblCuentaCorrienteExpoGranel(FechaDeConsulta,IngresosNormales,Nominacion,IngresosAjustes,Comentarios,EgresosAjustes) VALUES (@PFechaDeConsulta,@PIngresosNormales,@PNominacion,@PIngresosAjustes,@PComentarios,@PEgresosAjustes)");
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
            Console.WriteLine("Error: " +ex.Message);
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

}
