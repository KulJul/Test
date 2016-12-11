using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;

namespace Parser
{

    class Program
    {
        struct TimeStruct
        {
            public int idTime;
            public List<string> weather;
        }

        struct DateStruct
        {
            public string date;
            public List<TimeStruct> timeStruct;
        }

        struct TownStruct
        {
            public string towns;
            public List<DateStruct> dateStruct;
        }

        static Timer timer;
        const string adress = @"https://www.gismeteo.ru/";
        const string xpathAdress = "//div[@class='cities']/ul/li/a";
        const string xpathAdressWeather = "//div[@class='wsection wdata']/table/tbody/tr/td";
        const string xpathAdressWeatherDate = "//div[@class='wtabs wrap']/div/dl";
                 

        static private HtmlNodeCollection getNode(string adress, string xpathParser)
        {
            var web = (HttpWebRequest)HttpWebRequest.Create(adress);
            var urltowns = new Dictionary<string, string>();
            HtmlNodeCollection urlCode;

            using (var response = (HttpWebResponse)web.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var objSR = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8"), true);

                    HtmlDocument hap = new HtmlDocument();
                    hap.LoadHtml(objSR.ReadToEnd());
                    urlCode = hap.DocumentNode.SelectNodes(xpathParser);
                }
            }
            return urlCode;
        }

