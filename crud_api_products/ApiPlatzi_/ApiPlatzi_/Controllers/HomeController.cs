using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using ApiPlatzi_.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
 

namespace ApiPlatzi_.Controllers
{
    public class HomeController : Controller
    {
        private const string BaseApiUrl = "https://dummyjson.com/products";
        private const string AddProductUrl = BaseApiUrl + "/add";
        private static readonly HttpClient httpClient = new HttpClient();
        private const string urlSeacrh = "https://dummyjson.com/products/search?q=";

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Product()
        {
            var product = await httpClient.GetStringAsync(BaseApiUrl);
            var allProducts = JsonConvert.DeserializeObject<ProductsContainer>(product);
            return View(allProducts);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Product product)
        {

            var url = BaseApiUrl + $"/{product.id}";

            try
            {
                var jsonProduct = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(url, content);

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
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> CreateProduct(Product product)
        {
             try
            {
               
                    var jsonProduct = JsonConvert.SerializeObject(product);
                    var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                    //var response = await client.PostAsync(url, content);
                    var response = await httpClient.PostAsync(AddProductUrl, content).ConfigureAwait(false); // para evitar bloqueos de contexto de sincronización

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
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProducts(int id,   Product updatedProduct)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://dummyjson.com/products/" + id;
                string jsonContent = JsonConvert.SerializeObject(updatedProduct);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el éxito (puede devolver un JSON, un mensaje, etc.)
                    return Json(new { success = true, message = "Producto actualizado con éxito" });
                }
                else
                {
                    // Manejar el error (puede devolver un JSON con información del error, un mensaje, etc.)
                    return Json(new { success = false, message = "Error al actualizar el producto" });
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProductById(int id)
        { 
            try
            {

                var response = await httpClient.GetAsync($"{BaseApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Json(new { mensaje = "Datos obtenidos con éxito", respuesta = jsonResponse }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = "Error al obtener los datos" });
                }

            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteProductById(int id)
        {
            

            try
            {
                var response = await httpClient.DeleteAsync($"{BaseApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Json(new { mensaje = "Producto eliminado con éxito", respuesta = jsonResponse });
                }
                else
                {
                    return Json(new { error = "Error al eliminar el producto" });
                }
               
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> SearchProducts(string q)
        {
 
            try
            {
                var response = await httpClient.GetAsync($"{urlSeacrh}{q}");
                if (response.IsSuccessStatusCode)
                {
                   var jsonResponse = await response.Content.ReadAsStringAsync();
                   var data = JsonConvert.DeserializeObject<ProductsContainer>(jsonResponse);
                   return Json(new { success = true, data } , JsonRequestBehavior.AllowGet);
                }
                else
                {
                   return Json(new { success = false, error = "Error al obtener los datos" });
                }
                
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

    }
}