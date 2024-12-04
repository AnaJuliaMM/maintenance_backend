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
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            Machine? machine = await _machineRepository.GetById(id);

            if (machine == null)
            {
                throw new KeyNotFoundException($"Máquina com ID {id} não encontrada.");
            }

            return machine != null ? _mapper.Map<MachineDTO>(machine) : null;
        }

        public async Task<CreateUpdateMachineDTO> Add(CreateUpdateMachineDTO machineDTO)
        {
            if (machineDTO == null)
            {
                throw new ArgumentNullException(nameof(machineDTO), "Nenhum dado foi recebido.");
            }

            machineDTO.ManufactureDate = DateTime.SpecifyKind(
                machineDTO.ManufactureDate,
                DateTimeKind.Utc
            );

            Machine machine = _mapper.Map<Machine>(machineDTO);
            machine = await _machineRepository.Add(machine);

            return _mapper.Map<CreateUpdateMachineDTO>(machine);
        }

        public async Task Update(int id, CreateUpdateMachineDTO machineDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            if (machineDTO == null)
            {
                throw new ArgumentNullException(nameof(machineDTO), "Nenhum dado foi recebido.");
            }

            Machine? machine = await _machineRepository.GetById(id);

            if (machine == null)
            {
                throw new KeyNotFoundException($"Máquina com ID {id} não encontrada.");
            }

            machineDTO.ManufactureDate = DateTime.SpecifyKind(
                machineDTO.ManufactureDate,
                DateTimeKind.Utc
            );

            _mapper.Map(machineDTO, machine);

            await _machineRepository.Update(machine);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            Machine? machine = await _machineRepository.GetById(id);

            if (machine == null)
            {
                throw new KeyNotFoundException($"Máquina com ID {id} não encontrada.");
            }

            await _machineRepository.Delete(id);
        }
    }
}
