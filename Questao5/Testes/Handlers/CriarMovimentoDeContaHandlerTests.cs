using CSharpFunctionalExtensions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using Questao5.Testes.Mocks;
using Xunit;

namespace Questao5.Testes.Handlers
{
    public class CriarMovimentoDeContaHandlerTests
    {
        [Fact]
        public void CriarMovimentoComValorNegativo()
        {
            //Arrange          
            var handler = new CriarMovimentacaoDeContaHandler(new MockMovimentoRepository(), new MockContaCorrenteRepository());
            var request = new CriarMovimentacaoDeContaRequest
            {
                Valor = -10
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new CriarMovimentacaoDeContaResponse()
            {
                FoiSucesso = false,
                TipoMensagem = "INVALID_VALUE",
                Mensagem = "Valor invalido"
            };

            Assert.Equal(respostaEsperada.TipoMensagem, resultadoObterSaldoContaCorrente.Result.TipoMensagem);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }

        [Fact]
        public void CriarMovimentoComTipoDiferenteDosExistentes()
        {
            //Arrange          
            var handler = new CriarMovimentacaoDeContaHandler(new MockMovimentoRepository(), new MockContaCorrenteRepository());
            var request = new CriarMovimentacaoDeContaRequest
            {
                Valor = 10,
                TipoMovimento = 'P'
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new CriarMovimentacaoDeContaResponse()
            {
                FoiSucesso = false,
                TipoMensagem = "INVALID_TYPE",
                Mensagem = "Tipo de movimento invalido, apenas 'C' Credito e 'D' Debito são aceitos"
            };

            Assert.Equal(respostaEsperada.TipoMensagem, resultadoObterSaldoContaCorrente.Result.TipoMensagem);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }

        [Fact]
        public void CriarMovimentoComContaComIdInvalido()
        {
            var contaCorrenteRepositorio = Substitute.For<IContaCorrenteRepository>();
            contaCorrenteRepositorio.ObterContaCorrentePeloId("dca79a58-fac3-43f9-b41a-f06d63778c1e").ReturnsNull();

            //Arrange          
            var handler = new CriarMovimentacaoDeContaHandler(new MockMovimentoRepository(), contaCorrenteRepositorio);
            var request = new CriarMovimentacaoDeContaRequest
            {
                Valor = 10,
                TipoMovimento = 'C'
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new CriarMovimentacaoDeContaResponse()
            {
                FoiSucesso = false,
                TipoMensagem = "INVALID_ACCOUNT",
                Mensagem = $"Conta com id {request.IdContaCorrente} não encontrada"
            };

            Assert.Equal(respostaEsperada.TipoMensagem, resultadoObterSaldoContaCorrente.Result.TipoMensagem);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }

        [Fact]
        public void CriarMovimentoComContaComContaInativa()
        {
            var contaCorrente = new ContaCorrente { Ativo = false };
            var contaCorrenteRepositorio = Substitute.For<IContaCorrenteRepository>();
            contaCorrenteRepositorio.ObterContaCorrentePeloId("dca79a58-fac3-43f9-b41a-f06d63778c1e").Returns(contaCorrente);

            //Arrange          
            var handler = new CriarMovimentacaoDeContaHandler(new MockMovimentoRepository(), contaCorrenteRepositorio);
            var request = new CriarMovimentacaoDeContaRequest
            {
                IdContaCorrente = "dca79a58-fac3-43f9-b41a-f06d63778c1e",
                Valor = 10,
                TipoMovimento = 'C'
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new CriarMovimentacaoDeContaResponse()
            {
                FoiSucesso = false,
                TipoMensagem = "INACTIVE_ACCOUNT",
                Mensagem = "Conta inativa"
            };

            Assert.Equal(respostaEsperada.TipoMensagem, resultadoObterSaldoContaCorrente.Result.TipoMensagem);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }

        [Fact]
        public void CriarMovimentoComDadosValidos()
        {
            var contaCorrente = new ContaCorrente { Ativo = true };
            var contaCorrenteRepositorio = Substitute.For<IContaCorrenteRepository>();
            contaCorrenteRepositorio.ObterContaCorrentePeloId("dca79a58-fac3-43f9-b41a-f06d63778c1e").Returns(contaCorrente);

            //Arrange          
            var handler = new CriarMovimentacaoDeContaHandler(new MockMovimentoRepository(), contaCorrenteRepositorio);
            var request = new CriarMovimentacaoDeContaRequest
            {
                Valor = 10,
                TipoMovimento = 'C',
                IdContaCorrente = "dca79a58-fac3-43f9-b41a-f06d63778c1e"
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new CriarMovimentacaoDeContaResponse()
            {
                FoiSucesso = true,
            };

            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }
    }
}
