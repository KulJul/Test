using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;

namespace WcfServiceWeather
{
      // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Service1 : IService1
    {

        public class TimeStruct
        {
            public string Time;
            public List<string> weather;
        }

        public class DateStruct
        {
            public string date;
            public List<TimeStruct> timeStruct;
        }

        public class TownStruct
        {
            public string towns;
            public List<DateStruct> dateStruct;
        }


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        static private void savaDate(DataTable weatherCodeDate, DataTable weatherCode, List<TownStruct> weatherStr, DataTable townTable)
        {
            const int partsTimeTemp = 4; // парсим 3 дня
            const int partsWeatherTemp = 6; // парсим 6 элементов погоды

            string[] partArray = new string[] { "Ночь", "Утро", "День", "Вечер"};

            var count = 0;

            foreach (DataRow row in townTable.Rows)
            {
                TownStruct StrTempTown = new TownStruct();
                StrTempTown.towns = row[1].ToString();
                StrTempTown.dateStruct = new List<DateStruct>();

                foreach (DataRow mat in weatherCodeDate.Rows)
                {
                    DateStruct StrTempDate = new DateStruct();
                    StrTempDate.timeStruct = new List<TimeStruct>();

                    StrTempDate.date = mat[1].ToString();

                    for (int partOfDay = 0; partOfDay < partsTimeTemp; partOfDay++)
                    {
                        TimeStruct StrTempTime = new TimeStruct();
                        StrTempTime.weather = new List<string>();

                        for (int partOfWeather = 0; partOfWeather < partsWeatherTemp; partOfWeather++)
                        {
                            StrTempTime.weather.Add(weatherCode.Rows[count][4 + partOfWeather].ToString());
                        }

                        count++;
                        StrTempTime.Time = partArray[partOfDay];
                        StrTempDate.timeStruct.Add(StrTempTime);
                    }

                    StrTempTown.dateStruct.Add(StrTempDate);

                }
                weatherStr.Add(StrTempTown);
            }

        }

        public List<TownStruct> GetWeather()
        {
            const string connectString = "Database=test_perminova;Data Source=localhost;User Id=root;Password=123";
            DataTable townTable = new DataTable();
            DataTable dataTable = new DataTable();
            DataTable timeTable = new DataTable();
            DataTable weatherTable = new DataTable();

            const string queryStrTown = @"Select * from test_perminova.town";
            const string queryStrDate = @"Select * from test_perminova.date";
            const string queryStrWeather = @"Select * from test_perminova.weather";


            List<TownStruct> weatherStr = new List<TownStruct>();

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connectString;

                MySqlCommand comTown = new MySqlCommand(queryStrTown, con);
                MySqlCommand comDate = new MySqlCommand(queryStrDate, con);
                MySqlCommand comWeather = new MySqlCommand(queryStrWeather, con);
                con.Open();

                using (MySqlDataReader dr = comTown.ExecuteReader())
                {
                    if (dr.HasRows)
                        townTable.Load(dr);
                }

                using (MySqlDataReader dr = comDate.ExecuteReader())
                {
                    if (dr.HasRows)
                        dataTable.Load(dr);
                }
                
                using (MySqlDataReader dr = comWeather.ExecuteReader())
                {
                    if (dr.HasRows)
                        weatherTable.Load(dr);
                }

                con.Close();                
            }


            savaDate(dataTable, weatherTable, weatherStr, townTable);            

            return weatherStr;
        }

    }
}
