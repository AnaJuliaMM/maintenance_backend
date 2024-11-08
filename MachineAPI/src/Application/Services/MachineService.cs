using AutoMapper;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;
using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public MachineService(IMachineRepository machineRepository, IMapper mapper)
        {
            _machineRepository = machineRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MachineDTO>> GetAll()
        {
            IEnumerable<Machine> machines = await _machineRepository.GetAll();
            return _mapper.Map<IEnumerable<MachineDTO>>(machines);
        }

        public async Task<MachineDTO?> GetById(int id)
        {
            Machine? machine = await _machineRepository.GetById(id);
            return machine != null ? _mapper.Map<MachineDTO>(machine) : null;
        }

        public async Task Add(MachineDTO MachineDTO)
        {
            Machine machine = _mapper.Map<Machine>(MachineDTO);
            await _machineRepository.Add(machine);
        }

        public async Task Update(int id, MachineDTO MachineDTO)
        {
            Machine machine = _mapper.Map<Machine>(MachineDTO);
            await _machineRepository.Update(machine);
        }

        public async Task Delete(int id)
        {
            await _machineRepository.Delete(id);
        }
    }
}