        static private void connectBD(List<TownStruct> weatherStr)
        {
            int temp = 0;
            int temp2 = 0;
            int temp3 = 0;
            int temp4 = 0;

            const string connectString = "Database=test_perminova;Data Source=localhost;User Id=root;Password=123";

            string queryStrTown = "INSERT INTO test_perminova.town (NumberTown, TownName) value ";
            string queryStrDate = "INSERT INTO test_perminova.date (NumberDate, DateName) value ";
            string queryStrWeather = "INSERT INTO test_perminova.weather(NumberWeather, IdTown, IdDate," +
                                     "IdTime, Characteristic, Temperature, Pressure, Wind, Wet, Feeling) value ";

            string InsertStrTown = "";
            string InsertStrDate = "";
            string InsertStrWeather = "";

            string[] tables = { "test_perminova.town", "test_perminova.date", "test_perminova.weather" };
            

            var queryStrDel = "";
            MySqlConnection conn = new MySqlConnection(connectString);
            conn.Open();
            foreach (var table in tables)
                queryStrDel = queryStrDel + "Delete from " + table + ";";

            using (MySqlCommand cmd = new MySqlCommand(queryStrDel, conn))
                cmd.ExecuteNonQuery();
            conn.Close();


            foreach (var nodeTown in weatherStr)
            {
                temp++;
                InsertStrTown = InsertStrTown + queryStrTown + "( '" + temp + "', '" + nodeTown.towns + "' ) ;";
                
                foreach (var nodeDate in nodeTown.dateStruct)
                {
                    temp2++;
                    if (temp2 < 4)
                        InsertStrDate = InsertStrDate + queryStrDate + "( '" + temp2 + "', '" + nodeDate.date + "' ) ;";
                    
                    temp3 = 0;
                    foreach (var nodeTime in nodeDate.timeStruct)
                    {
                        temp3++;
                        temp4++;

                        InsertStrWeather =  InsertStrWeather + queryStrWeather + 
                                            "( '" + temp4 + "', '" + temp + "', '" + temp2 + "', '" + temp3 +
                                            "', '" + nodeTime.weather[0] + "', '" + nodeTime.weather[1] +
                                            "', '" + nodeTime.weather[2] + "', '" + nodeTime.weather[3] +
                                            "', '" + nodeTime.weather[4] + "', '" + nodeTime.weather[5] + "') ;";
                    }
                }
            }

            string queryStrUnion = InsertStrWeather + InsertStrTown + InsertStrDate;

            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(queryStrUnion, conn))
            {
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        static private void savaDate(HtmlNodeCollection weatherCodeDate, HtmlNodeCollection weatherCode, List<TownStruct> weatherStr, string urlTown)
        {
            int temp = 1;
            const int partsTimeTemp = 4; // парсим 3 дня
            const int partsWeatherTemp = 6; // парсим 6 элементов погоды
            TownStruct StrTempTown;
            StrTempTown.dateStruct = new List<DateStruct>();

            StrTempTown.towns = urlTown;

            foreach (var mat in weatherCodeDate)
            {
                DateStruct StrTempDate;
                StrTempDate.timeStruct = new List<TimeStruct>();

                StrTempDate.date = mat.InnerText.Insert(2, " ");

                for (int partOfDay = 0; partOfDay < partsTimeTemp; partOfDay++)
                {
                    TimeStruct StrTempTime;
                    StrTempTime.weather = new List<string>();

                    for (int partOfWeather = 0; partOfWeather < partsWeatherTemp; partOfWeather++)
                    {
                        StrTempTime.weather.Add(weatherCode.ElementAt(temp).InnerText);
                        temp++;
                    }

                    temp++;
                    StrTempTime.idTime = partOfDay;
                    StrTempDate.timeStruct.Add(StrTempTime);
                }

                StrTempTown.dateStruct.Add(StrTempDate);

            }

            weatherStr.Add(StrTempTown);

        }

        static private void formateDate(List<TownStruct> weatherStr)
        {
            foreach (var nodeTown in weatherStr)
            {
                foreach (var nodeDate in nodeTown.dateStruct)
                {
                    foreach (var nodeTime in nodeDate.timeStruct)
                    {
                        nodeTime.weather[1] = nodeTime.weather[1].Replace("&minus;", "-");
                        nodeTime.weather[1] = nodeTime.weather[1].Remove(nodeTime.weather[1].LastIndexOf("+"));
                        nodeTime.weather[2] = nodeTime.weather[2].Substring(0, 3);

                        var temp = nodeTime.weather[3].TrimStart('Ю', 'З', 'С', 'В');

                        if (temp.Count() >= 6)
                            temp = temp.Substring(0, 2);
                        else
                            temp = temp.Substring(0, 1);

                        nodeTime.weather[3] = nodeTime.weather[3].Remove(nodeTime.weather[3].IndexOf(temp)) + "  " + temp;

                        nodeTime.weather[4] = nodeTime.weather[4].Substring(0, 2);
                        nodeTime.weather[5] = nodeTime.weather[5].Replace("&minus;", "-");
                        nodeTime.weather[5] = nodeTime.weather[5].Remove(nodeTime.weather[5].LastIndexOf("+"));
                    }
                }
            }
        }

        static void MainVspom()
        {
            var urltowns = new Dictionary<string, string>();
            List<TownStruct> weatherStr = new List<TownStruct>();

            // Получение ссылок на погоду в других регионах
            HtmlNodeCollection urlCode = getNode(adress, xpathAdress);
            if (urlCode != null)
            {
                foreach (HtmlNode node in urlCode)
                {
                    if (!urltowns.ContainsKey(node.InnerText))
                        urltowns.Add(node.InnerText, node.GetAttributeValue("href", null));
                }
            }

            //получение погоды
            foreach (var urlTown in urltowns)
            {
                HtmlNodeCollection weatherCodeDate = getNode(adress + urltowns.First().Value, xpathAdressWeatherDate);
                HtmlNodeCollection weatherCode = getNode(adress + urlTown.Value, xpathAdressWeather);

                if ((weatherCode == null) || (weatherCodeDate == null))
                    continue;

                savaDate(weatherCodeDate, weatherCode, weatherStr, urlTown.Key);
            }

            formateDate(weatherStr);

            connectBD(weatherStr);
        }

        static void Main(string[] args)
        {
            try
            {
                TimerCallback tc = new TimerCallback(o=>MainVspom());
                timer = new Timer(tc, null, 0, 50000);
                while (true) 
                    Thread.Sleep(1000);
            }

            catch (Exception ex)
            {
                Console.Write("Ошибка: " + ex.Message);
            }

        }
    }

}
