using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;


using AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;


namespace AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories
{
	public class PersonRepository : Repository<Person>
	{
		readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
		public PersonRepository(AnaPreventionContext context) : base(context)
		{
		}

		public PersonDto? GetDtoById(Guid id)
		{
			return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
		}

		public Person? GetByEmail(Email email)
		{
			return _context.Set<Person>().SingleOrDefault(x => x.Email == email);
		}
		public bool EmailTakenForEdit(Guid personId, Email email)
		{
			return _context.Set<Person>().Any(c => c.Id != personId && c.Email == email);
		}

		public Person? GetbyDocumentNumber(string documentNumber, Guid identityDocumentTypeId)
		{
			return _context.Set<Person>().SingleOrDefault(t1 => t1.DocumentNumber == documentNumber && t1.IdentityDocumentTypeId == identityDocumentTypeId && t1.Status);
		}

		public bool DocumentNumberTakenForEdit(Guid personId, string documentNumber, Guid identityDocumentTypeId)
		{
			return _context.Set<Person>().Any(c => c.Id != personId && c.DocumentNumber == documentNumber && c.IdentityDocumentTypeId == identityDocumentTypeId);
		}

		public PersonDto? GetbyDtoDocumentNumber(string documentNumber, Guid identityDocumentTypeId)
		{
			return GetDtoQueryable().SingleOrDefault(t1 => t1.Status && t1.DocumentNumber == documentNumber && t1.IdentityDocumentTypeId == identityDocumentTypeId);
		}

		public List<PersonDto> GetListAll(string? namesSearch = "")
		{
			return GetDtoQueryable().Where(t1 => t1.Status && (t1.Names + " " + t1.LastName + " " + t1.SecondLastName ?? "").Contains(namesSearch ?? "")).ToList();
		}

		public List<PersonDto> GetListFilter(bool status = true, string namesSearch = "", string documentSearch = "")
		{

			var query = GetDtoQueryable().Where(t1 => t1.Status == status);

			if (!string.IsNullOrEmpty(namesSearch))
				query = query.Where(t1 => (t1.Names + " " + t1.LastName + " " + t1.SecondLastName ?? "").Contains(namesSearch));

			if (!string.IsNullOrEmpty(documentSearch))
				query = query.Where(t1 => t1.DocumentNumber.Contains(documentSearch));

			return query.ToList();
		}

		public Tuple<IEnumerable<PersonDto>, PaginationMetadata> GetList(
			int pageNumber, int pageSize, bool status = true, string namesSearch = "", string documentSearch = "")
		{
			if (pageSize > maxRowPageSize)
				pageSize = maxRowPageSize;

			var query = GetDtoQueryable().Where(t1 => t1.Status == status);

			if (!string.IsNullOrEmpty(namesSearch))
				query = query.Where(t1 => (t1.Names + " " + t1.LastName + " " + t1.SecondLastName ?? "").Contains(namesSearch));

			if (!string.IsNullOrEmpty(documentSearch))
				query = query.Where(t1 => t1.DocumentNumber.Contains(documentSearch));

			var listPersonDto = query
			 .Skip(pageSize * (pageNumber - 1))
			 .Take(pageSize).ToList();
			int totalItemCount = query.Count();

			var paginationMetadata = new PaginationMetadata(
			  totalItemCount, pageSize, pageNumber);

			return new Tuple<IEnumerable<PersonDto>, PaginationMetadata>
				(listPersonDto, paginationMetadata);
		}

		//public Person? GetByOccupationalHealthId(Guid occupationalHealthId)
		//{
		//	return (from t1 in _context.Set<Person>()
		//			join t2 in _context.Set<OccupationalHealthOrder>() on t1.Id equals t2.PersonId
		//			join t3 in _context.Set<OccupationalHealthOrderForm>() on t2.Id equals t3.OccupationalHealthOrderId
		//			join t4 in _context.Set<OccupationalHealth>() on t3.Id equals t4.OccupationalHealthOrderFormId
		//			where t4.Id == occupationalHealthId
		//			select t1).FirstOrDefault();
		//}

