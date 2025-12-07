

using AnaPrevention.GeneralMasterData.Api.Families.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Families.Application.Static
{
    public static class FamilyStatic
    {
       
        public const string LineMsgErrorRequiered = "Linea es obligatoria";

        public const string LineMsgErrorNotFound = "Linea no existe";

        

            private static readonly Dictionary<RelationshipType, string> RelationshipTypeNames = new()
            {
            { RelationshipType.MOTHER, "Mamá" },
            { RelationshipType.FATHER, "Papá" },
            { RelationshipType.SONS, "Hijos" },
            { RelationshipType.COUPLE, "Pareja" },
            { RelationshipType.BROTHERS, "Hermanos" },
            { RelationshipType.GRANDPARENTS, "Abuelos" },
            };

        public static string GetRelationshipTypeName(RelationshipType? result)
        {
            if (result == null)
            {
                return string.Empty;
            };

            if (RelationshipTypeNames.TryGetValue((RelationshipType)result, out string? name))
            {
                return name ?? string.Empty;
            }
            else
            {
                return "No definido";
            }
        }
    }
}
