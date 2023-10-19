using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Create;
using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Delete;
using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Get;
using CleanArchExample.Domain;
using CleanArchExample.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchExample.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    public class SuitabilityController : DefaultController
    {
        private readonly ILogger<SuitabilityController> _logger;
        private readonly ICustomerSuitabilityCreateUseCase _createUseCase;
        private readonly ICustomerSuitabilityGetUseCases _getUseCase;
        private readonly ICustomerSuitabilityDeleteUseCase _deleteUseCase;

        public SuitabilityController(ILogger<SuitabilityController> logger, ICustomerSuitabilityCreateUseCase createUseCase, ICustomerSuitabilityGetUseCases getUseCase, ICustomerSuitabilityDeleteUseCase deleteUseCase)
        {
            _logger = logger;
            _createUseCase = createUseCase;
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        /// <summary>
        /// Cria uma nova suitability baseada na resposta do cliente
        /// </summary>
        [HttpPost("suitability")]
        [ProducesResponseType(typeof(CustomerSuitability), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CustomerSuitabilityCreateRequest request)
        {
            var response = await _createUseCase.Handle(request);

            return DefaultResponse(response);
        }

        /// <summary>
        /// Busca todos os suitabilities de um CPF
        /// </summary>
        [HttpGet("suitability/{cpf}/history")]
        [ProducesResponseType(typeof(List<CustomerSuitability>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHistory(long cpf)
        {
            var response = await _getUseCase.HandleGetHistory(cpf);

            return DefaultResponse(response);
        }

        /// <summary>
        /// Busca o ultimo suitability de um CPF
        /// </summary>
        [HttpGet("suitability/{cpf}")]
        [ProducesResponseType(typeof(CustomerSuitability), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(long cpf)
        {
            var response = await _getUseCase.HandleGetLast(cpf);

            return DefaultResponse(response);
        }

        /// <summary>
        /// Busca todos os suitabilities pelos filtros definidos
        /// </summary>
        [HttpGet("suitability")]
        [ProducesResponseType(typeof(CustomerSuitability), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(Guid? id, long? cpf, InvestorProfile? investorProfile, int? totalValue)
        {
            var response = await _getUseCase.HandleGetAll(id, cpf, investorProfile, totalValue);

            return DefaultResponse(response);
        }

        /// <summary>
        /// Remove uma suitability por id
        /// </summary>
        [HttpDelete("suitability/{id}")]
        [ProducesResponseType(typeof(CustomerSuitability), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _deleteUseCase.Handle(id);

            return DefaultResponse(response);
        }
    }
}