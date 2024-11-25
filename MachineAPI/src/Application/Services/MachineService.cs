using AutoMapper;
using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;

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

        public async Task Add(CreateUpdateMachineDTO machineDTO)
        {
            Machine machine = _mapper.Map<Machine>(machineDTO);
            await _machineRepository.Add(machine);
        }

        public async Task Update(int id, CreateUpdateMachineDTO machineDTO)
        {
            Machine machine = await _machineRepository.GetById(id);

            if (machine == null)
            {
                throw new KeyNotFoundException($"Machine with ID {id} not found.");
            }
            _mapper.Map(machineDTO, machine);

            await _machineRepository.Update(machine);
        }

        public async Task Delete(int id)
        {
            await _machineRepository.Delete(id);
        }
    }
}
