using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;
using TestAplication.Models.ApiModels;
using TestAplication.Repository;

namespace TestAplication.Controllers
{

    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                var listaClientes = _unitOfWork.Clientes.Get();

                var listaClientesDto = new List<ClienteApiModel>();

                foreach (var cliente in listaClientes)
                {
                    listaClientesDto.Add(_mapper.Map<ClienteApiModel>(cliente));
                }
                return Ok(listaClientesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{clienteId:int}", Name = "GetCliente")]
        public IActionResult GetCliente(int clienteId)
        {
            try
            {
                var itemCliente = _unitOfWork.Clientes.Get(clienteId);

                if (itemCliente == null)
                {
                    return NotFound();
                }

                var itemClienteDto = _mapper.Map<ClienteApiModel>(itemCliente);
                return Ok(itemClienteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CrearCliente([FromBody] ClienteApiModel clienteAM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (clienteAM == null)
                {
                    return BadRequest(ModelState);
                }

                if (_unitOfWork.Clientes.Existe("Nombre", clienteAM.Nombre))
                {
                    ModelState.AddModelError("", "El cliente ya existe");
                    return StatusCode(404, ModelState);
                }

                var cliente = _mapper.Map<Cliente>(clienteAM);
                _unitOfWork.Clientes.Add(cliente);
                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal guardando el registro {cliente.Nombre}");
                    return StatusCode(500, ModelState);
                }

                return CreatedAtRoute("GetCliente", new { clienteId = cliente.ClienteId }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPatch("{clienteId:int}", Name = "ActualizarPatchCliente")]
        public IActionResult ActualizarPatchCliente(int clienteId, [FromBody] ClientePatchApiModel clienteAM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cliente = _mapper.Map<Cliente>(clienteAM);
                cliente.ClienteId = clienteId;

                _unitOfWork.Clientes.Update(cliente, "ClienteId");

                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el registro {cliente.Nombre}");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{clienteId:int}", Name = "ActualizarPutCliente")]
        public IActionResult ActualizarPutCliente(int clienteId, [FromBody] ClienteApiModel clienteAM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cliente = _mapper.Map<Cliente>(clienteAM);
                cliente.ClienteId = clienteId;

                _unitOfWork.Clientes.Update(cliente, "ClienteId");

                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el registro {cliente.Nombre}");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{clienteId:int}", Name = "BorrarCliente")]
        public IActionResult BorrarCliente(int clienteId)
        {
            try
            {
                if (!_unitOfWork.Clientes.Existe(clienteId))
                {
                    return NotFound();
                }

                _unitOfWork.Clientes.Delete(clienteId);
                if (!_unitOfWork.Save())
                {
                    ModelState.AddModelError("", $"Algo salió mal borrando el registro del cliente con id: {clienteId}");
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
