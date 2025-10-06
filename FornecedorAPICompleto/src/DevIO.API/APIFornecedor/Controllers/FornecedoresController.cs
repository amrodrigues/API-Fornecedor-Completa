using APIFornecedor.ViewModels;
using AutoMapper;
using Business.Intefaces;
using Business.Models;
using Business.Notificacoes;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APIFornecedor.Extensions;


namespace APIFornecedor.Controllers
{
    [Authorize]
    [Route("api/fornecedores")]

    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;
    
        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                      IMapper mapper, 
                                      IFornecedorService fornecedorService,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
            
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {

            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            return fornecedor;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {

            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return fornecedor;
        }


        [ClaimsAuthorize("Fornecedor","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(AdicionarFornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            //var result = await _fornecedorService.Adicionar(fornecedor);
            // if (!result) return BadRequest();
            // return Ok(fornecedor);

            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return CustomResponse(fornecedorViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar( Guid id, FornecedorViewModel fornecedorViewModel)
        {
            //if ( id != fornecedorViewModel.Id) return BadRequest();
            //if (!ModelState.IsValid) return BadRequest();

            //var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            //var result = await _fornecedorService.Atualizar(fornecedor);

            //if (!result) return BadRequest();

            //return Ok(fornecedor);

            if (id != fornecedorViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(fornecedorViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return CustomResponse(fornecedorViewModel);
        }


        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]

        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            //var result = await _fornecedorService.Remover(id);

            //if (!result) return BadRequest();

            //return Ok(fornecedor);

            await _fornecedorService.Remover(id);

            return CustomResponse();
        }

        [HttpGet("produtos-e-endereco/{id:guid}")]
        public async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

        [HttpGet("fornecedor-endereco/{id:guid}")]
        public async Task<FornecedorViewModel> ObterFornecedorEndereco (Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
    }
}
