using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using AutoMapper;
using ClientServiceApp.Models;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ClientServiceApp.Infrastructure.Helper
{
    public interface IUtility{
        public string SearilizeObjToXML<T>(object obj) where T : class;
        public Tdest GetAutoMappedObject<Tsrc, Tdest>(Tsrc srcObject) where Tsrc : class where Tdest : class;
    }
    public class Utility: IUtility 
    {
        #region PUBLIC METHODS
        public string SearilizeObjToXML<T>(object obj) where T : class
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            var resultStr = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings {Indent = true,OmitXmlDeclaration = true}))
                {
                    xsSubmit.Serialize(writer, obj, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                    resultStr = sww.ToString(); 
                }
            }
            return resultStr;
        }

        public Tdest GetAutoMappedObject<Tsrc, Tdest>(Tsrc srcObject) where Tsrc:class where Tdest : class
        {
            var config = GetMapperConfig<Tsrc, Tdest>();
            var mapper = new Mapper(config);
            return mapper.Map<Tdest>(srcObject);
        }
        #endregion

        #region PRIVATE METHODS
        private MapperConfiguration GetMapperConfig<Tsrc, Tdest>()
        {
            if (typeof(Tsrc) == typeof(ConsginmentDetails))
            {
                if (typeof(Tdest) == typeof(XConsignDetailsModel))
                {
                    return new MapperConfiguration(cfg =>
                                                     cfg.CreateMap<ConsginmentDetails, XConsignDetailsModel>()
                                                     .ForMember(dest => dest.ContactAddress, act => act.MapFrom(src => src.Source))
                                                     .ForMember(dest => dest.WarehouseAddress, act => act.MapFrom(src => src.Destination))
                                                     .ForMember(dest => dest.PackageDimensions, act => act.MapFrom(src => src.PackageDimensions))
                                                   );
                }
                else if (typeof(Tdest) == typeof(YConsignDetailsModel))
                {
                  return new MapperConfiguration(cfg =>
                                                     cfg.CreateMap<ConsginmentDetails, YConsignDetailsModel>()
                                                     .ForMember(dest => dest.Consignee, act => act.MapFrom(src => src.Source))
                                                     .ForMember(dest => dest.Consignor, act => act.MapFrom(src => src.Destination))
                                                     .ForMember(dest => dest.Cartons, act => act.MapFrom(src => src.PackageDimensions))
                                                 );

                }
                else if (typeof(Tdest) == typeof(ZConsignDetailsModel))
                {
                  return new MapperConfiguration(cfg =>
                                                     cfg.CreateMap<ConsginmentDetails, ZConsignDetailsModel>()
                                                     .ForMember(dest => dest.Source, act => act.MapFrom(src => src.Source))
                                                     .ForMember(dest => dest.Destination, act => act.MapFrom(src => src.Destination))
                                                     .ForMember(dest => dest.Packages, act => act.MapFrom(src => src.PackageDimensions))
                                                 );
                }
            }
            return null;
        }
        #endregion

    }
}
