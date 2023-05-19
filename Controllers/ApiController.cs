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
                //var json = await response.Content.ReadAsStringAsync();
                //var centroCostos = JsonConvert.DeserializeObject<List<CentroCostos>>(json);
                //return centroCostos;
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
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

        [HttpGet("loginAutorizador")]
        public async Task<ActionResult> Login(string usuario, string password)
        {
            var response = await _httpClient.GetAsync($"/api/Varios/GetAutorizador?usuario={usuario}&password={password}");
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
        [Route("movimientosPlanilla")]
        public async Task<ActionResult<string>> GetMovimientosPlanillaAsync()
        {
            var response = await _httpClient.GetAsync("/api/Varios/MovimientoPlanillaSelect");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPost]
        [Route("movimientosPlanilla/insert")]
        public async Task<ActionResult> InsertarMovimientoPlanillaAsync(string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de conceptos es: " + conceptos);

            var response = await _httpClient.GetAsync($"/api/Varios/MovimientoPlanillaInsert?conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}");

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
        [Route("movimientosPlanilla/update")]
        public async Task<ActionResult> EditarMovimientoPlanillaAsync(int codigoplanilla, string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de codigoMovimientoPlanilla es: " + codigoplanilla);

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Varios/MovimientoPlanillaUpdate?codigoplanilla={codigoplanilla}&conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}");

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

        [HttpDelete]
        [Route("movimientosPlanilla/delete")]
        public async Task<ActionResult> DeleteMovimientoPlanillaAsync(int codigomovimiento, string descripcionomovimiento)
        {
            Console.WriteLine("El valor de codigo es: " + codigomovimiento);

            var response = await _httpClient.GetAsync($"/api/Varios/MovimeintoPlanillaDelete?codigomovimiento={codigomovimiento}&descripcionomovimiento={descripcionomovimiento}");

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
        [Route("movimientosPlanilla/search")]
        public async Task<ActionResult> SearchMovimientoPlanillasAsync(string concepto)
        {
            Console.WriteLine("El valor de descripcion es: " + concepto);

            var response = await _httpClient.GetAsync($"/api/Varios/MovimientoPlanillaSearch?Concepto={concepto}");

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
        [Route("MovimientosExcepcion1y2")]
        public async Task<ActionResult> ObtenerMovimientosExcepcionAsync()
        {
            var response = await _httpClient.GetAsync($"/api/Varios/MovimientosExcepcion1y2");

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
        [Route("ObtenerMovimientosExcepcion3")]
        public async Task<ActionResult> ObtenerMovimientosExcepcion3Async()
        {
            var response = await _httpClient.GetAsync($"/api/Varios/MovimientosExcepcion3");

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
        [Route("GetTipoOperacion")]
        public async Task<ActionResult> GetTipoOperacionAsync()
        {
            var response = await _httpClient.GetAsync($"/api/Varios/TipoOperacion");

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
        [Route("GetTrabaAfectaIESS")]
        public async Task<ActionResult> GetTrabaAfectaIessAsync()
        {
            var response = await _httpClient.GetAsync($"/api/Varios/TrabaAfectaIESS");

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
        [Route("GetTrabAfecImpuestoRenta")]
        public async Task<ActionResult> GetTrabAfecImpuestoRentaAsync()
        {
            var response = await _httpClient.GetAsync($"/api/Varios/TrabAfecImpuestoRenta");

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
