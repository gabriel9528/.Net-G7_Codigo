using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Configuration
{
    public class UomSeed : IEntityTypeConfiguration<Uom>
    {
        public void Configure(EntityTypeBuilder<Uom> builder)
        {
            builder.HasData(new List<Uom>()
            {
              new Uom ("BOBINAS","4A","1","4A", Guid.NewGuid()),
new Uom ("BALDE","BJ","2","BJ", Guid.NewGuid()),
new Uom ("BARRILES","BLL","3","BLL", Guid.NewGuid()),
new Uom ("BOLSA","BG","4","BG", Guid.NewGuid()),
new Uom ("BOTELLAS","BO","5","BO", Guid.NewGuid()),
new Uom ("CAJA","BX","6","BX", Guid.NewGuid()),
new Uom ("CARTONES","CT","7","CT", Guid.NewGuid()),
new Uom ("CENTIMETRO CUADRADO","CMK","8","CMK", Guid.NewGuid()),
new Uom ("CENTIMETRO CUBICO","CMQ","9","CMQ", Guid.NewGuid()),
new Uom ("CENTIMETRO LINEAL","CMT","10","CMT", Guid.NewGuid()),
new Uom ("CIENTO DE UNIDADES","CEN","11","CEN", Guid.NewGuid()),
new Uom ("CILINDRO","CY","12","CY", Guid.NewGuid()),
new Uom ("CONOS","CJ","13","CJ", Guid.NewGuid()),
new Uom ("DOCENA","DZN","14","DZN", Guid.NewGuid()),
new Uom ("DOCENA POR 10**6","DZP","15","DZP", Guid.NewGuid()),
new Uom ("FARDO","BE","16","BE", Guid.NewGuid()),
new Uom ("GALON INGLES (4,545956L)","GLI","17","GLI", Guid.NewGuid()),
new Uom ("GRAMO","GRM","18","GRM", Guid.NewGuid()),
new Uom ("GRUESA","GRO","19","GRO", Guid.NewGuid()),
new Uom ("HECTOLITRO","HLT","20","HLT", Guid.NewGuid()),
new Uom ("HOJA","LEF","21","LEF", Guid.NewGuid()),
new Uom ("JUEGO","SET","22","SET", Guid.NewGuid()),
new Uom ("KILOGRAMO","KGM","23","KGM", Guid.NewGuid()),
new Uom ("KILOMETRO","KTM","24","KTM", Guid.NewGuid()),
new Uom ("KILOVATIO HORA","KWH","25","KWH", Guid.NewGuid()),
new Uom ("KIT","KT","26","KT", Guid.NewGuid()),
new Uom ("LATAS","CA","27","CA", Guid.NewGuid()),
new Uom ("LIBRAS","LBR","28","LBR", Guid.NewGuid()),
new Uom ("LITRO","LTR","29","LTR", Guid.NewGuid()),
new Uom ("MEGAWATT HORA","MWH","30","MWH", Guid.NewGuid()),
new Uom ("METRO","MTR","31","MTR", Guid.NewGuid()),
new Uom ("METRO CUADRADO","MTK","32","MTK", Guid.NewGuid()),
new Uom ("METRO CUBICO","MTQ","33","MTQ", Guid.NewGuid()),
new Uom ("MILIGRAMOS","MGM","34","MGM", Guid.NewGuid()),
new Uom ("MILILITRO","MLT","35","MLT", Guid.NewGuid()),
new Uom ("MILIMETRO","MMT","36","MMT", Guid.NewGuid()),
new Uom ("MILIMETRO CUADRADO","MMK","37","MMK", Guid.NewGuid()),
new Uom ("MILIMETRO CUBICO","MMQ","38","MMQ", Guid.NewGuid()),
new Uom ("MILLARES","MLL","39","MLL", Guid.NewGuid()),
new Uom ("MILLON DE UNIDADES","UM","40","UM", Guid.NewGuid()),
new Uom ("ONZAS","ONZ","41","ONZ", Guid.NewGuid()),
new Uom ("PALETAS","PF","42","PF", Guid.NewGuid()),
new Uom ("PAQUETE","PK","43","PK", Guid.NewGuid()),
new Uom ("PAR","PR","44","PR", Guid.NewGuid()),
new Uom ("PIES","FOT","45","FOT", Guid.NewGuid()),
new Uom ("PIES CUADRADOS","FTK","46","FTK", Guid.NewGuid()),
new Uom ("PIES CUBICOS","FTQ","47","FTQ", Guid.NewGuid()),
new Uom ("PIEZAS","C62","48","C62", Guid.NewGuid()),
new Uom ("PLACAS","PG","49","PG", Guid.NewGuid()),
new Uom ("PLIEGO","ST","50","ST", Guid.NewGuid()),
new Uom ("PULGADAS","INH","51","INH", Guid.NewGuid()),
new Uom ("RESMA","RM","52","RM", Guid.NewGuid()),
new Uom ("TAMBOR","DR","53","DR", Guid.NewGuid()),
new Uom ("TONELADA CORTA","STN","54","STN", Guid.NewGuid()),
new Uom ("TONELADA LARGA","LTN","55","LTN", Guid.NewGuid()),
new Uom ("TONELADAS","TNE","56","TNE", Guid.NewGuid()),
new Uom ("TUBOS","TU","57","TU", Guid.NewGuid()),
new Uom ("UNIDAD (BIENES)","NIU","58","NIU", Guid.NewGuid()),
new Uom ("UNIDAD (SERVICIOS)","ZZ","59","ZZ", Guid.NewGuid()),
new Uom ("US GALON (3,7843 L)","GLL","60","GLL", Guid.NewGuid()),
new Uom ("YARDA","YRD","61","YRD", Guid.NewGuid()),
new Uom ("YARDA CUADRADA","YDK","62","YDK", Guid.NewGuid()),


            });
        }
    }
}
