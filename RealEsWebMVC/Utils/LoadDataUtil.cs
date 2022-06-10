using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEsWebMVC.ViewModels;
using Repository.Implementations;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RealEsWebMVC.Utils
{
    public class LoadDataUtil
    {
        public static SelectList LoadTownData()
        {
            Uri urlTown = new Uri("https://localhost:44363/api/Town");
            var client = new WebClient();
            var body = "";

            body = client.DownloadString(urlTown);
            var responseData = JsonConvert.DeserializeObject<List<TownVM>>(body);
            return new SelectList(responseData, "Id", "TownName");
        }

        public static SelectList LoadApartmentData()
        {
            Uri urlTown = new Uri("https://localhost:44363/api/Apartment");
            var client = new WebClient();
            var body = "";

            body = client.DownloadString(urlTown);
            var responseData = JsonConvert.DeserializeObject<List<ApartmentVM>>(body);
            return new SelectList(responseData, "Id", "ApartmentType");
        }

        /*public static String ApartmentNameById(int id)
        {
            string apName = ;
            return apName;
        }*/


    }
}
