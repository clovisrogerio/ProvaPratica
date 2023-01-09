using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories;
using Questao5.Testes.Mocks;
using Xunit;
namespace Questao5.Testes.Handlers
{
    public class ObterSaldoContaCorrenteHandlerTests
    {
        [Fact]
        public void BuscaPorUmaContaCorrenteComIdInvalido()
        {
            //Arrange
            var contaCorrenteRepositorio = Substitute.For<IContaCorrenteRepository>();
            contaCorrenteRepositorio.ObterContaCorrentePeloId("dca79a58-fac3-43f9-b41a-f06d63778c1e").ReturnsNull();
            var handler = new ObterSaldoContaCorrenteHandler(new MockMovimentoRepository(), contaCorrenteRepositorio);
            var request = new ObterSaldoContaCorrenteRequest
            {
                IdContaCorrente = "dca79a58-fac3-43f9-b41a-f06d63778c1e"
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new ObterSaldoContaCorrenteResponse()
            {
                FoiSucesso = false,
                TipoMensagem = "INVALID_ACCOUNT",
                Mensagem = $"Conta com id {request.IdContaCorrente} não encontrada"
            };

            Assert.Equal(respostaEsperada.TipoMensagem, resultadoObterSaldoContaCorrente.Result.TipoMensagem);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }

        [Fact]
        public void BuscaPorUmaContaInativa()
        {
            //Arrange
            var contaCorrente = new ContaCorrente { Ativo = false };
            var contaCorrenteRepositorio = Substitute.For<IContaCorrenteRepository>();
            contaCorrenteRepositorio.ObterContaCorrentePeloId("dca79a58-fac3-43f9-b41a-f06d63778c1e").Returns(contaCorrente);
            var handler = new ObterSaldoContaCorrenteHandler(new MockMovimentoRepository(), contaCorrenteRepositorio);
            var request = new ObterSaldoContaCorrenteRequest
            {
                IdContaCorrente = "dca79a58-fac3-43f9-b41a-f06d63778c1e"
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new ObterSaldoContaCorrenteResponse()
            {
                FoiSucesso = false,
                TipoMensagem = "INACTIVE_ACCOUNT",
                Mensagem = "Conta inativa"
            };

            Assert.Equal(respostaEsperada.TipoMensagem, resultadoObterSaldoContaCorrente.Result.TipoMensagem);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }

        [Fact]
        public void ObterSaldoNegativo()
        {
            //Arrange
            var contaCorrente = new ContaCorrente { Ativo = true };
            var contaCorrenteRepositorio = Substitute.For<IContaCorrenteRepository>();
            contaCorrenteRepositorio.ObterContaCorrentePeloId("dca79a58-fac3-43f9-b41a-f06d63778c1e").Returns(contaCorrente);

            var listaMovimentos = new List<Movimento>() { new Movimento { TipoMovimento = TipoMovimento.Credito, Valor = 10 }, new Movimento { TipoMovimento = TipoMovimento.Debito, Valor = 20 } };
            var movimentoRepositorio = Substitute.For<IMovimentoRepository>();
            movimentoRepositorio.ObterMovimentosDaContaCorrente("dca79a58-fac3-43f9-b41a-f06d63778c1e").Returns(listaMovimentos);

            var handler = new ObterSaldoContaCorrenteHandler(movimentoRepositorio, contaCorrenteRepositorio);
            var request = new ObterSaldoContaCorrenteRequest
            {
                IdContaCorrente = "dca79a58-fac3-43f9-b41a-f06d63778c1e"
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new ObterSaldoContaCorrenteResponse()
            {
                FoiSucesso = true,
                SaldoAtual = -10
            };

            Assert.Equal(respostaEsperada.SaldoAtual, resultadoObterSaldoContaCorrente.Result.SaldoAtual);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }

        [Fact]
        public void ObterSaldoPositivo()
        {
            //Arrange
            var contaCorrente = new ContaCorrente { Ativo = true };
            var contaCorrenteRepositorio = Substitute.For<IContaCorrenteRepository>();
            contaCorrenteRepositorio.ObterContaCorrentePeloId("dca79a58-fac3-43f9-b41a-f06d63778c1e").Returns(contaCorrente);

            var listaMovimentos = new List<Movimento>() { new Movimento { TipoMovimento = TipoMovimento.Credito, Valor = 10 }, new Movimento { TipoMovimento = TipoMovimento.Credito, Valor = 10 } };
            var movimentoRepositorio = Substitute.For<IMovimentoRepository>();
            movimentoRepositorio.ObterMovimentosDaContaCorrente("dca79a58-fac3-43f9-b41a-f06d63778c1e").Returns(listaMovimentos);

            var handler = new ObterSaldoContaCorrenteHandler(movimentoRepositorio, contaCorrenteRepositorio);
            var request = new ObterSaldoContaCorrenteRequest
            {
                IdContaCorrente = "dca79a58-fac3-43f9-b41a-f06d63778c1e"
            };

            //Act
            var resultadoObterSaldoContaCorrente = handler.Handle(request, new CancellationToken());

            //Assert
            var respostaEsperada = new ObterSaldoContaCorrenteResponse()
            {
                FoiSucesso = true,
                SaldoAtual = 20
            };

            Assert.Equal(respostaEsperada.SaldoAtual, resultadoObterSaldoContaCorrente.Result.SaldoAtual);
            Assert.Equal(respostaEsperada.FoiSucesso, resultadoObterSaldoContaCorrente.Result.FoiSucesso);
        }
    }
}
