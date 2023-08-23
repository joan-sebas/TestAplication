using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;
using TestAplication.Models.ApiModels;
using TestAplication.Models.ApiModels.MovimientoModels;
using TestAplication.Models.ApiModels.ReporteMovimientos;
using TestAplication.Repository;
using TestAplication.Strategies;

namespace TestAplication.Controllers
{

    [Route("api/movimiento")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovimientoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovimientos()
        {
            try
            {
                var listaMovimientos = _unitOfWork.Movimientos.Get();

                var listaMovimientosDto = new List<MovimientoApiModel>();

                foreach (var movimiento in listaMovimientos)
                {
                    listaMovimientosDto.Add(_mapper.Map<MovimientoApiModel>(movimiento));
                }
                return Ok(listaMovimientosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{movimientoId:int}", Name = "GetMovimiento")]
        public IActionResult GetMovimiento(int movimientoId)
        {
            try
            {
                var itemMovimiento = _unitOfWork.Movimientos.Get(movimientoId);

                if (itemMovimiento == null)
                {
                    return NotFound();
                }

                var itemMovimientoDto = _mapper.Map<MovimientoApiModel>(itemMovimiento);
                return Ok(itemMovimientoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CrearMovimiento([FromBody] MovimientoApiModel movimientoAM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (movimientoAM == null)
                {
                    return BadRequest(ModelState);
                }

                var movimiento = _mapper.Map<Movimiento>(movimientoAM);
                var strategyContext= movimiento.Valor > 0 ?
                            new MovimientoContext(new MovimientoCreditoStrategy()) :
                            new MovimientoContext(new MovimientoDebitoStrategy()) ;

                strategyContext.Add(movimiento,_unitOfWork);
                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal guardando el registro");
                    return StatusCode(500, ModelState);
                }

                return CreatedAtRoute("GetMovimiento", new { movimientoId = movimiento.MovimientoId }, movimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        //[HttpPatch("{movimientoId:int}", Name = "ActualizarPatchMovimiento")]
        //public IActionResult ActualizarPatchMovimiento(int movimientoId, [FromBody] MovimientoPatchApiModel movimientoAM)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var movimiento = _mapper.Map<Movimiento>(movimientoAM);
        //        movimiento.MovimientoId = movimientoId;

        //        _unitOfWork.Movimientos.Update(movimiento, "MovimientoId");

        //        if (!_unitOfWork.Save())
        //        {
        //            ModelState.AddModelError("", $"Algo salió mal actualizando el registro con Id {movimientoId}");
        //            return StatusCode(500, ModelState);
        //        }
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        //    }
        //}

        //[HttpPut("{movimientoId:int}", Name = "ActualizarPutMovimiento")]
        //public IActionResult ActualizarPutMovimiento(int movimientoId, [FromBody] MovimientoApiModel movimientoAM)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var movimiento = _mapper.Map<Movimiento>(movimientoAM);
        //        movimiento.MovimientoId = movimientoId;

        //        _unitOfWork.Movimientos.Update(movimiento, "MovimientoId");

        //        if (!_unitOfWork.Save())
        //        {
        //            ModelState.AddModelError("", $"Algo salió mal actualizando el registro con id {movimientoId}");
        //            return StatusCode(500, ModelState);
        //        }
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        //    }
        //}

        [HttpDelete("{movimientoId:int}", Name = "BorrarMovimiento")]
        public IActionResult BorrarMovimiento(int movimientoId)
        {
            try
            {
                if (!_unitOfWork.Movimientos.Existe(movimientoId))
                {
                    return NotFound();
                }

                var movimiento = _unitOfWork.Movimientos.Get(movimientoId);
                _unitOfWork.Movimientos.Delete(movimiento.MovimientoId);
                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal borrando el registro con id {movimientoId}");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [Route("reporte")]
        [HttpGet]
        public IActionResult GetReporteMovimientos([FromBody] FormReporteMovimientosApiModel reporteMovimientoAM)
        {
            try
            {
                var listaReporteMovimientos = _unitOfWork.ReporteMovimiento.GetRangoFechas(reporteMovimientoAM.FechaInicio,
                                                                                     reporteMovimientoAM.FechaFin,
                                                                                     reporteMovimientoAM.Cliente);

                var listaReporteMovimientosDto = new List<ReporteMovimientosApiModel>();

                foreach (var reporteMovimiento in listaReporteMovimientos)
                {
                    listaReporteMovimientosDto.Add(_mapper.Map<ReporteMovimientosApiModel>(reporteMovimiento));
                }
                return Ok(listaReporteMovimientosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
