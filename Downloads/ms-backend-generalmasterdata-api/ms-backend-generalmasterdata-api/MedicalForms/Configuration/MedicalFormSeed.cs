using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.MedicalForms.Configuration
{
    public class MedicalFormSeed : IEntityTypeConfiguration<MedicalForm>
    {
        public void Configure(EntityTypeBuilder<MedicalForm> builder)
        {
            builder.HasData(new List<MedicalForm>()
            {
                //new MedicalForm("EKG",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("b5fe7999-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_EKG,"ekg",Guid.NewGuid()),
                //new MedicalForm("Examen Medico",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_MEDICAL_EXAM,"medicalexam",Guid.NewGuid()),
                //new MedicalForm("Triaje",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_TRIAGE,"triage",Guid.NewGuid()),
                //new MedicalForm("Oftalmologia",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("ce4dd950-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_OPHTHALMOLOGY,"ophthalmology",Guid.NewGuid()),
                //new MedicalForm("Antecedentes",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_HISTORY,"history",Guid.NewGuid()),
                //new MedicalForm("Musculoesqueletico Simple",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_MUSCULOSKELETAL_SIMPLE,"musculoskeletalsimple",Guid.NewGuid()),
                //new MedicalForm("Historial Ocupacional",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_WORK_HISTORY,"historywork",Guid.NewGuid()),
                //new MedicalForm("Anexo16A",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_ANEXO16,"anexo16",Guid.NewGuid()),
                //new MedicalForm("Escala de Epworth",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("d2e4c621-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_PSYCHOLOGY_EPWORTH_ESCALE,"epworthscale",Guid.NewGuid()),
                //new MedicalForm("Altura Estructural",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_WORK_AT_HEIGHT,"structuralheight",Guid.NewGuid()),
                //new MedicalForm("Examen Psicosensometrico",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("ed154945-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_PSYCHOSENSOMETRIC,"psychosensometric",Guid.NewGuid()),
                //new MedicalForm("Documento de Autorizacion",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_DOCUMENT,"documents",Guid.NewGuid()),
                //new MedicalForm("Ficha SAS",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_PSYCHOLOGY_FILE_SAS,"filesas",Guid.NewGuid()),
                //new MedicalForm("Psicologia",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("d2e4c621-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_PSYCHOLOGY,"psychology",Guid.NewGuid()),
                //new MedicalForm("Rayos X - OIT",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("f5c0b8c3-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_LATERAL_RAYS,"xraysoit",Guid.NewGuid()),
                //new MedicalForm("Audiometria",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("ace48e53-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_AUDIOMETRY,"audiometry",Guid.NewGuid()),
                //new MedicalForm("Musculoesqueletico Completo",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_MUSCULOSKELETAL_COMPLETE,"musculoskeletalcomplete",Guid.NewGuid()),
                //new MedicalForm("Laboratorio",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_LABORATORY,"laboratory",Guid.NewGuid()),
                //new MedicalForm("ElectroEncefalograma",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("342544d8-abde-11ed-bad3-02ff8634cf19"),MedicalFormsType.OCCUPATIONAL_ELECTROENCEPHALOGRAM,"electroencephalogram",Guid.NewGuid()),
                //new MedicalForm("Espirometría",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("9ba50dbf-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_SPIROMETRY,"spirometry",Guid.NewGuid()),
                //new MedicalForm("Prueba de Esfuerzo",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("b5fe7999-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_TEST_STRESS,"stresstest",Guid.NewGuid()),
                //new MedicalForm("Cuestionario de pittsburg",Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d"),Guid.Parse("d2e4c621-652f-11ed-a147-0a971fe1e98d"),MedicalFormsType.OCCUPATIONAL_PSYCHOLOGY_PITTSBURG,"pittsburg",Guid.NewGuid()),
            });
        }
    }
}
