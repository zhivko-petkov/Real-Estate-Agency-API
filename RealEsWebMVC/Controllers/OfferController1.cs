using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEsWebMVC.Utils;
using RealEsWebMVC.ViewModels;
using Repository.Implementations;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RealEsWebMVC.Controllers
{
    public class OfferController1 : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44363/api/Offer");

        // GET: OfferController1
        private readonly Uri url2 = new Uri("https://localhost:44363/api/Apartment");


            
        
        public async Task<ActionResult> Index()
        {
            //var currentResponseData = "";
            var currentClient = new HttpClient();
            
                currentClient.BaseAddress = url2;
                currentClient.DefaultRequestHeaders.Accept.Clear();
                currentClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await currentClient.GetAsync("");

                string jsonString2 = await responseMessage.Content.ReadAsStringAsync();
                var currentResponseData = JsonConvert.DeserializeObject<List<ApartmentVM>>(jsonString2);

            

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("");

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<OfferVM>>(jsonString);
                currentResponseData.ForEach(i =>
                {
                    responseData.ForEach(k =>
                    {
                        if(k.ApartmentId == i.Id)
                        {
                            k.ApartmentName = i.ApartmentType;
                            k.TownName = i.Town.TownName;
                            k.Img = i.Image;
                            k.Area = i.Area;
                        }
                    });
                });
                return View(responseData);
            }
        }

        // GET: OfferController1/Details/5
        public async Task<ActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("Offer/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<OfferVM>(jsonString);
                return View(responseData);
            }
        }

        // GET: OfferController1/Create
        public ActionResult Create()
        {
            ViewBag.Apartments = LoadDataUtil.LoadApartmentData();
            return View();
        }

        // POST: OfferController1/Create
        [HttpPost]
        public async Task<ActionResult> Create(OfferVM offerVM)
        {

            try
            {
                using (var client = new HttpClient())
                {

                    
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    /* apartmentVM.Town = new TownDTO();
                     apartmentVM.Town.Id = apartmentVM.TownId;*/

                    

                    var content = JsonConvert.SerializeObject(offerVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("", byteContent);

                    // for testing purposes
                    // string jsonString = await response.Content.ReadAsStringAsync();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                //ViewBag.Towns = LoadDataUtil.LoadTownData();
                return View();
            }
        }


        // POST: OfferController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("offer/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<OfferVM>(jsonString);
                return View(responseData);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OfferVM offerVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(offerVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("", byteContent);

                    // for testing purposes
                    // string jsonString = await response.Content.ReadAsStringAsync();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: OfferController1/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.DeleteAsync("offer/" + id);

                // for testing purposes
                // string jsonString = await response.Content.ReadAsStringAsync();
                // var responseData = JsonConvert.DeserializeObject<NationalityViewModel>(jsonString);
                // return View(responseData);
                return RedirectToAction("Index");
            }
        }
    }
}
