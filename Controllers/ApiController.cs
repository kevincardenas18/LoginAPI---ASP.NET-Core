using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LoginAPI___ASP.NET_Core.Models;


namespace LoginAPI___ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://apiservicios.ecuasolmovsa.com:3009");
        }

        [HttpGet]
        [Route("emisores")]
        public async Task<ActionResult<List<Emisor>>> GetEmisoresAsync()
        {
            var response = await _httpClient.GetAsync("/api/Varios/GetEmisor");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                //var emisores = JsonConvert.DeserializeObject<List<Emisor>>(json);
                return Ok(json);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            var response = await _httpClient.GetAsync($"/api/Usuarios?usuario={login.usuario}&password={login.contrasena}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (jsonString.Contains("INGRESO EXITOSO"))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("centroCostos")]
        public async Task<ActionResult<List<CentroCostos>>> GetCentroCostosAsync()
        {
            var response = await _httpClient.GetAsync("/api/Varios/CentroCostosSelect");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var centroCostos = JsonConvert.DeserializeObject<List<CentroCostos>>(json);
                return centroCostos;
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPost]
        [Route("centroCostos/insert")]
        public async Task<ActionResult> AgregarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var response = await _httpClient.GetAsync($"/api/Varios/CentroCostosInsert?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("centroCostos/delete")]
        public async Task<ActionResult> DeleteCentroCostosAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var response = await _httpClient.GetAsync($"/api/Varios/CentroCostosDelete?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("centroCostos/update")]
        public async Task<ActionResult> EditarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Varios/CentroCostosUpdate?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("centroCostos/search")]
        public async Task<ActionResult> SearchCentroCostosAsync(string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de descripcion es: " + descripcionCentroCostos);

            var response = await _httpClient.GetAsync($"/api/Varios/CentroCostosSearch?descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
