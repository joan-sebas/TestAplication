using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestAplication.EntityFramework.Data;
using TestAplication.Models.ApiModels;
using TestAplication.Models.ApiModels.CuentaModels;
using TestAplication.Models.ApiModels.MovimientoModels;
using TestAplication.Models.ApiModels.ReporteMovimientos;

namespace TestAplication.Mappers
{
    public class TestMapper : Profile
    {
        public TestMapper()
        {
            CreateMap<ClienteApiModel, Cliente>().ReverseMap();
            CreateMap<ClientePatchApiModel, Cliente>().ReverseMap();
            CreateMap<CuentaApiModel, Cuenta>().ReverseMap();
            CreateMap<CuentaPatchApiModel, Cuenta>().ReverseMap();
            CreateMap<MovimientoApiModel, Movimiento>().ReverseMap();
            CreateMap<MovimientoPatchApiModel, Movimiento>().ReverseMap();
            CreateMap<ReporteMovimientosApiModel, ReporteMovimiento>().ReverseMap();
            //CreateMap<AppUsuario, UsuarioDto>().ReverseMap();
            //CreateMap<AppUsuario, UsuarioDatosDto>().ReverseMap();
        }
    }
}
