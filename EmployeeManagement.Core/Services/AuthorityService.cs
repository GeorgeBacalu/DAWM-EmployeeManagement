using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class AuthorityService
    {
        private readonly AuthorityRepository _authorityRepository;
        private readonly IMapper _mapper;

        public AuthorityService(AuthorityRepository authorityRepository, IMapper mapper) => (_authorityRepository, _mapper) = (authorityRepository, mapper);

        public IList<AuthorityDto> GetAll() => _authorityRepository.GetAll().ToDtos(_mapper);

        public AuthorityDto GetById(int id) => _authorityRepository.GetById(id).ToDto(_mapper);

        public AuthorityDto Add(AuthorityDto authorityDto) => _authorityRepository.Add(authorityDto.ToEntity(_mapper)).ToDto(_mapper);
    }
}