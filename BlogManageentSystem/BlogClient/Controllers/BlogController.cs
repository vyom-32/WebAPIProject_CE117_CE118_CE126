using BlogClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

namespace BlogClient.Controllers
{
    public class BlogController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:51787/api");
        HttpClient client;

        public BlogController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            List<BlogViewModel> modelList = new List<BlogViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/blog").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<BlogViewModel>>(data);
            }
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/Json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/blog", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            BlogViewModel model = new BlogViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/blog/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<BlogViewModel>(data);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BlogViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/Json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/blog/"+model.BlogId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/blog/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            BlogViewModel model = new BlogViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/blog/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<BlogViewModel>(data);
            }
            return View(model);
        }
    }
}