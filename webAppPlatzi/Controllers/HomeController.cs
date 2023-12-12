using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using webAppPlatzi.Models;
using System.Linq;
using System;
using System.Text;

namespace webAppPlatzi.Controllers
{
    public class HomeController : Controller
    {
         string url = "https://api.escuelajs.co/api/v1/products/";
         public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Product()
        {
            HttpClient httpClient = new HttpClient();
            string url = "https://api.escuelajs.co/api/v1/products";
            var product = await httpClient.GetStringAsync(url);
            var allProducts = JsonConvert.DeserializeObject<List<Product>>(product);
            return View(allProducts);
        }

        [HttpPost]
        public async Task<JsonResult> AddProduct(Product newProduct)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var jsonProduct = JsonConvert.SerializeObject(newProduct);
                    var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return Json(new { mensaje = "Datos enviados con éxito", respuesta = jsonResponse });
                    }
                    else
                    {
                        // Manejar el error de la creación del producto
                        return Json(new { error = "Error al enviar los datos" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al agregar el producto: " + ex.Message });
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetProduct(int id)
        {
            HttpClient httpClient = new HttpClient();
            string url = $"https://api.escuelajs.co/api/v1/products/{id}";

            try
            {
                var product = await httpClient.GetStringAsync(url);
                var singleProduct = JsonConvert.DeserializeObject<Product>(product);
                return Json(singleProduct, JsonRequestBehavior.AllowGet);
            }
            catch (HttpRequestException)
            {
                return Json(new { error = "Product not found" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateProduct(Product updatedProduct)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string url = $"https://api.escuelajs.co/api/v1/products/{updatedProduct.id}";

                // Convierte el objeto a JSON y crea el contenido de la solicitud
                var content = new StringContent(JsonConvert.SerializeObject(updatedProduct), Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT con el contenido JSON
                var response = await httpClient.PutAsync(url, content);

                response.EnsureSuccessStatusCode();

                // El producto se actualizó exitosamente
                return Json(new { success = "Producto actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                // Ocurrió un error al actualizar el producto
                return Json(new { error = "Error al actualizar el producto: " + ex.Message });
            }
        }


    }

}