using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;
using TestAplication.Models.ApiModels;
using TestAplication.Models.ApiModels.CuentaModels;
using TestAplication.Repository;

namespace TestAplication.Controllers
{

    [Route("api/cuenta")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CuentaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCuentas()
        {
            try
            {
                var listaCuentas = _unitOfWork.Cuentas.Get();

                var listaCuentasDto = new List<CuentaApiModel>();

                foreach (var cuenta in listaCuentas)
                {
                    listaCuentasDto.Add(_mapper.Map<CuentaApiModel>(cuenta));
                }
                return Ok(listaCuentasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{cuentaId:int}", Name = "GetCuenta")]
        public IActionResult GetCuenta(int cuentaId)
        {
            try
            {
                var itemCuenta = _unitOfWork.Cuentas.Get(cuentaId);

                if (itemCuenta == null)
                {
                    return NotFound();
                }

                var itemCuentaDto = _mapper.Map<CuentaApiModel>(itemCuenta);
                return Ok(itemCuentaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CrearCuenta([FromBody] CuentaApiModel cuentaAM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (cuentaAM == null)
                {
                    return BadRequest(ModelState);
                }

               
                if (_unitOfWork.Cuentas.Existe("NumeroCuenta", cuentaAM.NumeroCuenta))
                {
                    ModelState.AddModelError("", "La cuenta ya existe");
                    return StatusCode(404, ModelState);
                }

                var cuenta = _mapper.Map<Cuenta>(cuentaAM);
                _unitOfWork.Cuentas.Add(cuenta);
                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal guardando el registro con numero de cuenta: {cuenta.NumeroCuenta}");
                    return StatusCode(500, ModelState);
                }

                return CreatedAtRoute("GetCuenta", new { cuentaId = cuenta.CuentaId }, cuenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPatch("{cuentaId:int}", Name = "ActualizarPatchCuenta")]
        public IActionResult ActualizarPatchCuenta(int cuentaId, [FromBody] CuentaPatchApiModel cuentaAM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cuenta = _mapper.Map<Cuenta>(cuentaAM);
                cuenta.CuentaId = cuentaId;

                _unitOfWork.Cuentas.Update(cuenta, "CuentaId");

                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el registro con número de cuenta: {cuenta.NumeroCuenta}");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{cuentaId:int}", Name = "ActualizarPutCuenta")]
        public IActionResult ActualizarPutCuenta(int cuentaId, [FromBody] CuentaApiModel cuentaAM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cuenta = _mapper.Map<Cuenta>(cuentaAM);
                cuenta.CuentaId = cuentaId;

                _unitOfWork.Cuentas.Update(cuenta, "CuentaId");

                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el registro con número de cuenta: {cuenta.NumeroCuenta}");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{cuentaId:int}", Name = "BorrarCuenta")]
        public IActionResult BorrarCuenta(int cuentaId)
        {
            try
            {
                if (!_unitOfWork.Cuentas.Existe(cuentaId))
                {
                    return NotFound();
                }

                var cuenta = _unitOfWork.Cuentas.Get(cuentaId);
                _unitOfWork.Cuentas.Delete(cuenta.CuentaId);
                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el registro con número de cuenta:  {cuenta.NumeroCuenta }");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
