using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FrontShipWar
{
    public static class Logic
    {
        public static readonly string IP = @"http://localhost:8080/";
        private static string playerId;
        public static string lobbyId;
        public static GameStates currentGameState;
        public static bool isPlayerHost;
        public static List<ShipButton> shipButtons = new List<ShipButton>();
        public static bool CreatePlayer()
        {
            playerId = Get(IP + "createplayer");
            return true;
        }
        public static bool CreateLobby()
        {
            Logic.lobbyId = Get(IP + "createlobby" + "?id=" + playerId);
            Logic.isPlayerHost= true;
            return true;
        }
        public static string ConnectToLobby(string lobbyId)
        {
            Logic.lobbyId = lobbyId;
            Logic.isPlayerHost= false;
            return Get(IP + "connecttolobby" + "?id=" + playerId+"&lobbyId="+lobbyId);
        }
        public static string SetMatrix(string shipMatrix)
        {
            return Post(IP + "setshipmatrix" + "?id=" + playerId + "&lobbyId=" + lobbyId,shipMatrix);
        }
        public static string Shoot(string shipShoot)
        {
            return Post(IP + "shoot" + "?id=" + playerId + "&lobbyId=" + lobbyId, shipShoot);
        }
        public static string GetLastOppShoot()
        {
            return Get(IP + "getlastoppshoot" + "?id=" + playerId + "&lobbyId=" + lobbyId);
        }
        public static async Task<string> GetLobbyState(string lobbyId)
        {
            return await GetAsync(IP + "getlobbygamestate" + "?"+ "lobbyId=" + lobbyId);
        }

        private static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        private static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        private static string Post(string uri, string data, string contentType = @"application/json", string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        private static async Task<string> PostAsync(string uri, string data, string contentType = @"application/json", string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (Stream requestBody = request.GetRequestStream())
            {
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