		public IQueryable<PersonDto> GetDtoQueryable()
		{

			return (from t1 in _context.Set<Person>()
					join t2 in _context.Set<IdentityDocumentType>() on t1.IdentityDocumentTypeId equals t2.Id
					join t3 in _context.Set<Gender>() on t1.GenderId equals t3.Id into into_t3
					from t3 in into_t3.DefaultIfEmpty()
					join t4 in _context.Set<MaritalStatus>() on t1.MaritalStatusId equals t4.Id into into_t4
					from t4 in into_t4.DefaultIfEmpty()
					join t5 in _context.Set<DegreeInstruction>() on t1.DegreeInstructionId equals t5.Id into into_t5
					from t5 in into_t5.DefaultIfEmpty()
					join t6 in _context.Set<IdentityDocumentType>() on t1.SecondIdentityDocumentTypeId equals t6.Id into into_t6
					from t6 in into_t6.DefaultIfEmpty()
					join t7 in _context.Set<Country>() on t1.CountryResidenceId equals t7.Id into into_t7
					from t7 in into_t7.DefaultIfEmpty()
					join t8 in _context.Set<Country>() on t1.CountryBirthId equals t8.Id into into_t8
					from t8 in into_t8.DefaultIfEmpty()
					join t9 in _context.Set<District>() on t1.DistrictResidenceId equals t9.Id into into_t9
					from t9 in into_t9.DefaultIfEmpty()
					join t10 in _context.Set<Province>() on t9.ProvinceId equals t10.Id into into_t10
					from t10 in into_t10.DefaultIfEmpty()
					join t11 in _context.Set<Department>() on t10.DepartmentId equals t11.Id into into_t11
					from t11 in into_t11.DefaultIfEmpty()
					join t12 in _context.Set<District>() on t1.DistrictBirthId equals t12.Id into into_t12
					from t12 in into_t12.DefaultIfEmpty()
					join t13 in _context.Set<Province>() on t12.ProvinceId equals t13.Id into into_t13
					from t13 in into_t13.DefaultIfEmpty()
					join t14 in _context.Set<Department>() on t13.DepartmentId equals t14.Id into into_t14
					from t14 in into_t14.DefaultIfEmpty()
					orderby t1.LastName
					select new PersonDto()
					{
						Id = t1.Id,
						PersonId = t1.Id,
						Name = t1.Names,
						DocumentNumber = t1.DocumentNumber,
						IdentityDocumentTypeId = t1.IdentityDocumentTypeId,
						IdentityDocumentTypeAbbreviation = t2.Abbreviation,
						IdentityDocumentTypeDescription = t2.Description,
						Names = t1.Names,
						LastName = t1.LastName,
						SecondLastName = t1.SecondLastName,
						PhoneNumber = t1.PhoneNumber,
						Email = t1.Email == null ? "" : t1.Email!.Value,
						GenderId = t1.GenderId,
						Gender = t3.Description,
						DateBirth = t1.DateBirth == null ? "" : t1.DateBirth!.DateTimeValue.ToString(CommonStatic.FormatDate),
						PersonalEmail = t1.PersonalEmail == null ? "" : t1.PersonalEmail!.Value,
						PersonalPhoneNumber = t1.PersonalPhoneNumber,
						SecondDocumentNumber = t1.SecondDocumentNumber,
						SecondIdentityDocumentTypeId = t1.SecondIdentityDocumentTypeId,
						SecondIdentityDocumentType = t6.Description,
						PersonalAddress = t1.PersonalAddress,
						EmergencyContactName = t1.EmergencyContactName,
						EmergencyContactNumberPhone = t1.EmergencyContactNumberPhone,
						EmergencyContactRelationship = t1.EmergencyContactRelationship,
						DegreeInstructionId = t1.DegreeInstructionId,
						DegreeInstruction = t5.Description,
						MaritalStatusId = t1.MaritalStatusId,
						MaritalStatus = t4.Description,
						CountryResidenceId = t1.CountryResidenceId,
						CountryResidence = t7.Description,
						CountryBirthId = t1.CountryBirthId,
						CountryBirth = t8.Description,
						DistrictResidenceId = t1.DistrictResidenceId,
						DistrictResidence = t9.Description,
						ProvinceResidenceId = t10.Id,
						ProvinceResidence = t10.Description,
						DepartmentResidenceId = t11.Id,
						DepartmentResidence = t11.Description,
						DistrictBirthId = t12.Id,
						DistrictBirth = t12.Description,
						ProvinceBirthId = t13.Id,
						ProvinceBirth = t13.Description,
						DepartmentBirthId = t14.Id,
						DepartmentBirth = t14.Description,
						DataProcessingAuthorization = t1.DataProcessingAuthorization,
						//ExistDoctor = _context.Set<Doctor>().Any(x => x.PersonId == t1.Id),
						//ExistUser = _context.Set<User>().Any(x => x.PersonId == t1.Id),
						Status = t1.Status,
						HealthInsurance = CommonStatic.ConvertJsonToOptionYesOrNot(t1.HealthInsuranceJson)

					});
		}
	}
}