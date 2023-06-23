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

        [HttpGet]
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

        [HttpGet]
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

        [HttpGet]
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

        [HttpGet]
        [Route("trabajador/select")]
        public async Task<ActionResult<string>> GetTrabajadorAsync(int sucursal)
        {
            var url = $"/api/Varios/TrabajadorSelect?sucursal={sucursal}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        [Route("trabajador/delete")]
        public async Task<ActionResult> DeleteTrabajadorAsync(int sucursal, string codigoempleado)
        {

            var url = $"/api/Varios/TrabajadorDelete?sucursal={sucursal}&codigoempleado={codigoempleado}";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetTipoTrabajador")]
        public async Task<ActionResult> GetTipoTrabajadorAsync()
        {
            var url = "/api/Varios/TipoTrabajador";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetGenero")]
        public async Task<ActionResult> GetGeneroAsync()
        {
            var url = "/api/Varios/Genero";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetEstadoTrabajador")]
        public async Task<ActionResult> GetEstadoTrabajadorAsync()
        {
            var url = "/api/Varios/EstadoTrabajador";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetTipoContrato")]
        public async Task<ActionResult> GetTipoContratoAsync()
        {
            var url = "/api/Varios/TipoContrato";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetTipoCese")]
        public async Task<ActionResult> GetTipoCeseAsync()
        {
            var url = "/api/Varios/TipoCese";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetEstadoCivil")]
        public async Task<ActionResult> GetEstadoCivilAsync()
        {
            var url = "/api/Varios/EstadoCivil";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetEsReingreso")]
        public async Task<ActionResult> GetEsReingresoAsync()
        {
            var url = "/api/Varios/EsReingreso";
            var response = await _httpClient.GetAsync(url);

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
        [Route("trabajador/GetTipoCuenta")]
        public async Task<ActionResult> GetTipoCuentaAsync()
        {
            var url = "/api/Varios/TipoCuenta";
            var response = await _httpClient.GetAsync(url);

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

        [HttpPost]
        [Route("trabajador/Insert")]
        public async Task<ActionResult> InsertarTrabajadorAsync(int COMP_Codigo, string Tipo_trabajador, string Apellido_Paterno, string Apellido_Materno, string Nombres, string Identificacion,
            string Entidad_Bancaria, string CarnetIESS, string Direccion, string Telefono_Fijo, string Telefono_Movil, string Genero, string Nro_Cuenta_Bancaria, string Codigo_Categoria_Ocupacion,
            string Ocupacion, string Centro_Costos, string Nivel_Salarial, string EstadoTrabajador, string Tipo_Contrato, string Tipo_Cese, string EstadoCivil, string TipodeComision, DateTime FechaNacimiento,
            DateTime FechaIngreso, DateTime FechaCese, int PeriododeVacaciones, DateTime FechaReingreso, DateTime Fecha_Ult_Actualizacion, string EsReingreso, int BancoCTA_CTE, string Tipo_Cuenta, int RSV_Indem_Acumul,
            int Año_Ult_Rsva_Indemni, int Mes_Ult_Rsva_Indemni, int FormaCalculo13ro, int FormaCalculo14ro, int BoniComplementaria, int BoniEspecial, int Remuneracion_Minima, int CuotaCuentaCorriente,
            string Fondo_Reserva)
        {



            var url = $"/api/Varios/TrabajadorInsert?COMP_Codigo={COMP_Codigo}&Tipo_trabajador={Tipo_trabajador}&Apellido_Paterno={Apellido_Paterno}&Apellido_Materno={Apellido_Materno}&Nombres={Nombres}&Identificacion={Identificacion}&Entidad_Bancaria={Entidad_Bancaria}&CarnetIESS={CarnetIESS}&Direccion={Direccion}&Telefono_Fijo={Telefono_Fijo}&Telefono_Movil={Telefono_Movil}&Genero={Genero}&Nro_Cuenta_Bancaria={Nro_Cuenta_Bancaria}&Codigo_Categoria_Ocupacion={Codigo_Categoria_Ocupacion}&Ocupacion={Ocupacion}&Centro_Costos={Centro_Costos}&Nivel_Salarial={Nivel_Salarial}&EstadoTrabajador={EstadoTrabajador}&Tipo_Contrato={Tipo_Contrato}&Tipo_Cese={Tipo_Cese}&EstadoCivil={EstadoCivil}&TipodeComision={TipodeComision}&FechaNacimiento={FechaNacimiento}&FechaIngreso={FechaIngreso}&FechaCese={FechaCese}&PeriododeVacaciones={PeriododeVacaciones}&FechaReingreso={FechaReingreso}&Fecha_Ult_Actualizacion={Fecha_Ult_Actualizacion}&EsReingreso={EsReingreso}&BancoCTA_CTE={BancoCTA_CTE}&Tipo_Cuenta={Tipo_Cuenta}&RSV_Indem_Acumul={RSV_Indem_Acumul}&Año_Ult_Rsva_Indemni={Año_Ult_Rsva_Indemni}&Mes_Ult_Rsva_Indemni={Mes_Ult_Rsva_Indemni}&FormaCalculo13ro={FormaCalculo13ro}&FormaCalculo14ro={FormaCalculo14ro}&BoniComplementaria={BoniComplementaria}&BoniEspecial={BoniEspecial}&Remuneracion_Minima={Remuneracion_Minima}&CuotaCuentaCorriente={CuotaCuentaCorriente}&Fondo_Reserva={Fondo_Reserva}";

            var response = await _httpClient.GetAsync(url);

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

        [HttpPost]
        [Route("trabajador/Edit")]
        public async Task<ActionResult> EditarTrabajadorAsync(
        [FromQuery] int COMP_Codigo, [FromQuery] int Id_Trabajador, [FromQuery] string Tipo_trabajador, [FromQuery] string Apellido_Paterno, [FromQuery] string Apellido_Materno,
        [FromQuery] string Nombres, [FromQuery] string Identificacion, [FromQuery] string Entidad_Bancaria, [FromQuery] string CarnetIESS, [FromQuery] string Direccion,
        [FromQuery] string Telefono_Fijo, [FromQuery] string Telefono_Movil, [FromQuery] string Genero, [FromQuery] string Nro_Cuenta_Bancaria, [FromQuery] string Codigo_Categoria_Ocupacion,
        [FromQuery] string Ocupacion, [FromQuery] string Centro_Costos, [FromQuery] string Nivel_Salarial, [FromQuery] string EstadoTrabajador, [FromQuery] string Tipo_Contrato,
        [FromQuery] string Tipo_Cese, [FromQuery] string EstadoCivil, [FromQuery] string TipodeComision, [FromQuery] DateTime FechaNacimiento, [FromQuery] DateTime FechaIngreso,
        [FromQuery] DateTime FechaCese, [FromQuery] int PeriododeVacaciones, [FromQuery] DateTime FechaReingreso, [FromQuery] DateTime Fecha_Ult_Actualizacion,
        [FromQuery] string EsReingreso, [FromQuery] int BancoCTA_CTE, [FromQuery] string Tipo_Cuenta, [FromQuery] int RSV_Indem_Acumul, [FromQuery] int Año_Ult_Rsva_Indemni,
        [FromQuery] int Mes_Ult_Rsva_Indemni, [FromQuery] int FormaCalculo13ro, [FromQuery] int FormaCalculo14ro, [FromQuery] int BoniComplementaria, [FromQuery] int BoniEspecial,
        [FromQuery] int Remuneracion_Minima, [FromQuery] int CuotaCuentaCorriente, [FromQuery] string Fondo_Reserva)
        {
            var url = $"/api/Varios/TrabajadorUpdate?COMP_Codigo={COMP_Codigo}&Id_Trabajador={Id_Trabajador}&Tipo_trabajador={Tipo_trabajador}&Apellido_Paterno={Apellido_Paterno}&Apellido_Materno={Apellido_Materno}&Nombres={Nombres}&Identificacion={Identificacion}&Entidad_Bancaria={Entidad_Bancaria}&CarnetIESS={CarnetIESS}&Direccion={Direccion}&Telefono_Fijo={Telefono_Fijo}&Telefono_Movil={Telefono_Movil}&Genero={Genero}&Nro_Cuenta_Bancaria={Nro_Cuenta_Bancaria}&Codigo_Categoria_Ocupacion={Codigo_Categoria_Ocupacion}&Ocupacion={Ocupacion}&Centro_Costos={Centro_Costos}&Nivel_Salarial={Nivel_Salarial}&EstadoTrabajador={EstadoTrabajador}&Tipo_Contrato={Tipo_Contrato}&Tipo_Cese={Tipo_Cese}&EstadoCivil={EstadoCivil}&TipodeComision={TipodeComision}&FechaNacimiento={FechaNacimiento}&FechaIngreso={FechaIngreso}&FechaCese={FechaCese}&PeriododeVacaciones={PeriododeVacaciones}&FechaReingreso={FechaReingreso}&Fecha_Ult_Actualizacion={Fecha_Ult_Actualizacion}&EsReingreso={EsReingreso}&BancoCTA_CTE={BancoCTA_CTE}&Tipo_Cuenta={Tipo_Cuenta}&RSV_Indem_Acumul={RSV_Indem_Acumul}&Año_Ult_Rsva_Indemni={Año_Ult_Rsva_Indemni}&Mes_Ult_Rsva_Indemni={Mes_Ult_Rsva_Indemni}&FormaCalculo13ro={FormaCalculo13ro}&FormaCalculo14ro={FormaCalculo14ro}&BoniComplementaria={BoniComplementaria}&BoniEspecial={BoniEspecial}&Remuneracion_Minima={Remuneracion_Minima}&CuotaCuentaCorriente={CuotaCuentaCorriente}&Fondo_Reserva={Fondo_Reserva}";

            var response = await _httpClient.PostAsync(url, null);

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
