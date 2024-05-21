using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class AuthorityService
    {
        private readonly AuthorityRepository _authorityRepository;
        private readonly IMapper _mapper;

        public AuthorityService(AuthorityRepository authorityRepository, IMapper mapper) => (_authorityRepository, _mapper) = (authorityRepository, mapper);

        public IList<AuthorityDto> GetAll()
        {
            IList<AuthorityDto> authorityDtos = _authorityRepository.GetAll().ToDtos(_mapper);
            return authorityDtos;
        }

        public AuthorityDto GetById(int id)
        {
            AuthorityDto authorityDto = _authorityRepository.GetById(id).ToDto(_mapper);
            return authorityDto;
        }

        public AuthorityDto Add(AuthorityDto authorityDto)
        {
            Authority authorityToAdd = authorityDto.ToEntity(_mapper);
            AuthorityDto addedAuthorityDto = _authorityRepository.Add(authorityToAdd).ToDto(_mapper);
            return addedAuthorityDto;
        }
    }
}